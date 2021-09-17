using System;
using System.Drawing;
using System.Diagnostics;
using TestGame.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Cell : ICell
    {
        public static readonly ArgumentException BallToShape = new ArgumentException("Cant move the ball into a shape cell!");
        public static readonly ArgumentException BallToVisited = new ArgumentException("Cant move the ball into a visited cell!");
        public static readonly InvalidOperationException BallToBall = new InvalidOperationException("The ball is ALREADY in this cell!");
        public static readonly char DefaultChar = '*';
        public static readonly ConsoleColor DefaultColor = ConsoleColor.Gray;

        private string DebuggerDisplay =>$"({this.Position.X},{this.Position.Y}): {State}";
        /// <summary>
        /// The Board hosting this <seealso cref="Cell"/>.
        /// </summary>
        public readonly Board Parent;

        /// <summary>
        /// The State of this <seealso cref="Cell"/> by the context of the game.
        /// </summary>
        public CellState State { get; protected internal set; }
        /// <summary>
        /// The position of this <seealso cref="Cell"/> in <see cref="Parent"/>.
        /// </summary>
        public Point Position { get {
                return Parent.GetPosition(this);
            } }
        /// <summary>
        /// The Color of the cell when rendered.
        /// </summary>
        public ConsoleColor Color { get; protected set; } = DefaultColor;
        /// <summary>
        /// The Char that should be used when drawing this cell.
        /// </summary>
        public char DrawChar { get; protected set; } = DefaultChar;

        /// <summary>
        /// Initializes a cell with the specified parent <seealso cref="Board"/> and empty <seealso cref="CellState"/>
        /// </summary>
        /// <param name="parent"></param>
        public Cell(Board parent):this(parent,CellState.Empty)
        {
        }

        /// <summary>
        /// Initializes a cell with the specified parent <seealso cref="Board"/> and <seealso cref="CellState"/>.
        /// </summary>
        /// <param name="parent"></param>
        public Cell(Board parent, CellState state) :this(parent,state,null)
        {
        }

        /// <summary>
        /// Initializes a cell with the specified parent <seealso cref="Board"/> and <seealso cref="CellState"/> that within
        /// the shape <paramref name="shape"/>.
        /// </summary>
        /// <param name="parent"></param>
        public Cell(Board parent, CellState state, Shape shape)
        {
            this.Parent = parent;
            this.State = state;
            if (shape is not null)
            {
                this.Color = shape.MatchingColor;
                this.DrawChar = shape.TheChar;
            }
            else
            {
                if (state == CellState.Ball)
                    Color = ConsoleColor.Green;
            }
        }

        /// <summary>
        /// If possible moves the ball into that <see cref="Cell"/>.
        /// </summary>
        protected internal void MoveBallIn()
        {
            if (this.State == CellState.Shape)
                throw BallToShape;
            if (this.State == CellState.Visited)
                throw BallToVisited;
            if (this.State == CellState.Ball)
                throw BallToBall;
            this.Color = ConsoleColor.Green;
            this.State = CellState.Ball;
        }
        /// <summary>
        /// If the ball is in there, moves it out and marks the cell as <see cref="CellState.Visited"/> and returns
        /// true, otherwise does nothing and returns false.
        /// </summary>
        /// <returns></returns>
        protected internal bool MoveBallOut()
        {
            if (State == CellState.Ball)
            {
                State = CellState.Visited;
                this.Color = ConsoleColor.Blue;
                return true;
            }
            return false;
        }
    }
}
