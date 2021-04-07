using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Projekt_pokusy2
{
    public class ScreenObject
    {
        public int FormerX;
        public int X;
        public int Y;
        public string Look;

        public ScreenObject(int formerX, int x, int y, string look)
        {
            FormerX = formerX;
            X = x;
            Y = y;
            Look = look;
        }
    }


    public class Enemy : ScreenObject
    {
        public Enemy(int formerX, int x, int y, string look) : base(formerX, x, y, look)
        {
        }

        public void MoveEnemy()
        {
            FormerX = X;

            Random randomNumber = new Random();
            int number = randomNumber.Next(1, 3);
            if (number == 1)
            {
                X++;
            }
            if (number == 2) X--;
        }
    }

    public class Player : ScreenObject
    {
        public Player(int formerX, int x, int y, string look) : base(formerX, x, y, look)
        {
        }
        public void MovePlayer(int number)
        {
            FormerX = X;
            X += number;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;
            Enemy enemy = new Enemy(0, width / 2, 1, "@");
            Player player = new Player(0, width / 2, height, "X");

            TimeSpan threadSleepTimeSpan = TimeSpan.FromMilliseconds(301);

            Console.CursorVisible = false;
            Console.Clear();

            var autoEvent = new AutoResetEvent(false);
            var consoleTimer = new Timer(ConsoleRender, autoEvent, 1000, 300);


            while (true)
            {
                if (Console.KeyAvailable)   //neceka, az neco zmacknu
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.RightArrow:
                            Console.SetCursorPosition(player.X, player.Y);
                            Console.Write(" ");
                            player.MovePlayer(1);
                            break;
                        case ConsoleKey.LeftArrow:
                            Console.SetCursorPosition(player.X, player.Y);
                            Console.Write(" ");
                            player.MovePlayer(-1);
                            break;
                    }
                }

                while (Console.KeyAvailable)    //zajisti, ze pro jedno zmacknuti to udela jednu akci, pohyb se "nezasekava"
                {
                    Console.ReadKey(true);
                }

                Thread.Sleep(threadSleepTimeSpan);   //uspani while, aby byl pohyb plynuly
            }

            void ConsoleRender(Object stateInfo)
            {
                enemy.MoveEnemy();
                Console.SetCursorPosition(enemy.FormerX, enemy.Y);
                Console.Write(" ");

                Console.SetCursorPosition(enemy.X, enemy.Y);
                Console.Write(enemy.Look);

                Console.SetCursorPosition(player.X, player.Y);
                Console.Write(player.Look);
            }

            //Console.ReadLine();

        }

    }
}




