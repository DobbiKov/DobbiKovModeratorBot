using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace DobbiKovModeratorBot.Domain.Types
{
    public class KeyboardMessage
    {
        public long chatId { get; private set; }
        public Message messageFromBot { get; private set; }
        public Message messageFromuser { get; private set; }

        public KeyboardMessage(long chatId, Message messageFromBot, Message messageFromuser)
        {
            this.chatId = chatId;
            this.messageFromBot = messageFromBot;
            this.messageFromuser = messageFromuser;
        }
    }
}
