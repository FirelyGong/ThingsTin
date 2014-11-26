using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ThingsTin.Locater
{
    public class ThingInfo
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Assembly")]
        public string Assembly { get; set; }
    }
}
