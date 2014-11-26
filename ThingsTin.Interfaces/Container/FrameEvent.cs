using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Interfaces.Container
{
    public class FrameEvent:EventArgs
    {
        public bool IsCanceled { get; set; }
        public string Message { get; set; }
    }
}
