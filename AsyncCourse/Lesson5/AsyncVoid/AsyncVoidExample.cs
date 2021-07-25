using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson5.AsyncVoid
{
    public class AsyncVoidExample
    {
        public void Go()
        {
            Main();
        }
        
        static AsyncVoidExample()
        {
            SynchronizationContext.SetSynchronizationContext(new TestSyncContext());
        }

        private void Main()
        {
            MethodAsync();

            Console.ReadKey();
        }

        // лучше использовать тип Task чтобы было где перехватывать исключение и все не упало 
        private static async void MethodAsync()
        {
            Console.WriteLine($"Код до await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() => Console.WriteLine($"Задача выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}"));
            
            // Без контекста синхронизации все упадет, потому что нет корректной обработки ошибок
            //await Task.Run(() => throw new AsyncVoidException("Ошибка при выполнении асинхронной задачи"));
            //throw new AsyncVoidException("Ошибка в асинхронном методе");
            
            Console.WriteLine($"Код после await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    }
    
    internal class AsyncVoidException : Exception
    {
        public AsyncVoidException(string message)
            : base(message) { }
    }
}