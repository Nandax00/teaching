using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Horse.Model
{
    public class HorseGameModel
    {
        #region Fields

        private Boolean[,] _table;
        private Int32 _figureX;
        private Int32 _figureY;
        private Int32 _score;
        private Int32 _fieldsDone;
        private Int32 _gameStepCount;
        private Int32 _maxsize;
        private Int32 _gameTime;
        Random _random;

        #endregion

        #region Properties

        // A játéktábla számára definiált indexer
        // A tábla ezáltal nem lesz a külvilág számára módosítható
        public Boolean this[Int32 i, Int32 j]
        {
            get =>_table [i, j];
            set => _table[i, j] = value;
        }     
        public Int32 FigureX => _figureX;
        public Int32 FigureY => _figureY;
        public Int32 FieldsDone => _fieldsDone;
        public Int32 GameStepCount => _gameStepCount;
        public Int32 Score => _score;
        public Int32 Size => _table.GetLength(0);
        public Boolean IsGameOver => ( _fieldsDone == _maxsize);

        #endregion

        #region Event handlers

        public event EventHandler<HorseEventArgs> StepReload;
        public event EventHandler<HorseEventArgs> GameOver;
        public event EventHandler<HorseEventArgs> StepBack;
        public event EventHandler<HorseEventArgs> GameAdvanced;

        #endregion

        #region Constructors
        public HorseGameModel()
        {
            _table = new Boolean[3, 3];
            _random = new Random();
        }

        #endregion

        #region Public methods
        public void NewGame(int size)
        {
            _fieldsDone = 1;
            _gameStepCount = 0;
            _figureX = 0;
            _figureY = 0;
            _score = 0;
            _table = new Boolean[size, size];
            _maxsize = size * size;
            _gameTime = 0;

            InitTable();
        }

        public bool Step(Int32 x, Int32 y)
        {
            if (!CheckStep(x,y))
                return false;

            if(!_table[x,y])
            {
                _score += 2;
                _fieldsDone++;
            }
            else
            {
                _score -= 1;
            }
            _table[_figureX, _figureY] = true;
            _gameStepCount++;

            OnStepReload(x, y, _figureX, _figureY);

            if (_gameStepCount % Size == 0)
            {
                int randX;
                int randY;

                do
                {
                    randX = _random.Next(0, Size - 1);
                    randY = _random.Next(0, Size - 1);
                }
                while (!_table[randX,randY] || randX == _figureX && randY == _figureY);
                _table[randX, randY] = false;

                if (_fieldsDone > 0)
                {
                    _fieldsDone--;
                }
                OnStepBack(randX, randY);
            }

            _figureX = x;
            _figureY = y;

            if (_fieldsDone == _maxsize) // ha vége a játéknak, jelezzük, hogy győztünk
            {
                OnGameOver();
            }
            return true;
        }

        public void AdvanceTime()
        {
            _gameTime++;
            OnGameAdvanced();
        }

        #endregion

        #region Private methods

        private Boolean CheckStep(int x, int y)
        {
            if(Math.Abs(x - _figureX) == 1 && Math.Abs(y-_figureY) == 2 || 
                Math.Abs(x - _figureX) == 2 && Math.Abs(y - _figureY) == 1)
            {
                return true;
            }
            return false;
        }

        private void InitTable()
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    _table[i, j] = false;
                }
            }
        }

        private void OnStepReload(int x, int y, int lx, int ly)
        {
            StepReload?.Invoke(this, new HorseEventArgs(x, y, lx, ly));
        }

        private void OnStepBack(int x, int y)
        {
            StepBack?.Invoke(this, new HorseEventArgs(x, y));
        }

        private void OnGameOver()
        {
            GameOver?.Invoke(this, new HorseEventArgs(_score));
        }

        private void OnGameAdvanced()
        {
            GameAdvanced?.Invoke(this, new HorseEventArgs(_gameTime, true));
        }

        #endregion
    }
}
