using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public static class AocDay11
{
    /// <summary>
    /// <c>MonkeyInTheMiddle</c> calculates the two most active monkeys
    /// </summary>
    public static void MonkeyInTheMiddle()
    {
        // create new MonkeyStat list
        List<MonkeyStats> monkeyStats = new List<MonkeyStats>();
        
        //-----------------
        //      PART 1
        //-----------------
         
        // populate list
        monkeyStats = GetMonkeyStartingPositions();
        // calculate moves monkeys will make
        CalculateMonkeyBusinessP1(monkeyStats);
        // clear list
        monkeyStats.Clear();
        
        //-----------------
        //      PART 2
        //-----------------
        
        // populate list
        monkeyStats = GetMonkeyStartingPositions();
        // calculate moves monkeys will make
        CalculateMonkeyBusinessP2(monkeyStats);
        // clear list
        monkeyStats.Clear();
    }

    /// <summary>
    /// <c>GetMonkeyStartingPositions</c> creates a list of MonkeyStats based on input data (starting positions)
    /// </summary>
    /// <returns>A list of MonkeyStats</returns>
    private static List<MonkeyStats> GetMonkeyStartingPositions()
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input11.txt");
        string[] inputSplit = input.Split("\r\n\r\n");

        // gather monkey stats at start
        List<MonkeyStats> monkeyStats = new List<MonkeyStats>();
        foreach (string monkey in inputSplit)
        {
            // split string by lines
            string[] monkeyDetails = monkey.Split("\r\n");
            
            // gather held items
            List<long> items = new List<long>();
            foreach (Match match in Regex.Matches(monkeyDetails[1], @"\d+")) 
                items.Add(Convert.ToInt32(match.Value));
            // gather operation details
            Operations xOperator = new Operations();
            foreach (char ch in monkeyDetails[2])
            {
                if (ch is '*' or '+')
                {
                    xOperator.Operator = ch;
                    break;
                }
            }
            int.TryParse(Regex.Match(monkeyDetails[2], @"\d+").Value, out int tempMultiplier);
            xOperator.Multiplier = tempMultiplier;
            // gather test instructions
            int test = int.Parse(Regex.Match(monkeyDetails[3], @"\d+").Value);
            int testTrue = int.Parse(Regex.Match(monkeyDetails[4], @"\d+").Value);
            int testFalse = int.Parse(Regex.Match(monkeyDetails[5], @"\d+").Value);
            
            // add new MonkeyStat to List
            monkeyStats.Add(new MonkeyStats(items, xOperator, test, testTrue, testFalse));
        }

        return monkeyStats;
    }
    
    /// <summary>
    /// <c>CalculateMonkeyBusinessP1</c> calculates the total amount of "monkey business" for part 1
    /// </summary>
    /// <param name="monkeyList">List of MonkeyStats</param>
    private static void CalculateMonkeyBusinessP1(List<MonkeyStats> monkeyList)
    {
        // calculate 20 rounds of monkey business
        for (int i = 0; i < 20; i++)
        {
            // calculate complete round
            foreach (MonkeyStats monkey in monkeyList)
            {
                for (int item = 0; item < monkey.Items.Count; item++)
                {
                    // set multiplier - if 0, it multiplies by itself
                    long multiplier = monkey.Operation.Multiplier == 0
                        ? monkey.Items[item]
                        : monkey.Operation.Multiplier;
                    
                    // worry multiplier
                    if (monkey.Operation.Operator == '*')
                        monkey.Items[item] *= multiplier;
                    else if (monkey.Operation.Operator == '+')
                        monkey.Items[item] += multiplier;

                    // divide worry level and round to the nearest int
                    monkey.Items[item] /= 3;
                    
                    // if item worry level is divisible by the test value
                    if (monkey.Items[item] % monkey.Test == 0)
                        monkeyList[monkey.TestTrue].Items.Add(monkey.Items[item]);
                    // if item worry level is not divisible by the test value
                    else
                       monkeyList[monkey.TestFalse].Items.Add(monkey.Items[item]);
                    
                    // increase number of items inspected
                    monkey.TotalItemsInspected++;
                }
                // remove all items from list
                monkey.Items.Clear();
            }
        }
        
        // multiply the number of times the two most active monkeys inspect items
        long monkeyBusiness = monkeyList
            .Select(x => x.TotalItemsInspected)
            .OrderByDescending(x => x)
            .Take(2)
            .Aggregate((a, b) => a * b);
        
        // print answer to console
        Console.WriteLine("The level of monkey business after 20 rounds is " + monkeyBusiness);
    }
    
    /// <summary>
    /// <c>CalculateMonkeyBusinessP2</c> calculates the total amount of "monkey business" for part 2
    /// </summary>
    /// <param name="monkeyList">List of MonkeyStats</param>
        private static void CalculateMonkeyBusinessP2(List<MonkeyStats> monkeyList)
    {
        // handle stress level by multiplying all the test values and then use it in the following way:
        // multipliedStressLevel % stressRelief
        // This allows the value to be decreased without affecting its divisible test
        int stressRelief = 0;
        foreach (MonkeyStats monkey in monkeyList)
        {
            if (stressRelief == 0)
                stressRelief = monkey.Test;
            else
                stressRelief = stressRelief * monkey.Test;
        }
        
        // calculate 10000 rounds of monkey business
        for (int i = 0; i < 10000; i++)
        {
            // calculate complete round
            foreach (MonkeyStats monkey in monkeyList)
            {
                for (int item = 0; item < monkey.Items.Count; item++)
                {
                    // set multiplier - if 0, it multiplies by itself
                    long multiplier = monkey.Operation.Multiplier == 0
                        ? monkey.Items[item]
                        : monkey.Operation.Multiplier;
                    
                    // worry multiplier
                    if (monkey.Operation.Operator == '*')
                        monkey.Items[item] = (monkey.Items[item] * multiplier) % stressRelief;
                    else if (monkey.Operation.Operator == '+')
                        monkey.Items[item] = (monkey.Items[item] + multiplier) % stressRelief;

                    // if item worry level is divisible by the test value
                    if (monkey.Items[item] % monkey.Test == 0)
                        monkeyList[monkey.TestTrue].Items.Add(monkey.Items[item]);
                    // if item worry level is not divisible by the test value
                    else
                       monkeyList[monkey.TestFalse].Items.Add(monkey.Items[item]);
                    
                    // increase number of items inspected
                    monkey.TotalItemsInspected++;
                }
                // remove all items from list
                monkey.Items.Clear();
            }
        }
        
        // multiply the number of times the two most active monkeys inspect items
        long monkeyBusiness = monkeyList
            .Select(x => x.TotalItemsInspected)
            .OrderByDescending(x => x)
            .Take(2)
            .Aggregate((a, b) => a * b);

        // print answer to console
        Console.WriteLine("The level of monkey business after 10000 rounds is " + monkeyBusiness);
    }
}

public class MonkeyStats
{
    public List<long> Items { get; }
    public Operations Operation { get; }
    public int Test { get; }
    public int TestTrue { get; }
    public int TestFalse { get; }
    public long TotalItemsInspected { get; set; }
    
    /// <summary>
    /// <c>MonkeyStats</c> is a constructor for type MonkeyStats
    /// </summary>
    /// <param name="items">list of items</param>
    /// <param name="xOperator">Type Operator which includes the operator and the multiplier</param>
    /// <param name="test">Monkey throw test (check if divisible by "test")</param>
    /// <param name="testTrue">Monkey to throw too if divisible by test</param>
    /// <param name="testFalse">Monkey to throw too if not divisible by test</param>
    public MonkeyStats(List<long> items, Operations xOperator, int test, int testTrue, int testFalse)
    {
        Items = items;
        Operation = xOperator;
        Test = test;
        TestTrue = testTrue;
        TestFalse = testFalse;
        TotalItemsInspected = 0;
    }
}

public class Operations
{
    public char Operator { get; set; }
    public int Multiplier { get; set; }
}