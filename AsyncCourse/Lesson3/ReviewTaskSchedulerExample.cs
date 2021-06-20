using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson3
{
    public class ReviewTaskSchedulerExample
    {
        public void Go()
        {
            Console.SetWindowSize(100, 45);
            Console.WriteLine($"Id потока метода Main - {Thread.CurrentThread.ManagedThreadId}");

            Task[] tasks = new Task[10];
            ReviewTaskScheduler reviewTaskScheduler = new ReviewTaskScheduler();

            QueueTaskTesting(tasks, reviewTaskScheduler);
            //TryExecuteTaskInlineTesting(tasks, reviewTaskScheduler);
            //TryDequeueTesting(tasks, reviewTaskScheduler);
            
            try
            {
                /**
                 * Методы ожидания могут пытаться выполнить синхронно задачи потоку вызова
                 */
                Task.WaitAll(tasks);
            }
            catch
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Несколько задач были отменены!");
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine($"Метод Main закончил свое выполнение");
            }

            Console.ReadKey();
        }
        
        private static void QueueTaskTesting(Task[] tasks, TaskScheduler scheduler)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}\n");
                });

                // Здесь задачи ставятся в очереди и выполняются в методе QueueTask
                tasks[i].Start(scheduler);
            }
        }

        private static void TryExecuteTaskInlineTesting(Task[] tasks, TaskScheduler scheduler)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task<int>(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}\n");
                    return 1;
                });
            }

            foreach (var task in tasks)
            {
                task.Start(scheduler);
                
                // Если ещё при выполнении сделать задержку в 2 секунды, то все будет выполнено синхронно в главном потоке
                task.Wait();
                
                // Result также заставит ждать выпонения всех потоков
                //int result = ((Task<int>)task).Result;
            }
        }

        private static void TryDequeueTesting(Task[] tasks, TaskScheduler scheduler)
        {
            #region Скоординированная отмена

            // Для управления отмены
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            // Удалятся только те задачи, которые не успели выполнится
            cts.CancelAfter(555);

            #endregion

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}\n");
                }, token);

                tasks[i].Start(scheduler);
            }
        }
    }
}