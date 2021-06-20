using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class TaskSetting
    {
        public void Go()
        {
            Console.WriteLine($"Task Id Main: {Task.CurrentId ?? -1}");
            Console.WriteLine($"Thread Id Main: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(new string('-', 80));

            Task task = new Task(new Action(DoSomething), TaskCreationOptions.PreferFairness |
                                                          TaskCreationOptions.LongRunning);
            
            task.Start();
            Thread.Sleep(50);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Main выполняется");
            }

            Console.ReadKey();
        }

        private static void DoSomething()
        {
            Console.WriteLine($"Task Id : {Task.CurrentId}");
            Console.WriteLine($"Thread Id : {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(new string('-', 80));

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Задача выполняется.");
                Thread.Sleep(100);
            }
            
            Console.WriteLine($"Задача Завершена в потоке : {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}