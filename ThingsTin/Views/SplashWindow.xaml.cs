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
using System.IO;

namespace ThingsTin.Views
{
    /// <summary>
    /// SplashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAppImage();
        }

        public void WriteLine(string message)
        {
            Dispatcher.Invoke(new Action(() => list.Items.Add(message)));
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void LoadAppImage()
        {
            string path = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
            string img = System.IO.Path.Combine(path, "app.png");
            var uri = new Uri(img, UriKind.Absolute);
            //MessageBox.Show(uri.AbsolutePath);
            if (File.Exists(img))
            {
                appImage.Source = new BitmapImage(uri);
            }
        }
    }
}
