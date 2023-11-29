namespace SaYStalkPlusPlus.src
{
    public class CommandResult
    {
        public bool Success { get; private set; }
        public string? ResultString { get; private set; }
        public string? ErrorString { get; private set; }
        public CommandResultType Type { get; private set; } = CommandResultType.Text;
        public CommandResult(bool success, string? resultString, string? errorString)
        {
            Success = success;
            ResultString = resultString;
            ErrorString = errorString;
        }
        public CommandResult(bool success, string? resultString, string? errorString, CommandResultType commandResultType)
        {
            Success = success;
            ResultString = resultString;
            ErrorString = errorString;
            Type = commandResultType;
        }
    }
    public enum CommandResultType { Text, Photo }
}
