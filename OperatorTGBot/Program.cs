using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace OperatorTGBot
{
    internal class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("1699624599:AAEHWmiMRmol4nYlpNibC7jQ-PCci1ghtbE");

        public static void ShowJson(Update update)
        {
            Console.WriteLine($"Update ID = {update.Id} | Message ID = {update.Message.MessageId} | User ID = {update.Message.Chat.Id}\n" +
            $"Имя и фамилия = {update.Message.Chat.FirstName} {update.Message.Chat.LastName} | Username = {update.Message.Chat.Username} | " +
            $"Date = {update.Message.Date}\nСообщение:\n{update.Message.Text}\n\n");
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            ShowJson(update);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {

                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {

                    await botClient.SendTextMessageAsync(message.Chat, $"Приветствуем вас, {update.Message.Chat.FirstName} {update.Message.Chat.LastName}");
                    return;
                }
                if (message.Text.ToLower() == "хуйло")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Сам ты хуйло, чепушила ебаная");
                    return;
                }
                if (message.Text.ToLower() == "как тебя зовут?")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Я оператор Иннокентий, а тебя как звать?");
                    return;
                }

                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\tЗапущен бот \"" + bot.GetMeAsync().Result.FirstName + "\"\n\n");

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
