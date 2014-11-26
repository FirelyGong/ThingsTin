using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ThingsTin.Interfaces.Container;
using ThingsTin.ViewModels;
using ThingsTin.Views;

namespace ThingsTin.Frame
{
    public class AppMessager:IMessager
    {
        private ThingsTinViewModel _viewModel;

        private IThingsTin _thingsTin;


        public AppMessager(IThingsTin thingsTin)
        {
            _thingsTin = thingsTin;
        }

        public void Message(MessageType type, string message)
        {
            _thingsTin.Dispatcher.Invoke(new Action(() =>
            {
                _viewModel.Message = string.Format(CultureInfo.InvariantCulture, "[{0}] - {1}]", DateTime.Now.ToString("HH:mm:ss"), message); ;
                switch (type)
                {
                    case MessageType.Success:
                        _viewModel.MessageType = new SolidColorBrush(Colors.Green);
                        break;
                    case MessageType.Hint:
                        _viewModel.MessageType = new SolidColorBrush(Colors.Black);
                        break;
                    case MessageType.Warning:
                        _viewModel.MessageType = new SolidColorBrush(Colors.Orange);
                        break;
                    case MessageType.Error:
                        _viewModel.MessageType = new SolidColorBrush(Colors.Red);
                        break;
                    case MessageType.Exception:
                        _viewModel.MessageType = new SolidColorBrush(Colors.Red);
                        break;
                    default:
                        _viewModel.MessageType = new SolidColorBrush(Colors.Black);
                        break;

                }
            }));
        }

        public void SetViewModel(ThingsTinViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public Window Owner { get; set; }

        public string MessageBox(string title, string message, MessageType messageType, string[] options)
        {
            string result = "";
            _thingsTin.Dispatcher.Invoke(new Action(() =>
               {
                   var window = new MessageWindow(title, message, messageType, options);
                   window.Owner = Owner;
                   window.ShowDialog();
                   result = window.SelectedOption;
               }));

            return result;
        }
    }
}
