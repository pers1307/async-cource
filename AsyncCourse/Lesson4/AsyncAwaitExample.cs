using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson4
{
    public class AsyncAwaitExample
    {
        public void Go()
        {
            Console.WriteLine($"Метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");

            // Запускается асинхронно 
            var task = WriteCharAsync('#').DisableAsyncWarning(); // Запуск метода асинхронно

            // Будет выполняться все синхронно, так как ожидает выполнения
            // t.GetAwaiter().GetResult();
            WriteChar('*'); // Запуск метода синхронно

            Console.WriteLine($"Метод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");
            Console.ReadKey();
        }
        
        private static async Task WriteCharAsync(char symbol)
        {
            Console.WriteLine($"Метод WriteCharAsync начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");

            // Начинает выполняться в исходном потоке
            // Сразу не выполниться, значит вернем управление главному потоку
            // Долго выполнялся и передался в другой поток
            // Все остальное уйдет в продолжение
            // await Task.Run(() => WriteChar(symbol));
            await Task.Run(() => WriteChar(symbol));

            // Будет закончивать выполнение уже в другом потоке
            Console.WriteLine($"Метод WriteCharAsync закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");
        }

        private static void WriteChar(char symbol)
        {
            Console.WriteLine($"Id потока - [{Thread.CurrentThread.ManagedThreadId}]. Id задачи - [{Task.CurrentId}]");
            Thread.Sleep(500);

            for (int i = 0; i < 80; i++)
            {
                Console.Write(symbol);
                Thread.Sleep(100);
            }
        }
    }
    
    // Это расширение позволяет убрать подчеркивание 
    internal static class MyAsyncExtensions
    {
        public static Task DisableAsyncWarning(this Task t)
        {
            return t;
        }
    }
}