# chatbot
The aim of the project is to help novice developers enter the job market.
Taking a beginner programmer costs a company a lot of investment and money until the developer can manage on his own, therefore, most companies will try to take developers with experience only. Even if it is basic experience, it changes the whole picture.

The goal of the project is to take novice developers, teach them the basics (how to work in a team, git, pull-request, code-review, etc.), so that when the person looks for a job, it will be much easier for him / her to integrate as a junior developer.

The project contains code from several juniors (and me :smile:).

# documentation

## deployment and Telegram integration
### deply the app to Heroku
To deploy the app we'll use **Heroku**, with heroku you can deploy the app straight from your GitHub account. also, heroku gives you the ability to automaticlly deploy a new version of the app, everytime you make commit to your GitHub repository.

1. Go to [Heroku](https://www.heroku.com/). if you don't have an account already, you should sign up.
2. After you logged into your account, Click 'create app' and give the app a unique name.
3. Go to 'deployment method' section and choose the GitHub option. if you're not logged into GitHub, it will ask you to authenticate.
4. Search for the repository you want to deploy your app from and hit 'connect'.
5. Go to settings and in 'Buildpacks' section click on 'Add buildpack'. Heroku doesn't have an officially supported buildpack for .NET applications. so, copy this URL 'https://github.com/jincod/dotnetcore-buildpack', which is a buildpack provided on GitHub, and paste it to heroku.
6. Go back to deploy by clicking the 'deploy' tab, and scroll down to 'Manual deploy' section. choose the branch you want to deploy your app from. (if you want you can hit the button 'Enable Automatic Deploys' to deploy your app everytime you add a new commit to the branch).
7. Click 'Deploy Branch'. After the process is done, you can click on 'Open app'. your application should be up and running.

If you want better control of the app on heroku, you should download the Heroku [CLI](https://devcenter.heroku.com/articles/heroku-cli).
with this tool you can see logs, errors, etc.

### setup Telegram


### debug Telegram bot on local device using ngrok




