using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThingsTin.Interfaces
{
    public interface INavigationItem:INotifyPropertyChanged
    {
        Guid PageId { get; set; }
        string ImageSource { get; set; }
        string Text { get; set; }
        bool IsExpanded { get; set; }
        bool IsCategory { get; set; }
        ICommand Navigate { get; set; }
        ObservableCollection<INavigationItem> Items { get; }
    }
}
