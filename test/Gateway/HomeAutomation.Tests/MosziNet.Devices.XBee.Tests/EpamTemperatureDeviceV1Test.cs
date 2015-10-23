using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.Devices.XBee;
using MosziNet.XBee.Frame;

namespace HomeAutomation.Tests.MosziNet.Devices.XBee.Tests
{
    [TestClass]
    public class EpamTemperatureDeviceV1Test
    {
        [TestMethod]
        public void TestTemperatureReadingFromFram()
        {
            // temperature 21.87
            // 7E 00 12 90 11 22 33 44 55 66 77 88 11 22 01 EA 01 FE EB AE 41 14

            byte[] frame = new byte[] { 0x7E, 0x00, 0x12, 0x90, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x11, 0x22, 0x01, 0xEA, 0x01, 0xFE, 0xEB, 0xAE, 0x41, 0x14 };

            // deserialize the frame
            ReceivePacket typedFrame = FrameSerializer.Deserialize(frame) as ReceivePacket;

            // test for the correct frame type
            Assert.IsNotNull(typedFrame);

            // process the frame with thetemperature device
            EpamTemperatureDeviceV1 temperatureDevice = new EpamTemperatureDeviceV1();
            temperatureDevice.ProcessFrame(typedFrame);

            // finally test the temperature
            Assert.IsTrue(21.86 < temperatureDevice.Temperature && temperatureDevice.Temperature < 21.88);
        }
    }
}
