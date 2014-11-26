using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ThingsTin.Interfaces;

namespace ThingsTin.Frame
{
    public class MenuBarItem : NotificationObject, IMenuBarItem
    {
        private Guid _menuId;
        private bool _enabled;
        private string _text;
        private ICommand _command;
        private Image _icon;

        public MenuBarItem()
        {
            SubMenu = new ObservableCollection<IMenuBarItem>();
        }

        public Guid MenuId
        {
            get
            {
                return _menuId;
            }
            set
            {
                _menuId = value;
                NotifyPropertyChanged("MenuId");
            }
        }

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                NotifyPropertyChanged("Enabled");
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                NotifyPropertyChanged("Text");
            }
        }

        public ICommand MenuCommand
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
                NotifyPropertyChanged("MenuCommand");
            }
        }

        public ObservableCollection<IMenuBarItem> SubMenu
        {
            get;
            private set;
        }

        public Image Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                NotifyPropertyChanged("Icon");
            }
        }
    }
}
