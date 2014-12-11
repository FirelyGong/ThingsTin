using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThingsTin.Frame;
using ThingsTin.Interfaces;
using ThingsTin.Interfaces.Container;
using ThingsTin.Views;

namespace ThingsTin.ViewModels
{
    public class MenuInitializer
    {
        private BootLoader _loader;
        private IThingsTin _thingsTin;

        public MenuInitializer(BootLoader loader, IThingsTin thingsTin)
        {
            _loader = loader;
            _thingsTin = thingsTin;
        }

        public void Initialize()
        {
            _loader.Menu.Clear();
            var file = new MenuBarItem { Enabled = true, Text = "文件", Icon = new Image { Source = new BitmapImage(new Uri("../Images/align-left.png", UriKind.Relative)) }};
            file.SubMenu.Add(new MenuBarItem { Enabled = true, Text = "退出", Icon = new Image { Source = new BitmapImage(new Uri("../Images/align-left.png", UriKind.Relative)) }, MenuCommand = new SimpleCommand(Close) });
            var setting = new MenuBarItem { Enabled = true, Text = "设置", Icon = new Image { Source = new BitmapImage(new Uri("../Images/align-left.png", UriKind.Relative)) } };
            var help = new MenuBarItem { Enabled = true, Text = "帮助", Icon = new Image { Source = new BitmapImage(new Uri("../Images/align-left.png", UriKind.Relative)) } };
            var about = new MenuBarItem { Enabled = true, Text = "关于", Icon = new Image { Source = new BitmapImage(new Uri("../Images/align-left.png", UriKind.Relative)) } };
            about.SubMenu.Add(new MenuBarItem { Enabled = true, Text = "ThingsTin", MenuCommand = new SimpleCommand(AboutThingsTin) });
            help.SubMenu.Add(about);
            _loader.Menu.Add(file);
            _loader.Menu.Add(setting);
            _loader.Menu.Add(help);
        }

        private void Close(object obj)
        {
            App.Current.Windows[0].Close();
        }

        private void AboutThingsTin(object obj)
        {
            Guid id = Guid.NewGuid();
            AboutView view=new AboutView(id, _thingsTin);
            _thingsTin.Dialog.ShowDialog(id, "About ThingsTin", view);
        }
    }
}
