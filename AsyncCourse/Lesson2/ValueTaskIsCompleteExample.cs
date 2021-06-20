using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class ValueTaskIsCompleteExample
    {
        public void Go()
        {
            var salary = 10000;
            ValueTask<double> valueTask = GetIndexing(salary);

            while (!valueTask.IsCompleted)
            {
                Console.Write('*');
                Thread.Sleep(200);
            }

            var task = valueTask.AsTask();

            task.ContinueWith((t) => Console.WriteLine($"\nИндексация зарплаты {salary} = {t.Result}%"));
        }
        
        private static ValueTask<double> GetIndexing(int salary)
        {
            Thread.Sleep(500);

            if (salary <= 0)
            {
                return new ValueTask<double>(0);
            }
            else if (salary > 5000)
            {
                return new ValueTask<double>(0);
            }
            else
            {
                return new ValueTask<double>(Task.Run(() =>
                {
                    var index = 0.0;
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(500);
                        index += 0.1;
                    }
                    return index;
                }));
            }
        }
    }
}