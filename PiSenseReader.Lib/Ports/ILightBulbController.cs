using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiSenseReader.Ports
{
    public interface ILightBulbController
    {
        Task<LightState> GetLightState();

        Task SetLightState(LightState state);
    }
}
