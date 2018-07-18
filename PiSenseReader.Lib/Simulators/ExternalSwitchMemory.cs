using PiSenseReader.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PiSenseReader.Simulators
{
    public class ExternalSwitchMemory : IExternalSwitch
    {
        private SwitchState currentState;

        public ExternalSwitchMemory(SwitchState initialState)
        {
            this.currentState = initialState;
        }

        public SwitchState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                this.currentState = value;
                OnStateChanged?.Invoke(this.currentState);
            }
        }

        public Action<SwitchState> OnStateChanged { get; set; }
    }
}
