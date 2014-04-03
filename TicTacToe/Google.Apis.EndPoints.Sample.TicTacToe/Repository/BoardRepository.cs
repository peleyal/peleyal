using DevRel.Apis.Tictactoe.v1;
using DevRel.Apis.Tictactoe.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.Apis.EndPoints.Sample.TicTacToe.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly TictactoeService service;

        public BoardRepository()
        {
            service = new TictactoeService(new BaseClientService.Initializer
                {
                    ApiKey = "PUT_YOUR_KEY_HERE",
                    ApplicationName = "TicTacToe Sample",
                });
        }

        public async Task<string> GetNextMoveAsync(string currentState)
        {
            Board board = await service.Board.Getmove(new Board
            {
                State = currentState
            }).ExecuteAsync();

            return board.State;
        }

        public string GetNextMove(string currentState)
        {
            Board board = service.Board.Getmove(new Board
            {
                State = currentState
            }).Execute();

            return board.State;
        }
    }
}
