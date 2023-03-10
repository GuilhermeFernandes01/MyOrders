using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Utils.Logger;

public class LoggerBuilder<T>
{
    private static LoggerBuilder<T> _instance;
    private readonly Mock<ILogger<T>> _logger;

    private LoggerBuilder()
    {
        if (_logger == null)
        {
            _logger = new Mock<ILogger<T>>();
        }
    }

    public static LoggerBuilder<T> Instance()
    {
        _instance = new LoggerBuilder<T>();
        return _instance;
    }

    public Mock<ILogger<T>> Build()
    {
        return _logger;
    }
}