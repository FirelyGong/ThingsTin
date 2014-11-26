using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using ThingsTin.Frame;

namespace ThingsTin.Views
{
    /// <summary>
    /// ProgressWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressWindow : Window
    {
        private IProgressToken _progressToken;

        public ProgressWindow(IProgressToken token)
        {
            InitializeComponent();
            _progressToken = token;
        }

        public void SetProgressTitle(string title)
        {
            this.title.Text = title;
        }

        public void SetCancelable(bool cancelable)
        {
            if (cancelable)
            {
                cancel.Visibility = Visibility.Visible;
            }
            else
            {
                cancel.Visibility = Visibility.Hidden;
            }
        }

        public void SetProgressMessage(string message)
        {
            this.message.Text = message;
            per.Text = "0%";
        }
        
        public void UpdateProgress(int percentage, bool finished)
        {
            this.per.Text = percentage.ToString() + "%";
            if (finished)
            {
                Close();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            _progressToken.Cancel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
