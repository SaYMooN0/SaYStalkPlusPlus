using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

namespace SaYStalkPlusPlus.src
{
    internal class Bot
    {
        private string _token;
        private CancellationTokenSource _cancellationToken;
        public Bot(string tokenString)
        {
            _token = tokenString;
            _cancellationToken = new CancellationTokenSource();
        }
        public async Task StartAsync()
        {
            var botClient = new TelegramBotClient(_token);
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: _cancellationToken.Token
            );

        }
        private void StopBot() { _cancellationToken.Cancel(); }
        private static async Task SendMessage(ITelegramBotClient botClient, long chatId, string messageText)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: messageText
            );
        }
        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            long chatId = message.Chat.Id;

            if (!messageText.StartsWith('/'))
            {
                await SendMessage(botClient, chatId, "ur msg: " + messageText);
                return;
            }
            await HandleCommand(botClient, cancellationToken, messageText, chatId);
        }
        private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        private static async Task HandleCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, string commandText, long chatId)
        {
            await SendMessage(botClient, chatId, "ur cmd: " + commandText);
        }
    }
}