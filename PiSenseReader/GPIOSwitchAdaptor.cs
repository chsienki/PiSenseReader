using PiSenseReader.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace PiSenseReader.Adaptors
{
    public class GPIOSwitchAdaptor : IExternalSwitch
    {
        public Action<SwitchState> OnStateChanged { get ; set; }

        public GPIOSwitchAdaptor()
        {
            var noAwait = RunGpioDetectLoop();
        }

        public async Task RunGpioDetectLoop()
        {
            GpioController gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                //TODO: need some sort of error handling. Set up a WebAPI so we can query?
                return; // GPIO not available on this system
            }

            // Open GPIO 3
            using (GpioPin pin = gpio.OpenPin(5))
            {
                // mke it an input
                pin.Write(GpioPinValue.Low);
                pin.SetDriveMode(GpioPinDriveMode.InputPullDown);
                pin.Write(GpioPinValue.Low);

                while (true)
                {
                    // read the state of the pin every 100 ms and react as needed
                    var status = pin.Read();

                    var switchState = (status == GpioPinValue.Low) ? SwitchState.Off : SwitchState.On;
                    this.OnStateChanged?.Invoke(switchState);

                    await Task.Delay(100);

                    //if (status == GpioPinValue.Low)
                    //{
                    //    // default, no VCC detected. Switch is OFF.
                    //    gpioStatus = false;
                    //    if (lastStatus == true)
                    //    {
                    //        lastStatus = false;
                    //        await this.lights.SetOff();
                    //    }
                    //}
                    //else
                    //{
                    //    // VCC connected. Switch is ON
                    //    gpioStatus = true;
                    //    if (lastStatus == false)
                    //    {
                    //        lastStatus = true;
                    //        await this.lights.SetOn();
                    //    }
                    //}
                }
            }
        }
    }
}
