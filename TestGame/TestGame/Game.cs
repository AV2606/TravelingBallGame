using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools.Extensions;
using TestGame.Extensions;

namespace TestGame
{
    class Game
    {
        /// <summary>
        /// The *CURRENT* map the game takes place in.
        /// </summary>
        public Board Map { get; protected set; }
        /// <summary>
        /// All the point that the player has earned during this game.
        /// </summary>
        public int GoodPoints { get; protected set; }
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// The round that the player is currently at.
        /// </summary>
        public int Round { get; protected set; }
        /// <summary>
        /// The number of shapes before the first round of the game.
        /// </summary>
        public int InitialShapesNumber { get; private set; }

        private Random rnd=new Random();

        /// <summary>
        /// Raises when a round or the whole game has finished.
        /// </summary>
        public event EventHandler<Results> GameFinished;

        /// <summary>
        /// Initialize a new game.
        /// </summary>
        /// <param name="playerName"></param>
        public Game(string playerName)
        {
            this.PlayerName = playerName;
            Round = 1;
            GoodPoints = 0;
            InitialShapesNumber = rnd.Next(2, 6);
        }
        public Results PlayTheGame(int startingRound=1,int startingPoints=0)
        {
            this.Round = startingRound;
            this.GoodPoints = startingPoints;

            Results r = new Results();

            for (int i = 0; i < 15; i++)
            {
                var f = PlayRound();
                if(f is Finish.MapCantBeInitialized)
                    Console.WriteLine("The map had a hard time initializing...");
                else if(f is Finish.OutOfRounds)
                    Console.WriteLine("Too much rounds");
                r.AddToResult(f);
                MessageThePlayer(r, f);
                if (i != 14)
                    Round++;
            }
            r.GoodPoints = this.GoodPoints;
            r.PlayerName = this.PlayerName;
            r.Round = this.Round;
            return r;
        }
        /// <summary>
        /// Informs the player why the round ended.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        private static void MessageThePlayer(Results r, Finish f)
        {
            switch (f)
            {
                case Finish.Cleard:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nGood job!! +100 points.");
                    r.GoodPoints += 100;
                    break;
                case Finish.LeftGameZone:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n\nThats not where you should be playing.");
                    break;
                case Finish.RegularVisitor:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\nUnfortunetly nostalgy does not adds points in this game, you" +
                        " cant revisit cells.");
                    break;

                case Finish.SmashedWithShape:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nCall an ambulance! he smashed his head in the shape!");
                    break;
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        /// <summary>
        /// Plays the upcoming round.
        /// </summary>
        /// <returns>The status at which the round ended.</returns>
        public Finish PlayRound()
        {
            //Checks that the round can be started.
            if(Round>15)
            {
                GameFinished?.Invoke(this, GenerateResults());
                return Finish.OutOfRounds;
            }  
            //initializing the round.
            var b=InitializeRound();
            //initializing the events.
            Map.BallMoved -= BallMoved;
            Map.BallMoved += BallMoved;
            //checks if the round couldnt be created.
            if(b==false)
            {
                GameFinished?.Invoke(this, GenerateResults());
                return Finish.MapCantBeInitialized;
            }
            Printer.ClearPrint(Map);
            TitleInfo(Map);
            //handles all the movments of the round.
            while (true)
            {
                TitleInfo(Map);
                //checks if the player has cleard the map.
                if (Map.EmptyCellsLeft < 1)
                    return Finish.Cleard;
                var direction=ReadMovement();
                var choice=Move(direction);
                if (choice.ShouldFinish)
                    return choice.Message;
            }
        }
        /// <summary>
        /// Returns true if the round should be finished and a message for it. otherwise returns false and no meaninful message.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private (bool ShouldFinish,Finish Message) Move(Direction d)
        {
            Point pos = GetNewBallPosition(d);
            bool validMove=false;
            try
            {
                validMove = Map.TryMoveBallTo(pos);
            }
            catch (InvalidOperationException e) 
            {
                if(InvalidOperationException.ReferenceEquals(e,Cell.BallToBall))
                {
                    return (true, Finish.TriesToReJump);
                }
            }
            catch (ArgumentException e)
            {
                if (object.ReferenceEquals(e, Cell.BallToShape))
                    return (true, Finish.SmashedWithShape);
                if (object.ReferenceEquals(e, Cell.BallToVisited))
                    return (true, Finish.RegularVisitor);
            }
            if (validMove == false)
            {
                GameFinished?.Invoke(this, GenerateResults());
                if (Map.IsPointInRange(pos) == false)
                    return (true,Finish.LeftGameZone);
                else
                    return (true,Finish.SmashedWithShape);
            }
            else
                GoodPoints++;
            return (false, Finish.EmptyMessage);
        }
        /// <summary>
        /// Returns the new position of the ball relative to the direction.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private Point GetNewBallPosition(Direction d)
        {
            Point pos = Map.BallPosition;
            switch (d)
            {
                case Direction.Up:
                    pos = pos.YMM();
                    break;
                case Direction.Right:
                    pos = pos.XPP();
                    break;
                case Direction.Down:
                    pos = pos.YPP();
                    break;
                default:
                    pos = pos.XMM();
                    break;

            }

            return pos;
        }


        /// <summary>
        /// Handles a momvent of the ball outside of the round maintaining.
        /// </summary>
        /// <param name="sender">Should be <see cref="Game.Map"/></param>
        /// <param name="e">Event arguments.</param>
        private void BallMoved(object sender,EventArgs e)
        {
            var b = sender as Board;
            //TitleInfo(b);
            Printer.PrintJustNeeded(b);
        }
        /// <summary>
        /// Writes the information of this game in the console title.
        /// </summary>
        /// <param name="b"></param>
        private void TitleInfo(Board b)
        {
            Console.Title = "Ball position: "+b.BallPosition.ToString() + $"\t, Round: {Round}\t" +
                $", Points: {GoodPoints}";//\t, Base number of shapes: {this.InitialShapesNumber+1}";
        }

        /// <summary>
        /// Reads the next key strike from the player and translates it to movement.
        /// </summary>
        /// <returns></returns>
        private Direction ReadMovement()
        {
            ConsoleKey input;
            while (true)
            {
                input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.PageUp:
                    case ConsoleKey.VolumeUp:
                    case ConsoleKey.W:
                        return Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.PageDown:
                    case ConsoleKey.VolumeDown:
                    case ConsoleKey.S:
                        return Direction.Down;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.RightWindows:
                    case ConsoleKey.D:
                        return Direction.Right;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.LeftWindows:
                    case ConsoleKey.A:
                        return Direction.Left;
                        break;
                }
            }
        }
        /// <summary>
        /// Initializing the round.
        /// </summary>
        /// <returns>True: if the round initialized succefully, false otherwise.</returns>
        private bool InitializeRound()
        {
            List<Shape> l = new List<Shape>();
            Map = new Board(l, new Point(rnd.Next(80),rnd.Next(25)), new Size(80, 25));
            for (int i = 0; i < Round+this.InitialShapesNumber; i++)
            {
                int tries = 0;
                while (true)
                {
                    var r = Map.TryAddShape(RandomShape());
                    if (r.Added)
                        break;
                    if (tries++ > 30)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Creates a list of random shape with random properties.
        /// </summary>
        /// <param name="shapeNumber">The amount of shapes to create.</param>
        /// <returns></returns>
        private IEnumerable<Shape> RandomShapes(int shapeNumber)
        {
            List<Shape> r = new List<Shape>();
            //0 is line
            //1 is triangle
            //2 is rectangle
            //3 is square
            for (int i = 0; i < shapeNumber; i++)
            {
                r.Add(RandomShape());
            }
            return r;
        }
        /// <summary>
        /// Craates one shape with random propeties.
        /// </summary>
        /// <returns></returns>
        private Shape RandomShape()
        {
            int shape = rnd.Next(4);
            switch (shape)
            {
                case 0:
                    return RandomLine();
                    break;
                case 1:
                    return RandomTrinagle();
                    break;
                case 2:
                    return RandomRectangle();
                    break;
                default:
                    return RandomSquare();
                    break;
            }
        }
        /// <summary>
        /// Returns a random position of within the <seealso cref="Game.Map"/>.
        /// </summary>
        /// <returns></returns>
        private Point GetRandomPosition()
        {
            int x = rnd.Next(Map.X_Length);
            int y = rnd.Next(Map.Y_Length);
            return new Point(x, y);
        }
        /// <summary>
        /// Creates a <see cref="Line"/> with randome properties.
        /// </summary>
        /// <returns></returns>
        private Line RandomLine()
        {
            int line = rnd.Next(2, 11);
            return new Line(Map, GetRandomPosition(), line);
        }
        /// <summary>
        /// Creates a <see cref="Trinagle"/> with randome properties.
        /// </summary>
        /// <returns></returns>
        private Trinagle RandomTrinagle()
        {
            int length = rnd.Next(2, 10);
            return new Trinagle(Map, GetRandomPosition(), length);
        }
        /// <summary>
        /// Creates a <see cref="Rectangle"/> with randome properties.
        /// </summary>
        /// <returns></returns>
        private Rectangle RandomRectangle()
        {
            int xl = rnd.Next(2, 11);
            int yl = rnd.Next(2, 11);
            return new Rectangle(Map,new Size(xl,yl) ,GetRandomPosition());
        }
        /// <summary>
        /// Creates a <see cref="Square"/> with randome properties.
        /// </summary>
        /// <returns></returns>
        private Square RandomSquare()
        {
            int length = rnd.Next(3, 11);
            return new Square(Map, length, GetRandomPosition());
        }
        /// <summary>
        /// Generates a <see cref="Results"/> instance from this game.
        /// </summary>
        /// <returns></returns>
        private Results GenerateResults()
        {
            return new Results() { Round = this.Round, GoodPoints = this.GoodPoints, PlayerName = this.PlayerName };
        }

    }
}
