using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.Apis.EndPoints.Sample.TicTacToe.ViewModels
{
    public enum CellData
    {
        Empty,
        X,
        O
    }

    public class CellViewModel
    {
        readonly int row;
        readonly int column;

        public CellViewModel(int row, int column)
        {
            this.row = row;
            this.column = column;
            this.Data = CellData.Empty;

            NextMoveCommand = new RelayCommand(ExecuteNextMove);
        }

        public RelayCommand NextMoveCommand { get; private set; }

        private void ExecuteNextMove()
        {
        }

        public CellData Data { get; set; }
        public int Row { get { return row; } }
        public int Column { get { return column; } }
    }

    public class BoardViewModel
    {
        public ObservableCollection<CellViewModel> Cells { get; private set; }

        public BoardViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
            for (int row = 0; row < 3; ++row)
            {
                for (int col = 0; col < 3; ++col)
                {
                    Cells.Add(new CellViewModel(row, col));
                }
            }
        }
    }
}
