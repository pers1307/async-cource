using System.Threading;

namespace AsyncCourse.Lesson5.ConsoleSynchronizationContext
{
    internal class Message
    {
        // Делегат
        public SendOrPostCallback Callback { get; set; }
        public object State { get; set; }

        public Message() { }

        public Message(SendOrPostCallback callback, object state)
        {
            Callback = callback;
            State = state;
        }
    }
}
