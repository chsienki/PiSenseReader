using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiSenseReader;
using PiSenseReader.Adaptors;
using PiSenseReader.Ports;
using PiSenseReader.Simulators;
using System.Threading.Tasks;

namespace PiSenseReader.Tests
{
    [TestClass]
    public class LightBulbControllerTests
    {

        [TestMethod]
        public async Task LightBulb_Goes_On()
        {
            // arrange
            ILightBulbController bulb1 = await GetBulbToTest(LightState.Off);

            // act
            await bulb1.SetLightState(LightState.On);
            var bulb1state = await bulb1.GetLightState();

            // assert
            Assert.AreEqual(LightState.On, bulb1state);
        }

        [TestMethod]
        public async Task LightBulb_Goes_Off()
        {
            // arrange
            ILightBulbController bulb1 = await GetBulbToTest(LightState.On);

            // act
            await bulb1.SetLightState(LightState.Off);
            var bulb1state = await bulb1.GetLightState();

            // assert
            Assert.AreEqual(LightState.Off, bulb1state);
        }

        [TestMethod]
        public async Task LightBulb_Stays_Off()
        {
            // arrange
            ILightBulbController bulb1 = await GetBulbToTest(LightState.Off);

            // act
            await bulb1.SetLightState(LightState.Off);
            var bulb1state = await bulb1.GetLightState();

            // assert
            Assert.AreEqual(LightState.Off, bulb1state);
        }

        [TestMethod]
        public async Task LightBulb_Stays_On()
        {
            // arrange
            ILightBulbController bulb1 = await GetBulbToTest(LightState.On);

            // act
            await bulb1.SetLightState(LightState.On);
            var bulb1state = await bulb1.GetLightState();

            // assert
            Assert.AreEqual(LightState.On, bulb1state);
        }


        // Set to true to run tests against a real, physical, lightbulb
        private bool isIntegration = false;

        private async Task<ILightBulbController> GetBulbToTest(LightState initialState)
        {
            if(isIntegration)
            {
                var bulb = new TPLinkLightBulbAdaptor("192.168.0.190");
                await bulb.SetLightState(initialState);
                return bulb;
            }
            else
            {
                return new LightBulbControllerMemory(initialState);
            }
        }
    }
}
