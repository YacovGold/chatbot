# Chatbot Training Project

A modular chatbot platform with console, web, and Telegram runners.

## Overview

This repository was used as a practical learning project for junior developers. Instead of working on isolated exercises, contributors worked in a shared codebase and gained experience contributing to a real project.

The commit history shows students contributing real work to a shared application through pull requests, feedback, fixes, and incremental improvements. It highlights student ownership of features and integrations, not just classroom exercises or isolated demos.

## Student-Built Work

Most of the visible work in this repository was built by students through pull requests. That includes plugins, the web and Telegram runners, documentation updates, bug fixes, refactors, and other feature work in the shared codebase.

- plugin development
- web and Telegram runner work
- deployment and Telegram documentation
- bug fixes and usability improvements
- refactoring and cleanup
- test additions and maintenance work

## What Students Practiced

- opening GitHub pull requests
- receiving and applying review feedback
- debugging and fixing issues in an existing codebase
- refactoring code to improve readability and maintainability
- contributing to a shared codebase with real integrations and documentation

## What The Project Includes

- Plugin-based chatbot architecture
- Multiple runners: console, Telegram polling, and web
- Telegram bot integration, webhook support, and local debugging guidance
- Dependency injection and shared services
- In-memory DAL for simple local runs, plus database support through `CONNECTION_STRING`
- GitHub Actions workflows for build and deployment automation
- Basic test coverage

## Example Plugins

The repository includes plugins such as:

- calculator
- trivia
- alarm clock
- timers
- list/task management
- count words
- numerology calculator
- date and time tools
- translation

## Tech Stack

- C#
- .NET
- ASP.NET Core
- Entity Framework Core
- GitHub Actions
- Telegram.Bot

## Solution Structure

- `Plugins/` - independent chatbot plugins
- `Runners/ConsoleApp/` - local console runner
- `Runners/TelegramPullRunner/` - Telegram polling runner
- `Runners/WebRunner/` - ASP.NET Core web runner
- `Infrastructure/` - dependency injection and plugin loading
- `Services/` - messaging and runner-related services
- `Dals/`, `DB/`, `Shared/Dal/` - persistence layer and database access
- `TestProject/` - basic tests

## Configuration

- If `CONNECTION_STRING` is not defined, the app uses an in-memory DAL for local development.
- To enable Telegram features, set `TelegramKey`.
- The app recognizes connection strings in `mysql:...` and `postgres://...` formats.
- Some optional integrations use additional environment variables such as Twilio and translation service settings.

## Deployment

### Deploy The Web Runner

You can deploy the web runner to any hosting environment that supports ASP.NET Core applications.

1. Build and publish the web runner:

```powershell
dotnet publish Runners/WebRunner/WebRunner.csproj -c Release
```

2. Deploy the published output from `Runners/WebRunner/bin/Release/net6.0/publish/` to your target server or hosting platform.
3. Configure the required environment variables for the features you want to enable.
4. Expose the application over HTTPS if you plan to use Telegram webhooks.

### Database Setup

If you want persistence in a deployed environment:

1. Provision a MySQL or PostgreSQL database.
2. Set `CONNECTION_STRING` in one of the supported formats.
3. Restart the application and verify it can connect successfully.

## Telegram Setup

### Create A Bot

1. Open Telegram and search for `@BotFather`.
2. Run `/newbot`.
3. Follow the instructions and save the bot token.

### Set The Webhook

To connect the web app to Telegram, open this URL in a browser:

```text
https://api.telegram.org/bot{token}/setWebhook?url={YOUR_WEB_APP_URL}
```

Example:

```text
https://api.telegram.org/botabc123/setWebhook?url=https://example.com/telegram
```

### Debug Locally With Ngrok

If you want to debug Telegram locally, expose your local app with ngrok:

```powershell
ngrok http --host-header=localhost {PORT}
```

Then use the generated public URL as the webhook target.

## License

[MIT](LICENSE)
