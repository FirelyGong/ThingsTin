using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThingsTin.Interfaces.Container;
using ThingsTin.Views;

namespace ThingsTin.Frame
{
    public class ProgressManager : IProgressManager, IProgressToken
    {
        ProgressWindow _progWindow;

        public ProgressManager()
        {
        }

        public void DoProgress(string title, string message, bool cancelable, Action<IProgressToken> job)
        {
            _progWindow = new ProgressWindow(this);
            if (!string.IsNullOrEmpty(title))
            {
                _progWindow.SetProgressTitle(title);
            }

            _progWindow.SetProgressMessage(message);
            _progWindow.SetCancelable(cancelable);

            ThreadPool.QueueUserWorkItem(o => job(this));
            _progWindow.Owner = Owner;
            _progWindow.ShowDialog();
        }

        public void UpdateProgress(int perentage, bool finished)
        {
            _progWindow.Dispatcher.Invoke(new Action(() => _progWindow.UpdateProgress(perentage, finished)));
        }

        public void Cancel()
        {
            IsCancelled = true;
            _progWindow.Close();
        }

        public bool IsCancelled
        {
            get;
            private set;
        }
        public Window Owner { get; set; }
    }
}
