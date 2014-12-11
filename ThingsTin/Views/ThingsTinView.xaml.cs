using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThingsTin.Frame;
using ThingsTin.Interfaces.Container;

namespace ThingsTin.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ThingsTinView : Window
    {
        private IThingsTin _thingsTin;

        public ThingsTinView(IThingsTin thingsTin)
        {
            InitializeComponent();
            _thingsTin = thingsTin;
            WindowState = WindowState.Maximized;
            LoadAppImage();
        }
        
        private void expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (!expander.IsExpanded)
            {
                if (splitter != null)
                {
                    treeviewCol.Width = new GridLength(30);
                    splitter.IsEnabled = false;
                }
            }
            else
            {
                treeviewCol.Width = new GridLength();
                if (splitter != null)
                {
                    splitter.IsEnabled = true;
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Environment.Exit(0);
        }

        private void LoadAppImage()
        {
            string path = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
            string img = System.IO.Path.Combine(path, "app.png");
            var uri = new Uri(img, UriKind.Absolute);
            if (File.Exists(img))
            {
                var bitmap=new BitmapImage(uri);
                thingImage.Source = bitmap;
                thingImage.Width = bitmap.Width;
                thingImage.Height = bitmap.Height;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FrameEvent evt = new FrameEvent();
            ((ThingsContainer)_thingsTin).ClosingFrame(evt);

            e.Cancel = evt.IsCanceled;
        }
    }
}
