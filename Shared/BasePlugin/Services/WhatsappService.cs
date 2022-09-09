using BasePlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace BasePlugin.Services
{
    public class WhatsappService : IService
    {
        private readonly string accountSid;
        private readonly string authToken;
        private readonly string whatsappBotNumber;

        public WhatsappService()
        {
            whatsappBotNumber = Environment.GetEnvironmentVariable("WHATSAPP_BOT_NUMBER");
            accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            Twilio.TwilioClient.Init(accountSid, authToken);
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
