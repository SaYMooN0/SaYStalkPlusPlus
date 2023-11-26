using System.Diagnostics;

namespace SaYStalkPlusPlus.src.Commands
{
    class GetActiveProcesses : ICommand
    {
        static public CommandResult Execute(string[] args)
        {
            Process[] activeProcesses = Process.GetProcesses().Where(p => p.MainWindowTitle.Length > 0).ToArray();
            string stringToReturn = "Active processes: \n";
            foreach (Process process in activeProcesses)
            {
                stringToReturn += $"{process.ProcessName} id: {process.Id} ({process.MainWindowTitle})\n";
            }
            return new CommandResult(true, stringToReturn, null);
        }
    }
}


