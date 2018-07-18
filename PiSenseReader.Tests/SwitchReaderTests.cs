using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiSenseReader.Simulators;
using PiSenseReader;

namespace PiSenseReader.Tests
{
    [TestClass]
    public class SwitchReaderTests
    {
        [TestMethod]
        public void Switch_Notifies_When_ToggledOn()
        {
            // arrange
            SwitchState state = SwitchState.Off;
            ExternalSwitchMemory externalSwitch = new ExternalSwitchMemory(state);
            SwitchReader reader = new SwitchReader(externalSwitch);
            reader.OnSwitchChanged = (s) => state = s;

            // act
            externalSwitch.CurrentState = SwitchState.On;

            // assert
            Assert.AreEqual(SwitchState.On, state);
        }

        [TestMethod]
        public void Switch_Notifies_When_ToggledOff()
        {
            // arrange
            SwitchState state = SwitchState.On;
            ExternalSwitchMemory externalSwitch = new ExternalSwitchMemory(state);
            SwitchReader reader = new SwitchReader(externalSwitch);
            reader.OnSwitchChanged = (s) => state = s;

            // act
            externalSwitch.CurrentState = SwitchState.Off;

            // assert
            Assert.AreEqual(SwitchState.Off, state);
        }

        [TestMethod]
        public void Switch_DoesNotNotify_When_ToggledOff_And_AlreadyOff()
        {
            // arrange
            int toggleCount = 0;
            ExternalSwitchMemory externalSwitch = new ExternalSwitchMemory(SwitchState.Off);
            SwitchReader reader = new SwitchReader(externalSwitch);
            externalSwitch.CurrentState = SwitchState.Off;
            reader.OnSwitchChanged = (s) => toggleCount++;

            // act
            externalSwitch.CurrentState = SwitchState.Off;

            // assert
            Assert.AreEqual(0, toggleCount);
        }

        [TestMethod]
        public void Switch_DoesNotNotify_When_ToggledOn_And_AlreadyOn()
        {
            // arrange
            int toggleCount = 0;
            ExternalSwitchMemory externalSwitch = new ExternalSwitchMemory(SwitchState.On);
            SwitchReader reader = new SwitchReader(externalSwitch);
            externalSwitch.CurrentState = SwitchState.On;
            reader.OnSwitchChanged = (s) => toggleCount++;

            // act
            externalSwitch.CurrentState = SwitchState.On;

            // assert
            Assert.AreEqual(0, toggleCount);
        }


        [TestMethod]
        public void Switch_NotifiesOnlyOnce_When_ToggledOnTwice()
        {
            // arrange
            int toggleCount = 0;
            ExternalSwitchMemory externalSwitch = new ExternalSwitchMemory(SwitchState.Off);
            SwitchReader reader = new SwitchReader(externalSwitch);
            reader.OnSwitchChanged = (s) => toggleCount++;

            // act
            externalSwitch.CurrentState = SwitchState.On;
            externalSwitch.CurrentState = SwitchState.On;

            // assert
            Assert.AreEqual(1, toggleCount);
        }
    }
}
