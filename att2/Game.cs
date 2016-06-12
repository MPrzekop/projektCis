using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Diagnostics;

namespace SpaceBattle
{
    class Game
    {
        private int enemyCount = 0;
        private Window win;
        private Canvas canv;
        private List<Ship> ships;
        private double spawnTimer;
        private bool game = true;
        private Random rand = new Random();
        public Game(Window win, Canvas canv)
        {
            this.win = win;
            this.canv = canv;
            ships = new List<Ship>();
            spawnTimer = 0;
            ships.Add(new Player(500, 500, canv, win));
        }
        public void action()
        {
            spawnTimer++;
            for (int i = ships.Count - 1; i >= 0; i--)
            {
                ships[i].move();
            }
            try
            {
                for (int i = ships.Count - 1; i >= 0; i--)
                {
                    if (ships[i].hp <= 0)
                    {
                        if (i != 0)
                        {
                            enemyCount--;
                            ships[0].points += ships[i].exp;
                            ships[i].death();
                            ships[i] = null;
                            ships.Remove(ships[i]);
                        }
                        else
                        {
                            endGame();
                        }

                    }
                }
                System.Threading.Thread.Sleep(10);
            }
            catch (ArgumentOutOfRangeException)
            {
                endGame();
            }
            catch (IndexOutOfRangeException)
            {
                endGame();
            }
            collisionDetection();
            if (spawnTimer >= 75) { SpawnEnemy(); }

        }

        private void endGame()
        {

            foreach (Ship i in ships)
            {
                i.death();
            }
            ships.Clear();
            win.Dispatcher.Invoke((Action)(() =>
            {
                canv.Children.Clear();
            }));
            game = false;
        }

        private void collisionDetection()
        {

            int shCount = ships.Count - 1;

            Task t = Task.Run(() =>
            {
                try
                {

                    for (int i = shCount; i >= 0; i--)
                    {
                        for (int j = ships[i].bullets.Count - 1; j >= 0; j--)
                        {
                            collisionBulletShip(i, j, shCount);
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
            });
            t.Wait();

        }

        private void collisionBulletShip(int i, int j, int shCount)
        {
            for (int k = shCount; k >= 0; k--)

            {
                try
                {
                    if (((ships[k].posX <= ships[i].bullets[j].posX && ships[k].posX + 40 >= ships[i].bullets[j].posX) && ships[k].posY <= ships[i].bullets[j].posY) && ships[k].posY + 40 >= ships[i].bullets[j].posY && ((ships[k].type != "player" && ships[i].bullets[j].side == 1) || (ships[k].type == "player" && ships[i].bullets[j].side == 0)))
                    {
                        ships[k].hp -= ships[i].str;
                        ships[i].bullets[j].delete();
                        return;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                }
                catch (NullReferenceException)
                {
                    return;


                }
            }
        }

        private void SpawnEnemy()
        {
            spawnTimer = 0;
            if (ships.Count < 10)
            {
                switch (rand.Next() % 4)
                {
                    case 0:
                        if (rand.Next() % 2 == 0)
                        {
                            ships.Add(new Enemy1(-10, rand.Next() % 900, canv, win, ships[0]));
                        }
                        else
                        {
                            ships.Add(new Enemy2(-10, rand.Next() % 900, canv, win, ships[0]));
                        }
                        enemyCount++;
                        return;
                    case 1:
                        if (rand.Next() % 2 == 0)
                        {
                            ships.Add(new Enemy1(910, rand.Next() % 900, canv, win, ships[0]));
                        }
                        else
                        {
                            ships.Add(new Enemy2(910, rand.Next() % 900, canv, win, ships[0]));
                        }
                        enemyCount++;
                        return;
                    case 2:
                        if (rand.Next() % 2 == 0)
                        {
                            ships.Add(new Enemy1(rand.Next() % 900, -10, canv, win, ships[0]));
                        }
                        else
                        {
                            ships.Add(new Enemy2(rand.Next() % 900, -10, canv, win, ships[0]));
                        }
                        enemyCount++;
                        return;
                    case 3:
                        if (rand.Next() % 2 == 0)
                        {
                            ships.Add(new Enemy1(rand.Next() % 900, 910, canv, win, ships[0]));
                        }
                        else
                        {
                            ships.Add(new Enemy2(rand.Next() % 900, 910, canv, win, ships[0]));
                        }
                        enemyCount++;
                        return;
                }
            }

        }

        public bool gameOn
        {
            get { return game; }
        }
    }
}
