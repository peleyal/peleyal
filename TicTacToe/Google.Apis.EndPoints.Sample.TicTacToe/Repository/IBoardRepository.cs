using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.Apis.EndPoints.Sample.TicTacToe.Repository
{
    public interface IBoardRepository
    {
        Task<string> GetNextMoveAsync(string currentState);
        string GetNextMove(string currentState);
    }
}
