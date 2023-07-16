using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace lesson_18
{
    public partial class TableForForm : Form
    {
        private List<char> _gameSymbols = new List<char>()
        {
            '!','@','#','$','%','*','+','^', //original
            '!','@','#','$','%','*','+','^', //copy
        };
        private List<Button> _gameCells = new List<Button>();

        private char _firstGameCell = ' ';
        private Button _firstGameCellButton;
        private char _secondGameCell = ' ';
        private Button _secondGameCellButton;

        //Timer
        private Timer _gameTimer;
        private int _gameTimerSeconds = 180; // 3 minutes

        //Timer for GameCells holding

        private Timer _showGameCellsTimer;
        //Dictionary<char, Button> _selectedGameCellMap = new Dictionary<char, Button>();

        public TableForForm()
        {
            InitializeComponent();
        }

        private void TableForForm_Load(object sender, EventArgs e)
        {
            ShuffleGameSymbols();
            InitializeGameCells();

            _gameTimer = new Timer();
            _gameTimer.Interval = 1000;
            _gameTimer.Tick += GameTimer_Tick;

            _gameTimer.Start();

            _showGameCellsTimer = new Timer();
            _showGameCellsTimer.Interval = 500;
            _showGameCellsTimer.Tick += ShowGameCellTimer_Tick;

        }

        private void ShowGameCellTimer_Tick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            _gameTimerSeconds--;
            if (_gameTimerSeconds <= 0)
            {
                _gameTimer.Stop();
                Close();
                MessageBox.Show("You're loser");
            }
            
            _timerLabel.Text = $"Timer : {(_gameTimerSeconds / 60):D2}:{(_gameTimerSeconds%60):D2}";
        }

        /// <summary>
        /// This method suffling the symbols 
        /// </summary>
        private void ShuffleGameSymbols()
        {
            Random random = new Random();
            for (int i = _gameSymbols.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                char temp = _gameSymbols[j];

                _gameSymbols[j] = _gameSymbols[i];
                _gameSymbols[i] = temp;

            }
        }

        /// <summary>
        /// THis method Initialize Game Cells
        /// </summary>
        private void InitializeGameCells()
        {
            Queue<char> _gameSymbolsQueue = new Queue<char>();

            foreach (char gameSymbol in _gameSymbols)
            {

                _gameSymbolsQueue.Enqueue(gameSymbol);
            }

            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    if (button.Tag == "GameCell")
                    {
                        button.Text = _gameSymbolsQueue.Dequeue().ToString();

                        _gameCells.Add(button);
                    }
                }
            }
        }

        private void GameCell_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (_firstGameCell == ' ')
                {
                    _firstGameCell = Char.Parse(button.Text);
                    _firstGameCellButton = button;
                    button.BackColor = Color.SteelBlue;
                    button.ForeColor = Color.White;
                }
                else
                {
                    if (_secondGameCell == ' ' && _firstGameCellButton != button)
                    {
                        _secondGameCell = Char.Parse(button.Text);
                        _secondGameCellButton = button;
                        button.BackColor = Color.SteelBlue;
                        button.ForeColor = Color.White;
                        
                        if (_firstGameCell == _secondGameCell)
                        {
                     

                            _firstGameCellButton.Visible = false;
                            _secondGameCellButton.Visible = false;


                            ResetGameCells();
                        }
                        else
                        {
                            _firstGameCellButton.BackColor = Color.White;
                            _secondGameCellButton.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        ResetGameCells();

                    }
                }


                bool isGameOver = _gameCells.All(button => button.Visible == false);

                if (isGameOver)
                {
                    _gameTimer.Stop();
                    Close();
                    MessageBox.Show("You're real Pablo Escobar");
                }
                
            }
        }

        private void ResetGameCells()
        {
            if (_firstGameCellButton is not null)
            {
                _firstGameCell = ' ';
                _firstGameCellButton.BackColor = Color.White;
            }
            if (_secondGameCellButton is not null)
            {
                _secondGameCell = ' ';
                _secondGameCellButton.BackColor = Color.White;
            }
        }
    }
}
