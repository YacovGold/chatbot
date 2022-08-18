## Deployment

### Deploy the app to Heroku

To deploy the app we'll use **Heroku**, With Heroku you can deploy the app straight from your GitHub account. also, Heroku gives you the ability to automatically deploy a new version of the app, every time you make a commit to your GitHub repository.

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

### Connecting to a web DB

On Heroku you can create a DB connection for the app, using Heroku Postgres.
To Create a Postgres database on Heroku and set the connection follow the next steps:

1. Go to your app on heroku and click 'Configure Add-ons', under the 'Overview' tab.
2. Search for 'Heroku Postgres' in the search bar, choose the free plan and click on 'Submit Order Form'. (To make sure you created a database and attached it to the app, you can go to 'Config Vars' under settings and you should see there now a variable named 'DATABASE_URL').
3. Install the package 'Npgsql.EntityFrameworkCore.PostgreSQL' from 'Nuget Package Manager' to the project where the DBContext class is in.
4. Where your DB configurations are, pass to the 'UseNpgsql' method, the connection string in this pattern:\
   "Server={Host};Port={Port};User Id={User};Password={Password};Database={Database};sslmode=Prefer;Trust Server Certificate=true".\
   You can find the database credentials by goimg to 'settings' in the database zone and click on '
   View Credentials…'.

If you go to the database on heroku, after you run the app and check the 'Overview', you should see the number of connections you have. which at this point it's probably 1.
