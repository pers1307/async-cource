using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class TaskWait
    {
        public void Go()
        {
            var tasks = new Task[]
            {
                new Task(DoSomething, 1000),
                new Task(DoSomething, 800),
                new Task(DoSomething, 2000),
                new Task(DoSomething, 1000),
                new Task(DoSomething, 3500)
            };

            Console.WriteLine($"Метод Main выполняется...");

            foreach (var task in tasks)
                task.Start();
            
            Console.WriteLine($"Метод Main ожидает...");
            
            foreach (var task in tasks)
                task.Wait(); // Ждать выполнение всех потоков
            
            // Task.WaitAll(tasks);
            // Task.WaitAny(tasks);
            
            Console.WriteLine($"Метод Main продолжает свою работу");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Main({i})");
            }
        }
        
        private static void DoSomething(object sleepTime)
        {
            Console.WriteLine($"Задача #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}");
            
            Thread.Sleep((int)sleepTime);
           
            Console.WriteLine($"Задача #{Task.CurrentId} завершилась в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}