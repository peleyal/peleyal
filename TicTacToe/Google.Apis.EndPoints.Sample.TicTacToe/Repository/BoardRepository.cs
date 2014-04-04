using DevRel.Apis.Tictactoe.v1;
using DevRel.Apis.Tictactoe.v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.EndPoints.Sample.TicTacToe.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private TictactoeService service;

        public async Task Login(string user)
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new FileStream(@"Assets\client_secrets.json", FileMode.Open, FileAccess.Read),
                new[] { TictactoeService.Scope.UserinfoEmail },
                "user",
                CancellationToken.None);

            service = new TictactoeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "TicTacToe Sample",
            });
        }

        public async Task<string> GetNextMoveAsync(string currentState)
        {
            if (service == null)
            {
                throw new InvalidOperationException("User must login first!");
            }

            Board board = await service.Board.Getmove(new Board
            {
                State = currentState
            }).ExecuteAsync();

            return board.State;
        }
    }
}
