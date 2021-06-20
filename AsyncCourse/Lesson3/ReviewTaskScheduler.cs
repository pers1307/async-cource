using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCourse.Lesson3
{
    public class ReviewTaskScheduler : TaskScheduler
    {
        private readonly LinkedList<Task> tasksList = new LinkedList<Task>();

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasksList;
        }

        /// <summary>
        /// Метод вызывается методом Start класса Task
        /// </summary>
        /// <param name="task"></param>
        protected override void QueueTask(Task task)
        {
            Console.WriteLine($"    [QueueTask] Задача #{task.Id} поставлена в очередь..");
            tasksList.AddLast(task);
            // Выполнять задачу в контексте вторичных потоков
            ThreadPool.QueueUserWorkItem(ExecuteTasks, null);
            
            // Все задачи тогда будут выполняться синхронно
            //ExecuteTasks(null);
        }

        /// <summary>
        /// Метод вызывается методами ожидания, к примеру Wait, WaitAll...
        /// </summary>
        /// <param name="task"></param>
        /// <param name="taskWasPreviouslyQueued"></param>
        /// <returns></returns>
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            Console.WriteLine($"        [TryExecuteTaskInline] Попытка выполнить задачу #{task.Id} синхронно..");

            lock (tasksList)
            {
                tasksList.Remove(task);
            }

            return base.TryExecuteTask(task);
        }

        /// <summary>
        /// Метод вызывается при отмене выполнения задачи
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        protected override bool TryDequeue(Task task)
        {
            Console.WriteLine($"            [TryDequeue] Попытка удалить задачу {task.Id} из очереди..");
            bool result = false;

            lock (tasksList)
            {
                result = tasksList.Remove(task);
            }

            if(result == true)
            {
                Console.WriteLine($"                [TryDequeue] Задача {task.Id} была удалена из очереди на выполнение..");
            }

            return result;
        }

        private void ExecuteTasks(object _)
        {
            while (true)
            {
                //Thread.Sleep(2000); // Убрать комментарий для проверки TryExecuteTaskInline
                Task task = null;

                // Синхронизация доступа к общему ресурсу
                lock (tasksList)
                {
                    if (tasksList.Count == 0)
                    {
                        break;
                    }

                    task = tasksList.First.Value;
                    tasksList.RemoveFirst();
                }

                if(task == null)
                {
                    break;
                }

                // Запуск задач
                base.TryExecuteTask(task);
            }
        }
    }
}