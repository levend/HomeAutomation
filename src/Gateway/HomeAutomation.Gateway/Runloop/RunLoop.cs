using System;
using System.Collections;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MosziNet.HomeAutomation
{
    /// <summary>
    /// Provides a simple way of cooperatively run tasks.
    /// </summary>
    public class RunLoop
    {
        List<IRunLoopParticipant> participants = new List<IRunLoopParticipant>();
        bool runloopShouldRun = true;
        Task looperTask;

        /// <summary>
        /// Adds a new run loop participant.
        /// </summary>
        /// <param name="participant"></param>
        public void AddRunLoopParticipant(IRunLoopParticipant participant)
        {
            participants.Add(participant);
        }

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
            looperTask = StartLooper();
        }

        private async Task StartLooper()
        {
            await Task.Run(() =>
            {
                // make a copy of the main list to ensure it's not being changed while we 
                // are looping
                IList<IRunLoopParticipant> localList = participants.AsReadOnly();

                while (runloopShouldRun)
                {
                    foreach (IRunLoopParticipant participant in localList)
                    {
                        try
                        {
                            participant.Execute();
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Run loop exception: " + ex.FormatToLog());
                        }
                    }
                }
            });
        }
    }
}