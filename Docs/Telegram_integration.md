## Telegram setup and integration

### Set up a Telegram bot

Go to the Telegram app and find a telegram bot named '@BotFather', type '/newbot' and follow the instructions it gives you.\
Keep the token you got, somewhere safe.

### Telegram Webhooks integration

To retrieve data from Telegram, you will have to set up Telegram Webhooks Integration. You'll do that by going to the browser and in the URL bar, type the following line:\
"ht<span>tps://</span>api.telegram.org/bot{token}/setWebhook?url={OUR_WEB_APP_URL}"\
For example, if the token is 'abc123' and the app url is ht<span>tps://</span>abc.com/telegram, then type in the URL bar:\
"ht<span>tps://</span>api.telegram.org/botxyz123/setWebhook?url=ht<span>tps://</span>xyz.com/telegram"

### Debug Telegram bot on local device using Ngrok

If you want to debug the Telegram bot on your local device, you have to expose your local server port, on which the app is running, to the Internet. For that you can use Ngrok.
Go to ngrok.com, sign up, download the version that suits your OS, and follow the installation instructions.\
Run the command 'ngrok http --host-header=localhost {PORT}' and in the window that opened up you should see the URL that ngrok generates to your local port.\
Now you have set webhook to the ngrok URL.
