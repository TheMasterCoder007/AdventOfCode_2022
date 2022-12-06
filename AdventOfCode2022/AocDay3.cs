using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022;

public static class AocDay3
{
    
    public static void RuckSackReorganization()
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input3.txt");
        string[] inputSplit = input.Split("\r\n");

        // PART 1
        // parse each line to find the sum of the priorities of the misplaced items
        List<int> items = new List<int>();
        foreach (string ruckSack in inputSplit)
        {
            int splitLength = ruckSack.Length / 2;

            // compare first half of list with second half to find total priorities of the misplaced items (part 1)
            for (int i = 0; i < splitLength; i++)
            {
                bool itemFound = false;
                for (int x = splitLength; x < ruckSack.Length; x++)
                {
                    if (ruckSack[i] != ruckSack[x]) continue;
                    items.Add(CalculateMisplacedItemPriority(ruckSack[i]));
                    itemFound = true;
                    break;
                }

                if (itemFound)
                    break;
            }
        }
        
        // PART 2
        // parse the rucksacks of each group to find the sum of the priorities of the badge numbers in every group
        List<int> badges = new List<int>();
        for (int group = 0; group <= (inputSplit.Length - 3); group += 3)
        {
            bool badgeFound = false;
            
            for (int member1Item = 0; member1Item < inputSplit[group].Length; member1Item++)
            {
                // compare item from member1 to items from member2
                for (int member2Item = 0; member2Item < inputSplit[group + 1].Length; member2Item++)
                {
                    // if item from member1 matches item from member2
                    if (inputSplit[group][member1Item] == inputSplit[group + 1][member2Item])
                    {
                        // check to see if member3 also has the same item
                        for (int member3Item = 0; member3Item < inputSplit[group + 2].Length; member3Item++)
                        {
                            // all three members have the same item
                            if (inputSplit[group + 1][member2Item] == inputSplit[group + 2][member3Item])
                            {
                                badges.Add(CalculateMisplacedItemPriority(inputSplit[group][member1Item]));
                                badgeFound = true;
                                break;
                            }
                        }
                    }
                    if (badgeFound)
                        break;
                }
                if (badgeFound)
                    break;
            }
        }
        
        // write answers to console
        Console.WriteLine("After checking the list, the total priorities of the misplaced items is " + items.Sum());
        Console.WriteLine("The total priorities of the badges for each group is found to be " + badges.Sum());
    }

    private static int CalculateMisplacedItemPriority(char item)
    {
        // initialize variables
        int lowerCaseOffset = (int)'a' - 1;
        int upperCaseOffset = 38;
        
        // check what range (lower case or upper case) the item is in and return the priority
        if ((int)item >= (int)'a')
            return ((int)item - lowerCaseOffset);
        else
            return ((int)item - upperCaseOffset);
    }
}