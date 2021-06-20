using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson2
{
    public class ValueTaskResultExample
    {
        public void Go()
        {
            int res = Sum(0, 3).Result;
            Console.WriteLine(res);

            Console.ReadKey();
        }
        
        private static ValueTask<int> Sum(int a, int b)
        {
            if (a == 0)
            {
                return new ValueTask<int>(b);
            }
            else if (b == 0)
            {
                return new ValueTask<int>(a);
            }
            else if (a == 0 && b == 0)
            {
                return new ValueTask<int>(0);
            }

            return new ValueTask<int>(Task.Run(() => { return a + b; }));
        }

        
    }
}