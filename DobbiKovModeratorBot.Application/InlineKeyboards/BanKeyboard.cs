using DobbiKovModeratorBot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DobbiKovModeratorBot.Application.InlineKeyboards
{
    public class BanKeyboard : InlineKeyboard
    {
        public override KeyboardIdentifier[] ID { get; set; } = new KeyboardIdentifier[] { KeyboardIdentifier.banCommandYes, KeyboardIdentifier.banCommandNo };

        public override async Task Execute(CallbackQuery e, int result, ITelegramBotClient client)
        {
            var keyboardMessage = services.GetKeyboardMessage(e.Message.Chat.Id, e.Message);
            CommandHandler.keyboardMessages.Remove(keyboardMessage);
            var userMessage = keyboardMessage.messageFromuser;
            if (e.From.Id == userMessage.From.Id)
            {
                if(result == 0)
                {
                    try
                    {
                        await client.EditMessageReplyMarkupAsync(e.Message.Chat.Id, e.Message.MessageId);
                        await client.KickChatMemberAsync(userMessage.ReplyToMessage.Chat.Id, userMessage.ReplyToMessage.From.Id);
                    }
                    catch
                    {
                        await client.SendTextMessageAsync(e.Message.Chat.Id, "Произошла ошибка, возможно бот не является администратором.");

                        await client.AnswerCallbackQueryAsync(
                        callbackQueryId: e.Id,
                        text: $"Ошибка."
                        );
                        return;
                    }

                    await client.SendTextMessageAsync(e.Message.Chat.Id, $"Пользователь {userMessage.ReplyToMessage.From.FirstName ?? ""} {userMessage.ReplyToMessage.From.LastName ?? ""} был изгнан из беседы.");
                    await client.AnswerCallbackQueryAsync(
                        callbackQueryId: e.Id,
                        text: $"Вы заблокировали пользователя."
                    );
                }
                else
                {
                    await client.AnswerCallbackQueryAsync(
                        callbackQueryId: e.Id,
                        text: $"Вы отказались блокировать пользователя."
                    );
                    await client.EditMessageReplyMarkupAsync(e.Message.Chat.Id, e.Message.MessageId);
                    await client.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
                }
            }
            else
            {
                await client.AnswerCallbackQueryAsync(
                        callbackQueryId: e.Id,
                        text: $"Вы не можете нажимать эти кнопки."
                    );
            }
        }
    }
}
