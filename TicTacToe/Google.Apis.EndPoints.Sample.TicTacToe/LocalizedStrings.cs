using Google.Apis.EndPoints.Sample.TicTacToe.Resources;

namespace Google.Apis.EndPoints.Sample.TicTacToe
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}