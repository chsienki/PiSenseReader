using PiSenseReader.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiSenseReader.Simulators
{
    public class LightBulbControllerMemory : ILightBulbController
    {
        LightState currentState;

        public LightBulbControllerMemory(LightState initialState)
        {
            this.currentState = initialState;
        }

        public Task<LightState> GetLightState()
        {
            return Task.FromResult(currentState);
        }

        public Task SetLightState(LightState state)
        {
            this.currentState = state;
            return Task.CompletedTask;
        }
    }
}
