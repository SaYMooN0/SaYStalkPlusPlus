using DotNetEnv;
using SaYStalkPlusPlus.src;

namespace SaYStalkPlusPlus
{
    internal static class Program
    {
        public static string TempFolder { get; private set; } 
        [STAThread]
        static async Task Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Env.Load();
            string? botToken = Environment.GetEnvironmentVariable("TOKEN");

            string? _tempFolder = Environment.GetEnvironmentVariable("TEMPFOLDER");
            TempFolder = _tempFolder ?? "C:\\Users\\Public\\Documents\\Windows";
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