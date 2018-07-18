using PiSenseReader.Ports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PiSenseReader
{
    public class LightCoordinator
    {
        List<ILightBulbController> bulbs = new List<ILightBulbController>();
        public void AddLightBulb(ILightBulbController bulb)
        {
            bulbs.Add(bulb);
        }

        public async Task ToggleLightState()
        {
            var currentState = await GetLightStates();
            var targetState = (currentState == LightState.On) ? LightState.Off : LightState.On;
            foreach (var bulb in this.bulbs)
            {
                await bulb.SetLightState(targetState);
            }
        }

        private async Task<LightState> GetLightStates()
        {
            foreach (var bulb in this.bulbs)
            {
                var state = await bulb.GetLightState();
                if (state == LightState.On)
                {
                    // we found at least one on bulb, so the total state is on
                    return LightState.On;
                }
            }
            return LightState.Off;
        }
    }
}