﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    public partial class Form1 : Form
    {
        List<Keys> keysPressed = new List<Keys>();
        bool gameOver = true;
        private Game game;
        Random random = new Random();
        private string HeaderText = "Welcome";

        public Form1()
        {
            InitializeComponent();
        }

        private void Game_GameOver()
        {
            gameplayTimer.Stop();
            HeaderText = "GAME OVER LOOSER";
            gameOver = true;
            Invalidate();
        }
        
        private void RestartGame()
        {
            if(game != null)
                game.GameOver -= Game_GameOver;
            game = new Game(random, ClientRectangle);
            game.GameOver += Game_GameOver;
            gameOver = false;
            gameplayTimer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();
            if (gameOver)
                if(e.KeyCode == Keys.S)
                {
                    RestartGame();
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
            game?.Twinkle();
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
                    g.FillRectangle(Brushes.Black, ClientRectangle);
                    var clientRectangleMiddle = new Point(ClientRectangle.Width / 2, ClientRectangle.Height / 2);
                    g.DrawString(HeaderText, font, Brushes.Yellow, 20, clientRectangleMiddle.Y - 50);
                    g.DrawString("Press S to start a new game or Q to quit", font, Brushes.White, 20, clientRectangleMiddle.Y + 50);
                }
            }
            else
                game.Draw(g, animationCell);         
        }
    }
}
