using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Timers;

namespace SpaceBattle
{
    class GameController
    {


        Canvas canv;
        Window win;
        public GameController(Canvas Grid, Window win)
        {
            this.win = win;
            canv = Grid;
        }
        public void StartGame()
        {



            Task.Run(() =>
            {
                StartMenu strt = new StartMenu(canv, win);
                while (true)
                {
                    strt.waitForInput();
                }
            });
        }
    }
}
