using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ThingsTin.Interfaces.Container
{
    public interface IOperationPage
    {
        Guid PageId { get; set; }

        string PageTitle { get; set; }

        UIElement PageContent { get; set; }

        void Initialize(object parameter);

        void OnPageClosing(PageEvent e);

        void OnPageClosed();
    }
}
