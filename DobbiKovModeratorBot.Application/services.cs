using DobbiKovModeratorBot.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DobbiKovModeratorBot.Application
{
    public static class services
    {
        public static bool IsUserAdmin(ITelegramBotClient client, long chatId, int userId)
        {
            var admins = client.GetChatAdministratorsAsync(chatId);

            foreach (var admin in admins.Result)
            {
                if (admin.User.Id == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public static KeyboardMessage GetKeyboardMessage(long chatId, Message botMessage)
        {
            foreach(var comm in CommandHandler.keyboardMessages)
            {
                if (comm.chatId == chatId && comm.messageFromBot.MessageId == botMessage.MessageId)
                    return comm;
            }
            throw new Exception("Not message in List!");
        }
    }
}
