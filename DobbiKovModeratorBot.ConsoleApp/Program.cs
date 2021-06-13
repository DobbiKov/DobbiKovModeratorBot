using DobbiKovModeratorBot.Application;
using DobbiKovModeratorBot.ConsoleApp;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace DobbiKovModeratorBot.ConsoleApp
{
    class Program
    {
        private static ITelegramBotClient client;
        static void Main(string[] args)
        {
            client = new TelegramBotClient(Config.Token);

            client.StartReceiving();
            Console.WriteLine("Bot started.");
            CommandHandler.Start();
            client.OnMessage += Client_OnMessage;
            client.OnCallbackQuery += Client_OnCallbackQuery;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void Client_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var callbackQuery = e.CallbackQuery;
            await CommandHandler.CallbackExecute(callbackQuery, client);
        }

        private static async void Client_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            await CommandHandler.CommandExecute(message, client);
        }
    }
}
