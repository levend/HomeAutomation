using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Core.Scheduler
{
    public class ScheduledTaskManager
    {
        private ScheduledTaskRunner taskRunner;
        private List<ScheduledTaskInfo> taskList = new List<ScheduledTaskInfo>();

        public ScheduledTaskManager()
        {
            taskRunner = new ScheduledTaskRunner(taskList);
        }

        public void ScheduleRealtimeTask(IScheduledTask task)
        {
            ScheduleTask(task, 0);
        }

        /// <summary>
        /// Register a scheduled task.
        /// </summary>
        /// <param name="task">The task to be scheduled.</param>
        /// <param name="period">The period in seconds to wait between the tasks are run. This is not an exact time period, but 
        /// it should be considered at least + best effort. If set to zero or negative, the system will try to execute the task 
        /// as many times as possible.</param>
        public void ScheduleTask(IScheduledTask task, int period)
        {
            taskList.Add(new ScheduledTaskInfo()
            {
                Task = task,
                ScheduledTimePeriod = period
            });

            taskRunner.TaskListChanged();
        }

        public IRunner Runner
        {
            get
            {
                return taskRunner;
            }
        }
    }
}
