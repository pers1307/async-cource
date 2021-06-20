using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class TaskContinuation
    {
        public void Go()
        {
            var task = new Task(new Action<object>(OperationAsync), "Hello world");
            var continuation = task.ContinueWith(Continuation);

            /**
             * Можно строить цепочки продолжений 
             */
            continuation.ContinueWith(t =>
            {
                // Какой - либо код
                // Console.WriteLine($"{t.Result}");
                Console.WriteLine($"Продолжение #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}");
            // Также можно добавлять опции 
            }, TaskContinuationOptions.ExecuteSynchronously);
            
            Console.WriteLine($"Статус продолжения - {continuation.Status}");
            
            task.Start();

            Console.ReadKey();
        }
        
        private static void OperationAsync(object arg)
        {
            Console.WriteLine($"Задача #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Arg - {arg}");
            Console.WriteLine($"Задача #{Task.CurrentId} завершилась в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void Continuation(Task task)
        {
            Console.WriteLine($"Продолжение #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Параметр задачи - {task.AsyncState}");
            Console.WriteLine($"Конец");
        }
    }
}