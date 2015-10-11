using HomeAutomation.Logging;
using HomeAutomation.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomation.Core.Service
{
    /// <summary>
    /// Provides a simple way of cooperatively run tasks.
    /// </summary>
    public class ServiceRunner
    {
        bool runloopShouldRun = true;

        /// <summary>
        /// Stops the run loop.
        /// </summary>
        public void Stop()
        {
            runloopShouldRun = false;
        }

        /// <summary>
        /// Starts the runloop.
        /// </summary>
        public void Start()
        {
            runloopShouldRun = true;

            Task.Factory.StartNew((o) =>
            {
                while (runloopShouldRun)
                {
                    StepOneLoop();
                }
            }, 
            TaskCreationOptions.LongRunning);
        }

        public void StepOneLoop()
        {
            // make a copy of the main list to ensure it's not being changed while we 
            // are looping
            IList<ICooperativeService> localList = HomeAutomationSystem.ServiceRegistry.GetServiceList();

            foreach (ICooperativeService participant in localList)
            {
                try
                {
                    participant.ExecuteTasks();
                }
                catch (Exception ex)
                {
                    Log.Error("ServiceRunner exception: " + ex.FormatToLog());
                }
            }
        }
    }
}