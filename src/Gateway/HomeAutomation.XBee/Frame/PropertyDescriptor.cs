namespace MosziNet.HomeAutomation.XBee.Frame
{
    /// <summary>
    /// Property Descriptor for a frame value. eg. Address, Frame Type
    /// </summary>
    public class PropertyDescriptor
    {
        public byte ByteCount;
        public string PropertyName;
        public PropertyType PropertyType;

        public PropertyDescriptor(byte byteCount, string propertyName, PropertyType propertyType)
        {
            ByteCount = byteCount;
            PropertyName = propertyName;
            PropertyType = propertyType;
        }

        public PropertyDescriptor(byte byteCount)
        {
            ByteCount = byteCount;
        }
    }
}
