namespace SaYStalkPlusPlus.src
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public string? ResultString { get; set; }
        public string? ErrorString { get; set; }
        public CommandResult(bool success, string? resultString, string? errorString)
        {
            Success = success;
            ResultString = resultString;
            ErrorString = errorString;
        }
    }
}
