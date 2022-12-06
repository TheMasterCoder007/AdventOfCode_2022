using System;

namespace AdventOfCode2022;

public static class AocDay6
{
    public static void TuningTrouble()
    {
        // pull input into string
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input6.txt");
        
        // check each packet until data is found
        bool packetMarkerFound = false;
        for (int packet = 0; packet < input.Length; packet++)
        {
            // find the position of the start of packet marker (part 1)
            if (PacketCompare(input, packet, 4) && !packetMarkerFound)
            {
                int packetsProcessed = packet + 4;
                Console.WriteLine("The number of characters processed before the start of packet marker " +
                                  "is detected is " + packetsProcessed);

                packetMarkerFound = true;
            }
            
            // find the position of the start of message marker (part 2)
            if (PacketCompare(input, packet, 14))
            {
                int packetsProcessed = packet + 14;
                Console.WriteLine("The number of characters processed before the start of message marker " +
                                  "is detected is " + packetsProcessed);

                break;
            }
        }
    }

    private static bool PacketCompare(string datastream, int packetNum, int numOfPackets)
    {
        // grab substring from data stream
        string packetCheck = datastream.Substring(packetNum, numOfPackets);

        foreach (char packet in packetCheck)
        {
            // found repeat, not a match
            if (packetCheck.IndexOf(packet) != packetCheck.LastIndexOf(packet))
            {
                return false;
            }
        }

        // no repeat found, found match
        return true;
    }
}