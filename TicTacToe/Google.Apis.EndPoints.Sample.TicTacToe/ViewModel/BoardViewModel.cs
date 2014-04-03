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
            return Data == EmptyCell && board.CanExecuteNextMove();
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

    public class BoardViewModel
    {
        public ObservableCollection<CellViewModel> Cells { get; private set; }

        private bool isBusy;

        private readonly IBoardRepository repository;

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
        }

        public bool CanExecuteNextMove()
        {
            return !isBusy && Cells.Any(c => c.Data == CellViewModel.EmptyCell);
        }

        public async Task ExecuteNextMove()
        {
            if (Cells.All(c => c.Data != CellViewModel.EmptyCell))
            {
                return;
            }

            isBusy = true;
            string currentState = string.Join("", (from c in Cells
                                                   select c.Data).ToArray());
            try
            {
                string newState = await repository.GetNextMoveAsync(currentState);
                for (int i = 0; i < Cells.Count; ++i)
                {
                    Cells[i].Data = newState[i];
                }
            }
            catch (Exception ex)
            {
                // TODO(peleyal): Handle exception.
            }
            finally
            {
                isBusy = false;
            }
        }
    }
}
