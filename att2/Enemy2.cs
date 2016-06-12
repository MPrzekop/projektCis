using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SpaceBattle
{
    class Enemy2 : Ship, shipChar
    {
        private Ship player;
        private Random rand = new Random();
        private double shootTimer = 0;

        public Enemy2(int x, int y, System.Windows.Controls.Canvas canv, Window win, Ship player) : base(x, y, canv, win)
        {
            speed = 200 * (rand.Next() % 10 + 1) / 10 + player.level * 5;
            this.player = player;
            win.Dispatcher.Invoke((Action)(() =>
            {


                sprite.Source = new BitmapImage(new Uri("Enemy2.png", UriKind.Relative));
                sprite.Height = 40;
                sprite.Width = 40;

                canv.Children.Add(sprite);
                Canvas.SetLeft(sprite, posX);
                Canvas.SetTop(sprite, posY);
            }));
            shootSpeed = 40;
            exp = 50;
            hp = 75 + player.level * 20;
            maxHp = hp;
            str = 10;
            type = "enemy2";

        }

        public override void move()
        {
            shootTimer++;

            if (player != null)
            {
                if (Math.Sqrt(Math.Pow(player.posX - posX, 2) + Math.Pow(player.posY - posY, 2)) > 200)
                {
                    if (player.posX > posX)
                    {
                        posX += speed / (float)deltaTime;
                    }
                    if (player.posY > posY)
                    {
                        posY += speed / (float)deltaTime;
                    }
                    if (player.posX < posX)
                    {
                        posX -= speed / (float)deltaTime;
                    }
                    if (player.posY < posY)
                    {
                        posY -= speed / (float)deltaTime;
                    }

                }
                else if (Math.Sqrt(Math.Pow(player.posX - posX, 2) + Math.Pow(player.posY - posY, 2)) < 180)
                {
                    if (player.posX > posX)
                    {
                        posX -= speed * (rand.Next() % 7 + 1) / 7 / (float)deltaTime;
                    }
                    if (player.posY > posY)
                    {
                        posY -= speed * (rand.Next() % 7 + 1) / 7 / (float)deltaTime;
                    }
                    if (player.posX < posX)
                    {
                        posX += speed * (rand.Next() % 7 + 1) / 7 / (float)deltaTime;
                    }
                    if (player.posY < posY)
                    {
                        posY += speed * (rand.Next() % 7 + 1) / 7 / (float)deltaTime;
                    }
                    if (shootTimer > shootSpeed)
                    {
                        shootTimer = 0;
                        shoot();
                    }
                }
                else
                {
                    if (shootTimer > shootSpeed)
                    {
                        shootTimer = 0;
                        shoot();
                    }
                }

                rotate(player);
                base.move();
            }

        }//move close

        public override void shoot()
        {
            if (player.posX - 10 > posX && player.posY - 10 > posY)
            {
                bullets.Add(new Bullet(posX + 10, posY + 10, (int)Directions.downRight, canv, win, 0));

            }
            else if (player.posX + 10 < posX && player.posY - 10 > posY)
            {
                bullets.Add(new Bullet(posX + 10, posY + 10, (int)Directions.downLeft, canv, win, 0));

            }
            else if (player.posX + 10 < posX && player.posY + 10 < posY)
            {
                bullets.Add(new Bullet(posX + 10, posY + 10, (int)Directions.upLeft, canv, win, 0));

            }
            else if (player.posX - 10 > posX && player.posY + 10 < posY)
            {
                bullets.Add(new Bullet(posX + 10, posY + 10, (int)Directions.upRight, canv, win, 0));

            }
            else if (player.posX + 10 < posX && player.posY < posY + 10 && player.posY > posY - 10)
            {
                bullets.Add(new Bullet(posX + 10, posY + 10, (int)Directions.left, canv, win, 0));

            }
            else if (player.posX > posX && player.posY < posY + 10 && player.posY > posY - 10)
            {
                bullets.Add(new Bullet(posX + 10, posY + 10, (int)Directions.right, canv, win, 0));

            }
            else if (player.posX < posX + 10 && player.posX > posX - 10 && player.posY - 10 > posY)
            {
                bullets.Add(new Bullet(posX + 10, posY + 10, (int)Directions.down, canv, win, 0));

            }
            else if (player.posX < posX + 10 && player.posX > posX && player.posY + 10 < posY)
            {
                bullets.Add(new Bullet(posX + 10, posY + 10, (int)Directions.up, canv, win, 0));

            }
        }


    }
}
