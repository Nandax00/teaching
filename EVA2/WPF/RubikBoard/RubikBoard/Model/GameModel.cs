using System;

namespace RubikBoard.Model
{
    public enum Direction { Up, Down, Left, Right }
    public class GameModel
    {
        #region Fields
        private Int32 size;
        private Boolean started = false;
        private Random random = new Random();
        #endregion

        #region Properties
        public Int32[,] Table { get; private set; }

        public Int32 GameStepCount { get; private set; }
        #endregion

        #region Events
        public event EventHandler<GameEventArgs> GameOver;
        #endregion

        #region Constructors
        public GameModel()
        {
            GameStepCount = 0;
            started = false;
        }
        #endregion

        #region Public methods
        public void NewGame(Int32 n)
        {
            started = false;
            Table = new Int32[n, n];
            for(Int32 i = 0; i < n; i++)
            {
                for(Int32 j = 0; j < n; j++)
                {
                    Table[i, j] = i;
                }
            }
            size = n;
            Mix(size);
            started = true;
        }

        public void Step(Int32 x, Int32 y, Direction direction)
        {
            Int32 remember;
            switch(direction)
            {
                case Direction.Up:
                    remember = Table[0, y];
                    for(Int32 i = 0; i < size-1; i++)
                    {
                        Table[i, y] = Table[i + 1, y];
                    }
                    Table[size - 1, y] = remember;
                    break;

                case Direction.Down:
                    remember = Table[size-1, y];
                    for (Int32 i = size - 1; i > 0; i--)
                    {
                        Table[i, y] = Table[i - 1, y];
                    }
                    Table[0, y] = remember;
                    break;

                case Direction.Left:
                    remember = Table[x, 0];
                    for (Int32 i = 0; i < size - 1; i++)
                    {
                        Table[x, i] = Table[x, i + 1];
                    }
                    Table[x, size - 1] = remember;
                    break;

                case Direction.Right:
                    remember = Table[x, size - 1];
                    for (Int32 i = size - 1; i > 0; i--)
                    {
                        Table[x, i] = Table[x, i - 1];
                    }
                    Table[x, 0] = remember;
                    break;
            }

            if((IsGameOverRow() || IsGameOverColumn()) && started)
            {
                OnGameOver();
            }

            if (started)
            {
                GameStepCount++;
                if (GameStepCount == size + 1)
                {
                    GameStepCount = 0;
                }
            }
        }

        public void Mix(Int32 n)
        {
            for(Int32 i = 0; i < n * n * n; i++)
            {
                Int32 dir = random.Next(1, 4);
                Int32 row = random.Next(0, n - 1);
                Int32 column = random.Next(0, n - 1);
                Step(row, column, (Direction)dir);
            }
        }

        public Boolean IsGameOverRow()
        {
            for(Int32 i = 0; i < size; i++)
            {
                for(Int32 j = 1; j < size; j++)
                {
                    if(Table[i,j] != Table[i,j-1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Boolean IsGameOverColumn()
        {
            for (Int32 i = 1; i < size; i++)
            {
                for (Int32 j = 0; j < size; j++)
                {
                    if (Table[i, j] != Table[i - 1, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Private methods
        private void OnGameOver()
        {
            if (GameOver != null)
                GameOver(this, new GameEventArgs());
        }
        #endregion
    }
}
