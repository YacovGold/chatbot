using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace List
{
    record PersistentDataStructure(List<string> List);

    public class TranslatePlugin : IPlugin
    {
        public const string _Id = "translate";
        public string Id => _Id;

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
                input.Callbacks.SendMessage(translate(input.Message, input).ToString());
            }
        }

        public string translate(string text, PluginInput input)
        {
            var value = Environment.GetEnvironmentVariable("TranslateKey");
            input.Callbacks.SendMessage(value);
            string len = input.PersistentData;
            if (len == null)
            {
                return "Type a language to translate:\"len \"";
            }
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                Headers =
            {
                { "X-RapidAPI-Key", value },
                { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
            },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "q", text },
                    { "target", len },
                    { "source", "" },
                }),
            };
            using (var response = client.Send(request))
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync();
                return body.Result.Split(':')[3].Split(',')[0];
            }
        }
    }
}
