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
        Int32.TryParse(commandInput, out daySelect);
        
        // if in valid range
        if (daySelect is > 0 and < 26)
        {
            // execute command
            switch (daySelect)
            {
                 case 1:
                     AocDay1.CalorieCounting();
                     break;
                 
                 case 2:
                     AocDay2.RockPaperScissors();
                     break;
                 
                 case 3:
                     AocDay3.RuckSackReorganization();
                     break;
                 
                 case 4:
                     AocDay4.CampCleanup();
                     break;
                 
                 case 5:
                     AocDay5.SupplyStacks();
                     break;
                 
                 case 6:
                     AocDay6.TuningTrouble();
                     break;
                 
                 case 7:
                     AocDay7.LowDeviceSpace();
                     break;
                 
                 case 8:
                     AocDay8.TreetopTreeHouse();
                     break;
                 
                 case 9:
                     AocDay9.RopeBridge(1);
                     AocDay9.RopeBridge(2);
                     break;
                 
                 case 10:
                     AocDay10.CathodeRayTube();
                     break;
                 
                 case 11:
                     AocDay11.MonkeyInTheMiddle();
                     break;
                 
                 default:
                     break;
            }   
        }
        // else display error
        else
        {
            Console.WriteLine("The input did not match valid parameters");
        }
    }
}