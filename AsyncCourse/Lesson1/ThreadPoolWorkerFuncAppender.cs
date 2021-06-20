using System;
using System.Threading;

namespace AsyncCourse.Lesson1
{
    public class ThreadPoolWorkerFuncAppender
    {
        public void Go()
        {
            Console.WriteLine("Запускаем?");
            Console.ReadKey();

            var threadPoolWorker = new ThreadPoolWorkerFunc<int>(SumNumber);
            threadPoolWorker.Start(1000);

            while (threadPoolWorker.Complete == false)
            {
                Console.Write('*');
                Thread.Sleep(35);
            }
            
            Console.WriteLine();
            Console.WriteLine($"Результат = {threadPoolWorker.Result:N}");
        }

        public static int SumNumber(object arg)
        {
            int number = (int) arg;
            int sum = 0;

            for (int i = 0; i < number; i++)
            {
                sum += i;
                Thread.Sleep(1);
            }

            return sum;
        }
    }
}