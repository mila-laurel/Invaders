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
        bool gameOver = false;
        private Game game;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();
            if (gameOver)
                if(e.KeyCode == Keys.S)
                {
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

        int cell = 0;
        int frame = 0;
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            frame++;
            if (frame >= 6)
                frame = 0;
            switch (frame)
            {
                case 0:
                    cell = 0;
                    break;
                case 1:
                    cell = 1;
                    break;
                case 2:
                    cell = 2;
                    break;
                case 3:
                    cell = 3;
                    break;
                case 4:
                    cell = 2;
                    break;
                case 5:
                    cell = 1;
                    break;
                default:
                    cell = 0;
                    break;
            }

        }
    }
}
