using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DobbiKovModeratorBot.Application.Commands
{
    public class HelloCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "hello", "hi" };

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, $"Hello!!!");
        }
    }
}
