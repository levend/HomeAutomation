using System;

namespace MosziNet.HomeAutomation.Gateway.Device
{
    /// <summary>
    /// Contains metadata information about the component.
    /// </summary>
    public class ComponentInfo
    {
        /// <summary>
        /// The name of this component. Expect this same name in a <see cref="ComponentState"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A string value for the type of this component.
        /// </summary>
        public string Type { get; set; }
    }
}
