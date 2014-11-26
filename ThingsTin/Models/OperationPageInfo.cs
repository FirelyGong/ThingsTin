using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ThingsTin.Models
{
    public class OperationPageInfo:TabItem
    {        
        public ICommand ClosePage { get; set; }

        public OperationPageInfo()
        {
        }
    }
}
