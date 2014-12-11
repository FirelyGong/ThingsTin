using ThingsTin.Interfaces.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ThingsTin.Interfaces.Container
{
    public interface IPageManager
    {
        void OpenStartPage(UIElement control);

        void OpenPage(IOperationPage page, object parameter);

        void ClosePage(Guid pageId);

        void CloseAllPages(bool force);

        int PagesCount{get;}
    }
}
