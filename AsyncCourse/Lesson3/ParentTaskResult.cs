using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson3
{
    public class ParentTaskResult
    {
        public void Go()
        {
            Task<string> parent = new Task<string>(() =>
            {
                var t1 = new Task<int>(() => Addition(5000));
                var t2 = new Task<int>(() => Addition(10000));
                var t3 = new Task<int>(() => Addition(50000));
                
                t1.Start();
                t2.Start();
                t3.Start();

                // t1.ContinueWith((t) => Console.WriteLine($"Сложение[1] = {t.Result}"));
                // t1.ContinueWith((t) => Console.WriteLine($"Сложение[2] = {t.Result}"));
                // t1.ContinueWith((t) => Console.WriteLine($"Сложение[3] = {t.Result}"));

                // return "Выполнена";
                
                // Если Result не готов, то идет блокировка потока
                // Поэтому пока все потоки не выполняться поток будет стоять
                return (t1.Result + t2.Result + t3.Result).ToString();
            });
            
            parent.Start();

            /**
             * Нет привязки к родительской задаче
             */

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