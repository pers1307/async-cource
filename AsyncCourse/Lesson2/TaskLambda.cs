using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public struct Box
        {
            public int a;
            public int b;
        }
    
    public class TaskLambda
    {
        public void Go()
        {
            var a = 3;
            var b = 3;

            Box box;
            box.a = a;
            box.b = b;

            var task = new Task<int>(Calc, box);
            task.Start();
            
            Console.WriteLine($"Сумма чисел : {task.Result}");
            Console.WriteLine(new string('-', 80));

            var lambda = new Task<int>(() => Calc(a, 5));
            lambda.Start();

            Console.WriteLine($"Сумма : {lambda.Result}");
            Console.WriteLine(new string('-', 80));
            
            var taskRun = Task.Run<int>(() =>
                {
                    int a1 = 5;
                    int b1 = 5;

                    return Calc(a1, b1) + Calc(a, b);
                }
            );

            Console.WriteLine($"Сумма : {taskRun.Result}");
            Console.WriteLine(new string('-', 80));
        }
        
        private static int Calc(object arg)
        {
            var box = (Box) arg;
            return box.a + box.b;
        }

        private static int Calc(int a, int b)
        {
            return a + b;
        }
    }
}