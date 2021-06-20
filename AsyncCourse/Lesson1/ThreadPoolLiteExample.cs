using System;
using System.Threading;

namespace AsyncCourse.Lesson1
{
    public class ThreadPoolLiteExample
    {
        public void Go()
        {
            Console.WriteLine($"Id потока {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Запускаем?");
            Console.ReadKey();

            Report();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Example1));
            Report();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Example2));
            Report();
            
            Console.ReadKey();
            Report();
        }
        
        private static void Example1(object state)
        {
            Console.WriteLine($"Метод Example1 начал выполняться в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine($"Метод Example1 закончил выполняться в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
        
        private static void Example2(object state)
        {
            Console.WriteLine($"Метод Example2 начал выполняться в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Метод Example2 закончил выполняться в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
        
        private static void Report()
        {
            ThreadPool.GetMaxThreads(out int maxWorker, out int maxPortThread);
            ThreadPool.GetAvailableThreads(out int workerThreads, out int portThreads);
            
            Console.WriteLine($"Рабочих потоков {workerThreads} из {maxWorker}");
            Console.WriteLine($"IO потоки {portThreads} из {maxPortThread}");
            Console.WriteLine(new string('-', 80));
        }
    }
}