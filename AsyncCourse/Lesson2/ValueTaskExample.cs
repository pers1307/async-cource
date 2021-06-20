using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class ValueTaskExample
    {
        public void Go()
        {
            CalculateAndShowAsync(99)
                .GetAwaiter()
                .GetResult();

            Console.ReadKey();
        }
        
        private static ValueTask CalculateAndShowAsync(int ceiling)
        {
            if (ceiling < 0)
            {
                return new ValueTask();
            }
            else
            {
                return new ValueTask(Task.Run(() =>
                {
                    Calculator(ceiling);
                }));
            }
        }

        private static void Calculator(int ceiling)
        {
            var sum = 0;

            for (int i = 0; i < ceiling; i++)
                sum += i;
            
            Console.WriteLine($"Результат - {sum}. Найден в задаче #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}