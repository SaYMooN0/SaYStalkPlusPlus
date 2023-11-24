using DotNetEnv;
using SaYStalkPlusPlus.src;

namespace SaYStalkPlusPlus
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Env.Load();
            string? botToken = Environment.GetEnvironmentVariable("TOKEN");
            if (botToken is null) {
                return;
            }
            Bot bot = new(botToken);
            bot.Start();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

         
         
        }
    }
}