using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using Google.Apis.EndPoints.Sample.TicTacToe.ViewModel;

namespace Google.Apis.EndPoints.Sample.TicTacToe.Utils
{
    public class StatusValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BoardViewModel.Status s = (BoardViewModel.Status)value;
            switch (s)
            {
                case BoardViewModel.Status.Lost:
                    return "You lost!";
                case BoardViewModel.Status.Won:
                    return "You won!!!";
                case BoardViewModel.Status.Waiting:
                    return "Waiting...";
                case BoardViewModel.Status.Tie:
                    return "Tie";
                case BoardViewModel.Status.NotLoggedIn:
                    return "Please log-in";
                case BoardViewModel.Status.NotDone:
                    return "Your turn...";
            }

            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
