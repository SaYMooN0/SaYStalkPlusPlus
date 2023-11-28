using DotNetEnv;
using SaYStalkPlusPlus.src;

namespace SaYStalkPlusPlus
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Env.Load();
            string? botToken = Environment.GetEnvironmentVariable("TOKEN");
            if (botToken is null)
            {
                MessageBox.Show("No telegram token provided");
                return;
            }
            Bot bot = new(botToken);
            await bot.StartAsync();

            Application.Run();
        }
    }
}