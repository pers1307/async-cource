using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson5.ConsoleSynchronizationContext
{
    public class ConsoleSynchronizationContextExample
    {
        public void Go()
        {
            Main();
        }
        
        static ConsoleSynchronizationContextExample()
        {
            SynchronizationContext.SetSynchronizationContext(new ConsoleSynchronizationContext());
        }

        public void Main()
        {
            Message message = new Message(ApplicationMain, null);
            MessageListenter.AddMessage(message);

            new MessageListenter().Listen();

            Console.ReadKey();
        }

        private async void ApplicationMain(object _)
        {
            Console.WriteLine($"Наш метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");

            StubMethod1();

            await MethodAsync();
                // .ConfigureAwait(false); в этом случае продолжение будет выполняться в пуле потоков
                // иначе, по умолчанию, он будет выполняться в контексте синхронизации (в основном потоке)
            
            StubMethod2();

            Console.WriteLine($"Наш метод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private async Task MethodAsync()
        {
            Console.Write(new string('-', 80));

            Console.WriteLine($"Код до оператора await выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(Method);
            
            // Код будет выполняться тоже в первом потоке, это потому что есть контекст синхронизации
            Console.WriteLine($"Код после оператора await выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine(new string('-', 80));
        }
        private static void StubMethod1()
        {
            Console.WriteLine($"Пример метода1!!! Id потока: {Thread.CurrentThread.ManagedThreadId}");
        }

        private void StubMethod2()
        {
            Console.WriteLine($"Пример метода2!!! Id потока: {Thread.CurrentThread.ManagedThreadId}");
        }
        
        private void Method()
        {
            Console.WriteLine($"Метод {nameof(Method)} был выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}