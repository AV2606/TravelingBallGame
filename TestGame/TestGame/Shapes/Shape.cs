using System;
using TestGame.Interfaces;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestGame
{
    /// <summary>
    /// The Base class of all the shapes of this game.
    /// </summary>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public abstract class Shape : ICloneable, IPositioned,IParented
    {
        public Point Position { get; protected set; }

        /// <summary>
        /// The <see cref="char"/> that should be printed in the console and indicates that this <see cref="Shape"/>
        /// is currently there.
        /// </summary>
        public abstract char TheChar { get; }
        /// <summary>
        /// The color <seealso cref="TheChar"/> should be printed with.
        /// </summary>
        public abstract ConsoleColor MatchingColor { get; }
        public Board Parent { get; internal set; }

        /// <summary>
        /// Creates a clone of this <see cref="Shape"/> with the same parameters.
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();
        /// <summary>
        /// A string that should be added to the <see cref="Shape.GetDebuggerDisplay"/> value in the
        /// debugger display.
        /// </summary>
        /// <returns></returns>
        protected virtual string AddDebbugerDeisplay()
        {
            return "";
        }
        /// <summary>
        /// A basic debugger display that suits all the <see cref="Shape"/> derived classes.
        /// </summary>
        /// <returns></returns>
        private string GetDebuggerDisplay()
        {
            return $"{this.GetType().Name}: pos: {Position.ToString()}, {AddDebbugerDeisplay()}";
        }
    }
}
