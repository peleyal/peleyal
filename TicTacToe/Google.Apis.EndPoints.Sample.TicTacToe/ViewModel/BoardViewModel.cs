using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Google.Apis.EndPoints.Sample.TicTacToe.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.Apis.EndPoints.Sample.TicTacToe.ViewModel
{
    // TODO(peleyal): Use enum instead of string
    //public enum CellData
    //{
    //    Empty,
    //    X,
    //    O
    //}

    public class CellViewModel : ViewModelBase
    {
        readonly int row;
        readonly int column;
        readonly BoardViewModel board;

        public const char EmptyCell = '-';

        public CellViewModel(BoardViewModel board, int row, int column)
        {
            this.board = board;
            this.row = row;
            this.column = column;
            this.Data = EmptyCell;

            NextMoveCommand = new RelayCommand(async () =>
                {
                    await ExecuteNextMove();
                }, CanExecuteNextMove);
        }

        public RelayCommand NextMoveCommand { get; private set; }

        private async Task ExecuteNextMove()
        {
            Data = 'X';
            await board.ExecuteNextMove().ConfigureAwait(false);
        }

        private bool CanExecuteNextMove()
        {
            return board.CanExecuteNextMove();
        }

        private char data;
        public char Data
        {
            get { return data; }
            set
            {
                Set("Data", ref data, value);
            }
        }

        public int Row { get { return row; } }
        public int Column { get { return column; } }
    }

    public class BoardViewModel : ViewModelBase
    {
        public ObservableCollection<CellViewModel> Cells { get; private set; }

        public enum Status
        {
            NotLoggedIn,
            NotDone,
            Won,
            Lost,
            Tie,
            Waiting,
        }

        private Status boardStatus;
        public Status BoardStatus
        {
            get { return boardStatus; }
            set
            {
                Set("BoardStatus", ref boardStatus, value);
            }
        }

        private readonly IBoardRepository repository;

        public RelayCommand LoginCommand { get; private set; }

        public BoardViewModel(IBoardRepository repository)
        {
            this.repository = repository;

            Cells = new ObservableCollection<CellViewModel>();
            for (int row = 0; row < 3; ++row)
            {
                for (int col = 0; col < 3; ++col)
                {
                    Cells.Add(new CellViewModel(this, row, col));
                }
            }

            LoginCommand = new RelayCommand(async () =>
            {
                await repository.Login("user");
                BoardStatus = Status.NotDone;
                RaiseCanExecuteChanged();
            });
        }

        private void RaiseCanExecuteChanged()
        {
            foreach (var cell in Cells)
            {
                cell.NextMoveCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanExecuteNextMove()
        {
            return BoardStatus == Status.NotDone;
        }

        public async Task ExecuteNextMove()
        {
            BoardStatus = CheckForVictory();
            if (boardStatus != Status.NotDone)
            {
                // TODO(peleyal): Send score and clear the board.
                RaiseCanExecuteChanged();
                return;
            }

            BoardStatus = Status.Waiting;
            string currentState = string.Join("", (from c in Cells
                                                   select c.Data).ToArray());

            string newState = await repository.GetNextMoveAsync(currentState);

            BoardStatus = CheckForVictory();
            if (boardStatus != Status.NotDone)
            {
                // TODO(peleyal): Send score and clear the board.
                return;
            }

            for (int i = 0; i < Cells.Count; ++i)
            {
                Cells[i].Data = newState[i];
            }
        }

        private Status CheckForVictory()
        {
            Tuple<Status, bool> status;

            // Check rows and columns.
            for (int i = 0; i < 3; i++)
            {
                status = CheckCellsEquals(Cells[i * 3].Data, Cells[i * 3 + 1].Data, Cells[i * 3 + 2].Data);
                if (status.Item2)
                {
                    return status.Item1;
                }
                status = CheckCellsEquals(Cells[i].Data, Cells[i + 3].Data, Cells[i + 6].Data);
                if (status.Item2)
                {
                    return status.Item1;
                }
            }

            // Check top-left to bottom-right.
            status = CheckCellsEquals(Cells[0].Data, Cells[4].Data, Cells[8].Data);
            if (status.Item2)
            {
                return status.Item1;
            }

            // Check top-right to bottom-left.
            status = CheckCellsEquals(Cells[2].Data, Cells[4].Data, Cells[6].Data);
            if (status.Item2)
            {
                return status.Item1;
            }

            if (Cells.All(c => c.Data != CellViewModel.EmptyCell))
            {
                return Status.Tie;
            }

            return Status.NotDone;
        }

        private Tuple<Status, bool> CheckCellsEquals(char first, char second, char third)
        {
            if (first == second && second == third)
            {
                if (first == 'O')
                {
                    return Tuple.Create(Status.Lost, true);
                }
                if (first == 'X')
                {
                    return Tuple.Create(Status.Won, true);
                }
            }

            return Tuple.Create(Status.NotDone, false);
        }

    }
}
