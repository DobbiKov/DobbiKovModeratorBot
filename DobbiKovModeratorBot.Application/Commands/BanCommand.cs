using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Linq;
using Telegram.Bot.Types.Enums;

namespace DobbiKovModeratorBot.Application.Commands
{
    public class BanCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "ban", "бан" };

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            if(message.Chat.Type != ChatType.Group && message.Chat.Type != ChatType.Supergroup)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Данную команду можно использовать только в чате!");
                return;
            }
            
            if(!services.IsUserAdmin(client, message.Chat.Id, message.From.Id))
            {
                await client.SendTextMessageAsync(message.Chat.Id, $"Вы не Администратор группы!");
                return;
            }


            if (message.ReplyToMessage == null)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Ответьте на сообщение пользователя, которого хотите выгнать.");
                return;
            }

            if (message.ReplyToMessage.From.IsBot)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Бот не может разблокировать бота. Вы можете это сделать самостоятельно!");
                return;
            }

            if(message.From.Id == message.ReplyToMessage.From.Id)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Вы не можете кикнуть себя!");
                return;
            }

            if(services.IsUserAdmin(client, message.Chat.Id, message.ReplyToMessage.From.Id) || message.ReplyToMessage.From.Id == 716720991)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Пользователь, которого вы хотите кикнуть является администратором.");
                return;
            }

            try
            {
                await client.KickChatMemberAsync(message.Chat.Id, message.ReplyToMessage.From.Id);
            }
            catch
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Произошла ошибка, возможно бот не является администратором.");
                return;
            }

            await client.SendTextMessageAsync(message.Chat.Id, $"Пользователь {message.ReplyToMessage.From.FirstName ?? ""} {message.ReplyToMessage.From.LastName ?? ""} был изгнан из беседы.");
        }
    }
}
