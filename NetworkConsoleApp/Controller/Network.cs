class Network
{
    private List<Device> devices;
    private List<Connection> connections;

    public Network()
    {
        devices = new List<Device>();
        connections = new List<Connection>();
    }

    public void AddDevice(DeviceType deviceType, string deviceName, int strength)
    {
        Device device = new Device(deviceType, deviceName, strength);
        devices.Add(device);
    }

    public bool DeviceExists(string deviceName)
    {
        return devices.Any(d => d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));
    }

    public void Connect(string sourceDeviceName, string[] destinationDeviceNames)
{
    Device sourceDevice = devices.FirstOrDefault(d => d.Name.Equals(sourceDeviceName, StringComparison.OrdinalIgnoreCase));

    if (sourceDevice != null)
    {
        foreach (string destinationDeviceName in destinationDeviceNames)
        {
            Device destinationDevice = devices.FirstOrDefault(d => d.Name.Equals(destinationDeviceName, StringComparison.OrdinalIgnoreCase));

            if (destinationDevice != null && sourceDevice != destinationDevice)
            {
                // Check if devices are already connected.
                if (!AreDevicesConnected(sourceDevice, destinationDevice))
                {
                    // Establish a connection between sourceDevice and destinationDevice.
                    sourceDevice.ConnectToDevice(destinationDevice);
                    destinationDevice.ConnectToDevice(sourceDevice);

                    Console.WriteLine($"Connected {sourceDevice.Name} to {destinationDevice.Name}.");
                }
                else
                {
                    Console.WriteLine($"{sourceDevice.Name} is already connected to {destinationDevice.Name}.");
                }
            }
            else if (destinationDevice == sourceDevice)
            {
                Console.WriteLine($"Cannot connect {sourceDevice.Name} to itself.");
            }
            else
            {
                Console.WriteLine($"Device {destinationDeviceName} not found.");
            }
        }
    }
    else
    {
        Console.WriteLine($"Device {sourceDeviceName} not found.");
    }
}

private bool AreDevicesConnected(Device device1, Device device2)
{
    // Check if device1 is already connected to device2.
    return device1.ConnectedDevices.Contains(device2) || device2.ConnectedDevices.Contains(device1);
}

// public void DisplayRoute(string sourceDeviceName, string destinationDeviceName)
// {
//     Device sourceDevice = devices.FirstOrDefault(d => d.Name.Equals(sourceDeviceName, StringComparison.OrdinalIgnoreCase));
//     Device destinationDevice = devices.FirstOrDefault(d => d.Name.Equals(destinationDeviceName, StringComparison.OrdinalIgnoreCase));

//     if (sourceDevice != null && destinationDevice != null)
//     {
//         if (sourceDevice.Type == DeviceType.Repeater || destinationDevice.Type == DeviceType.Repeater)
//         {
//             Console.WriteLine("Error: Source or destination device cannot be a repeater.");
//             return;
//         }

//         Console.WriteLine($"Route from {sourceDevice.Name} to {destinationDevice.Name}:");

//         List<Device> route = FindRouteWithStrength(sourceDevice, destinationDevice, new List<Device>(), int.MaxValue);

//         if (route.Count > 0)
//         {
//             Console.Write(route[0].Name);

//             for (int i = 1; i < route.Count; i++)
//             {
//                 Device currentDevice = route[i - 1];
//                 Device nextDevice = route[i];

//                 int strength = GetConnectionStrength(currentDevice, nextDevice);

//                 Console.Write($" -> {nextDevice.Name} (Strength: {strength})");
//             }

//             Console.WriteLine();
//         }
//         else
//         {
//             Console.WriteLine("Error: No route found.");
//         }
//     }
//     else
//     {
//         Console.WriteLine($"Invalid device names. Check device names and try again.");
//     }
// }

public void DisplayRoute(string sourceDeviceName, string destinationDeviceName)
{
    Device sourceDevice = devices.FirstOrDefault(d => d.Name.Equals(sourceDeviceName, StringComparison.OrdinalIgnoreCase));
    Device destinationDevice = devices.FirstOrDefault(d => d.Name.Equals(destinationDeviceName, StringComparison.OrdinalIgnoreCase));

    if (sourceDevice != null && destinationDevice != null)
    {
        if (sourceDevice.Type == DeviceType.Repeater || destinationDevice.Type == DeviceType.Repeater)
        {
            Console.WriteLine("Error: Source or destination device cannot be a repeater.");
            return;
        }

        Console.Write($"Route from {sourceDevice.Name} to {destinationDevice.Name}: ");

        List<Device> route = FindRouteWithStrength(sourceDevice, destinationDevice, new List<Device>(), int.MaxValue);

        if (route.Count > 0)
        {
            Console.Write(route[0].Name);

            for (int i = 1; i < route.Count; i++)
            {
                Device currentDevice = route[i - 1];
                Device nextDevice = route[i];

                int strength = GetConnectionStrength(currentDevice, nextDevice);

                Console.Write($" -> {nextDevice.Name} (Strength: {strength})");
            }

            Console.WriteLine(); // Ensure a newline after the route is printed.
        }
        else
        {
            Console.WriteLine("Error: No route found.");
        }
    }
    else
    {
        Console.WriteLine($"Invalid device names. Check device names and try again.");
    }
}

private int GetConnectionStrength(Device sourceDevice, Device destinationDevice)
{
    // Find the connection strength between two devices.
    Connection connection = connections.FirstOrDefault(c =>
        (c.SourceDevice == sourceDevice && c.DestinationDevice == destinationDevice) ||
        (c.SourceDevice == destinationDevice && c.DestinationDevice == sourceDevice));

    return connection?.Strength ?? 0;
}

private List<Device> FindRouteWithStrength(Device currentDevice, Device destinationDevice, List<Device> currentPath, int maxStrength)
{
    List<Device> route = new List<Device>();

    if (currentDevice == destinationDevice)
    {
        route.Add(currentDevice);
        return route;
    }

    foreach (var connectedDevice in currentDevice.ConnectedDevices)
    {
        if (!currentPath.Contains(connectedDevice))
        {
            List<Device> newPath = new List<Device>(currentPath);
            newPath.Add(currentDevice);

            List<Device> subRoute = FindRouteWithStrength(connectedDevice, destinationDevice, newPath, maxStrength);

            if (subRoute.Count > 0 && GetTotalStrength(subRoute) <= maxStrength)
            {
                route.AddRange(subRoute);
                break;
            }
        }
    }

    return route;
}

private int GetTotalStrength(List<Device> route)
{
    // Calculate the total strength of a route.
    int totalStrength = 0;

    for (int i = 0; i < route.Count - 1; i++)
    {
        Device currentDevice = route[i];
        Device nextDevice = route[i + 1];

        totalStrength += GetConnectionStrength(currentDevice, nextDevice);
    }

    return totalStrength;
}
    public void SetDeviceStrength(string deviceName, int strength)
    {
        Device device = devices.FirstOrDefault(d => d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));

        if (device != null)
        {
            if (device.Type == DeviceType.Repeater)
            {
                Console.WriteLine("Error: Strength cannot be defined for a repeater.");
            }
            else
            {
                device.Strength = Math.Max(0, strength);
                Console.WriteLine($"Strength set for {device.Name}: {device.Strength}");
            }
        }
        else
        {
            Console.WriteLine($"Device {deviceName} not found.");
        }
    }

    private List<Device> FindRoute(Device currentDevice, Device destinationDevice, List<Device> currentPath)
    {
        List<Device> route = new List<Device>();

        if (currentDevice == destinationDevice)
        {
            route.Add(currentDevice);
            return route;
        }

        foreach (var connectedDevice in currentDevice.ConnectedDevices)
        {
            if (!currentPath.Contains(connectedDevice))
            {
                List<Device> newPath = new List<Device>(currentPath);
                newPath.Add(currentDevice);

                List<Device> subRoute = FindRoute(connectedDevice, destinationDevice, newPath);

                if (subRoute.Count > 0)
                {
                    route.AddRange(subRoute);
                    break;
                }
            }
        }

        return route;
    }
}


