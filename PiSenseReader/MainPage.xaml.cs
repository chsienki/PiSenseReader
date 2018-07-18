using PiSenseReader.Adaptors;
using PiSenseReader.Simulators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PiSenseReader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //public bool lightsOn = false;

        // public bool gpioStatus = false;

        //public LightController lights = new LightController(new string[]{ "192.168.0.190", "192.168.0.124" });

        public LightCoordinator lightCoordinator;

        public SwitchReader switchReader;

        public MainPage()
        {
            this.InitializeComponent();
            
            // Create a coordinator and add some bulbs for it talk to 
            this.lightCoordinator = new LightCoordinator();
            this.lightCoordinator.AddLightBulb(new TPLinkLightBulbAdaptor("192.168.0.190"));
            this.lightCoordinator.AddLightBulb(new TPLinkLightBulbAdaptor("192.168.0.124"));

            // Set up the reader, and trigger the coordinator when it changes
            this.switchReader = new SwitchReader(new GPIOSwitchAdaptor())
            {
                OnSwitchChanged = async (state) => await this.lightCoordinator.ToggleLightState()
            };
        }


        //public async Task DoWork()
        //{
        //    var result = await lights.GetState();

        //    var GPIOTask = RunGpioDetectLoop();
        //    var ReactionTask = RunReactionLoop();

        //    await Task.WhenAll(GPIOTask, ReactionTask);
        //}

        //public async Task RunGpioDetectLoop()
        //{
        //    GpioController gpio = GpioController.GetDefault();
        //    if (gpio == null)
        //    {
        //        //TODO: need some sort of error handling. Set up a WebAPI so we can query?
        //        return; // GPIO not available on this system
        //    }

        //    bool lastStatus = false;

        //    // Open GPIO 3
        //    using (GpioPin pin = gpio.OpenPin(5))
        //    {
        //        // mke it an input
        //        pin.Write(GpioPinValue.Low);
        //        pin.SetDriveMode(GpioPinDriveMode.InputPullDown);
        //        pin.Write(GpioPinValue.Low);

        //        while (true)
        //        {
        //            // read the state of the pin every 100 ms and react as needed
        //            var status = pin.Read();
        //            Debug.WriteLine("GPIO 5 is " + status);
        //            if (status == GpioPinValue.Low)
        //            {
        //                // default, no VCC detected. Switch is OFF.
        //                gpioStatus = false;
        //                if(lastStatus == true)
        //                {
        //                    lastStatus = false;
        //                    await this.lights.SetOff();
        //                }
        //            }
        //            else
        //            {
        //                // VCC connected. Switch is ON
        //                gpioStatus = true;
        //                if (lastStatus == false)
        //                {
        //                    lastStatus = true;
        //                    await this.lights.SetOn();
        //                }
        //            }
        //            await Task.Delay(100);
        //        }
        //    }
        //}

        //public async Task RunReactionLoop()
        //{
        //    await Task.Delay(100);
        //}

    }
}
