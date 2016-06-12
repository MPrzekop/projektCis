using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SpaceBattle
{
    class Enemy1 : Ship, shipChar
    {
        private Ship player;

        public Enemy1(int x, int y, System.Windows.Controls.Canvas canv, Window win, Ship player) : base(x, y, canv, win)
        {
            speed = 50 + player.level * 5;
            this.player = player;
            win.Dispatcher.Invoke((Action)(() =>
            {

                sprite.Source = new BitmapImage(new Uri("Enemy1.png", UriKind.Relative));
                sprite.Height = 40;
                sprite.Width = 40;
                canv.Children.Add(sprite);
                Canvas.SetLeft(sprite, posX);
                Canvas.SetTop(sprite, posY);
            }));

            exp = 100;
            hp = 150 + player.level * 20;
            maxHp = hp;
            str = 50;
            type = "enemy1";

        }

        public override void move()
        {
            rotate(player);
            drawHealth();
            if (player != null)
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
                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < 40; j++)
                    {
                        if ((int)player.posX + 20 == (int)posX + i && (int)player.posY + 20 == (int)posY + j)
                        {
                            shoot();
                        }
                    }
                }
            }
            base.move();
        }

        public override void shoot()
        {
            player.hp -= str;
            hp = 0;
            death();
        }


    }
}
