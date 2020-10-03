using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace SharedLibrary.Services
{
    public static class DeviceService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string _url = "http://api.openweathermap.org/data/2.5/weather?q=%C3%96rebro&appid=b9f2df37033f5febf912e841800f475a&units=metric&cnt=6";


        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {
            try
            {
                var response = await httpClient.GetAsync(_url);

                if (response.IsSuccessStatusCode)
                {
                    //                          var json = JsonConvert.DeserializeObject<WeatherModel.Root>(await response.Content.ReadAsStringAsync());
                    //var data = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<WeatherModel.Root>(await response.Content.ReadAsStringAsync()));

                    //var payload = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<WeatherModel.Root>(await response.Content.ReadAsStringAsync()))));
                    await deviceClient.SendEventAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<WeatherModel.Root>(await response.Content.ReadAsStringAsync())))));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load request {ex.Message}");
            }
        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {

                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;

                Console.WriteLine($"Message received: {Encoding.UTF8.GetString(payload.GetBytes())}");
                await deviceClient.CompleteAsync(payload);
            }
        }
    }
}
