using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using ThingsTin.Frame;
using ThingsTin.Interfaces;
using ThingsTin.Interfaces.Application;
using ThingsTin.Interfaces.Container;
using ThingsTin.Locater;
using ThingsTin.Utils;
using ThingsTin.ViewModels;
using ThingsTin.Views;

namespace ThingsTin
{
    public class BootLoader
    {
        private ThingsContainer _thingsTin;
        private SplashWindow _startingWin;

        public BootLoader()
        {
            Functions = new List<INavigationItem>();
            Menu = new List<IMenuBarItem>();
        }

        public List<INavigationItem> Functions { get; private set; }

        public List<IMenuBarItem> Menu{get; private set;}

        public string IconPath { get; private set; }

        public string Title { get; private set; }

        public bool Startup(ThingsContainer thingsTin, SplashWindow startingWin)
        {
            _thingsTin = thingsTin;
            _startingWin = startingWin;
            _startingWin.WriteLine("Exploring things...");
            var things = LoadThings();
            int index = 1;
            int count = things.Count;
            foreach (var thing in things)
            {
                bool started = StartThing(thing, index + "/" + count);
                if (!started)
                {
                    return false;
                }
            }

            _startingWin.WriteLine("Ready for ThingsTin...");

            return true;
        }

        private IList<IThingsSense> LoadThings()
        {
            ThingsLocater locater;
            string basePath=Path.GetDirectoryName(GetType().Assembly.Location);
            string xmlFile=Path.Combine(basePath, "ThingsList.xml");
            if (!File.Exists(xmlFile))
            {
                return new List<IThingsSense>();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(ThingsLocater));
            using (FileStream fs = new FileStream(xmlFile, FileMode.Open, FileSystemRights.ReadData, FileShare.Read,8,FileOptions.None))
            {
                locater = (ThingsLocater)serializer.Deserialize(fs);
            }

            IList<IThingsSense> things = new List<IThingsSense>();
            foreach (var thing in locater.Things)
            {
                var file = Path.Combine(basePath, thing.Assembly);
                Assembly ass = Assembly.LoadFrom(file);
                things.Add((IThingsSense)ass.CreateInstance(thing.Type));
            }

            return things;
        }

        private bool StartThing(IThingsSense thing, string index)
        {
            _startingWin.WriteLine(string.Format(CultureInfo.InvariantCulture, "Starting things({0})...", index));
            thing.Initialize(_thingsTin);
            FrameEvent frameEvent = new FrameEvent();
            _thingsTin.StartingFrame(frameEvent);
            if (frameEvent.IsCanceled)
            {
                MessageBox.Show("The startup is canceled! Detail message:[" + frameEvent.Message + "]", thing.About.AppName);
                return false;
            }

            var features = thing.Functions.GetFeatures();
            Functions.AddRange(features);
            thing.Functions.GenerateMenu(Menu);

            _startingWin.WriteLine(string.Format(CultureInfo.InvariantCulture, "Loading resources from things({0})...", index));
            if (!string.IsNullOrEmpty(thing.About.AppIcon))
            {
                string iconPath = string.Format(CultureInfo.InvariantCulture, "pack://application:,,,/{0};component/{1}", thing.GetType().Assembly.GetName().Name, thing.About.AppIcon);
                IconPath = iconPath;// new BitmapImage(new Uri(iconPath));
            }
            if (thing.About != null && !string.IsNullOrEmpty(thing.About.AppName))
            {
                Title = thing.About.AppName;
            }
            //if (thing.About.Background != null)
            //{
            //    _thingsTin.MainViewModel.BackgroundPage = thing.About.Background;
            //}

            _startingWin.WriteLine(string.Format(CultureInfo.InvariantCulture, "Things({0}) loaded...", index));

            return true;
        }
    }
}
