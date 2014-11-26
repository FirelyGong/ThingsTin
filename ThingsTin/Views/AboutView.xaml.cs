using System;
using System.Collections.Generic;
using System.Globalization;
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
using ThingsTin.Interfaces.Container;

namespace ThingsTin.Views
{
    /// <summary>
    /// AboutView.xaml 的交互逻辑
    /// </summary>
    public partial class AboutView : UserControl
    {
        private Guid _viewId;
        private IThingsTin _container;

        public AboutView(Guid viewId, IThingsTin container)
        {
            InitializeComponent();

            _viewId = viewId;
            _container = container;
            version.Text = string.Format(CultureInfo.InvariantCulture, "v{0}", GetType().Assembly.GetName().Version.ToString());

            license.Text = "Open source (https://github.com/FirelyGong/ThingsTin)";
            
            StringBuilder supportInfo = new StringBuilder();
            supportInfo.AppendLine(string.Format(CultureInfo.InvariantCulture, "E-Mail:{0}", @"254365905@qq.com"));
            supportInfo.AppendLine(string.Format(CultureInfo.InvariantCulture, "QQ:{0}", "254365905"));
            supportInfo.Append(string.Format(CultureInfo.InvariantCulture, "Tel:{0}", "13771865233"));
            support.Text = supportInfo.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _container.Dialog.CloseDialog(_viewId);
        }
    }
}
