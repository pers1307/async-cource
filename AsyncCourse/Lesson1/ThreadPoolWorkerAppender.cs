using System;
using System.Threading;

namespace AsyncCourse.Lesson1
{
    public class ThreadPoolWorkerAppender
    {
        public void Go()
        {
            Console.WriteLine("Запускаем?");
            Console.ReadKey();

            ThreadPoolWorker threadPoolWorker = new ThreadPoolWorker(new Action<object>(StarWriter));
            threadPoolWorker.Start('*');

            for (int i = 0; i < 40; i++)
            {
                Console.Write('-');
                Thread.Sleep(50);
            }

            threadPoolWorker.Wait();
            
            Console.WriteLine("Все");
        }
        
        private static void StarWriter(object arg)
        {
            char item = (char) arg;
            for (int i = 0; i < 120; i++)
            {
                Console.Write(item);
                Thread.Sleep(50);
            }
        }
    }
}