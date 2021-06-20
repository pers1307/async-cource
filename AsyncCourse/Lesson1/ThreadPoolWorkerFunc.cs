using System;
using System.Threading;

namespace AsyncCourse.Lesson1
{
    public class ThreadPoolWorkerFunc<TResult>
    {
        private readonly Func<object, TResult> func;
        private TResult result;
        
        public ThreadPoolWorkerFunc(Func<object, TResult> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
            result = default;
        }
        
        public bool Success { get; private set; } = false;
        public bool Complete { get; private set; } = false;
        public Exception Exception { get; private set; } = null;

        public TResult Result
        {
            get
            {
                while (Complete == false)
                {
                    Thread.Sleep(150);
                }
                
                return Success == true && Exception == null ? result : throw Exception;
            }
        }
        
        public void Start(object state)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), state);
        }
        
        private void ThreadExecution(object state)
        {
            try
            {
                result = func.Invoke(state);
                Success = true;
            }
            catch (Exception ex)
            {
                Exception = ex;
                Success = false;
            }
            finally
            {
                Complete = true;
            }
        }
    }
}