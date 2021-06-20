using System;
using System.Threading;

namespace AsyncCourse.Lesson1
{
    public class ClassicThread
    {
        public void Go()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(WriteChar));
            
            Console.WriteLine("Запускаем?");
            Console.ReadKey();
            
            // Стартануть поток
            thread.Start('*');
            
            // thread.Abort();
            
            for (int i = 0; i < 80; i++)
            {
                Console.Write('-');
                Thread.Sleep(70);
            }
        }
        
        private static void WriteChar(object arg)
        {
            char item = (char) arg;
            for (int i = 0; i < 80; i++)
            {
                Console.Write(item);
                Thread.Sleep(70);
            }
        }
    }
}