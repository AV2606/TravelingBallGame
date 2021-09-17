using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    /// <summary>
    /// A <see cref="Shape"/> of line.
    /// </summary>
    public class Line : Shape
    {
        /// <summary>
        /// The <see cref="char"/> that indicates this shape is being printed to the <see cref="Console"/>.
        /// </summary>
        const char Char = '=';
        /// <summary>
        /// The color that indicates this shape is being printed to the <see cref="Console"/>.
        /// </summary>
        const ConsoleColor Color = ConsoleColor.Cyan;

        public override char TheChar => Line.Char;
        public override ConsoleColor MatchingColor => Color;

        /// <summary>
        /// The length of this shape that its size should be derived from.
        /// </summary>
        public int Length { get; private set; }

        public Line(Board parent, Point position=default, int length=1)
        {
            this.Parent = parent;
            this.Position = position;
            this.Length = length;
        }
        public Line(Board parent,(int x,int y) position,int length = 1):this(parent,new Point(position.x,position.y),length)
        {

        }

        public override Line Clone()
        {
            return new Line(Parent, Position, Length);
        }
        protected override string AddDebbugerDeisplay()
        {
            return $"Length: {this.Length}";
        }
    }
}
