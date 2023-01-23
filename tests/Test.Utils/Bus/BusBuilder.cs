using Microsoft.Extensions.Logging;
using Moq;
using Test.Utils.Logger;
using MassTransit;

namespace Test.Utils.Bus;

public class BusBuilder
{
    private static BusBuilder _instance;
    private readonly Mock<IBus> _bus;

    private BusBuilder()
    {
        if (_bus == null)
        {
            _bus = new Mock<IBus>();
        }
    }

    public static BusBuilder Instance()
    {
        _instance = new BusBuilder();
        return _instance;
    }

    public IBus Build()
    {
        return _bus.Object;
    }
}