using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Interfaces
{
    /// <summary>
    /// The basic rules of any object that has a position.
    /// </summary>
    public interface IPositioned
    {
        /// <summary>
        /// The position of this object.
        /// </summary>
        public Point Position { get;}
    }
}
