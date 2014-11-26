using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ThingsTin.Interfaces.Container
{    
    public interface IThingsTin
    { 
        void RegistryService(Guid serviceId, IThingsService service);

        IThingsService ResolveService(Guid serviceId);

        IPageManager Pages { get; }

        IDialogManager Dialog { get; }

        IProgressManager Progress { get; }

        IMessager Messager { get; }

        Dispatcher Dispatcher { get; set; }
        
        event Action<FrameEvent> OnFrameStarting;

        event Action<FrameEvent> OnFrameStarted;

        event Action<FrameEvent> OnFrameClosing;

        event Action<FrameEvent> OnFrameClosed;
    }
}
