using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceBattle
{
    public interface shipChar
    {
        void move();
        void shoot();
    }
    abstract class Ship : shipChar
    {
        private int _hp;
        private float _posX;
        private float _posY;
        private string _type;
        private Image _sprite;
        private Image _bullet;
        private Image _healthBar;
        private Image _health;
        private List<Image> _explosion;
        private List<Bullet> _bullets;
        private Canvas _canv;
        private System.Windows.Window _win;
        private int _shootSpeed;
        private int _exp;
        private int _str;
        private int _points;
        private int _level;
        protected int maxHp;
        protected int speed;
        protected double deltaTime;
        protected enum Directions { up, upRight, right, downRight, down, downLeft, left, upLeft };
        //class variables


        public Ship(int x, int y, Canvas canv, System.Windows.Window win)
        {
            deltaTime = 100;
            _bullets = new List<Bullet>();
            posX = x;
            posY = y;
            this.win = win;
            this.canv = canv;
            win.Dispatcher.Invoke((Action)(() =>
            {
                sprite = new Image();
                sprite.RenderTransformOrigin = new Point(0.5, 0.5);
                health = new Image();
                healthBar = new Image();
                health.Source = new BitmapImage(new Uri("Health.png", UriKind.Relative));
                healthBar.Source = new BitmapImage(new Uri("HealthBackground.png", UriKind.Relative));
                healthBar.Width = 40;
                healthBar.Height = 10;
                health.Height = 10;
                canv.Children.Add(healthBar);
                canv.Children.Add(health);
                Canvas.SetTop(healthBar, posY + 44);
                Canvas.SetLeft(healthBar, posX);
                Canvas.SetTop(health, posY + 44);
                Canvas.SetLeft(health, posX);
                health.Width = 40 * ((float)hp / (float)maxHp);
            }));
        }//constructor close

        virtual public void move()
        {

            moveBullets();
            draw();
        }

        virtual public void shoot()
        {

        }

        public void moveBullets()
        {
            try
            {
                foreach (Bullet i in bullets)
                {
                    i.move();

                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    if ((bullets[i].posX < 0 || bullets[i].posX > 900 || bullets[i].posY > 900 || bullets[i].posY < 0))
                    {
                        win.Dispatcher.Invoke((Action)(() =>
                        {
                            canv.Children.Remove(bullets[i].sprite);
                        }));
                        bullets.Remove(bullets[i]);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }
        protected void rotate(Ship player)
        {
            drawHealth();
            win.Dispatcher.Invoke((Action)(() =>
            {
                if (player.posX - 10 > posX && player.posY - 10 > posY)
                {

                    sprite.RenderTransform = new RotateTransform(135);
                }
                else if (player.posX + 10 < posX && player.posY - 10 > posY)
                {

                    sprite.RenderTransform = new RotateTransform(-135);
                }
                else if (player.posX + 10 < posX && player.posY + 10 < posY)
                {

                    sprite.RenderTransform = new RotateTransform(-45);
                }
                else if (player.posX - 10 > posX && player.posY + 10 < posY)
                {

                    sprite.RenderTransform = new RotateTransform(45);
                }
                else if (player.posX + 10 < posX && player.posY < posY + 10 && player.posY > posY - 10)
                {

                    sprite.RenderTransform = new RotateTransform(-90);
                }
                else if (player.posX > posX && player.posY < posY + 10 && player.posY > posY - 10)
                {

                    sprite.RenderTransform = new RotateTransform(90);
                }
                else if (player.posX < posX + 10 && player.posX > posX - 10 && player.posY - 10 > posY)
                {

                    sprite.RenderTransform = new RotateTransform(180);
                }
                else if (player.posX < posX + 10 && player.posX > posX && player.posY + 10 < posY)
                {

                    sprite.RenderTransform = new RotateTransform(0);
                }
            }));
        }
        public void drawHealth()
        {
            if (hp < 0)
            {
                hp = 0;
            }
            win.Dispatcher.Invoke((Action)(() =>
            {
                Canvas.SetTop(healthBar, posY + 44);
                Canvas.SetLeft(healthBar, posX);
                Canvas.SetTop(health, posY + 44);
                Canvas.SetLeft(health, posX);
                health.Width = 40 * ((float)hp / (float)maxHp);
                //health.Width = 20;

            }));
        }

        public void draw()
        {
            win.Dispatcher.Invoke((Action)(() =>
            {
                Canvas.SetLeft(sprite, posX);
                Canvas.SetTop(sprite, posY);
            }));
            for (int j = bullets.Count - 1; j >= 0; j--)
            {
                win.Dispatcher.Invoke((Action)(() =>
                {
                    Canvas.SetLeft(bullets[j].sprite, bullets[j].posX);
                    Canvas.SetTop(bullets[j].sprite, bullets[j].posY);
                }));

            }
        }
        public void death()
        {
            win.Dispatcher.Invoke((Action)(() =>
            {
                canv.Children.Remove(sprite);
                canv.Children.Remove(health);
                canv.Children.Remove(healthBar);
                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    canv.Children.Remove(bullets[i].sprite);
                    bullets.Remove(bullets[i]);
                }
            }));
        }

        public int level
        {
            get { return _level; }
            set { _level = value; }
        }
        public int shootSpeed
        {
            get { return _shootSpeed; }
            set { _shootSpeed = value; }
        }
        public int str
        {
            get { return _str; }
            set { _str = value; }
        }
        public int exp
        {
            get { return _exp; }
            set { _exp = value; }
        }
        public int points
        {
            get { return _points; }
            set { _points = value; }
        }
        public List<Bullet> bullets
        {
            get { return _bullets; }

        }
        public Image health
        {
            get { return _health; }
            set { _health = value; }
        }
        public Image healthBar
        {
            get { return _healthBar; }
            set { _healthBar = value; }
        }
        public Image sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }//sprite close

        public Image bullet
        {
            get { return _bullet; }
        }//bullet close
        public int hp
        {
            get { return _hp; }
            set { _hp = value; }
        }//hp close
        public float posX
        {
            get { return _posX; }
            set { _posX = value; }
        }//posX close

        public float posY
        {
            get { return _posY; }
            set { _posY = value; }
        }//posY close

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }//type close

        public System.Windows.Window win
        {
            get { return _win; }
            set { _win = value; }
        }//main window close

        public Canvas canv
        {
            get { return _canv; }
            set { _canv = value; }
        }//main canvas close



    }
}
