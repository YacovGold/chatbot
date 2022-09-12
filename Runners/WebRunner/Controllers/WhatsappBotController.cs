using BasePlugin.Interfaces;
using Dals;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using BasePlugin.Records;
using Services;

namespace WebRunner.Controllers
{
    [ApiController]
    [Route("whatsapp")]
    public class WhatsappBotController : TwilioController
    {
        private static PluginExecutor _pluginExecutor;

        public WhatsappBotController(PluginExecutor pluginExecutor)
        {
            _pluginExecutor = pluginExecutor;
        }

        [HttpGet] public IActionResult Get() => Content("Hello whatsapp");

        [HttpPost]
        public IActionResult Post([FromForm] SmsRequest incomingMessage)
        {
            User user = new User(incomingMessage.From, RunnerType.Whatsapp);
            _pluginExecutor.Run(incomingMessage.Body, user);
            return Ok();
        }
    }
}
