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
    }
}
