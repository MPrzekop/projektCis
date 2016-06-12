using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SpaceBattle
{
    class StartMenu
    {
        private TextBlock menu;
        private Window win;
        private Canvas canv;
        private Game game;
        public StartMenu(Canvas canv, Window win)
        {
            this.win = win;
            this.canv = canv;
            win.Dispatcher.Invoke((Action)(() =>
            {
                menu = new TextBlock();
                menu.FontSize = 40;
                menu.Foreground = Brushes.White;
                menu.Text = "start game: S\nexit: Q";
                canv.Children.Add(menu);
                menu.Width = 400;
                menu.Height = 200;
                Canvas.SetTop(menu, 100);
                Canvas.SetLeft(menu, 300);

            }));
        }

        public void waitForInput()
        {
            bool gameOn = false;
            win.Dispatcher.Invoke((Action)(() =>
            {
                if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0) { gameOn = true; canv.Children.Remove(menu); }
                else if ((Keyboard.GetKeyStates(Key.Q) & KeyStates.Down) > 0) { Application.Current.Shutdown(); }
            }));
            if (gameOn)
            {
                game = new Game(win, canv);
                gameLoop();
            }
        }

        public void gameLoop()
        {
            while (game.gameOn)
            {

                game.action();

            }
            win.Dispatcher.Invoke((Action)(() =>
            {
                canv.Children.Add(menu);
            }));
        }
    }
}
