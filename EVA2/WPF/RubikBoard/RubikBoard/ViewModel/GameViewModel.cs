using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using RubikBoard.Model;

namespace RubikBoard.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        #region Fields
        private Random random = new Random();
        private GameModel model;
        private Int32 size = 0;
        private Int32 timerCount;
        private Model.Direction dir = Model.Direction.Up;
        #endregion

        #region Properties
        public ObservableCollection<Field> Fields { get; set; }

        public DelegateCommand LvlCommand { get; private set; }
        public DelegateCommand SetDirectionCommand { get; private set; }

        public String Time { get { return TimeSpan.FromSeconds(timerCount).ToString("g"); } }
        public DispatcherTimer Timer { get; private set; }

        public Int32 Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructors
        public GameViewModel(GameModel model)
        {
            this.model = model;

            LvlCommand = new DelegateCommand(param => 
            {
                SetUpGame(Convert.ToInt32(param));
                Size = (Convert.ToInt32(param));
            });

            SetDirectionCommand = new DelegateCommand(param => { SetDirection(Convert.ToInt32(param)); });

            model.GameOver += new EventHandler<GameEventArgs>(Model_GameOver);
        }
        #endregion

        #region Public methods
        public void SetUpGame(Int32 n)
        {
            timerCount = 0;
            Size = n;
            Fields = new ObservableCollection<Field>();
            for (Int32 i = 0; i < n; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < n; j++)
                {
                    Fields.Add(new Field
                    {
                        Color = i,
                        X = i,
                        Y = j,
                        Number = i * n + j, // a gomb sorszáma, amelyet felhasználunk az azonosításhoz
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                        // ha egy mezőre léptek, akkor jelezzük a léptetést, változtatjuk a lépésszámot
                    });
                }
            }

            model.NewGame(n);
            RefreshTable();

            if (Timer == null)
            {
                Timer = new DispatcherTimer();
                Timer.Interval = TimeSpan.FromSeconds(1);
                Timer.Tick += new EventHandler(Timer_Tick);
                Timer.Start();
            }
            else
            {
                Timer.Start();
            }
        }
        #endregion

        #region Private methods
        private void SetDirection(Int32 d)
        {
            dir = (Model.Direction)d;
        }

        private void StepGame(Int32 index)
        {
            Field field = Fields[index];
            model.Step(field.X, field.Y, dir);
            RefreshTable();

            // Minden n. lépés után beszúr egy random lépést
            if(model.GameStepCount == Size)
            {     
                Int32 direction = random.Next(0, 3);
                Int32 row = random.Next(0, size - 1);
                Int32 column = random.Next(0, size - 1);
                model.Step(row, column, (Model.Direction)direction);
                RefreshTable();
            }
        }

        private void RefreshTable()
        {
            for (Int32 i = 0; i < Size; i++)
            {
                for (Int32 j = 0; j < Size; j++)
                {
                    Fields[i * Size + j].Color = model.Table[i, j];
                    OnPropertyChanged("Fields");
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timerCount++;
            OnPropertyChanged("Time");
        }

        private void Model_GameOver(object sender, GameEventArgs e)
        {
            RefreshTable();
        }
        #endregion
    }
}
