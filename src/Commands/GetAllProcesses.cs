using System.Diagnostics;
namespace SaYStalkPlusPlus.src.Commands
{
    class GetAllProcesses : ICommand
    {

        static public CommandResult Execute(string[] args)
        {
            Process[] allProcesses = Process.GetProcesses();
            string stringToReturn = "All processes: \n";
            foreach (Process process in allProcesses)
            {
                stringToReturn += $"{process.ProcessName} id: {process.Id}\n";
            }
            return new CommandResult(true, stringToReturn, null);

        }
    }
}