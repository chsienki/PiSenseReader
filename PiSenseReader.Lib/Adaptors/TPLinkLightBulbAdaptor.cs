using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PiSenseReader.Ports;

namespace PiSenseReader.Adaptors
{
    public class TPLinkLightBulbAdaptor : ILightBulbController
    {
        readonly static string off = "{\"smartlife.iot.smartbulb.lightingservice\":{ \"transition_light_state\": {  \"on_off\" : 0, \"brightness\": 0, \"transition_period\":0} } }";

        readonly static string on = "{\"smartlife.iot.smartbulb.lightingservice\":{ \"transition_light_state\": {  \"on_off\" : 1, \"brightness\": 100, \"transition_period\":0 } } }";

        readonly static string getState = "{\"smartlife.iot.smartbulb.lightingservice\":{ \"get_light_state\": {} } }";

        private readonly string address;

        public TPLinkLightBulbAdaptor(string ipAddress)
        {
            this.address = ipAddress;
        }

        public async Task<LightState> GetLightState()
        {
            var response = await SendMessage(address, getState, true);

            //todo: should use json parsing
            LightState result = LightState.Off;
            if (response.Length > 0)
            {
                var resultIndex = response.IndexOf("on_off\":");
                if(response.Substring(resultIndex + 8, 1) == "1")
                {
                    result = LightState.On;
                }
            }

            return result;
        }

        public async Task SetLightState(LightState state)
        {
            var message = (state == LightState.On) ? on : off;
            await SendMessage(this.address, message);
        }

        private Task SendMessage(string address, string message)
        {
            return SendMessage(address, message, false);
        }

        private async Task<string> SendMessage(string address, string message, bool awaitResponse)
        {
            var packet = Encode(message);

            UdpClient client = new UdpClient();
            await client.SendAsync(packet, packet.Length, address, 9999);

            string response = string.Empty;
            if (awaitResponse)
            {
                var result = await client.ReceiveAsync().SetTimeout(5000);
                if (result.Buffer != null)
                {
                    response = Decode(result.Buffer.ToArray());
                }
            }
            return response;
        }

        static byte[] Encode(string data)
        {
            var output = Encoding.ASCII.GetBytes(data);

            byte key = 0xAB;
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (byte)(output[i] ^ key);
                key = output[i];
            }

            return output;
        }

        static string Decode(byte[] data)
        {
            byte key = 0xAB;
            for (int i = 0; i < data.Length; i++)
            {
                var nextKey = data[i];
                data[i] = (byte)(data[i] ^ key);
                key = nextKey;
            }

            return Encoding.ASCII.GetString(data);
        }
    }
}
