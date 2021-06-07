using DobbiKovModeratorBot.Application.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DobbiKovModeratorBot.Application
{
    public static class CommandHandler
    {
        private static List<Command> commands;
        private static void Start()
        {
            commands = new List<Command>();
            commands.AddRange(
            new List<Command>()
            {
                new HelloCommand(),
                new BanCommand(),
                new UnbanCommand()
            });
        }

        public static async Task CommandExecute(Message message, ITelegramBotClient client)
        {
            if(commands == null)
            {
                Console.WriteLine("Not Commands!");
                Start();
            }
            if(message.Text != null)
            {
                foreach(var command in commands)
                {
                    if (command.Contains(message.Text.ToLower()))
                    {
                        await command.Execute(message, client);
                        break;
                    }
                }
            }
        }
    }
}
