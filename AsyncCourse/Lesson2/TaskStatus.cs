using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class TaskStatus
    {
        public void Go()
        {
            var task = new Task(new Action(Method));
            
            // Created
            Console.WriteLine($"{task.Status}");
            
            task.Start();
            
            // WaitingToRun
            Console.WriteLine($"{task.Status}");
            Thread.Sleep(1000);
            
            // Running
            Console.WriteLine($"{task.Status}");
            Thread.Sleep(2000);
            
            // RanToComplite
            Console.WriteLine($"{task.Status}");
            Thread.Sleep(1000);

            Console.ReadKey();
        }

        private static void Method()
        {
            Thread.Sleep(2000);
        }
    }
}