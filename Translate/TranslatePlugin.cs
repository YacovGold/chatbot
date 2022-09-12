using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Web;

namespace List
{
    public class TranslatePlugin : IPlugin
    {
        public const string _Id = "translate";
        public string Id => _Id;

        private static readonly string apiEndpoint = "https://api.cognitive.microsofttranslator.com/";

        private string _location;
        private string _subscriptionKey;

        public TranslatePlugin()
        {
            var subscriptionKey = "b10961be28e94111809e936c94648b85"; //Environment.GetEnvironmentVariable("TranslateKey");
            var location = "eastus2";// Environment.GetEnvironmentVariable("locationTranslate");

            if (subscriptionKey == null || location == null)
            {
                throw new Exception("Need to set env");
            }

            _subscriptionKey = subscriptionKey;
            _location = location;
        }

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
                input.Callbacks.SendMessage("Out of translation.");
            }
            else if (input.Message.ToLower().StartsWith("len"))
            {
                var str = input.Message.Substring("len".Length).Trim();
                input.Callbacks.SavePluginUserData(str);

                input.Callbacks.SendMessage($"the translation language: {str}");
            }
            else if (input.PersistentData == null)
            {
                input.Callbacks.SavePluginUserData("he");
                input.Callbacks.SendMessage(translate(input.Message, "he"));
            }
            else
            {
                input.Callbacks.SendMessage(translate(input.Message, input.PersistentData));
            }
        }

        string translate(string textToTranslate, string len)
        {
            // Input and output languages are defined as parameters.
            string route = $"/translate?api-version=3.0&from=&to={HttpUtility.UrlEncode(len)}&to=it";
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(apiEndpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", _location);

                // Send the request and get response.
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                // Read response as a string.
                string result = response.Content.ReadAsStringAsync().Result;
                return result.Split(':')[5].Split(',')[0].Split('"')[1];
            }
        }
    }
}
