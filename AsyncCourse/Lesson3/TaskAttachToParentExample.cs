using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson3
{
    public class TaskAttachToParentExample
    {
        public void Go()
        {
            var parent = new Task(() =>
            {
                new Task(() =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Nested 1 completed");
                // }).Start();
                // Parent задача будет ожидать выполнения всех своих дочерних задач
                }, TaskCreationOptions.AttachedToParent).Start();
                
                new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Nested 2 completed");
                // }).Start();
                }, TaskCreationOptions.AttachedToParent).Start();
                
                Thread.Sleep(200);
            });
            // Запретить задачам прикрепляться к задаче
            // }, TaskCreationOptions.DenyChildAttach);
            
            // При использовании Task.Run автоматически передается TaskCreationOptions.DenyChildAttach внутрь 
            parent.Start();
            parent.Wait();
            
            Console.WriteLine("Completed");

            Console.ReadKey();
        }
    }
}