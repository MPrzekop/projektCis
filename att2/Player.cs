using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace SpaceBattle
{

    class Player : Ship, shipChar
    {



        private double shootTimer;
        TextBlock txtScore;
        TextBlock txtStats;
        public Player(int x, int y, System.Windows.Controls.Canvas canv, Window win) : base(x, y, canv, win)
        {
            shootTimer = 0;
            speed = 300;
            hp = 100;
            maxHp = hp;
            str = 20;
            shootSpeed = 25;
            points = 0;
            type = "player";

            win.Dispatcher.Invoke((Action)(() =>
            {
                sprite.Source = new BitmapImage(new Uri("Player.png", UriKind.Relative));
                sprite.Height = 40;
                sprite.Width = 40;
                Canvas.SetLeft(sprite, posX);
                Canvas.SetTop(sprite, posY);

                txtScore = new TextBlock();
                txtStats = new TextBlock();
                txtStats.Text = "level: " + level.ToString() + "\nstrength: " + str.ToString() + "\nshoot speed: " + (25 - shootSpeed).ToString() + "\nhealth: " + hp.ToString() + "\nspeed: " + speed.ToString();
                txtScore.Text = points.ToString();
                txtStats.Width = 140;
                txtStats.Height = 140;
                txtScore.Width = 70;
                txtScore.Height = 40;
                txtStats.Foreground = Brushes.White;
                txtScore.Foreground = Brushes.White;
                txtScore.FontSize = 20;
                txtStats.FontSize = 20;
                canv.Children.Add(txtScore);
                canv.Children.Add(txtStats);
                canv.Children.Add(sprite);
                Canvas.SetTop(txtStats, 10);
                Canvas.SetLeft(txtStats, 50);
                Canvas.SetTop(txtScore, 10);
                Canvas.SetLeft(txtScore, 800);
            }));

        }//constructor close

        public override void move()
        {
            levelUp();
            drawHealth();
            shootTimer++;
            win.Dispatcher.Invoke((Action)(() =>
            {
                txtStats.Text = "level: " + level.ToString() + "\nstrength: " + str.ToString() + "\nshoot speed: " + (25 - shootSpeed).ToString() + "\nhealth: " + hp.ToString() + "\nspeed: " + speed.ToString();
                txtScore.Text = points.ToString();
                rotate();
                moveCross();
                moveDiagonally();
                if (checkForArrow() && shootTimer > shootSpeed) //shoot in given interval (x*10) time in milliseconds
                {
                    shoot();
                    shootTimer = 0;
                }
            }));
            base.move();

        }//move close



        private void moveCross()
        {
            if (a() && !w() && !d() && !s())
            {
                posX -= speed / (float)deltaTime; // pikxels per second
            }
            if (!a() && w() && !d() && !s())
            {
                posY -= speed / (float)deltaTime;
            }
            if (!a() && !w() && d() && !s())
            {
                posX += speed / (float)deltaTime;
            }
            if (!a() && !w() && !d() && s())
            {
                posY += speed / (float)deltaTime;
            }
        }
        private void moveDiagonally()
        {

            if (!a() && !w() && d() && s())
            {
                posY += (speed / (float)deltaTime) / (float)Math.Sqrt(2);
                posX += (speed / (float)deltaTime) / (float)Math.Sqrt(2);
            }
            if (!a() && w() && d() && !s())
            {
                posY -= (speed / (float)deltaTime) / (float)Math.Sqrt(2);
                posX += (speed / (float)deltaTime) / (float)Math.Sqrt(2);
            }
            if (a() && !w() && !d() && s())
            {
                posY += (speed / (float)deltaTime) / (float)Math.Sqrt(2);
                posX -= (speed / (float)deltaTime) / (float)Math.Sqrt(2);
            }
            if (a() && w() && !d() && !s())
            {
                posY -= (speed / (float)deltaTime) / (float)Math.Sqrt(2);
                posX -= (speed / (float)deltaTime) / (float)Math.Sqrt(2);
            }
        }
        public override void shoot()
        {
            if (up() && !down() && !left() && !right())
            {
                bullets.Add(new Bullet(posX + 20, posY + 20, (int)Directions.up, canv, win, 1));
            }
            else if (up() && !down() && left() && !right())
            {
                bullets.Add(new Bullet(posX + 20, posY + 20, (int)Directions.upLeft, canv, win, 1));
            }
            else if (up() && !down() && !left() && right())
            {
                bullets.Add(new Bullet(posX + 20, posY + 20, (int)Directions.upRight, canv, win, 1));
            }
            else if (!up() && !down() && !left() && right())
            {
                bullets.Add(new Bullet(posX + 20, posY + 20, (int)Directions.right, canv, win, 1));
            }
            else if (!up() && !down() && left() && !right())
            {
                bullets.Add(new Bullet(posX + 20, posY + 20, (int)Directions.left, canv, win, 1));
            }
            else if (!up() && down() && !left() && right())
            {
                bullets.Add(new Bullet(posX + 20, posY + 20, (int)Directions.downRight, canv, win, 1));
            }
            else if (!up() && down() && left() && !right())
            {
                bullets.Add(new Bullet(posX + 20, posY + 20, (int)Directions.downLeft, canv, win, 1));
            }
            else if (!up() && down() && !left() && !right())
            {
                bullets.Add(new Bullet(posX + 20, posY + 20, (int)Directions.down, canv, win, 1));
            }


        }//shoot close

        private bool checkForArrow()
        {
            if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0) { return true; }
            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0) { return true; }
            if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0) { return true; }
            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0) { return true; }

            return false;

        }//arrow is pressed check
        private void rotate()
        {
            if (a() && w() && !d() && !s())
            {
                sprite.RenderTransform = new RotateTransform(-45);
            }
            if (a() && !w() && !d() && !s())
            {
                sprite.RenderTransform = new RotateTransform(-90);
            }
            if (!a() && w() && !d() && !s())
            {
                sprite.RenderTransform = new RotateTransform(0);
            }
            if (!a() && w() && d() && !s())
            {
                sprite.RenderTransform = new RotateTransform(45);
            }
            if (!a() && !w() && d() && !s())
            {
                sprite.RenderTransform = new RotateTransform(90);
            }
            if (!a() && !w() && d() && s())
            {
                sprite.RenderTransform = new RotateTransform(135);
            }
            if (!a() && !w() && !d() && s())
            {
                sprite.RenderTransform = new RotateTransform(180);
            }
            if (a() && !w() && !d() && s())
            {
                sprite.RenderTransform = new RotateTransform(-135);
            }
        }// rotate sprite

        private void levelUp()
        {
            if (points > Math.Pow(level, 2) * 100)
            {
                hp += 10;
                level++;
                str += 5;
                shootSpeed--;
                speed += 15;
            }
        }

        private bool up()
        {
            if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0) { return true; }
            return false;
        }
        private bool down()
        {
            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0) { return true; }
            return false;
        }
        private bool right()
        {
            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0) { return true; }
            return false;
        }
        private bool left()
        {
            if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0) { return true; }
            return false;
        }
        private bool w()
        {
            if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0) { return true; }
            return false;
        }
        private bool s()
        {
            if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0) { return true; }
            return false;
        }
        private bool d()
        {
            if ((Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0) { return true; }
            return false;
        }
        private bool a()
        {
            if ((Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0) { return true; }
            return false;
        }



    }
}
