using MosziNet.HomeAutomation.XBee;
using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace HomeAutomation.NetCore.RPI
{
    /// <summary>
    /// Implements the <see cref="IXBeeSerialPort"/> needed for XBee serial communication.
    /// </summary>
    public class XBeeSerialPort : IXBeeSerialPort
    {
        DeviceWatcher serialPortWatcher;
        SerialDevice serialPort;
        DataReader dataReader;
        DataWriter dataWriter;


        Task<byte[]> frameReader;

        uint baudRate;
        SerialParity parity;
        SerialStopBitCount stopBitCount;
        ushort dataBits;

        bool isPortReady;

        public XBeeSerialPort(uint baudRate, SerialParity parity, SerialStopBitCount stopBitCount, ushort dataBits)
        {
            this.baudRate = baudRate;
            this.parity = parity;
            this.stopBitCount = stopBitCount;
            this.dataBits = dataBits;

            StartListeningForSerialPortChanges();
        }

        #region / Serial Port change watcher /

        private void StartListeningForSerialPortChanges()
        {
            serialPortWatcher = DeviceInformation.CreateWatcher(SerialDevice.GetDeviceSelector());

            serialPortWatcher.Added += SerialPortWatcher_Added;
            serialPortWatcher.Removed += SerialPortWatcher_Removed;
            serialPortWatcher.Updated += SerialPortWatcher_Updated;

            serialPortWatcher.Start();
        }

        private void SerialPortWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            
        }

        private void SerialPortWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            
        }

        private async void SerialPortWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            if (serialPort == null)
            {
                serialPort = await SerialDevice.FromIdAsync(args.Id);

                if (serialPort != null)
                {
                    serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
                    serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
                    serialPort.BaudRate = baudRate;
                    serialPort.Parity = parity;
                    serialPort.StopBits = stopBitCount;
                    serialPort.DataBits = dataBits;

                    dataReader = new DataReader(serialPort.InputStream);

                    dataReader.InputStreamOptions = InputStreamOptions.ReadAhead;
                    dataReader.ByteOrder = ByteOrder.BigEndian;

                    dataWriter = new DataWriter(serialPort.OutputStream);
                    dataWriter.ByteOrder = ByteOrder.BigEndian;

                    isPortReady = true;
                }
            }
        }

        #endregion / Serial Port change watcher /

        #region / IXBeeSerialPort interface implementation /

        public byte[] GetNextAvailableFrame()
        {
            if (!isPortReady)
                return null;

            byte[] result = null;

            if (frameReader == null)
            {
                frameReader = GetReadFrameTask();
            }
            
            if (frameReader.IsCompleted)
            {
                result = frameReader.Result;
                frameReader = null;
            }

            return result;
        }

        public void WriteFrame(byte[] frame)
        {
            // TODO: Maybe do an async implementation here
            dataWriter.WriteBytes(frame);
            dataWriter.StoreAsync().AsTask().Wait();
        }

        #endregion / IXBeeSerialPort interface implementation /

        private async Task<byte[]> GetReadFrameTask()
        {
            await dataReader.LoadAsync(1);
            if (dataReader.ReadByte() == 0x7e)
            {
                await dataReader.LoadAsync(2);

                // read the frame length from the stream
                var frameLength = dataReader.ReadUInt16();

                // read the frame content + checksum byte from the stream
                await dataReader.LoadAsync(frameLength + 1u);
                byte[] frameContent = new byte[frameLength + 1];
                dataReader.ReadBytes(frameContent);

                // now build the complete frame
                byte[] completeFrame = new byte[frameLength + 4];

                // set the first well known bytes (frame start, 2 bytes frame length)
                completeFrame[0] = 0x7e;
                completeFrame[1] = (byte)(frameLength / 256);
                completeFrame[2] = (byte)(frameLength % 256);

                // then copy the content of the frame with the checksum byte
                Array.Copy(frameContent, 0, completeFrame, 3, frameLength + 1);

                return completeFrame;
            }

            return null;
        }

    }
}
