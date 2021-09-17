using System;
using System.Drawing;
using System.Collections.Generic;
using TestGame.Interfaces;

using Tools.Extensions;
using TestGame.Extensions;

namespace TestGame
{
    /// <summary>
    /// The direction of which the player is wishing to move to.
    /// </summary>
    public enum Direction
    {
        Up=0xab,
        Down=0xd011e,
        Left=0x1ef7,
        Right=0x7187
    }
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game("Avichay");
            var r=game.PlayTheGame();
            Console.WriteLine("The game finished! press any key to continue");
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Good job {r.PlayerName}!\n" +
                $"You have managed to get ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{r.GoodPoints}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" Points!\n\n" +
                $"{r.PlayerName}'s title: {r.PlayerTitle}\n\n" +
                $"Rounds played: {r.Round}\n" +
                $"Number of cell revisits in the game: {r.ReVisits}\n" +
                $"Number of colisions with shapes: {r.ShapesColisions}\n" +
                $"Number of times tried to escape the map: {r.TimesPlayerGotOutOfTheMap}");
        }
        //static Board RandomizeBaord()
        //{
        //    Random rnd = new Random();
        //    Board b = new Board(null,new System.Drawing.Point(0,0),new System.Drawing.Size(10,10));
        //    //b.board = new Cell[10, 10];
        //    for (int i = 0; i < b.X_Length; i++)
        //    {
        //        for (int j = 0; j < b.Y_Length; j++)
        //        {
        //            b[i, j] = new Cell(b, (CellState)rnd.Next(typeof(CellState)));
        //        }
        //    }
        //    return b;
        //}
        ///// <summary>
        ///// Plays the game, returns true if the player wins-false if he lost.
        ///// </summary>
        ///// <param name="b">The board on which the game whould take place.</param>
        ///// <returns></returns>
        //static (bool Won,Exception WhyLost) Play(Board b)
        //{
           
        //}
        //static int timesRePrintCalled = 0;
        //static void RePrintBoard(object board,EventArgs e)
        //{
        //    var b = board as Board;
        //    timesRePrintCalled++;
        //    Console.Title = b.BallPosition.ToString()+ $", Times RePrintCalled= {timesRePrintCalled}";
        //    Printer.PrintJustNeeded(b);
        //    //Printer.ClearPrint(b);
        //}
        //private static bool TryMove(Board b, Direction d)
        //{
        //    switch (d)
        //    {
        //        case Direction.Up:
        //            return b.TryMoveBallTo(b.BallPosition.YMM());
        //        case Direction.Down:
        //            return b.TryMoveBallTo(b.BallPosition.YPP());
        //        case Direction.Right:
        //            return b.TryMoveBallTo(b.BallPosition.XPP());
        //        default:
        //            return b.TryMoveBallTo(b.BallPosition.XMM());
        //    }
        //}
    }
}
