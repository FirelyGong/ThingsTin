using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Interfaces.Container
{
    public interface IProgressManager
    {
        //void ShowProgress(Guid id, string m);
        //void UpdateProgress(Guid id, int p, bool f);

        void DoProgress(string title, string message, bool cancelable, Action<IProgressToken> job);
    }
}
