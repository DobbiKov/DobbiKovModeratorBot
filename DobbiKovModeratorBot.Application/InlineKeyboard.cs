using DobbiKovModeratorBot.Domain.Enums;
using DobbiKovModeratorBot.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DobbiKovModeratorBot.Application
{
    public abstract class InlineKeyboard
    {
        public abstract KeyboardIdentifier[] ID { get; set; }
        public abstract Task Execute(CallbackQuery e, int result, ITelegramBotClient client);
        public InlineKeyboardResult<int> Contains(string id)
        {
            for(int i = 0; i < ID.Length; i++ )
            {
                if (KeyboardIdenifiers.GetIdentifier(ID[i]).Equals(id))
                    return new InlineKeyboardResult<int>(true, i);
            }
            return new InlineKeyboardResult<int>(false, -1);
        }
    }
}
