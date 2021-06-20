using System;
using System.Threading;

namespace AsyncCourse.Lesson1
{
    internal class ThreadPoolWorker
    {
        private readonly Action<object> action;

        public ThreadPoolWorker(Action<object> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public bool Success { get; private set; } = false;
        public bool Complete { get; private set; } = false;
        public Exception Exception { get; private set; } = null;

        public void Start(object state)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), state);
        }
        
        public void Wait()
        {
            while (Complete == false)
            {
                Thread.Sleep(150);
            }

            if (Exception != null)
            {
                throw Exception;
            }
        }

        private void ThreadExecution(object state)
        {
            try
            {
                // ?
                action.Invoke(state);
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