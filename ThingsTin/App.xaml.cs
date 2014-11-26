using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ThingsTin.Exceptions;
using ThingsTin.Frame;
using ThingsTin.Interfaces.Application;
using ThingsTin.Interfaces.Container;
using ThingsTin.Utils;
using ThingsTin.ViewModels;
using ThingsTin.Views;

namespace ThingsTin
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public const string PipeLineName = "ThingsTinPipe";

        protected override void OnStartup(StartupEventArgs e)
        {
            LoadResources();
            base.OnStartup(e);

            SplashWindow startingWin = new SplashWindow();
            startingWin.Show();
            bool startupDone = true;
            ThingsContainer thingsTin = new ThingsContainer();
            thingsTin.Dispatcher = Dispatcher;
            BootLoader loader = new BootLoader();
            MenuInitializer init = new MenuInitializer(loader, thingsTin);
            init.Initialize();

            ThreadPool.QueueUserWorkItem(new WaitCallback(obj =>
            {
                try
                {
                    startupDone = loader.Startup(thingsTin, startingWin);
                    if (startupDone)
                    {
                        Thread.Sleep(2000);
                        Dispatcher.Invoke(new Action(() =>
                        {
                            StartMainView(thingsTin, startingWin, loader);
                        }));
                    }
                }
                catch (Exception ex)
                {
                    ThingsTinExceptionProcesser.Instance.ProcessException("App", "OnStartup", ex);
                    MessageBox.Show(ex.Message);
                    Environment.Exit(0);
                }
            }));
        }

        private void StartMainView(ThingsContainer thingsTin,SplashWindow startingWin ,BootLoader loader)
        {
            thingsTin.StartUp(startingWin, loader);
            FrameUiExceptionHandler uiExHdlr = new FrameUiExceptionHandler();
            uiExHdlr.Initialize(thingsTin);
            ThingsTinExceptionProcesser.Instance.UIExceptionHandler = uiExHdlr;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void LoadResources()
        {
            const string resourceConfigKey="ResourcesFrom";
            var config = ConfigurationManager.AppSettings[resourceConfigKey];
            if (config == null)
            {
                return;
            }

            var resourceUri = config.ToString();
            Application.Current.Resources.MergedDictionaries.Add(
                    System.Windows.Application.LoadComponent(
                        new Uri(resourceUri,
                        UriKind.Relative)) as System.Windows.ResourceDictionary);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ThingsTinExceptionProcesser.Instance.ProcessException("[null]", "[null]", (Exception)e.ExceptionObject);
        }
    }
}
