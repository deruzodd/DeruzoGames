using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace GamesRapp
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, GameInfo> _games = new Dictionary<string, GameInfo>();
        private readonly FlowLayoutPanel flowLayoutPanelGames = new FlowLayoutPanel();

        public Form1()
        {
            InitializeComponent();
            InitializeFlowLayoutPanel();
            PopulateGameList();
        }

        private void InitializeFlowLayoutPanel()
        {
            flowLayoutPanelGames.Dock = DockStyle.Fill;
            this.Controls.Add(flowLayoutPanelGames);
        }

        private void PopulateGameList()
        {
            _games.Add("Змейка", new GameInfo("Змейка", @"D:\Visual Studio\source\repos\GamesRapp\Games\Snake\snake\bin\Debug\snake.exe"));
            _games.Add("Крестики-нолики", new GameInfo("Крестики-нолики", @"D:\Visual Studio\source\repos\GamesRapp\Games\TicTacToe\tictactoe\bin\Debug\tictactoe.exe"));
            _games.Add("Пазлы", new GameInfo("Пазлы", @"D:\Visual Studio\source\repos\GamesRapp\Games\Picture Puzzle Game\Picture Puzzle\bin\Debug\Picture Puzzle.exe"));
            _games.Add("Тетрис", new GameInfo("Тетрис", @"D:\Visual Studio\source\repos\GamesRapp\Games\tetris\bin\Debug\Tetris.exe"));
            _games.Add("Шахматы", new GameInfo("Шахматы", @"D:\Visual Studio\source\repos\GamesRapp\Games\Chess Game\Chess\bin\Debug\Chess.exe"));
            _games.Add("Flappy Bird", new GameInfo("Flappy Bird", @"D:\Visual Studio\source\repos\GamesRapp\Games\Flappy Bird Game\12- Flappy Bird\bin\Debug\12- Flappy Bird"));

            foreach (var game in _games)
            {
                GameControl gameControl = new GameControl(game.Value);      
                flowLayoutPanelGames.Controls.Add(gameControl);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class GameInfo
    {
        public string Name { get; }
        public string Path { get; }

        public GameInfo(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }

    public class GameControl : UserControl
    {   
        private GameInfo _game;

        public GameControl(GameInfo game)
        {
            _game = game;

            PictureBox pictureBox = new PictureBox
            {
                ImageLocation = @"Images\" + game.Name + ".jpg",
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 100,
                Height = 100
            };

            Label label = new Label
            {
                Text = game.Name,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom
            };

            this.Controls.Add(pictureBox);
            this.Controls.Add(label);

            pictureBox.Click += PictureBox_Click;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Вы уверены, что хотите запустить игру '{_game.Name}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Process.Start(_game.Path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка запуска игры: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
