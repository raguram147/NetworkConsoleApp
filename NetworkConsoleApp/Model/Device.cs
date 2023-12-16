class Device
{
    public DeviceType Type { get; }
    public string Name { get; }
    public int Strength { get; set; }
    public List<Device> ConnectedDevices { get; }

    public Device(DeviceType type, string name, int strength)
    {
        Type = type;
        Name = name;
        Strength = strength;
        ConnectedDevices = new List<Device>();
    }

    public void ConnectToDevice(Device otherDevice)
    {
        if (this != otherDevice && !ConnectedDevices.Contains(otherDevice))
        {
            ConnectedDevices.Add(otherDevice);
        }
    }
}
