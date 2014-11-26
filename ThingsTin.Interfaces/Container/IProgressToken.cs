using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Interfaces.Container
{
    public interface IProgressToken
    {
        void UpdateProgress(int percentage, bool finished);

        void Cancel();

        bool IsCancelled { get; }
    }
}
