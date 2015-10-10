using HomeAutomation.Core;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MosziNet.HomeAutomation.Gateway.Service
{
    /// <summary>
    /// Provides a simple way of cooperatively run tasks.
    /// </summary>
    internal class ServiceRunner
    {
        bool runloopShouldRun = true;
        Task looperTask;

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
            looperTask = GetLooperTask();
        }

        private async Task GetLooperTask()
        {
            await Task.Run(() =>
            {
                // make a copy of the main list to ensure it's not being changed while we 
                // are looping
                IList<ICooperativeService> localList = ServiceRegistry.Instance.GetServiceList();

                while (runloopShouldRun)
                {
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
            });
        }
    }
}