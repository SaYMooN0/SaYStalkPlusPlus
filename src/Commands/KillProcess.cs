using System.Diagnostics;

namespace SaYStalkPlusPlus.src.Commands
{
    class KillProcess : ICommand
    {
        static public CommandResult Execute(string[] args)
        {
            if (args.Length==0 || !int.TryParse(args[0], out int processId))
                return new CommandResult(false, null, "Given an incorrect ID");

            try
            {
                Process process = Process.GetProcessById(processId);
                process.Kill();
                return new CommandResult(true, $"The process with ID {processId} has been successfully terminated", null);
            }
            catch (ArgumentException)
            {
                return new CommandResult(false, null, $"No process with ID {processId} is currently running");
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return new CommandResult(false, null, "Insufficient privileges to terminate this process");
            }
            catch(Exception ex)
            {
                return new CommandResult(false, null, ex.Message);
            }
        }
    }
}
