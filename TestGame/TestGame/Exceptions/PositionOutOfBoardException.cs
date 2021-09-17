using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    /// <summary>
    /// An <see cref="Exception"/> which thrown when the given position is out of the current boundries.
    /// 
    /// </summary>
    public class PositionOutOfBoardException:ApplicationException

    {
        public PositionOutOfBoardException(string message) : base(message) { }
    }
}
