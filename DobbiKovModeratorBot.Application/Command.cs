using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DobbiKovModeratorBot.Application
{
    public abstract class Command
    {
        public abstract string[] Names { get; set; }
        public abstract Task Execute(Message message, ITelegramBotClient client);
        public bool Contains(string message)
        {
            foreach(var command in Names)
            {
                if (message.Equals(command) || message.Equals($"/{command}"))
                    return true;
            }
            return false;
        }
    }
}
