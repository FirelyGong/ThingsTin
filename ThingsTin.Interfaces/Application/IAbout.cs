using System;
using System.Windows;

namespace ThingsTin.Interfaces.Application
{
    public interface IAbout
    {
        string AppName { get; set; }

        string AppIcon { get; }

        Version AppVersion { get; set; }

        string Information { get; set; }

        string Copyright { get; set; }

        string Contact { get; set; }

        string[] Addtional { get; set; }
    }
}
