using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public static class AocDay4
{
    private static readonly int elf1Min = 0;
    private static readonly int elf1Max = 1;
    private static readonly int elf2Min = 2;
    private static readonly int elf2Max = 3;
    
    public static void CampCleanup()
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input4.txt");
        string[] inputSplit = input.Split("\r\n");
        
        // check tasks for each pair
        int numberOfPairsFullyContained = 0;
        int numberOfPairsOverlapped = 0;
        foreach (string pair in inputSplit)
        {
            // parse range numbers to List
            List<int> assignments = new List<int>();
            foreach (Match m in Regex.Matches(pair, @"\d+"))
            {
                assignments.Add(Convert.ToInt32(m.Value));
            }

            // check to see if one elves assignment is fully contained in another (part 1)
            if ( (assignments[elf1Min] <= assignments[elf2Min] && assignments[elf1Max] >= assignments[elf2Max]) 
                || (assignments[elf2Min] <= assignments[elf1Min] && assignments[elf2Max] >= assignments[elf1Max]) )
            {
                numberOfPairsFullyContained++;
            }
            
            // find all overlapping assignments
            if ((assignments[elf1Min] >= assignments[elf2Min] && assignments[elf1Min] <= assignments[elf2Max])
                || (assignments[elf2Min] >= assignments[elf1Min] && assignments[elf2Min] <= assignments[elf1Max]))
            {
                numberOfPairsOverlapped++;
            }
        }
        // print answers to console
        Console.WriteLine("The number of fully overlapped assignments pairs is " + numberOfPairsFullyContained);
        Console.WriteLine("The complete number of overlapped assignments is " + numberOfPairsOverlapped);
    }
}