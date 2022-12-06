using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public static class AocDay5
{
    public static void SupplyStacks()
    {
        // pull input into string and then split the string at empty line (between stacks and commands)
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input5.txt");
        string[] inputSplit = input.Split("\r\n\r\n");

        // pull starting stacks from string
        string[] initialStacks = inputSplit[0].Split("\r\n");
        
        //create array for storing the crate data
        List<char> row = new List<char>();
        List<char> column = new List<char>();
        foreach (string r in initialStacks)
        {
            for (int c = 1; c < (r.Length - 5); c += 4)
            {
                row.Add(r[c]);
            }
        }
        Console.WriteLine(string.Join(",", row));
    }
}