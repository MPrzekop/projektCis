using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SpaceBattle
{
    class Bullet
    {
        private Image _sprite;
        private enum Directions { up, upRight, right, downRight, down, downLeft, left, upLeft };
        private float _posX;
        private float _posY;
        private int _side;
        private int dir;
        private Canvas canv;
        private Window win;
        private int speed = 8;

        public Bullet(float x, float y, int dir, Canvas canv, Window win, int side)
        {
            this.canv = canv;
            this.win = win;
            this._side = side;
            this._posX = x;
            this._posY = y;
            this.dir = dir;
            win.Dispatcher.Invoke((Action)(() =>
            {
                _sprite = new Image();
                if (side == 1)
                {
                    _sprite.Source = new BitmapImage(new Uri("GoodBullet.png", UriKind.Relative));
                }
                else
                {
                    _sprite.Source = new BitmapImage(new Uri("BadBullet.png", UriKind.Relative));
                }
                _sprite.Height = 20;
                _sprite.Width = 20;
                canv.Children.Add(_sprite);
            }));
        }
        public void move()
        {

            switch (dir)
            {
                case (int)Directions.up:
                    posY -= speed;
                    break;
                case (int)Directions.upRight:
                    posY -= (float)speed / (float)Math.Sqrt(2);
                    posX += (float)speed / (float)Math.Sqrt(2);
                    break;
                case (int)Directions.right:
                    posX += speed;
                    break;
                case (int)Directions.downRight:
                    posY += (float)speed / (float)Math.Sqrt(2);
                    posX += (float)speed / (float)Math.Sqrt(2);
                    break;
                case (int)Directions.down:
                    posY += speed;
                    break;
                case (int)Directions.downLeft:
                    posY += (float)speed / (float)Math.Sqrt(2);
                    posX -= (float)speed / (float)Math.Sqrt(2);
                    break;
                case (int)Directions.left:
                    posX -= speed;
                    break;
                case (int)Directions.upLeft:
                    posY -= (float)speed / (float)Math.Sqrt(2);
                    posX -= (float)speed / (float)Math.Sqrt(2);
                    break;
            }

        }

        public void delete()
        {
            posX = 1000;
            posY = 1000;
            win.Dispatcher.Invoke((Action)(() =>
            {
                canv.Children.Remove(_sprite);
            }));
        }
        public float posX
        {
            get { return _posX; }
            set { _posX = value; }
        }

        public Image sprite
        {
            get { return this._sprite; }
        }
        public float posY
        {
            get { return _posY; }
            set { _posY = value; }
        }
        public int side
        {
            get { return _side; }
        }
    }
}
