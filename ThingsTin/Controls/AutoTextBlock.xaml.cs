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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThingsTin.Controls
{
    /// <summary>
    /// AutoTextBlock.xaml 的交互逻辑
    /// </summary>
    public partial class AutoTextBlock : UserControl
    {
        public static readonly DependencyProperty AutoTextProperty = DependencyProperty.Register("AutoText", typeof(string), typeof(AutoTextBlock), new PropertyMetadata(new PropertyChangedCallback(OnAutoTextBlockChanged)));

        //private bool _isUpdating;

        public AutoTextBlock()
        {
            InitializeComponent();
        }
        public string AutoText
        {
            get
            {
                return (string)GetValue(AutoTextProperty);
            }
            set
            {
                SetValue(AutoTextProperty, value);
            }
        }

        private static void OnAutoTextBlockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AutoTextBlock)d).text.Text = e.NewValue.ToString();
            if (!string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                ((AutoTextBlock)d).story.Stop();
                ((AutoTextBlock)d).story.Seek(TimeSpan.FromSeconds(0));
                ((AutoTextBlock)d).story.Begin();
            }
        }

        private void story_Completed(object sender, EventArgs e)
        {
            AutoText = "";
        }
    }
}
