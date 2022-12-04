using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public static class AocDay1
{
    public static void CalorieCounting()
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input1.txt");
        string[] inputSplit = input.Split("\r\n\r\n");
        
        // parse the strings, sum up the calories, and find the elf with the highest count
        int elfCaloriesPos1 = 0;
        int elfCaloriesPos2 = 0;
        int elfCaloriesPos3 = 0;
        
        foreach (string tempString in inputSplit)
        {
            // move all values in block to list
            List<int> tempNumbers = new List<int>();
            foreach (Match m in Regex.Matches(tempString, @"\d+"))
            {
                tempNumbers.Add(Convert.ToInt32(m.Value));
            }

            // sum up all the calories for each elf
            int totalCalories = 0;
            foreach (int sumNumbers in tempNumbers)
            {
                totalCalories += sumNumbers;
            }
            
            // check if the current elf has more calories than the previous highest elves
            if (totalCalories > elfCaloriesPos1)
            {
                if (elfCaloriesPos1 > elfCaloriesPos2)
                    elfCaloriesPos2 = elfCaloriesPos1;
                
                elfCaloriesPos1 = totalCalories;
            }
            else if (totalCalories > elfCaloriesPos2)
            {
                if (elfCaloriesPos2 > elfCaloriesPos3)
                    elfCaloriesPos3 = elfCaloriesPos2;
                
                elfCaloriesPos2 = totalCalories;
            }
            else if (totalCalories > elfCaloriesPos3)
            {
                elfCaloriesPos3 = totalCalories;
            }
        }
        
        // sum up top three elves
        int sumTopThreeElves = elfCaloriesPos1 + elfCaloriesPos2 + elfCaloriesPos3;
        
        // print answer to console
        Console.WriteLine("The highest amount of calories carried by a single elf is " + elfCaloriesPos1);
        Console.WriteLine("The highest amount of calories carried by the top three elves is " + sumTopThreeElves);
    }
}