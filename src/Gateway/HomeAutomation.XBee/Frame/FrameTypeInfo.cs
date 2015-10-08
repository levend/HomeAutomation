namespace MosziNet.HomeAutomation.XBee.Frame
{
    internal static class FrameTypeInfo
    {
        public static IFrameTypeInfo APIV1FrameTypeInfo { get; private set; }

        static FrameTypeInfo()
        {
            APIV1FrameTypeInfo = new ZigBee.APIv1Descriptor();
        }
    }
}
