using DobbiKovModeratorBot.Application.Commands;
using DobbiKovModeratorBot.Application.InlineKeyboards;
using DobbiKovModeratorBot.Domain.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace DobbiKovModeratorBot.Application
{
    public static class CommandHandler
    {
        private static List<Command> commands;
        private static List<InlineKeyboard> inlineKeyboards;
        public static List<KeyboardMessage> keyboardMessages;

        public static void Start()
        {
            commands = new List<Command>();
            commands.AddRange(
                new List<Command>()
                {
                    new HelloCommand(),
                    new BanCommand(),
                    new UnbanCommand()
                }
            );

            inlineKeyboards = new List<InlineKeyboard>();
            inlineKeyboards.Add(
                    new BanKeyboard()
            );

            keyboardMessages = new List<KeyboardMessage>();
        }

        public static async Task CommandExecute(Message message, ITelegramBotClient client)
        {
            if(commands == null || inlineKeyboards == null)
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

        public static async Task CallbackExecute(CallbackQuery e, ITelegramBotClient client)
        {
            if (inlineKeyboards == null)
            {
                Console.WriteLine("Not keyboards!");
                Start();
            }
            Console.WriteLine("Tut!");
            foreach (var keyboard in inlineKeyboards)
            {
                var _keyboard = keyboard.Contains(e.Data);
                Console.WriteLine("Tut!!");
                if (_keyboard.isSucces)
                {
                    Console.WriteLine($"Tut!!! {_keyboard.result}");
                    await keyboard.Execute(e, _keyboard.result, client);
                    break;
                }
            }
        }
    }
}
