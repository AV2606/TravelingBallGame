using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Extensions
{
    public static class TestExtensions
    {
        /// <summary>
        /// Adds to <see cref="Point.X"/> of <paramref name="position"/> the value of <paramref name="adder"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="adder"></param>
        /// <returns></returns>
        public static Point AddX(this Point position,int adder)=> new Point { X = position.X + adder, Y = position.Y };
        /// <summary>
        /// Adds 1 to <see cref="Point.X"/> of <paramref name="position"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Point XPP(this Point position) => position.AddX(1);
        /// <summary>
        /// Subs 1 from <see cref="Point.X"/> of <paramref name="position"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Point XMM(this Point position) => position.AddX(-1);

        /// <summary>
        /// Adds to <see cref="Point.Y"/> of <paramref name="position"/> the value of <paramref name="adder"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="adder"></param>
        /// <returns></returns>
        public static Point AddY(this Point position, int adder) => new Point { X = position.X, Y = position.Y + adder };
        /// <summary>
        /// Adds 1 to <see cref="Point.Y"/> of <paramref name="position"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Point YPP(this Point position) => position.AddY(1);
        /// <summary>
        /// Subs 1 from <see cref="Point.Y"/> of <paramref name="position"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Point YMM(this Point position) => position.AddY(-1);
    }
}
