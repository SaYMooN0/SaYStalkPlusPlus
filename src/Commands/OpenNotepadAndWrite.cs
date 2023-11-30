using System.Diagnostics;

namespace SaYStalkPlusPlus.src.Commands
{
    class OpenNotepadAndWrite : ICommand
    {
        static public CommandResult Execute(string?[] args)
        {
            string filePath = Path.Combine(Program.TempFolder, DateTime.Now.ToString("u").Replace(':', '-'));
            string? textToWrite;

            try
            {
                if (!Directory.Exists(Program.TempFolder)) Directory.CreateDirectory(Program.TempFolder);
                textToWrite = args[0];
                if (textToWrite is null) throw new IndexOutOfRangeException();

                File.WriteAllText(filePath, textToWrite);
                Process.Start("notepad.exe", filePath);

            }
            catch (IndexOutOfRangeException)
            {
                return new CommandResult(false, null, "Given an incorrect text to write");
            }
            catch (Exception ex)
            {
                return new CommandResult(false, null, $"Error while writing into notepad: {ex.Message}");
            }

            return new CommandResult(true, $"Notepad with text '{textToWrite}' successfully opened", null);
        }
    }
}
