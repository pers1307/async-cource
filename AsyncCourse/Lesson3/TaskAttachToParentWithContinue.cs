using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson3
{
    public class TaskAttachToParentWithContinue
    {
        public void Go()
        {
            Task<string> parent = new Task<string>(() =>
            {
                var t1 = new Task<int>(() => Addition(5000), TaskCreationOptions.AttachedToParent);
                var t2 = new Task<int>(() => Addition(10000), TaskCreationOptions.AttachedToParent);
                var t3 = new Task<int>(() => Addition(50000), TaskCreationOptions.AttachedToParent);
                
                t1.Start();
                t2.Start();
                t3.Start();

                t1.ContinueWith((t) => Console.WriteLine($"Сложение[1] = {t.Result}"), TaskContinuationOptions.AttachedToParent);
                t1.ContinueWith((t) => Console.WriteLine($"Сложение[2] = {t.Result}"), TaskContinuationOptions.AttachedToParent);
                t1.ContinueWith((t) => Console.WriteLine($"Сложение[3] = {t.Result}"), TaskContinuationOptions.AttachedToParent);

                // Сначала выполняться задачи и продолжения потом завершится сама задача 
                return "Выполнена";
            });
            
            parent.Start();
            
            Console.WriteLine($"Результат задачи - {parent.Result}");
            Console.ReadKey();
        }
        
        private static int Addition(int lenght)
        {
            int sum = 0;
            Thread.Sleep(3000);

            for (int i = 0; i < lenght; i++)
            {
                sum++;
            }

            return sum;
        }
    }
}