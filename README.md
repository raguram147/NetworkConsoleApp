The network will consist of various devices connected to each other (wired/wireless). There will be two types of
devices :- computers and repeaters.
Connections (wired/wireless) between devices can transfer information both ways.
When information must be transferred between two devices and if there is no direct connection between two
devices, it must be transferred from device to device until it reaches the destination.
The message sent by a device has a 'strength', limiting it to travel only a specific distance. The strength of
a message will reduce by 1 unit after every device it reaches from the source computer.
If the message being sent encounters a repeater, it's strength will be amplified by double its current
strength.

Examples of networks are given below. Computers are represented with the prefix 'C' and repeaters are represented with

the prefix 'R'.

C1
/ \
C2 C3 — R1 — C4 — C5
|
C6
|
C7

C1
/ \
C2 R1 — C3
| |
R2 — C4 — C5

These are just examples. The network topography will be different.

You are expected to develop a console application that allows you to do the following operations:-
Add a device to a network.
Add connections between two devices.
To print the route that must be taken if information is to be passed between two devices.
The above-mentioned operations should be carried out using the following commands:-
ADD <DEVICE_TYPE> <DEVICE_NAME>

Every device must have a unique name (at least one character).
The device type must be an enumeration of either 'computer' or 'repeater'.
CONNECT <DEVICE_NAME> <DEVICE_LIST>
A device cannot be connected to itself.
A device can be connected to any number of devices.
A device does not necessarily need to be connected to other devices

INFO_ROUTE <SRC_DEVICE_NAME>

<DEST_DEVICE_NAME>
If no route is found between two devices, then an error message must be displayed.
The route for a device to itself should only have a source and destination which are both itself.
The source or the destination device cannot be a repeater.
SET_DEVICE_STRENGTH <DEVICE_NAME>

<#STRENGTH>
The strength defined for a device must be a number and it cannot be negative.
A strength cannot be defined for a repeater. It only doubles the strength of the incoming message.
If not defined, the default strength will be 5.
Appropriate validations should be applied and error messages should appear. All data must be stored in-memory.

(Don't use any filesystem or database)
Sample Input/Output

> ADD COMPUTER A1
Successfully added A1.
> ADD COMPUTER A2
Successfully added A2.
> ADD COMPUTER A3
Successfully added A3.
> ADD
Error: Invalid command syntax.
> ADD PHONE A1
Error: Invalid command syntax.
> ADD COMPUTER A1
Error: That name already exists.
> ADD COMPUTER A4
Successfully added A4.
> ADD COMPUTER A5
Successfully added A5.
> ADD COMPUTER A6
Successfully added A6.
> ADD REPEATER R1
Successfully added R1.
> SET_DEVICE_STRENGTH A1 HELLOWORLD
Error: Invalid command syntax.
> SET_DEVICE_STRENGTH A1 2
Successfully defined strength.
> CONNECT A1 A2
Successfully connected.
> CONNECT A1 A3
Successfully connected.
> CONNECT A1 A1

Error: Cannot connect device to itself.
> CONNECT A1 A2
Error: Devices are already connected.
> CONNECT A5 A4
Successfully connected.
> CONNECT R1 A2
Successfully connected.
> CONNECT R1 A5
Successfully connected.
> CONNECT A1
Error: Invalid command syntax.
> CONNECT
Error: Invalid command syntax.
> CONNECT A8 A1
Error: Node not found.
> CONNECT A2 A4
Successfully connected.
> INFO_ROUTE A1 A4
A1 -> A2 -> A4
> INFO_ROUTE A1 A5
A1 -> A2 -> R1 -> A5
> INFO_ROUTE A4 A3
A4 -> A2 -> A1 -> A3
> INFO_ROUTE A1 A1
A1 -> A1
> INFO_ROUTE A1 A6
Error: Route not found!
> INFO_ROUTE A2 R1
Error: Route cannot be calculated with a repeater.

> INFO_ROUTE A3
Error: Invalid command syntax.
> INFO_ROUTE
Error: Invalid command syntax.
> INFO_ROUTE A1 A10
Error: Node not found.
