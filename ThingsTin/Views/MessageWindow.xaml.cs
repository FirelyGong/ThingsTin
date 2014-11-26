using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThingsTin.Interfaces.Container;
using ThingsTin.Utils;

namespace ThingsTin.Views
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow()
        {
            InitializeComponent();
        }

        public MessageWindow(string title, string message, MessageType messageType, string[] options)
            : this()
        {
            titleBlock.Text = title;
            messageBlock.Text = message;
            opContainer.Children.Clear();
            foreach (string op in options)
            {
                Button btn = new Button { Width = 75, Height = 22, Margin = new Thickness(0), Content=op };
                btn.Style = (Style)ResourcesLoader.LoadResource("OperationButton");
                btn.Click += btn_Click;
                opContainer.Children.Add(btn);
            }

            switch (messageType)
            {
                case MessageType.Success:
                    msIndicator.Fill = new SolidColorBrush(Colors.Green);
                    break;
                case MessageType.Warning:
                    msIndicator.Fill = new SolidColorBrush(Colors.Orange);
                    break;
                case MessageType.Error:
                    msIndicator.Fill = new SolidColorBrush(Colors.Red);
                    break;
                case MessageType.Exception:
                    msIndicator.Fill = new SolidColorBrush(Colors.Red);
                    break;
                default:
                    break;

            }
        }

        public string SelectedOption { get; private set; }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            SelectedOption = btn.Content.ToString();
            Close();
        }
    }
}
