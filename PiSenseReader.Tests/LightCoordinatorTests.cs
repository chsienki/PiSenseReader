using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiSenseReader.Simulators;
using System.Threading.Tasks;

namespace PiSenseReader.Tests
{
    [TestClass]
    public class LightCoordinatorTests
    {
        [TestMethod]
        public async Task Set_LightOff_When_Toggled_AndIsOn()
        {
            // arrange
            LightBulbControllerMemory bulb1 = new LightBulbControllerMemory(LightState.On);
            LightCoordinator coordinator = new LightCoordinator();
            coordinator.AddLightBulb(bulb1);

            // act
            await coordinator.ToggleLightState();
            var newState = await bulb1.GetLightState();

            // assert
            Assert.AreEqual(LightState.Off, newState);
        }

        [TestMethod]
        public async Task Set_LightOn_When_Toggled_AndIsOff()
        {
            // arrange
            LightBulbControllerMemory bulb1 = new LightBulbControllerMemory(LightState.Off);
            LightCoordinator coordinator = new LightCoordinator();
            coordinator.AddLightBulb(bulb1);

            // act
            await coordinator.ToggleLightState();
            var newState = await bulb1.GetLightState();

            // assert
            Assert.AreEqual(LightState.On, newState);
        }

        [TestMethod]
        public async Task Set_LightOn_When_ToggledTwice_AndIsOn()
        {
            // arrange
            LightBulbControllerMemory bulb1 = new LightBulbControllerMemory(LightState.On);
            LightCoordinator coordinator = new LightCoordinator();
            coordinator.AddLightBulb(bulb1);

            // act
            await coordinator.ToggleLightState();
            await coordinator.ToggleLightState();
            var newState = await bulb1.GetLightState();

            // assert
            Assert.AreEqual(LightState.On, newState);
        }

        [TestMethod]
        public async Task Set_AllLightsOff_When_Toggled_AndAllAreOn()
        {
            // arrange
            LightBulbControllerMemory bulb1 = new LightBulbControllerMemory(LightState.On);
            LightBulbControllerMemory bulb2 = new LightBulbControllerMemory(LightState.On);
            LightCoordinator coordinator = new LightCoordinator();
            coordinator.AddLightBulb(bulb1);
            coordinator.AddLightBulb(bulb2);


            // act
            await coordinator.ToggleLightState();
            var newState1 = await bulb1.GetLightState();
            var newState2 = await bulb2.GetLightState();

            // assert
            Assert.AreEqual(LightState.Off, newState1);
            Assert.AreEqual(LightState.Off, newState2);
        }

        [TestMethod]
        public async Task Set_AllLightsOn_When_Toggled_AndAllAreOff()
        {
            // arrange
            LightBulbControllerMemory bulb1 = new LightBulbControllerMemory(LightState.Off);
            LightBulbControllerMemory bulb2 = new LightBulbControllerMemory(LightState.Off);
            LightCoordinator coordinator = new LightCoordinator();
            coordinator.AddLightBulb(bulb1);
            coordinator.AddLightBulb(bulb2);


            // act
            await coordinator.ToggleLightState();
            var newState1 = await bulb1.GetLightState();
            var newState2 = await bulb2.GetLightState();

            // assert
            Assert.AreEqual(LightState.On, newState1);
            Assert.AreEqual(LightState.On, newState2);
        }

        [TestMethod]
        public async Task Set_AllLightsOff_When_ToggledTwice_AndAllAreOff()
        {
            // arrange
            LightBulbControllerMemory bulb1 = new LightBulbControllerMemory(LightState.Off);
            LightBulbControllerMemory bulb2 = new LightBulbControllerMemory(LightState.Off);
            LightCoordinator coordinator = new LightCoordinator();
            coordinator.AddLightBulb(bulb1);
            coordinator.AddLightBulb(bulb2);


            // act
            await coordinator.ToggleLightState();
            await coordinator.ToggleLightState();
            var newState1 = await bulb1.GetLightState();
            var newState2 = await bulb2.GetLightState();

            // assert
            Assert.AreEqual(LightState.Off, newState1);
            Assert.AreEqual(LightState.Off, newState2);
        }

        [TestMethod]
        public async Task Set_AllLightsOff_When_Toggled_AndOneIsOn()
        {
            // arrange
            LightBulbControllerMemory bulb1 = new LightBulbControllerMemory(LightState.On);
            LightBulbControllerMemory bulb2 = new LightBulbControllerMemory(LightState.Off);
            LightCoordinator coordinator = new LightCoordinator();
            coordinator.AddLightBulb(bulb1);
            coordinator.AddLightBulb(bulb2);


            // act
            await coordinator.ToggleLightState();
            var newState1 = await bulb1.GetLightState();
            var newState2 = await bulb2.GetLightState();

            // assert
            Assert.AreEqual(LightState.Off, newState1);
            Assert.AreEqual(LightState.Off, newState2);
        }

        [TestMethod]
        public async Task Set_AllLightsOn_When_ToggledTwice_AndOneIsOn()
        {
            // arrange
            LightBulbControllerMemory bulb1 = new LightBulbControllerMemory(LightState.On);
            LightBulbControllerMemory bulb2 = new LightBulbControllerMemory(LightState.Off);
            LightCoordinator coordinator = new LightCoordinator();
            coordinator.AddLightBulb(bulb1);
            coordinator.AddLightBulb(bulb2);


            // act
            await coordinator.ToggleLightState();
            await coordinator.ToggleLightState();
            var newState1 = await bulb1.GetLightState();
            var newState2 = await bulb2.GetLightState();

            // assert
            Assert.AreEqual(LightState.On, newState1);
            Assert.AreEqual(LightState.On, newState2);
        }
    }
}
