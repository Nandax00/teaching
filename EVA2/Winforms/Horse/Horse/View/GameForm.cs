using System;
using System.Drawing;
using System.Windows.Forms;
using Horse.Model;

namespace Horse.View
{
    public partial class GameForm : Form
    {
        #region Fields

        private HorseGameModel _model;
        private Button[,] _buttonGrid;
        private Timer _timer;
        private Boolean _paused;

        #endregion

        #region Constructors
   
        public GameForm()
        {
            InitializeComponent();
            this.AutoSize = true;
        }

        #endregion

        #region Public methods

        public void GameForm_Load(Object sender, EventArgs e)
        {
            _model = new HorseGameModel();
            _model.GameOver += new EventHandler<HorseEventArgs>(Game_GameOver);
            _model.StepReload += new EventHandler<HorseEventArgs>(Game_StepReload);
            _model.StepBack += new EventHandler<HorseEventArgs>(Game_StepBack);
            _model.GameAdvanced += new EventHandler<HorseEventArgs>(Game_GameAdvanced);

            _paused = false;

            GenerateTable(3);
            _model.NewGame(3);

            SetTimer();
        }

        #endregion

        #region Menu event handlers

        private void MenuGameEasy_Click(Object sender, EventArgs e)
        {
            SetGame(3);
        }

        private void MenuGameMedium_Click(Object sender, EventArgs e)
        {
            SetGame(4);
        }

        private void MenuGameHard_Click(Object sender, EventArgs e)
        {
            SetGame(6);
        }

        private void Pause_Click(Object sender, EventArgs e)
        {
            if (!_paused)
            {
                foreach (Button b in _buttonGrid)
                    b.Enabled = false;

                _paused = true;
                _timer.Stop();
            }
            else
            {
                foreach (Button b in _buttonGrid)
                    b.Enabled = true;

                _paused = false;
                _timer.Start();
            }
        }

        #endregion

        #region Private methods

        private void Game_GameOver(Object sender, HorseEventArgs e)
        {
            foreach (Button b in _buttonGrid)
                b.Enabled = false;

            _timer.Stop();

            MessageBox.Show("Gratulálok, győztél!" + Environment.NewLine +
                                "Összesen " + e.Score + " pontot szereztél! ",
                                "Bejárás huszárral",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);

            SetGame(_model.Size);
            SetTimer();
        }

        private void GenerateTable(Int32 size)
        {
            if (_buttonGrid != null)
            {
                for (Int32 i = 0; i < _buttonGrid.GetLength(0); i++)
                {
                    for (Int32 j = 0; j < _buttonGrid.GetLength(0); j++)
                    {
                        Controls.Remove(_buttonGrid[i, j]);
                    }
                }
            }

            _buttonGrid = new Button[size, size];
            Int32 x = 30;
            Int32 y = 50;

            for (Int32 i = 0; i < size; i++)
            {
                for (Int32 j = 0; j < size; j++)
                {
                    _buttonGrid[i, j] = new Button
                    {
                        Location = new Point(x, y),             // elhelyezkedés
                        Size = new Size(40, 40),                // méret
                        Enabled = true,                         // bekapcsolt állapot
                        TabIndex = 100 + i * _model.Size + j    // a gomb számát a TabIndex-ben tároljuk
                    };

                    _buttonGrid[i, j].BackColor = (i + j) % 2 == 0 ? Color.White : Color.Black;

                    _buttonGrid[i, j].MouseClick += new MouseEventHandler(ButtonClick);

                    Controls.Add(_buttonGrid[i, j]);
                    y += 40;
                }

                x += 40;
                y = 50;
            }

            _buttonGrid[0, 0].BackColor = Color.Yellow;
        }

        private void SetGame(Int32 size)
        {
            _model.NewGame(size);
            GenerateTable(size);
            Score.Text = _model.Score.ToString();
        }

        private void SetTimer()
        {
            _timer = new Timer { Interval = 1000 };
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }

        private void ButtonClick(Object sender, EventArgs e)
        {
            Int32 x = (((Button) sender).TabIndex - 100) / _model.Size;
            Int32 y = (((Button) sender).TabIndex - 100) % _model.Size;
            _model.Step(x, y);
        }

        private void Game_StepReload(Object sender, HorseEventArgs e)
        {
            _buttonGrid[e.LastX, e.LastY].BackColor = (e.LastX + e.LastY) % 2 == 0 ? Color.LightBlue : Color.Navy;
            _buttonGrid[e.X, e.Y].BackColor = Color.Yellow;
            Score.Text = _model.Score.ToString();
        }

        private void Game_StepBack(Object sender, HorseEventArgs e)
        {
            _buttonGrid[e.BackX, e.BackY].BackColor = (e.BackX + e.BackY) % 2 == 0 ? Color.White : Color.Black;
        }

        private void Timer_Tick(Object sender, EventArgs e)
        {
            _model.AdvanceTime();
        }

        private void Game_GameAdvanced(Object sender, HorseEventArgs e)
        {
            TimeLabel.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g"); // "g" <= format string
        }

        #endregion
    }
}
