namespace SaYStalkPlusPlus.src
{
    public interface ICommand
    {
        static abstract public CommandResult Execute(string[] args);
    }
}