using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Interfaces.Container
{
    public enum MessageType
    {
        Success = -1,
        Hint = 0,
        Warning = 1,
        Error = 2,
        Exception = 3
    }
}
