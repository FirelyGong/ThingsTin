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

namespace ThingsTin.Views
{
    /// <summary>
    /// DialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public void AddControl(FrameworkElement control)
        {
            container.Children.Clear();
            container.Children.Add(control);
            Width = control.Width + 16;
            Height = control.Height + 72;
        }
    }
}
