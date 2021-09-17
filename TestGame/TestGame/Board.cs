using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    /// <summary>
    /// The game board.
    /// </summary>
    public class Board
    {
        //length0=x, (0,0)=top left
        //length1=y

        private Cell[,] board;

        //internal set should not stay at release!
        /// <summary>
        /// Returns the <see cref="Cell"/> in the <paramref name="x"/> and <paramref name="y"/> indexes.
        /// </summary>
        /// <param name="x">The x part of the indexers.</param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell this[int x,int y] { get => board[x, y]; internal set => board[x, y] = value; }
        /// <summary>
        /// Returns the cell in the corresponds position.
        /// </summary>
        /// <param name="position">The cell position.</param>
        /// <returns></returns>
        public Cell this[Point position] { get => this[position.X, position.Y]; internal set => this[position.X, position.Y] = value; }
        /// <summary>
        /// The width of this <see cref="board"/>.
        /// </summary>
        public int X_Length { get => board.GetLength(0); }
        /// <summary>
        /// The height of this <see cref="board"/>.
        /// </summary>
        public int Y_Length { get => board.GetLength(1); }
        /// <summary>
        /// The number of cells with <see cref="Cell.State"/> equals to <see cref="CellState.Empty"/>.
        /// </summary>
        public int EmptyCellsLeft => EmptyCells();
        /// <summary>
        /// The position of the ball in this <see cref="board"/>.
        /// </summary>
        public Point BallPosition => BallPos();
        /// <summary>
        /// The previous position of the ball.
        /// </summary>
        public Point LastBallPosition { get; protected set; } = new Point(-1, -1);
        
        /// <summary>
        /// Raises when the ball has been moved.
        /// </summary>
        public event EventHandler<EventArgs> BallMoved;

        /// <summary>
        /// Helper for <seealso cref="Board.EmptyCellsLeft"/>.
        /// </summary>
        /// <returns></returns>
        private int EmptyCells()
        {
            int r = 0;
            foreach (var cell in board)
            {
                if (cell.State == CellState.Empty)
                    r++;
            }
            return r;
        }
        /// <summary>
        /// Helper for <see cref="Board.BallPosition"/>.
        /// </summary>
        /// <returns></returns>
        private Point BallPos()
        {
            foreach (var cell in board)
            {
                if (cell.State == CellState.Ball)
                    return cell.Position;
            }
            return new Point(-1, -1);
        }

        public Board(IEnumerable<Shape> shapes,Point ballPosition,Size size)
        {
            board = new Cell[size.Width, size.Height];
            Validate(ballPosition, size);

            //First- create a board without shapes.
            for (int i = 0; i < X_Length; i++)
            {
                for (int j = 0; j < Y_Length; j++)
                {
                    if (i == ballPosition.X && j == ballPosition.Y)
                        this[i, j] = new Cell(this, CellState.Ball);
                    else
                        this[i, j] = new Cell(this, CellState.Empty);
                }
            }

            //iterate through the shapes and place them
            foreach (var shape in shapes)
            {
                var t=TryAddShape(shape);
                if (t.Added == false)
                    throw t.WhyCant;
                /*shape.Parent = this;
                if(shape is Rectangle r)
                {
                    int startx = r.Position.X;
                    if (r.Width + startx > this.X_Length|| r.Height + r.Position.Y > this.Y_Length)
                        throw new PositionOutOfBoardException("The shape extends out of the board boundries!");
                    for (int j = 0; j < r.Height; j++)
                    {
                        int y = j + r.Position.Y;
                        for (int i = 0; i < r.Width; i++)
                        {
                            if (this[startx + i,y].State != CellState.Empty)
                                throw new ColidingObjectsException($"The position ({startx + i},{y}) is already" +
                                    $" occupied by {this[startx + i, y].State}");
                            this[startx + i, y] = new Cell(this, CellState.Shape, r);
                        }
                    }
                    continue;
                }
                if(shape is Trinagle t)
                {
                    int start = t.Position.X;
                    if (t.Length + start > this.X_Length||t.Length+t.Position.Y>this.Y_Length)
                        throw new PositionOutOfBoardException("The shape extends out of the board boundries!");
                    for (int j = 0; j < t.Length; j++)
                    {
                        int length = j + 1;
                        int y = j + t.Position.Y;
                        for (int i = 0; i < length; i++)
                        {
                            if (this[start + i, y].State != CellState.Empty)
                                throw new ColidingObjectsException($"The position ({start + i},{y}) is already" +
                                    $" occupied by {this[start + i, y].State}");
                            this[start + i, y] = new Cell(this, CellState.Shape, t);
                        }
                    }
                    continue;
                }
                if(shape is Line l)
                {
                    int start = l.Position.X;
                    if (l.Length + start > this.X_Length)
                        throw new PositionOutOfBoardException("The shape extends out of the board boundries!");
                    for (int i = 0; i < l.Length; i++)
                    {
                        if (this[start + i, l.Position.Y].State != CellState.Empty)
                            throw new ColidingObjectsException($"The position ({start+i},{l.Position.Y}) is already" +
                                $" occupied by {this[start + i, l.Position.Y].State}");
                        this[start + i, l.Position.Y] = new Cell(this, CellState.Shape, l);
                    }
                    continue;
                }*/
            }
        }
        /// <summary>
        /// Validates the size and the position of the ball.
        /// </summary>
        /// <param name="ballPosition"></param>
        /// <param name="size"></param>
        private void Validate(Point ballPosition, Size size)
        {

            if (IsPointInRange(ballPosition) == false)
                throw new PositionOutOfBoardException("The position is out side of the board!");
            if (size.Height < 1 || size.Width < 1)
                throw new ArgumentOutOfRangeException("The size should be grater than 0!");
        }

        /// <summary>
        /// Indicates whether or not <paramref name="pos"/> is in the plane of this <see cref="Board"/>.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsPointInRange(Point pos)
        {
            if (pos.X >= this.X_Length || pos.Y >= this.Y_Length || pos.X < 0 || pos.Y < 0)
                return false;
            return true;
        }
        /// <summary>
        /// Gets all the cells in <paramref name="cells"/> to be empty again.
        /// </summary>
        /// <param name="cells"></param>
        private void RestoreToEmpty(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                this[cell.Position] = new Cell(this, CellState.Empty);
            }
        }

        /// <summary>
        /// Returns the position of the cell <paramref name="c"/>.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Point GetPosition(Cell c)
        {
            for (int i = 0; i <X_Length; i++)
            {
                for (int j = 0; j < Y_Length; j++)
                {
                    if (board[i, j] == c)
                        return new Point(i, j);
                }
            }
            return new Point(-1,-1);
        }
        /// <summary>
        /// Tries to move the ball to <paramref name="position"/> and if possiable-does it.
        /// </summary>
        /// <param name="position">The new desired position of the ball.</param>
        /// <returns>True if the ball moves succesfully, false otherwise.</returns>
        public bool TryMoveBallTo(Point position)
        {
            if (this.IsPointInRange(position) == false)
                return false;
            LastBallPosition = BallPosition;
            bool r = this[BallPosition].MoveBallOut();
            this[position].MoveBallIn();
            BallMoved?.Invoke(this, new EventArgs());
            return r;
        }
        /// <summary>
        /// Tries to add the <paramref name="shape"/> to the board.
        /// </summary>
        /// <param name="shape">The shape to add to this <see cref="Board"/>.</param>
        /// <returns>True and unmeaningful exception if the shape has been added succefully, false and an explaining 
        /// reason in the exception otherwise.</returns>
        public (bool Added,Exception WhyCant) TryAddShape(Shape shape)
        {
            shape.Parent = this;
            List<Cell> checkList = new List<Cell>();
            if (shape is Rectangle r)
            {
                int startx = r.Position.X;
                if (r.Width + startx > this.X_Length || r.Height + r.Position.Y > this.Y_Length)
                    return (false, new PositionOutOfBoardException("The shape extends out of the board boundries!"));
                for (int j = 0; j < r.Height; j++)
                {
                    int y = j + r.Position.Y;
                    for (int i = 0; i < r.Width; i++)
                    {
                        if (this[startx + i, y].State != CellState.Empty)
                        {
                            RestoreToEmpty(checkList);
                            return (false, new ColidingObjectsException($"The position ({startx + i},{y}) is already" +
                                $" occupied by {this[startx + i, y].State}"));
                        }
                        var c= new Cell(this, CellState.Shape, r);
                        this[startx + i, y] = c;
                        checkList.Add(c);
                    }
                }
                return (true,new Exception("Its all good.."));
            }
            if (shape is Trinagle t)
            {
                int start = t.Position.X;
                if (t.Length + start > this.X_Length || t.Length + t.Position.Y > this.Y_Length)
                    return (false, new PositionOutOfBoardException("The shape extends out of the board boundries!"));
                for (int j = 0; j < t.Length; j++)
                {
                    int length = j + 1;
                    int y = j + t.Position.Y;
                    for (int i = 0; i < length; i++)
                    {
                        if (this[start + i, y].State != CellState.Empty)
                        {
                            RestoreToEmpty(checkList);
                            return (false, new ColidingObjectsException($"The position ({start + i},{y}) is already" +
                                $" occupied by {this[start + i, y].State}"));
                        }
                        var c= new Cell(this, CellState.Shape, t);
                        checkList.Add(c);
                        this[start + i, y] = c;
                    }
                }
                return (true, new Exception("Its all good.."));
            }
            if (shape is Line l)
            {
                int start = l.Position.X;
                if (l.Length + start > this.X_Length)
                    return (false, new PositionOutOfBoardException("The shape extends out of the board boundries!"));
                for (int i = 0; i < l.Length; i++)
                {
                    if (this[start + i, l.Position.Y].State != CellState.Empty)
                    {
                        RestoreToEmpty(checkList);
                        return (false, new ColidingObjectsException($"The position ({start + i},{l.Position.Y}) is" +
                            $" already" + $" occupied by {this[start + i, l.Position.Y].State}"));
                    }
                    var c= new Cell(this, CellState.Shape, l);
                    checkList.Add(c);
                    this[start + i, l.Position.Y] = c;
                }
                return (true, new Exception("Its all good.."));
            }
            return (false, new UnKnownErrorException("Unknown error occured..."));
        }

    }
}
