using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Interfaces
{
    /// <summary>
    /// The base behavior of every 'cell' object.
    /// </summary>
    public interface ICell:IPositioned
    {
        /// <summary>
        /// The state of which the <see cref="ICell"/>'s field is currently abide.
        /// </summary>
        public CellState State { get;}
    }
}
