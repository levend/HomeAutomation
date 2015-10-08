using System;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace MosziNet.HomeAutomation.XBee
{
    /// <summary>
    /// Responsible of creating various types of XBee frames.
    /// </summary>
    public static class XBeeFrameBuilder
    {
        /// <summary>
        /// Creates a new remote command to be sent to an XBee device.
        /// </summary>
        /// <param name="atCommand"></param>
        /// <param name="frameId"></param>
        /// <param name="address"></param>
        /// <param name="networkAddress"></param>
        /// <returns></returns>
        public static RemoteATCommand CreateRemoteATCommand(byte[] atCommand, byte frameId, byte[] address, byte[] networkAddress)
        {
            return new RemoteATCommand()
            {
                Address = address,
                NetworkAddress = networkAddress,
                ATCommand = atCommand,
                FrameId = frameId,
                CommandOptions = RemoteATCommand.OptionCommitChanges
            };
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
        public static RemoteATCommand CreateRemoteATCommand(byte[] atCommand, byte frameId, byte[] address, byte[] networkAddress, byte[] parameters, byte commandOptions)
        {
            return new RemoteATCommand()
            {
                Address = address,
                NetworkAddress = networkAddress,
                ATCommand = atCommand,
                Parameters = parameters,
                CommandOptions = commandOptions,
                FrameId = frameId
            };
        }
    }
}
