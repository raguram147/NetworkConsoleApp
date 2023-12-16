class Connection
{
    public Device SourceDevice { get; }
    public Device DestinationDevice { get; }
    public int Strength { get; } // Added Strength property.

    public Connection(Device sourceDevice, Device destinationDevice, int strength)
    {
        SourceDevice = sourceDevice;
        DestinationDevice = destinationDevice;
        Strength = strength;
    }
}