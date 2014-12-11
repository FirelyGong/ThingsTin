using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ThingsTin.Interfaces;
using ThingsTin.Interfaces.Container;
using ThingsTin.ViewModels;
using ThingsTin.Views;

namespace ThingsTin.Frame
{
    public class ThingsContainer:IThingsTin
    {
        private Dictionary<Guid, IThingsService> _frameServices;
        private OperationPageManager _pagesManager;
        private AppMessager _messager;


        public ThingsContainer()
        {
            _frameServices = new Dictionary<Guid, IThingsService>();
            Progress = new ProgressManager();
            _pagesManager = new OperationPageManager(this);
            _messager = new AppMessager(this);
            Dialog = new DialogManager();
        }

        public void RegistryService(Guid serviceId, IThingsService service)
        {
            if (!_frameServices.ContainsKey(serviceId))
            {
                _frameServices.Add(serviceId, service);
            }
        }

        public IThingsService ResolveService(Guid serviceId)
        {
            if (!_frameServices.ContainsKey(serviceId))
            {
                return null;
            }

            return _frameServices[serviceId];
        }

        public IPageManager Pages
        {
            get
            {
                return _pagesManager;
            }
        }

        public event Action<FrameEvent> OnFrameStarting;

        public event Action<FrameEvent> OnFrameStarted;

        public event Action<FrameEvent> OnFrameClosing;

        public event Action<FrameEvent> OnFrameClosed;

        public void StartUp(SplashWindow startingWin, BootLoader loader)
        {
            ThingsTinView mainView = new ThingsTinView(this);
            ThingsTinViewModel containerViewModel = new ThingsTinViewModel();
            mainView.Show();
            containerViewModel.AddFunctions(loader.Functions);
            containerViewModel.AddMenu(loader.Menu);
            if (!string.IsNullOrEmpty(loader.Title))
            {
                containerViewModel.Title = loader.Title;
            }
            if (!string.IsNullOrEmpty(loader.IconPath))
            {
                containerViewModel.Icon = new BitmapImage(new Uri(loader.IconPath));
            }
            mainView.DataContext = containerViewModel;
            SetContainerWindow(mainView);
            SetViewModel(containerViewModel);

            FrameEvent frameEvent = new FrameEvent();
            FrameStarted(frameEvent);
            startingWin.Close();
            if (frameEvent.IsCanceled)
            {
                mainView.Close();
            }
        }

        public void StartingFrame(FrameEvent e)
        {
            if (OnFrameStarting != null)
            {
                OnFrameStarting(e);
            }
        }

        public void FrameStarted(FrameEvent e)
        {
            if (OnFrameStarted != null)
            {
                OnFrameStarted(e);
            }
        }

        public void ClosingFrame(FrameEvent e)
        {
            if (OnFrameClosing != null)
            {
                OnFrameClosing(e);
            }
        }

        public void FrameClosed(FrameEvent e)
        {
            if (OnFrameClosed != null)
            {
                OnFrameClosed(e);
            }
        }

        public IDialogManager Dialog
        {
            get;
            private set;
        }

        public IProgressManager Progress { get; private set; }

        public Dispatcher Dispatcher { get; set; }

        public IMessager Messager
        {
            get
            {
                return _messager;
            }
        }

        private void SetContainerWindow(Window window)
        {
            ((ProgressManager)Progress).Owner = window;
            ((AppMessager)Messager).Owner = window;
            ((DialogManager)Dialog).Owner = window;
        }

        private void SetViewModel(ThingsTinViewModel viewModel)
        {
            _pagesManager.SetViewModel(viewModel);
            _messager.SetViewModel(viewModel);
        }
    }
}
