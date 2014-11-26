using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Interfaces.Container
{
    public interface IMessager
    {
        void Message(MessageType type, string message);

        string MessageBox(string title, string message, MessageType messageType, string[] options);
    }
}
