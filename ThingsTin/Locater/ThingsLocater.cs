using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ThingsTin.Locater
{
    [XmlRoot("ThingsLocater")]
    public class ThingsLocater
    {
        [XmlArray("Things")]
        public List<ThingInfo> Things { get; set; }
    }
}
