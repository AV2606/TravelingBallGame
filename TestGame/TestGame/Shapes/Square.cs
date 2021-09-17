using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestGame
{
    /// <summary>
    /// A <see cref="Shape"/> of square.
    /// </summary>
    public class Square : Rectangle
    {
        /// <summary>
        /// The color that indicates this shape is being printed to the <see cref="Console"/>.
        /// </summary>
        const ConsoleColor color = ConsoleColor.DarkYellow;
        public override ConsoleColor MatchingColor => color;
        /// <summary>
        /// The length of each side of this <see cref="Square"/>.
        /// </summary>
        public int SideLength => base.Size.Width;

        public Square(Board parent,int sideLength=1,Point position=default
            ) : base(parent, sideLength, position) { }

        public override Square Clone()
        {
            return base.Clone() as Square;
        }
        protected override string AddDebbugerDeisplay()
        {
            return $"sides length: {this.Size.Width}";
        }
    }
}
