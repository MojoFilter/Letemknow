using DeviceDetectorNET;

namespace Letemknow.Server;

public interface IDeviceDetectorFactory
{
    public DeviceDetector Build(string useragent);
}

internal sealed class DeviceDetectorFactory : IDeviceDetectorFactory
{
    public DeviceDetector Build(string useragent) => new DeviceDetector(useragent);
}
