using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class TaskFactorySimple
    {
        private static Random random = new Random();
        
        public void Go()
        {
            var taskFactory = new TaskFactory();

            var t1 = taskFactory.StartNew(() => { return Calculate(1); });
            var t2 = taskFactory.StartNew(() => { return Calculate(2); });
            var t3 = taskFactory.StartNew(() => { return Calculate(3); });
            var t4 = taskFactory.StartNew(() => { return Calculate(4); });
            var t5 = taskFactory.StartNew(() => { return Calculate(5); });

            // Продолжить когда все завершаться
            taskFactory.ContinueWhenAll(
                new Task[] {t1, t2, t3, t4, t5},
                completedTask =>
                {
                    double sum = 0;

                    foreach (var task in completedTask)
                    {
                        var item = (Task<double>) task;
                        sum += item.Result;
                    }
                    
                    Console.WriteLine($"Результат : {sum:N}");
                }
            );

            Console.ReadKey();
        }
        
        private static double Calculate(int x)
        {
            double res = 0.0;

            for (int i = 0; i < 10; i++)
            {
                res += (i * random.Next(1, x) / (x * 2) * x);
            }

            return res;
        }
    }
}