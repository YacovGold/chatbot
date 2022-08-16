# chatbot

The aim of the project is to help novice developers enter the job market.
Taking a beginner programmer costs a company a lot of investment and money until the developer can manage on his own, therefore, most companies will try to take developers with experience only. Even if it is basic experience, it changes the whole picture.

The goal of the project is to take novice developers, teach them the basics (how to work in a team, git, pull-request, code-review, etc.), so that when the person looks for a job, it will be much easier for him / her to integrate as a junior developer.

The project contains code from several juniors (and me :smile:).

# Documentation

## Deployment and Telegram integration

### Deploy the app to Heroku

To deploy the app we'll use **Heroku**, With Heroku you can deploy the app straight from your GitHub account. also, Heroku gives you the ability to automatically deploy a new version of the app, every time you make a commit to your GitHub repository.\

On Heroku you can easily generate a CI (Continuous Integration)/CD (Continuous Delivery) pipeline.

1. Go to [Heroku](https://www.heroku.com/). If you don't have an account already, sign up.
2. After you've logged into your account, Click 'create app' and give the app a unique name.
3. Go to 'deployment method' section and choose the GitHub option. If you're not logged into GitHub, it will ask you to authenticate.
4. Search for the repository you want to deploy your app from and hit 'connect'.
5. Go to settings and in 'Buildpacks' section click on 'Add buildpack'. Heroku doesn't have an officially supported buildpack for .NET applications. so, copy this URL 'ht<span>tps://</span>github.com/jincod/dotnetcore-buildpack', which is a buildpack provided on GitHub, and paste it to Heroku.
6. Now configure the environment variables. Add a few variables. First the Telegram bot token. Second, because our app has many projects, we need to specify the startup project name and provide the path to the project files. So, click the 'Reveal Config Vars' at the 'Config Vars' section and add 3 key-value pairs:

   - PROJECT_FILE - Runners/TelegramWebRunner/TelegramWebRunner.csproj.
   - PROJECT_NAME - TelegramWebRunner.
   - TelegramKey - {YOUR_TOKEN}.

7. Go back to deploy by clicking the 'deploy' tab, and scroll down to 'Manual deploy' section. Choose the branch you want to deploy your app from. (If you want you can hit the button 'Enable Automatic Deploys' to deploy your app every time you add a new commit to the branch).
8. Click 'Deploy Branch'. After the process is done, you can click on 'Open app'. Your application should be up and running.

If you want better control of the app on Heroku, you should download the Heroku [CLI](https://devcenter.heroku.com/articles/heroku-cli).\
With this tool you can see logs, errors, etc.

### Telegram setup and integration

#### Set up a Telegram bot

Go to the Telegram app and find a telegram bot named '@BotFather', type '/newbot' and follow the instructions it gives you.\
Keep the token you got, somewhere safe.

#### Telegram Webhooks integration

To retrieve data from Telegram, you will have to set up Telegram Webhooks Integration. You'll do that by going to the browser and in the URL bar, type the following line:\
"ht<span>tps://</span>api.telegram.org/bot{token}/setWebhook?url={OUR_WEB_APP_URL}"\
For example, if the token is 'abc123' and the app url is ht<span>tps://</span>abc.com, then type in the URL bar:\
"ht<span>tps://</span>api.telegram.org/botxyz123/setWebhook?url=ht<span>tps://</span>xyz.com"

### Debug Telegram bot on local device using Ngrok

If you want to debug the Telegram bot on your local device, you have to expose your local server port, on which the app is running, to the Internet. For that you can use Ngrok.
Go to ngrok.com, sign up, download the version that suits your OS, and follow the installation instructions.\
Run the command 'ngrok http --host-header=localhost <'PORT'>' and in the window that opened up you should see the URL that ngrok generates to your local port.\
Now you have set webhook to the ngrok URL.
