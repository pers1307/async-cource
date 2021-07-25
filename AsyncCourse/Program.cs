using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AsyncCourse.Lesson5;
using AsyncCourse.Lesson5.AsyncVoid;
using AsyncCourse.Lesson5.ConsoleSynchronizationContext;
using TaskSchedulerAwait;

namespace AsyncCourse
{
    internal class Program
    {
        // static Program()
        // {
        //     SynchronizationContext.SetSynchronizationContext(new TestSyncContext());
        // }
        //
        // private static void Main()
        // {
        //     MethodAsync();
        //
        //     Console.ReadKey();
        // }
        //
        // // лучше использовать тип Task чтобы было где перехватывать исключение и все не упало 
        // private static async void MethodAsync()
        // {
        //     Console.WriteLine($"Код до await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
        //     await Task.Run(() => Console.WriteLine($"Задача выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}"));
        //     
        //     // Без контекста синхронизации все упадет, потому что нет корректной обработки ошибок
        //     //await Task.Run(() => throw new AsyncVoidException("Ошибка при выполнении асинхронной задачи"));
        //     //throw new AsyncVoidException("Ошибка в асинхронном методе");
        //     
        //     Console.WriteLine($"Код после await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
        // }
        // 1:30
    }
}