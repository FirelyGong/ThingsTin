using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsTin.Interfaces.Container;
using ThingsTin.Models;

namespace ThingsTin.Frame
{
    public class OpenedPage
    {
        public IOperationPage Page { get; set; }
        public OperationPageInfo Model { get; set; }
    }
}
