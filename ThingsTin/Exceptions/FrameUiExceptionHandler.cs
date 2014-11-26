using ExceptionRuler;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsTin.Interfaces.Container;

namespace ThingsTin.Exceptions
{
    public class FrameUiExceptionHandler : IExceptionHandler
    {
        private IThingsTin _frame;

        public void Initialize(object parameter)
        {
            _frame = parameter as IThingsTin;
        }

        public void HandleException(string source, string method, Exception ex)
        {
            if (_frame != null)
            {
                if (_frame != null)
                {
                    string message;
                    if (ex is KnownException)
                    {
                        message = ex.ToString();
                    }
                    else
                    {
                        message = ex.Message;
                    }

                    _frame.Messager.MessageBox("出现未知错误", message, MessageType.Exception, new[] { "OK" });
                }
            }
        }
    }
}
