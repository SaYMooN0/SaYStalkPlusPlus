namespace SaYStalkPlusPlus.src
{
    public interface ICommand
    {
        public CommandResult Execute(string[] args);
    }
}