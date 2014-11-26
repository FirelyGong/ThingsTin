using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThingsTin.Interfaces.Application
{
    public interface IThingsFunction
    {
        IList<INavigationItem> GetFeatures();

        void GenerateMenu(IList<IMenuBarItem> menu);
    }
}
