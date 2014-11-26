using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ThingsTin.Interfaces.Container
{
    public interface IDialogManager
    {
        void ShowDialog(Guid refId, string title, FrameworkElement control);

        void CloseDialog(Guid refId);
    }
}
