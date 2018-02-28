# NoxiousPluKsEggTimerBot
A simple Telegram bot that allows you to set reminders (test it on @NoxiousPluKsEggTimerBot)

Also works in groups

## Requirements
* .NET Core 2.0 (package `dotnet-sdk-2.x`)
* Any OS that runs .NET Core 2.0 (Linux, Windows, Mac OS, whatever)

## Setup bot
* Create a new bot in Botfather
* /setcommands
`set - For example: /set 600 pizza  —  Warn me in 10 minutes (600 seconds)`
* /setprivacy 
`DISABLED` (or it will only work in PM, or in supergroups where it is admin)
* Put the API code you got in [Program.cs](Program.cs)
* Type `dotnet run` to start the bot
