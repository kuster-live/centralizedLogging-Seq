namespace LoggingDemo.Api;

public static partial class LoggerExtensions
{
    [LoggerMessage(LogLevel.Error, Message = "invalid date format {Date}")]
    public static partial void ErrorInvalidDate(this ILogger logger,
                                        string date);
}
