using BasePlugin.Interfaces;
using Dals;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;

namespace WebRunner.Controllers
{
    [ApiController]
    [Route("whatsapp")]
    public class WhatsappBotController : TwilioController, IMessageSender
    {
        private static PluginExecutor pluginExecutor;
        private readonly string accountSid;
        private readonly string authToken;
        private readonly string whatsappBotNumber;

        public WhatsappBotController()
        {
            pluginExecutor ??= new PluginExecutor(this, new DbDal(), new PluginsMenu(), new PluginsManager());
            whatsappBotNumber = Environment.GetEnvironmentVariable("WHATSAPP_BOT_NUMBER");
            accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            Twilio.TwilioClient.Init(accountSid, authToken);
        }

        [HttpGet] public IActionResult Get() => Content("Hello whatsapp");

        [HttpPost]
        public IActionResult Post([FromForm] SmsRequest incomingMessage)
        {
            pluginExecutor.Run(incomingMessage.Body, incomingMessage.From);
            return Ok();
        }

        public void SendMessage(string userId, string data)
        {
            MessageResource.Create(
                body: data,
                from: new Twilio.Types.PhoneNumber(whatsappBotNumber),
                to: new Twilio.Types.PhoneNumber(userId)
                );
        }
    }
}
