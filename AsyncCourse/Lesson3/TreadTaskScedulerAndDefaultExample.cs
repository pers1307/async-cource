using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson3
{
    public class TreadTaskScedulerAndDefaultExample
    {
        public void Go()
        {
            Console.SetWindowSize(100, 45);
            Console.WriteLine($"Поток метода Main - {Thread.CurrentThread.ManagedThreadId}.");
            
            // Вызывает методы в контексте пула потоков
            // один поток уходит на таймер
            Timer timer = new Timer(ShowThreadPoolInfo, null, 1000, 1000);
            Task[] tasks = new Task[30];

            TaskScheduler scheduler = null;
            scheduler = TaskScheduler.Default;
            //scheduler = new ThreadTaskScheduler();

            Console.WriteLine($"TaskScheduler - {scheduler.GetType()}");

            for (int i = 0; i < 30; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(3000);
                    Console.WriteLine($"Выполнена задача #{Task.CurrentId} в потоке {Thread.CurrentThread.ManagedThreadId} из пула потоков - {Thread.CurrentThread.IsThreadPoolThread}");
                });

                tasks[i].Start(scheduler);
            }

            Task.WaitAll(tasks);
            Thread.Sleep(2000);
            timer.Dispose();

            Console.ReadKey();
        }
        
        private static void ShowThreadPoolInfo(object _)
        {
            ThreadPool.GetAvailableThreads(out int threads, out int completionPorts);
            ThreadPool.GetMaxThreads(out int maxThreads, out int maxCompletionPorts);
            Console.WriteLine($"                                Worker Threads - [{threads}:{maxThreads}]");
            Console.WriteLine($"                                Completion Ports - [{completionPorts}:{maxCompletionPorts}]");
        }
    }
}