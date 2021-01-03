using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helicopter_Shooter_Game_MOO_ICT
{
    public partial class Form1 : Form
    {

        bool goUp, goDown, shot, gameOver;

        int score = 0;
        int speed = 8;
        int UFOspeed = 10;

        Random rand = new Random();

        int playerSpeed = 7;
        int index = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            if (goUp == true && player.Top > 0)
            {
                player.Top -= playerSpeed;
            }
            if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += playerSpeed;
            }

            ufo.Left -= UFOspeed;

            if (ufo.Left + ufo.Width < 0)
            {
                ChangeUFO();
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "pillar")
                {
                    x.Left -= speed;


                    if (x.Left < -200)
                    {
                        x.Left = 1000;
                    }

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        GameOver();
                    }

                }

                if (x is PictureBox && (string)x.Tag == "bullet")
                {
                    x.Left += 25;

                    if (x.Left > 900)
                    {
                        RemoveBullet(((PictureBox)x));
                    }


                    if (ufo.Bounds.IntersectsWith(x.Bounds))
                    {
                        RemoveBullet(((PictureBox)x));
                        score += 1;
                        ChangeUFO();
                    }

                }

            }


            if (player.Bounds.IntersectsWith(ufo.Bounds))
            {
                GameOver();
            }

            if (score > 10)
            {
                speed = 12;
                UFOspeed = 18;
            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }

            if (e.KeyCode == Keys.Space && shot == false)
            {
                MakeBullet();
                shot = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (shot == true)
            {
                shot = false;
            }

            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }

        }

        private void RestartGame()
        {

            goUp = false;
            goDown = false;
            shot = false;
            gameOver = false;
            score = 0;
            speed = 8;
            UFOspeed = 10;

            txtScore.Text = "Score: " + score;

            ChangeUFO();

            player.Top = 119;

            pillar1.Left = 579;
            pillar2.Left = 253;

            GameTimer.Start();


        }

        private void GameOver()
        {
            GameTimer.Stop();
            txtScore.Text = "Score: " + score + " Game over, press enter to retry!";
            gameOver = true;
        }

        private void RemoveBullet(PictureBox bullet)
        {
            this.Controls.Remove(bullet);
            bullet.Dispose();
        }

        private  void MakeBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.BackColor = Color.Maroon;
            bullet.Height = 5;
            bullet.Width = 10;

            bullet.Left = player.Left + player.Width;
            bullet.Top = player.Top + player.Height / 2;

            bullet.Tag = "bullet";

            this.Controls.Add(bullet);

        }

        private void ChangeUFO()
        {


            if (index > 3)
            {
                index = 1;
            }
            else
            {
                index += 1;
            }


            switch (index)
            {
                case 1:
                    ufo.Image = Properties.Resources.alien1;
                    break;
                case 2:
                    ufo.Image = Properties.Resources.alien2;
                    break;
                case 3:
                    ufo.Image = Properties.Resources.alien3;
                    break;
            }

            ufo.Left = 1000;

            ufo.Top = rand.Next(20, this.ClientSize.Height - ufo.Height);

        }
    }
}
