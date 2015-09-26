using System;
using Microsoft.SPOT;
using System.Collections;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation
{
    public class RunLoop
    {
        IRunLoopParticipant[] participants = new IRunLoopParticipant[] { };

        public void AddRunLoopParticipant(IRunLoopParticipant participant)
        {
            IRunLoopParticipant[] newParticipants = new IRunLoopParticipant[participants.Length + 1];
            Array.Copy(participants, newParticipants, participants.Length);

            newParticipants[newParticipants.Length - 1] = participant;
            participants = newParticipants;
        }

        public void Run()
        {
            while (true)
            {
                foreach(IRunLoopParticipant participant in participants)
                {
                    try
                    {
                        participant.Execute();
                    }
                    catch(Exception ex)
                    {
                        Log.Error("Run loop exception: " + ExceptionFormatter.Format(ex));
                    }
                }
            }
        }
    }
}
