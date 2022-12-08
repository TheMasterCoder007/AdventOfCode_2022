using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public static class AocDay5
{
    // create array to hold stacks
    static Stack<char>[] _crateStacks = new Stack<char>[9];

    /// <summary>
    /// <c>SupplyStacks</c> is called to calculate and print the correct answer for the days
    /// challenges to the console.
    /// </summary>
    public static void SupplyStacks()
    {
        // pull input into string and then split the string at empty line (between stacks and commands)
        string input = File.ReadAllText(@"..\..\..\inputs\input5.txt");
        string[] inputSplit = input.Split("\r\n\r\n");

        // split stacks by row
        string[] initialStacks = inputSplit[0].Split("\r\n");

        // create stacks
        for (int i = 0; i < _crateStacks.Length; i++)
        {
            _crateStacks[i] = new Stack<char>();
        }
        
        // find columns
        int[] stackColumn = new int[9];
        int stackNumInput = initialStacks.Length - 1;
        for (int crate = 0; crate < initialStacks[stackNumInput].Length; crate++)
        {
            if (initialStacks[stackNumInput][crate] == '1')
                stackColumn[0] = crate;
            else if (initialStacks[stackNumInput][crate] == '2')
                stackColumn[1] = crate;
            else if (initialStacks[stackNumInput][crate] == '3')
                stackColumn[2] = crate;
            else if (initialStacks[stackNumInput][crate] == '4')
                stackColumn[3] = crate;
            else if (initialStacks[stackNumInput][crate] == '5')
                stackColumn[4] = crate;
            else if (initialStacks[stackNumInput][crate] == '6')
                stackColumn[5] = crate;
            else if (initialStacks[stackNumInput][crate] == '7')
                stackColumn[6] = crate;
            else if (initialStacks[stackNumInput][crate] == '8')
                stackColumn[7] = crate;
            else if (initialStacks[stackNumInput][crate] == '9')
            {
                stackColumn[8] = crate;
                break;
            }
        }

        // find initial crate id's and push to stacks
        InitializeStacks(initialStacks, stackColumn);

        // calculate top crates after rearrangement procedure (9000)
        string[] rearrangementProcedure = inputSplit[1].Split("\r\n");
        foreach (string row in rearrangementProcedure)
        {
            List<int> crates = new List<int>();
            foreach (Match crate in Regex.Matches(row, @"\d+"))
            {
                crates.Add(Convert.ToInt32(crate.Value));
            }
            
            MoveCrates9000(crates[0], crates[1], crates[2]);
        }

        // print answers to console for part 1 and then clear the stacks
        Console.Write("The top crates after rearrangement is initially thought to be ");
        for (int stack = 0; stack < 9; stack++)
        {
            Console.Write("{0}", _crateStacks[stack].Peek());
            _crateStacks[stack].Clear();
        }
        Console.Write("\n");
        
        
        
        // reset initial crate configuration and push to stacks
        InitializeStacks(initialStacks, stackColumn);
        
        // calculate top crates after rearrangement procedure (9001)
        foreach (string row in rearrangementProcedure)
        {
            List<int> crates = new List<int>();
            foreach (Match crate in Regex.Matches(row, @"\d+"))
            {
                crates.Add(Convert.ToInt32(crate.Value));
            }
            
            MoveCrates9001(crates[0], crates[1], crates[2]);
        }
        
        // print the answers to the console for part 2
        Console.Write("After finding out the crane type, the top crates will be ");
        for (int stack = 0; stack < 9; stack++)
            Console.Write("{0}", _crateStacks[stack].Peek());
        
        Console.Write("\n");
    }

    /// <summary>
    /// With this crane (CrateMover9000), you can only move one crate at a time.
    /// Crates are pushed from one stack to another and end up in reverse.
    /// <c>MoveCrates9000</c> rearranges the crates following this logic
    /// </summary>
    /// <param name="numberOfMoves"></param>
    /// <param name="fromStack"></param>
    /// <param name="toStack"></param>
    private static void MoveCrates9000(int numberOfMoves, int fromStack, int toStack)
    {
        for (int moves = 0; moves < numberOfMoves; moves++)
        {
            // move crates from one stack to another
            _crateStacks[toStack - 1].Push(_crateStacks[fromStack - 1].Pop());
        }
    }
    
    /// <summary>
    /// With this crane (CrateMover 9001), you can move multiple crates at a time.
    /// Crates are pushed to a temporary stack which is then transferred to the
    /// destination stack. This keeps the stacks in the same order but moves them
    /// to the right stack. <c>MoveCrates9001</c> rearranges the crates following this logic
    /// </summary>
    /// <param name="numberOfMoves"></param>
    /// <param name="fromStack"></param>
    /// <param name="toStack"></param>
    private static void MoveCrates9001(int numberOfMoves, int fromStack, int toStack)
    {
        Stack<char> tempStack = new Stack<char>();
        
        // move crates to temp stack
        for (int moves = 0; moves < numberOfMoves; moves++)
        {
            tempStack.Push(_crateStacks[fromStack - 1].Pop());
        }
        // move crates to destination stack
        for (int moves = 0; moves < numberOfMoves; moves++)
        {
            _crateStacks[toStack - 1].Push(tempStack.Pop());
        }
    }

    /// <summary>
    /// <c>InitializeStacks</c> parses the initial crate configuration and pushes
    /// the info to stacks
    /// </summary>
    /// <param name="initialStacks"></param>
    /// <param name="stackColumn"></param>
    private static void InitializeStacks(string[] initialStacks, int[] stackColumn)
    {
        int bottomOfStacks = initialStacks.Length - 2;
        for (int row = bottomOfStacks; row >= 0; row--)
        {
            for (int column = 0; column < 9; column++)
            {
                if (initialStacks[row][stackColumn[column]] != ' ')
                    _crateStacks[column].Push(initialStacks[row][stackColumn[column]]);
            }
        }
    }
}