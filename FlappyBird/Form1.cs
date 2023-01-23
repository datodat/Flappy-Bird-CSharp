using System;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class World : Form
    {
        private const int SPEED = 8;

        private const int LEFT_EDGE = -100;
        private const int RIGHT_EDGE = 420;
        
        private int gravity = 10;

        private int score = 0;

        public World()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, EventArgs e)
        {
            bird.Top += gravity;

            pipeTop.Left -= SPEED;
            pipeBottom.Left -= SPEED;

            scoreTable.Text = string.Format("Score: {0}", score);

            // Relocating pipes to right
            if (pipeTop.Left < LEFT_EDGE)
            {
                pipeTop.Left = RIGHT_EDGE;
                score++;
            }

            if (pipeBottom.Left < LEFT_EDGE)
            {
                pipeBottom.Left = RIGHT_EDGE;
                score++;
            }

            // OnLose
            if (bird.Bounds.IntersectsWith(ground.Bounds) ||
                bird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                bird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                bird.Top < 0)
            {
                GameOver();
            }
        }

        // Space Key Handlers
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 10;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -10;
            }
        }

        // Game Over
        private void GameOver()
        {
            Timer.Stop();
            scoreTable.Text = string.Format("Game Over, Your score is: {0}", score);
            score = 0;
            restartButton.Visible = true;
            exitButton.Visible = true;
        }

        // Restart
        private void RestartHandler(object sender, EventArgs e)
        {
            Application.Restart();
        }

        // Exit
        private void ExitHandler(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
