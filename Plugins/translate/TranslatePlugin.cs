using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace List
{
    public class TranslatePlugin : IPlugin
    {
        public const string _Id = "translate";
        public string Id => _Id;
        private static readonly string subscriptionKey = "b10961be28e94111809e936c94648b85";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";
        private static readonly string location = "eastus2";

        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage("Type a sentence to translate. Enter 'len' to which language to translate into. Enter 'Exit' to stop");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage("List stopped.");
            }
            else if (input.Message.ToLower().StartsWith("len"))
            {
                var str = input.Message.Substring("len".Length).Trim();
                input.Callbacks.SavePluginUserData(str);

                input.Callbacks.SendMessage($"the translation language: {str}");
            }
            else
            {
                translate(input.Message, input).ToString();
            }
        }
        static async Task translate(string textToTranslate, PluginInput input)
        {
            // Input and output languages are defined as parameters.
            string len = input.PersistentData;
            string route = "/translate?api-version=3.0&from=&to="+ len + "&to=it";
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                input.Callbacks.SendMessage(result.Split(':')[5].Split(',')[0]);
            }
            
        }
    }
}
