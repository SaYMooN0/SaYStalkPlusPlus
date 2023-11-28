using System.Diagnostics;

namespace SaYStalkPlusPlus.src.Commands
{
    class KillSaYStalkPlusPlus
    {
        public static CommandResult Execute(string?[] args)
        {
            var task = KillSSPP();
  
            return new CommandResult(true, "Trying to kill SaYStalkPlusPlus", null);
        }
        async private static Task<CommandResult> KillSSPP()
        {
            var processes = Process.GetProcessesByName("SaYStalkPlusPlus");

            if (processes.Length == 0)
                return new CommandResult(false, null, "Process SaYStalkPlusPlus not found :|");

            foreach (var process in processes)
            {
                try
                {
                    await Task.Delay(3500);
                    process.Kill();
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    return new CommandResult(false, null, "Insufficient privileges to terminate the SaYStalkPlusPlus process");
                }
                catch (Exception ex)
                {
                    return new CommandResult(false, null, $"Error while terminating the process: {ex.Message}");
                }
            }

            return new CommandResult(true, "SaYStalkPlusPlus process has been successfully terminated", null);
        }
    }
}
