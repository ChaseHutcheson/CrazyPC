using System;
using System.Threading;
using System.Media;
using System.Diagnostics;
using System.Windows.Forms;

namespace CrazyPC.Threads
{
    internal class CrazyPcClass
    {
        public static Random _random = new Random();
        public static void CrazyFunctionCall()
        {
            while (true)
            {
                Thread crazy_mouse = new Thread(new ThreadStart(CrazyMouseThread));
                Thread crazy_keys = new Thread(new ThreadStart(CrazyKeyboardThread));
                Thread crazy_sounds = new Thread(new ThreadStart(CrazySpeakersThread));

                crazy_mouse.Start();
                crazy_keys.Start();
                crazy_sounds.Start();

                // Wait for 5 seconds
                Thread.Sleep(5000);

                // Request threads to stop
                stopThreads = true;

                // Join threads to ensure they finish before proceeding
                crazy_mouse.Join();
                crazy_keys.Join();
                crazy_sounds.Join();

                // Wait for 5 minutes
                Thread.Sleep(300000);

                // Reset stopThreads flag for the next iteration
                stopThreads = false;
            }
        }

        private static volatile bool stopThreads = false;

        static void CrazyMouseThread()
        {
            int moveX = 0;
            int moveY = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (!stopThreads && stopwatch.ElapsedMilliseconds <= 5000)
            {
                moveX = _random.Next(20) - 10;
                Console.WriteLine(moveX);
                moveY = _random.Next(20) - 10;
                Console.WriteLine(moveY);

                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);
                Thread.Sleep(100);
            }
        }

        static void CrazyKeyboardThread()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (!stopThreads && stopwatch.ElapsedMilliseconds <= 5000)
            {
                int random_num = _random.Next(0, 26);
                char random_letter = (char)('a' + random_num);
                SendKeys.SendWait(random_letter.ToString());
            }
        }

        static void CrazySpeakersThread()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (!stopThreads && stopwatch.ElapsedMilliseconds <= 5000)
            {
                int random_num = _random.Next(3);

                switch (random_num)
                {
                    case 0:
                        SystemSounds.Exclamation.Play();
                        Thread.Sleep(500);
                        break;
                    case 1:
                        SystemSounds.Question.Play();
                        Thread.Sleep(500);
                        break;
                    case 2:
                        SystemSounds.Beep.Play();
                        Thread.Sleep(500);
                        break;
                    case 3:
                        SystemSounds.Asterisk.Play();
                        Thread.Sleep(500);
                        break;
                }
            }
        }
    }
}
