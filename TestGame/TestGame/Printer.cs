using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    internal static class Printer
    {
        public static void ClearPrint(Board board)
        {
            Console.Clear();
            for (int j = 0; j < board.Y_Length; j++)
            {
                for (int i = 0; i < board.X_Length; i++)
                {
                    var a = board[i, j];
                    Console.ForegroundColor = a.Color;
                    Console.Write(a.DrawChar);
                }
                Console.WriteLine();
            }
        }

        public static void PrintJustNeeded(Board board)
        {
            //saving the tuple that represents the cursor position.
            var CPos = Console.GetCursorPosition();
            //set a new position to the cursor as the ball position and recolor it.
            Console.SetCursorPosition(board.BallPosition.X, board.BallPosition.Y);
            Console.ForegroundColor = board[board.BallPosition].Color;
            Console.Write(board[board.BallPosition].DrawChar);

            //set a new position to the cursor as the last ball position and recolor it.
            Console.SetCursorPosition(board.LastBallPosition.X, board.LastBallPosition.Y);
            Console.ForegroundColor = board[board.LastBallPosition].Color;
            Console.Write(board[board.LastBallPosition].DrawChar);

            //restore the original cursor position.
            Console.SetCursorPosition(CPos.Left,CPos.Top);
        }

    }
}
