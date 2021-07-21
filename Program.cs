using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EggTimerBot
{
    class Program
    {
        static TelegramBotClient EggTimerBot = new TelegramBotClient("YOUR API CODE HERE");

        static void Main(string[] args)
        {
            Console.Write("Connecting... ");
            EggTimerBot.OnMessage += EggTimerBot_OnMessage;
            EggTimerBot.StartReceiving();
            Console.WriteLine("OK!");
            Console.ReadKey();
        }

        private static String FormName(User tgUser)
        {
            String nameString = "";
            if (tgUser.FirstName != null && tgUser.FirstName.Length > 0)
            {
                nameString += tgUser.FirstName;
            }

            if (tgUser.LastName != null && tgUser.LastName.Length > 0)
            {
                nameString += " " + tgUser.LastName;
            }

            if (tgUser.Username != null && tgUser.Username.Length > 0)
            {
                nameString += " (@" + tgUser.Username + ")";
            }
            return nameString;
        }

        private async static void EggTimerBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != MessageType.Text) return;

            if (message.Text.StartsWith("/set"))
            {
                string resultString = Regex.Match(message.Text, @"\d+").Value;
                bool isNumeric = int.TryParse(resultString, out int n);
                if (isNumeric && n > 0 && n <= 3600)
                {
                    Console.WriteLine("Added timer for " + FormName(message.From) + " in " + n.ToString() + " seconds.");
                    await EggTimerBot.SendTextMessageAsync(message.Chat.Id, "Tick tock...", ParseMode.Default, default, default, default, message.MessageId);
                    await Task.Delay(n*1000);
                    await EggTimerBot.SendTextMessageAsync(message.Chat.Id, "Ding!",ParseMode.Default, default, default, default, message.MessageId);
                    Console.WriteLine("Timer for " + FormName(message.From) + " done.");
                }
                else
                {
                    string returnMessage = "ℹ️ <b>Egg timer guide:</b>" + Environment.NewLine;
                    returnMessage += Environment.NewLine;
                    returnMessage += "  /set 600 pizza   --   Warn me in 10 minutes (600 seconds)" + Environment.NewLine;
                    returnMessage += Environment.NewLine;
                    returnMessage += "Maximum value is 3600 (one hour)" + Environment.NewLine;
                    await EggTimerBot.SendTextMessageAsync(message.Chat.Id, returnMessage, ParseMode.Html, default, default, default, message.MessageId);
                }
            }
        }
    }
}
