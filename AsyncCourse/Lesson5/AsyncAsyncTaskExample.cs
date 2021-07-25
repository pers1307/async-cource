using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TaskSchedulerAwait;

namespace AsyncCourse.Lesson5
{
    public class AsyncAsyncTaskExample
    {
        public void Go()
        {
            Main();
        }
        
        private async Task Main()
        {
            Console.SetWindowSize(90, 40);
            
            ShowData("Main выполнился до await");

            var mainTask = new Task<Task>(MethodAsync);
            mainTask.Start(new AwaitableTestTaskScheduler());
            
            // Код после await будет помещен в задачу, так как наш планировщик умеет работать только с задачами
            var result = await mainTask;                    
            await result;

            ShowData("Main выполнился после await");
            
            Console.ReadKey();
        }

        private async Task MethodAsync()
        {
            ShowData("MethodAsync выполнился до await");

            var task = new Task(TestMethod);
            task.Start();

            await task;
            // Запретить выполнение продолжения в захваченном контексте
            // await task.ConfigureAwait(false); 

            ShowData("MethodAsync выполнился после await");
        }

        private void TestMethod()
        {
            ShowData("TestMethod выполнился");
        }

        private void ShowData(string description)
        {
            Console.WriteLine($"{description}");

            Console.WriteLine($"Имя потока: {Thread.CurrentThread.Name} ");
            Console.WriteLine($"Id потока: {Thread.CurrentThread.ManagedThreadId}. Поток из пула потоков: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Id задачи: {Task.CurrentId}");
            Console.WriteLine($"Текущий планировщик задач: {typeof(TaskScheduler).GetProperty("InternalCurrent", BindingFlags.Static | BindingFlags.NonPublic).GetValue(typeof(TaskScheduler))}");

            Console.WriteLine(new string('-', 80));
            Console.WriteLine();
        }
    }
}