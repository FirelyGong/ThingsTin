using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ThingsTin.Interfaces.Container;
using ThingsTin.Views;

namespace ThingsTin.Frame
{
    public class DialogManager:IDialogManager
    {
        private DialogWindow _hostWindow;

        public Window Owner { get; set; }

        public void ShowDialog(Guid refId, string title, FrameworkElement control)
        {
            _hostWindow = new DialogWindow();
            _hostWindow.titleBlock.Text = title;
            _hostWindow.Owner = Owner;
            _hostWindow.AddControl(control);
            _hostWindow.ShowDialog();
        }

        public void CloseDialog(Guid refId)
        {
            if (_hostWindow != null)
            {
                _hostWindow.Close();
            }
        }
    }
}
