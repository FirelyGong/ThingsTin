using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Utils
{
    public class CrossProcessMessager
    {
        public static string ReceiveMessage(string serverName)
        {
            while (true)
            {
                using (NamedPipeServerStream stream = new NamedPipeServerStream(serverName, PipeDirection.InOut))
                {
                    stream.WaitForConnection();
                    if (stream.IsConnected)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static bool SendMessage(string serverName,string message)
        {
            using (NamedPipeClientStream stream = new NamedPipeClientStream(serverName))
            {
                try
                {
                    stream.Connect(1000);
                    if (stream.IsConnected)
                    {
                        using (StreamWriter write = new StreamWriter(stream))
                        {
                            write.WriteLine(message);
                        }
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
