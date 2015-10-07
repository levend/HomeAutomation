using System;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace MosziNet.HomeAutomation.XBee
{
    /// <summary>
    /// Responsible of creating various types of XBee frames.
    /// </summary>
    public class XBeeFrameBuilder
    {
        /// <summary>
        /// Creates a new remote command to be sent to an XBee device.
        /// </summary>
        /// <param name="atCommand"></param>
        /// <param name="frameId"></param>
        /// <param name="address"></param>
        /// <param name="networkAddress"></param>
        /// <returns></returns>
        public RemoteATCommand CreateRemoteATCommand(byte[] atCommand, byte frameId, byte[] address, byte[] networkAddress)
        {
            // build the frame to ask the device type id
            RemoteATCommand frame = new RemoteATCommand();
            frame.Address = address;
            frame.NetworkAddress = networkAddress;

            frame.ATCommand = atCommand;
            frame.FrameId = frameId;
            frame.CommandOptions = RemoteATCommand.OptionCommitChanges;

            return frame;
        }

        /// <summary>
        /// Creates a new remote command to be sent to an XBee device.
        /// </summary>
        /// <param name="atCommand"></param>
        /// <param name="frameId"></param>
        /// <param name="address"></param>
        /// <param name="networkAddress"></param>
        /// <param name="commandOptions"></param>
        /// <returns></returns>
        public RemoteATCommand CreateRemoteATCommand(byte[] atCommand, byte frameId, byte[] address, byte[] networkAddress, byte[] parameters, byte commandOptions)
        {
            // build the frame to ask the device type id
            RemoteATCommand frame = new RemoteATCommand();
            frame.Address = address;
            frame.NetworkAddress = networkAddress;

            frame.ATCommand = atCommand;
            frame.Parameters = parameters;
            
            frame.CommandOptions = commandOptions;

            frame.FrameId = frameId;

            return frame;
        }
    }
}
