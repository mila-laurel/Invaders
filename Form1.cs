using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invaders
{
    public partial class Form1 : Form
    {
        List<Keys> keysPressed = new List<Keys>();
        bool gameOver;
        private Game game;
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            game = new Game(random, ClientRectangle);
            gameOver = false;
            gameplayTimer.Start();
            game.GameOver += Game_GameOver;
        }

        private void Game_GameOver(object sender, EventArgs e)
        {
            gameplayTimer.Stop();
            gameOver = true;
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();
            if (gameOver)
                if(e.KeyCode == Keys.S)
                {
                    gameOver = false;
                    game = new Game(random, ClientRectangle);
                    gameplayTimer.Start();
                    return;
                }
            if (e.KeyCode == Keys.Space)
                game.FireShot();
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
            keysPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }

        int animationCell = 0;
        int frame = 0;
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            game.Twinkle();
            frame++;
            if (frame >= 6)
                frame = 0;
            switch (frame)
            {
                case 0:
                    animationCell = 0;
                    break;
                case 1:
                    animationCell = 1;
                    break;
                case 2:
                    animationCell = 2;
                    break;
                case 3:
                    animationCell = 3;
                    break;
                case 4:
                    animationCell = 2;
                    break;
                case 5:
                    animationCell = 1;
                    break;
                default:
                    animationCell = 0;
                    break;
            }
            Invalidate();
        }

        private void gameplayTimer_Tick(object sender, EventArgs e)
        {
            game.Go();
            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Left)
                {
                    game.MovePlayer(Direction.Left);
                    return;
                }
                else if (key == Keys.Right)
                {
                    game.MovePlayer(Direction.Right);
                    return;
                }
            }
            if (gameOver)
                gameplayTimer.Stop();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (gameOver)
            {
                using (Font font = new Font("Arial", 32, FontStyle.Bold))
                {
                    g.DrawString("GAME OVER", font, Brushes.Yellow, 210, 230);
                    g.DrawString("Press S to start a new game or Q to quit", font, Brushes.White, 630, 420);
                }
            }
            game.Draw(g, animationCell);         
        }
    }
}
