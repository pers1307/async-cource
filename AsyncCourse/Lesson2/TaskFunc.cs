using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class TaskFunc
    {
        private static bool flag = false;
        
        public void Go()
        {
            Task<int> task1 = new Task<int>(new Func<int>(GetIntResult));
            task1.Start();
            Console.WriteLine($"Результат операции №1 - {task1.Result}");
            Thread.Sleep(1000);

            TaskFactory taskFactory = new TaskFactory();
            Task<bool> task2 = taskFactory.StartNew(new Func<bool>(GetBoolResult));
            Console.WriteLine($"Результат операции №2 - {task2.Result}");
            
            Thread.Sleep(1000);

            Task<bool> task3 = Task.Run(new Func<bool>(GetBoolResult));
            Console.WriteLine($"Результат операции №3 - {task3.Result}");
        }

        private static int GetIntResult()
        {
            return 1;
        }

        private static bool GetBoolResult()
        {
            if (flag)
            {
                flag = false;
                return true;
            }
            
            flag = true;
            return false;    
        }
    }
}