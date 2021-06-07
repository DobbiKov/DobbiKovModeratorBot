using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DobbiKovModeratorBot.Application.Commands
{
    public class UnbanCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "unban", "разбан" };

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            if (message.Chat.Type != ChatType.Group && message.Chat.Type != ChatType.Supergroup)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Данную команду можно использовать только в чате!");
                return;
            }

            if (!services.IsUserAdmin(client, message.Chat.Id, message.From.Id))
            {
                await client.SendTextMessageAsync(message.Chat.Id, $"Вы не Администратор группы!");
                return;
            }

            if (message.ReplyToMessage == null)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Ответьте на сообщение пользователя, которого хотите разблокировать.");
                return;
            }

            try
            {
                await client.UnbanChatMemberAsync(message.Chat.Id, message.ReplyToMessage.From.Id);
            }
            catch
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Произошла ошибка, возможно бот не является администратором, или пользователь не заблокирован.");
                return;
            }

            await client.SendTextMessageAsync(message.Chat.Id, $"Пользователь {message.ReplyToMessage.From.FirstName ?? ""} {message.ReplyToMessage.From.LastName ?? ""} был разблокирован.");

        }
    }
}
