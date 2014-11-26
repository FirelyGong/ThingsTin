using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ThingsTin.Interfaces
{
    public interface IMenuBarItem
    {
        Guid MenuId { get; set; }
        bool Enabled { get; set; }
        string Text { get; set; }
        ICommand MenuCommand { get; set; }
        Image Icon { get; set; }
        ObservableCollection<IMenuBarItem> SubMenu { get; }
    }
}
