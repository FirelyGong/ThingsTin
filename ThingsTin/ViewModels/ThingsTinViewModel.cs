using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ThingsTin.Frame;
using ThingsTin.Interfaces;
using ThingsTin.Models;
using ThingsTin.Utils;

namespace ThingsTin.ViewModels
{
    public class ThingsTinViewModel:NotificationObject
    {
        private string _message;
        private Brush _messageType;
        private ResourceDictionary _resourceDictionary;
        private OperationPageInfo _currentPage;
        private Brush _pageContainerBackground;
        private ImageSource _icon;
        private string _title;
        private UIElement _backgroundPage;

        public ThingsTinViewModel()
        {
            Pages = new ObservableCollection<OperationPageInfo>();
            Pages.CollectionChanged += Pages_CollectionChanged;
            FunctionItems = new ObservableCollection<INavigationItem>();
            Menu = new ObservableCollection<IMenuBarItem>();
        }

        public ObservableCollection<OperationPageInfo> Pages { get; private set; }

        public ObservableCollection<IMenuBarItem> Menu { get; private set; }

        public ObservableCollection<INavigationItem> FunctionItems { get; private set; }
                
        public ImageSource Icon
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

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }



        public UIElement BackgroundPage
        {
            get
            {
                return _backgroundPage;
            }

            set
            {
                _backgroundPage = value;
                NotifyPropertyChanged("BackgroundPage");
            }
        }


        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public Brush MessageType
        {
            get
            {
                return _messageType;
            }

            set
            {
                _messageType = value;
                NotifyPropertyChanged("MessageType");
            }
        }

        public OperationPageInfo CurrentPage
        {
            get
            {
                return _currentPage;
            }

            set
            {
                _currentPage = value;
                NotifyPropertyChanged("CurrentPage");
            }
        }

        public Brush PageContainerBackground
        {
            get
            {
                return _pageContainerBackground;
            }

            set
            {
                _pageContainerBackground = value;
                NotifyPropertyChanged("PageContainerBackground");
            }
        }

        public void AddFunctions(IList<INavigationItem> functions)
        {
            functions.ToList().ForEach(f =>
            {
                if (!FunctionItems.Contains(f))
                {
                    FunctionItems.Add(f);
                }
            });
        }

        public void AddMenu(IList<IMenuBarItem> menu)
        {
            menu.ToList().ForEach(f =>
            {
                if (!Menu.Contains(f))
                {
                    Menu.Add(f);
                }
            });
        }

        public OperationPageInfo AddPage(Guid pageId, string title, UIElement pageContent, ICommand closePageCommand)
        {
            if (_resourceDictionary == null)
            {
                LoadResource();
            }

            var page = new OperationPageInfo { Tag = pageId, ToolTip = title, Header = title, Content = pageContent, ClosePage = closePageCommand };
            page.Style = _resourceDictionary["tabItem"] as Style;
            Pages.Add(page);
            CurrentPage = page;
            return page;
        }

        public void RemovePage(OperationPageInfo page)
        {
            Pages.Remove(page);
        }

        private void LoadResource()
        {
            _resourceDictionary = ResourcesLoader.LoadResource();
        }

        private void Pages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PageContainerBackground = Pages.Count == 0 ? new SolidColorBrush(Colors.Transparent) : new SolidColorBrush(Colors.White);
        }
    }
}
