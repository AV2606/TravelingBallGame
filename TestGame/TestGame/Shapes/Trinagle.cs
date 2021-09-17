using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    /// <summary>
    /// A <see cref="Shape"/> of triangle.
    /// </summary>
    public class Trinagle : Line
    {
        /// <summary>
        /// The <see cref="char"/> that indicates this shape is being printed to the <see cref="Console"/>.
        /// </summary>
        const char @char = '#';
        public override char TheChar => @char;

        /// <summary>
        /// The color that indicates this shape is being printed to the <see cref="Console"/>.
        /// </summary>
        const ConsoleColor color = ConsoleColor.Red;
        public override ConsoleColor MatchingColor => color;

        public Trinagle(Board parent,Point position=default,int length=1) : base(parent, position, length) { }
        public Trinagle(Board parent, (int, int) position, int length = 1) : base(parent, position, length) { }

        public override Trinagle Clone()
        {
            return base.Clone() as Trinagle;
        }
    }
}
