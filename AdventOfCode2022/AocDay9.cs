using System;

namespace AdventOfCode2022;

public static class AocDay9
{
    // global variables to be used within class
    private static readonly bool[,] Grid = new bool[1000, 1000];
    private static readonly int[] XLocation = new int[10];
    private static readonly int[] YLocation = new int[10];
    
    public static void RopeBridge(int partNum)
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input9.txt");
        string[] inputSplit = input.Split("\r\n");
        
        // initialize grid
        for (int y = 0; y < 1000; y++)
        {
            for (int x = 0; x < 1000; x++)
            {
                Grid[x, y] = false;
            }
        }
            
        // initialize start location to the center of the grid
        for (int i = 0; i < 10; i++)
        {
            XLocation[i] = 499;
            YLocation[i] = 499;
        }
        
        // parse input and calculate all the positions the tail visits
        foreach (string line in inputSplit)
        {
            // split string into command and number of moves
            string[] move = line.Split(" ");
            int numOfMoves = Convert.ToInt32(move[1]);
            
            // calculate moves
            VisitLocations(move[0], numOfMoves, partNum);
        }
        
        // calculate the total number of visited locations by the tail
        int locVisits = 0;
        for (int y = 0; y < 1000; y++)
        {
            for (int x = 0; x < 1000; x++)
            {
                if (!Grid[x, y]) continue;
                locVisits++;
            }
        }
        if (partNum == 1)
            Console.WriteLine("The total number of locations the 2 knot tail visits is " + locVisits);
        else if (partNum == 2)
            Console.WriteLine("The total number of locations the 10 knot tail visits is " + locVisits);
    }

    private static void VisitLocations(string direction, int numOfMoves, int partNum)
    {
        // move head based on instructions
        for (int move = 1; move <= numOfMoves; move++)
        {
            // if direction is up
            if (direction[0] == 'U')
                YLocation[0]++;
            // if direction is right
            else if (direction[0] == 'R')
                XLocation[0]++;
            // if direction is down
            else if (direction[0] == 'D')
                YLocation[0]--;
            // if direction is left
            else if (direction[0] == 'L')
                XLocation[0]--;
            
            // find new location of tail
            if (partNum == 1)
                LocationCompare(1);
            else if (partNum == 2)
                LocationCompare(9);

            // record tail location
            if (partNum == 1)
                Grid[XLocation[1], YLocation[1]] = true;
            else if (partNum == 2)
                Grid[XLocation[9], YLocation[9]] = true;
        }
    }

    private static void LocationCompare(int maxKnots)
    {
        // process all knots and until you find the tail location
        bool inSurroundingGrid = false;
        for (int knot = 0; knot < maxKnots; knot++)
        {
            // check surrounding grid for tail
            for (int y = YLocation[knot] - 1; y <= YLocation[knot] + 1; y++)
            {
                for (int x = XLocation[knot] - 1; x <= XLocation[knot] + 1; x++)
                {
                    // if tail is within the surrounding grid of head, do not move tail
                    if (XLocation[knot + 1] == x && YLocation[knot + 1] == y)
                    {
                        inSurroundingGrid = true;
                        break;
                    }
                }

                if (inSurroundingGrid)
                    break;
            }

            if (!inSurroundingGrid)
            {
                //---------------------------------------------------
                //          vertical and horizontal moves
                //---------------------------------------------------
                // head is moving right and tail is on the same row
                if (YLocation[knot] == YLocation[knot + 1] && XLocation[knot] > XLocation[knot + 1])
                    XLocation[knot + 1] = XLocation[knot] - 1;
                // head is moving left and tail is on the same row
                else if (YLocation[knot] == YLocation[knot + 1] && XLocation[knot] < XLocation[knot + 1])
                    XLocation[knot + 1] = XLocation[knot] + 1;
                // head is moving up and tail is on the same column
                else if (XLocation[knot] == XLocation[knot + 1] && YLocation[knot] > YLocation[knot + 1])
                    YLocation[knot + 1] = YLocation[knot] - 1;
                // head is moving down and tail is on the same column
                else if (XLocation[knot] == XLocation[knot + 1] && YLocation[knot] < YLocation[knot + 1])
                    YLocation[knot + 1] = YLocation[knot] + 1;

                //---------------------------------------------------
                //                  diagonal moves
                //---------------------------------------------------
                // head is to the right or left and moving up
                else if (YLocation[knot] > YLocation[knot + 1] + 1 && 
                         (XLocation[knot] == XLocation[knot + 1] + 1 || XLocation[knot] == XLocation[knot + 1] - 1))
                {
                    YLocation[knot + 1] = YLocation[knot] - 1;
                    XLocation[knot + 1] = XLocation[knot];
                }
                // head is to the right or left and moving down
                else if (YLocation[knot] < YLocation[knot + 1] - 1 && 
                         (XLocation[knot] == XLocation[knot + 1] + 1 || XLocation[knot] == XLocation[knot + 1] - 1))
                {
                    YLocation[knot + 1] = YLocation[knot] + 1;
                    XLocation[knot + 1] = XLocation[knot];
                }
                // head is up or down and moving to the right
                else if (XLocation[knot] > XLocation[knot + 1] + 1 && 
                         (YLocation[knot] == YLocation[knot + 1] + 1 || YLocation[knot] == YLocation[knot + 1] - 1))
                {
                    YLocation[knot + 1] = YLocation[knot];
                    XLocation[knot + 1] = XLocation[knot] - 1;
                }
                // head is up or down and moving to the left
                else if (XLocation[knot] < XLocation[knot + 1] - 1 && 
                         (YLocation[knot] == YLocation[knot + 1] + 1 || YLocation[knot] == YLocation[knot + 1] - 1))
                {
                    YLocation[knot + 1] = YLocation[knot];
                    XLocation[knot + 1] = XLocation[knot] + 1;
                }
                // previous knot is up and right by 2 cells
                else if (XLocation[knot] == XLocation[knot + 1] + 2 && YLocation[knot] == YLocation[knot + 1] + 2)
                {
                    XLocation[knot + 1]++;
                    YLocation[knot + 1]++;
                }
                // previous knot is up and left by 2 cells
                else if (XLocation[knot] == XLocation[knot + 1] - 2 && YLocation[knot] == YLocation[knot + 1] + 2)
                {
                    XLocation[knot + 1]--;
                    YLocation[knot + 1]++;
                }
                // previous knot is down and right by 2 cells
                else if (XLocation[knot] == XLocation[knot + 1] + 2 && YLocation[knot] == YLocation[knot + 1] - 2)
                {
                    XLocation[knot + 1]++;
                    YLocation[knot + 1]--;
                }
                // previous knot is down and left by 2 cells
                else if (XLocation[knot] == XLocation[knot + 1] - 2 && YLocation[knot] == YLocation[knot + 1] - 2)
                {
                    XLocation[knot + 1]--;
                    YLocation[knot + 1]--;
                }
            }
            else
                inSurroundingGrid = false;
        }
    }
}