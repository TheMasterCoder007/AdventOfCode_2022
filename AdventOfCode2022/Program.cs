using System;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main(string[] args)
    {
        // copy command list text file into string for display on console
        string commandListHeader = System.IO.File.ReadAllText(@"..\..\..\inputs\ListOfCommands.txt");
        
        Console.WriteLine(commandListHeader);
        
        // receive and assess command
        int daySelect = 0;
        string? commandInput = Console.ReadLine();
        if (commandInput != null)
            daySelect = int.Parse(commandInput);
        
        // execute command
        switch (daySelect)
        {
            case 1:
                AocDay1.CalorieCounting();
                Console.Read();
                break;
            
            case 2:
                AocDay2.RockPaperScissors();
                Console.Read();
                break;
            
            case 3:
                AocDay3.RuckSackReorganization();
                Console.Read();
                break;
            
            case 4:
                AocDay4.CampCleanup();
                Console.Read();
                break;
            
            case 5:
                AocDay5.SupplyStacks();
                Console.Read();
                break;
            
            case 6:
                AocDay6.TuningTrouble();
                Console.Read();
                break;
            
            case 7:
                AocDay7.LowDeviceSpace();
                Console.Read();
                break;
            
            case 8:
                AocDay8.TreetopTreeHouse();
                Console.Read();
                break;
            
            case 9:
                AocDay9.RopeBridge(1);
                AocDay9.RopeBridge(2);
                Console.Read();
                break;
        }
    }
}