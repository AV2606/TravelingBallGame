using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    /// <summary>
    /// A <see cref="Shape"/> of rectangle.
    /// </summary>
    public class Rectangle : Shape
    {
        /// <summary>
        /// The <see cref="char"/> that indicates this shape is being printed to the <see cref="Console"/>.
        /// </summary>
        const char @char = 'ם';
        public override char TheChar => @char;

        /// <summary>
        /// The color that indicates this shape is being printed to the <see cref="Console"/>.
        /// </summary>
        const ConsoleColor color = ConsoleColor.Yellow;
        public override ConsoleColor MatchingColor => color;

        /// <summary>
        /// The size of this <see cref="Rectangle"/>.
        /// </summary>
        public Size Size { get; protected set; }
        /// <summary>
        /// The Height part of this <see cref="Shape"/>'s size.
        /// </summary>
        public int Height => Size.Height;
        /// <summary>
        /// The Width part of this <see cref="Shape"/>'s size.
        /// </summary>
        public int Width => Size.Width;

        public Rectangle(Board parent,int sidesLength,Point position):this(parent,new Size(sidesLength,sidesLength),position)
        {
        }

        public Rectangle(Board parent, Size size=default,Point position=default)
        {
            this.Size = size;
            this.Position = position;
            this.Parent = parent;
        }

        public override Rectangle Clone()
        {
            return new Rectangle(Parent, Size, Position);
        }
        protected override string AddDebbugerDeisplay()
        {
            return $"Width: {Size.Width}, Height: {Size.Height}";
        }
    }
}
