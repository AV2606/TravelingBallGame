using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Interfaces
{
    /// <summary>
    /// The basic behavior of and object that has a parent of type <see cref="Board"/>.
    /// </summary>
    interface IParented
    {
        /// <summary>
        /// The parent of this object which handles it.
        /// </summary>
        public Board Parent { get; }
    }
}
