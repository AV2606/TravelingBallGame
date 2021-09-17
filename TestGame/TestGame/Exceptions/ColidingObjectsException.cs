using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    /// <summary>
    /// An <see cref="Exception"/> that indicates two object have collided with each other.
    /// </summary>
    public class ColidingObjectsException:ApplicationException
    {
        public ColidingObjectsException(string? message) : base(message) { }
    }
}
