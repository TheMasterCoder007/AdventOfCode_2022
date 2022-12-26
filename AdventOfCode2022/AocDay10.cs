using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022;

public static class AocDay10
{
    private static int _crtPixel = 1;
    private static int _clockTicks = 1;
    private static int _variableX = 1;
    private static int _totalSignalStrength = 0;
    private static bool _signalStrengthCalculated = false;
    private static List<int> _signalStrength = new List<int>();

    /// <summary>
    /// <c>CathodeRayTube</c> calculates the signal strength of your device and writes to the CRT display
    /// </summary>
    public static void CathodeRayTube()
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input10.txt");
        string[] inputSplit = input.Split("\r\n");

        // parse instruction set and find answers
        foreach (string command in inputSplit)
        {
            if (command.Contains("noop"))
            {
                // call method to draw CRT pixels
                ToggleCrtPixels(1);
                
                // call method to execute instruction set
                ExecuteInstructionSet(1, 0);
            }
            else if (command.Contains("addx"))
            {
                string[] splitCommand = command.Split(" ");
                int instructionSet = int.Parse(splitCommand[1]);
                
                // call method to draw CRT pixels
                ToggleCrtPixels(2);

                // call method to execute instruction set
                ExecuteInstructionSet(2, instructionSet);
            } 
        }
        
        // print answer to console (part 1)
        Console.WriteLine("\nThe sum of all 6 signal strength reading is " + _totalSignalStrength);
    }

    /// <summary>
    /// <c>ExecuteInstructionSet</c> calculates the signal strength of the device, keeps track
    /// of the clock ticks, and executes the instruction set that moves the CRT sprite (_variableX)
    /// </summary>
    /// <param name="ticksToComplete">Number of ticks the process takes to complete</param>
    /// <param name="valueToAdd">Value to add to the x register when the process completes</param>
    private static void ExecuteInstructionSet(int ticksToComplete, int valueToAdd)
    {
        // begin execution of command
        for (int ticks = 0; ticks < ticksToComplete; ticks++)
        {
            // if signal strength has not been found yet
            if (!_signalStrengthCalculated)
            {
                // tick hits read period
                if (_clockTicks is 20 or 60 or 100 or 140 or 180 or 220)
                {
                    // calculate signal strength and add to list
                    _signalStrength.Add(_clockTicks * _variableX);

                    if (_clockTicks is 220)
                    {
                        _totalSignalStrength = _signalStrength.Sum();
                        _signalStrengthCalculated = true;
                    }
                }
            }
            
            // increase clock ticks
            _clockTicks++;
        }
        // read period is not hit and ticks for execution is complete, execute command
        _variableX += valueToAdd;
    }

    /// <summary>
    /// <c>ToggleCrtPixels</c> writes to the the pixels one at a time (one per clock tick)
    /// using the instruction set (if CRT sprite covers current pixel write '#', else write '.')
    /// </summary>
    /// <param name="ticksToComplete">Number of ticks the process takes to complete</param>
    private static void ToggleCrtPixels(int ticksToComplete)
    {
        for (int ticks = 0; ticks < ticksToComplete; ticks++)
        {
            // if the sprite is covering the currently drawing pixel
            if (_crtPixel == _variableX || _crtPixel == _variableX + 1 || _crtPixel == _variableX + 2)
                Console.Write('#');
            // if the sprite is out of range of the currently drawing pixel
            else
                Console.Write('.');
            
            // if the last pixel on the row has been drawn, reset _crtPixel and move to next row
            if (_crtPixel is 40)
            {
                Console.Write('\n');
                _crtPixel = 1;
            }
            // otherwise move to next pixel
            else
            {
                _crtPixel++;
            }
        }
    }
}