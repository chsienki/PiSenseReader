using PiSenseReader.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiSenseReader
{
    /// <summary>
    /// Notifies when a switch changes state. Handles debounce, state tracking etc
    /// </summary>
    public class SwitchReader
    {
        private readonly IExternalSwitch externalSwitch;

        private SwitchState? lastState = null;

        public SwitchReader(IExternalSwitch externalSwitch)
        {
            this.externalSwitch = externalSwitch;
            this.externalSwitch.OnStateChanged = HandleExternalChange;
        }

        public Action<SwitchState> OnSwitchChanged { get; set; }

        //TODO: handle debounce etc.

        private void HandleExternalChange(SwitchState state)
        {
            if (lastState != state)
            {
                lastState = state;
                OnSwitchChanged?.Invoke(state);
            }
        }
    }
}
