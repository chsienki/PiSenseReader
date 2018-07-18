using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PiSenseReader.Ports
{
    public interface IExternalSwitch
    { 
        Action<SwitchState> OnStateChanged { get; set; }
    }
}
