using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse
{
    internal class Program
    {
//        public static void Main(string[] args)
//        {
//            
//        }
        
        private static async Task Main()
        {
            Console.WriteLine($"Метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");

            await WriteCharAsync('#'); // Запуск метода асинхронно
            WriteChar('*'); // Запуск метода синхронно

            Console.WriteLine($"Метод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");
            Console.ReadKey();
        }

        private static async Task WriteCharAsync(char symbol)
        {
            Console.WriteLine($"Метод WriteCharAsync начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");

            await Task.Run(() => WriteChar(symbol));

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
        
        
        //************************************
        
        //private static async Task Main()
        private static void Main()
        {
            int x = 3, y = 5;

            Task<int> additionTask = AdditionAsync("[асинхронно]", x, y);

            int syncSum = Addition("[синхронно]", x, y);

            int asyncSum = 0;

            // Разные способы получения результата из асинхронной задачи:
            asyncSum = additionTask.Result;
            //asyncSum = additionTask.GetAwaiter().GetResult();
            //asyncSum = await additionTask;
            Console.WriteLine($"\nРезультат асинхронного выполнения: {asyncSum}.");
            Console.WriteLine($"Результат синхронного выполнения: {syncSum}.");

            Console.WriteLine($"Метод Main завершил свою работу");
            Console.ReadKey();
        }

        private static int Addition(string operationName, int x, int y)
        {
            Console.WriteLine($"Метод Addition вызван {operationName} в потоке: {Thread.CurrentThread.ManagedThreadId}");
            // Thread.Sleep - имитация нагруженной и тяжелой работы.
            Thread.Sleep(3000);
            return x + y;
        }

        private static async Task<int> AdditionAsync(string operationName, int x, int y)
        {
            // Первый способ

            int result = await Task.Run<int>(() => Addition(operationName, x, y));
            return result;

            // ---------------------------------------------- //

            // Второй способ

            //return await Task.Run<int>(() => Addition(operationName, x, y));

            // ---------------------------------------------- //

            // Ошибочный способ
            //return Task.Run<int>(() => Addition(operationName, x, y));
        }

    }
}