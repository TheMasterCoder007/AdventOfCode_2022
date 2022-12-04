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
            
            default:
                break;
        }
    }
}