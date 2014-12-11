using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Interfaces.Container
{
    public class PageEvent:EventArgs
    {
        public PageEvent(Guid pageId)
        {
            PageId = pageId;
        }

        public Guid PageId { get; private set; }
        public bool IsForce { get; set; }
        public bool IsCanceled { get; set; }
        public string Message { get; set; }
    }
}
