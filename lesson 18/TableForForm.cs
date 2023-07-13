using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        //Dictionary<char, Button> _selectedGameCellMap = new Dictionary<char, Button>();

        public TableForForm()
        {
            InitializeComponent();
        }

        private void TableForForm_Load(object sender, EventArgs e)
        {
            ShuffleGameSymbols();
            InitializeGameCells();
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
                    if (_secondGameCell == ' ')
                    {
                        _secondGameCell = Char.Parse(button.Text);
                        _secondGameCellButton = button;
                        button.BackColor = Color.SteelBlue;
                        button.ForeColor = Color.White;

                        if (_firstGameCell == _secondGameCell)
                        {
                            Controls.Remove(_firstGameCellButton);
                            Controls.Remove(_secondGameCellButton);
                        }
                    }
                    else
                    {
                        _firstGameCell = ' ';
                        _firstGameCellButton.BackColor = Color.White;
                        _secondGameCell = ' ';
                        _secondGameCellButton.BackColor = Color.White;
                    }
                }

            }
        }
    }
}
