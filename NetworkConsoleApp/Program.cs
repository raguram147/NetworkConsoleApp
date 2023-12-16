using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Network network = new Network();

        while (true)
        {
            Console.Write("Enter command: ");
            string input = Console.ReadLine();

            string[] tokens = input.Split(' ');

            if (tokens.Length > 0)
            {
                string command = tokens[0].ToUpper();

                switch (command)
                {
                    case "ADD":
                        if (tokens.Length >= 3)
                        {
                            string deviceType = tokens[1];
                            string deviceName = tokens[2];

                            if (Enum.TryParse(deviceType, true, out DeviceType type) &&
                                !string.IsNullOrWhiteSpace(deviceName) &&
                                !network.DeviceExists(deviceName))
                            {
                                int defaultStrength = (type == DeviceType.Repeater) ? 0 : 5;
                                network.AddDevice(type, deviceName, defaultStrength);
                                Console.WriteLine($"Device {deviceName} of type {type} added to the network with default strength {defaultStrength}.");
                            }
                            else if(network.DeviceExists(deviceName)){
                                Console.WriteLine($"Device {deviceName} is already added to the network.");
                            }
                            else
                            {
                                Console.WriteLine("Error: Invalid ADD command. Usage: ADD <DEVICE_TYPE> <DEVICE_NAME>");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ADD command. Usage: ADD <DEVICE_TYPE> <DEVICE_NAME>");
                        }
                        break;

                    case "CONNECT":
                        if (tokens.Length >= 3)
                        {
                            string sourceDeviceName = tokens[1];
                            string[] destinationDeviceNames = tokens.Skip(2).ToArray();

                            network.Connect(sourceDeviceName, destinationDeviceNames);
                        }
                        else
                        {
                            Console.WriteLine("Invalid CONNECT command. Usage: CONNECT <SOURCE_DEVICE_NAME> <DESTINATION_DEVICE_NAMES>");
                        }
                        break;

                    case "INFO_ROUTE":
                        if (tokens.Length == 3)
                        {
                            string sourceDeviceName = tokens[1];
                            string destinationDeviceName = tokens[2];

                            network.DisplayRoute(sourceDeviceName, destinationDeviceName);
                        }
                        else
                        {
                            Console.WriteLine("Invalid INFO_ROUTE command. Usage: INFO_ROUTE <SRC_DEVICE_NAME> <DEST_DEVICE_NAME>");
                        }
                        break;

                    case "SET_DEVICE_STRENGTH":
                        if (tokens.Length == 3)
                        {
                            string deviceName = tokens[1];
                            if (int.TryParse(tokens[2], out int strength))
                            {
                                network.SetDeviceStrength(deviceName, strength);
                            }
                            else
                            {
                                Console.WriteLine("Invalid SET_DEVICE_STRENGTH command. Usage: SET_DEVICE_STRENGTH <DEVICE_NAME> <#STRENGTH>");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid SET_DEVICE_STRENGTH command. Usage: SET_DEVICE_STRENGTH <DEVICE_NAME> <#STRENGTH>");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }
    }
}


