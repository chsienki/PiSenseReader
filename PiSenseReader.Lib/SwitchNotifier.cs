using PiSenseReader.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiSenseReader
{
    public class SwitchNotifier
    {
        private readonly IExternalSwitch externalSwitch;

        public SwitchNotifier(IExternalSwitch externalSwitch)
        {
            this.externalSwitch = externalSwitch;
        }


    }
}
