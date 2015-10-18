﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomeAutomation.Application.Configuration
{
    [DataContract]
    public class ControllerConfiguration
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Factory { get; set; }

        [DataMember]
        public Dictionary<string, object> Configuration { get; set; }

    }
}
