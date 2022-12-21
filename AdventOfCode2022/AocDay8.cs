using System;

namespace AdventOfCode2022;

public static class AocDay8
{
    public static void TreetopTreeHouse()
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input8.txt");
        string[] inputSplit = input.Split("\r\n");
        
        // parse input and create tree grid
        int treeGridY = inputSplit.Length;
        int treeGridX = inputSplit[0].Length;
        int[,] treeGrid = new int[treeGridY, treeGridX];
        for (int row = 0; row < treeGridY; row++)
        {
            for (int tree =  0; tree < treeGridX; tree++)
            {
                treeGrid[row, tree] = inputSplit[row][Convert.ToInt32(tree)];
            }
        }
        
        // find trees visible from the edge of the grid
        int visibleTrees = 0;
        int highestScenicScore = 0;
        for (int row = 0; row < treeGridY; row++)
        {
            for (int tree = 0; tree < treeGridX; tree++)
            {
                // if first or last row, or if first or last tree in row, count as visible
                if (row == 0 || row == (treeGridY - 1) || tree == 0 || tree == (treeGridX - 1))
                {
                    visibleTrees++;
                }
                // check visibility of all trees inside outer layer of grid
                else
                {
                    if (TreeCheck(row, tree, treeGridX, treeGridY, treeGrid))
                        visibleTrees++;
                }
                
                // calculate scenic score for each tree and find the tree with the largest scenic score.
                int tempScenicScore = ScenicScore(row, tree, treeGridX, treeGridY, treeGrid);
                if (tempScenicScore > highestScenicScore)
                    highestScenicScore = tempScenicScore;
            }
        }

        // print answer to console
        Console.WriteLine("The total number of trees visible from the outside of the grid is " + visibleTrees);
        Console.WriteLine("The highest scenic score the elves found was " + highestScenicScore);
    }

    private static bool TreeCheck(int row, int tree, int rowSize, int columnSize, int[,] grid)
    {
        // check trees to north
        for (int rw = (row - 1); rw >= 0; rw--)
        {
            // if tree is equal height or taller, break loop
            if (grid[rw, tree] >= grid[row, tree])
            {
                break;
            }
            // if tree height of all trees to edge of grid are lower
            else if (rw == 0)
            {
                return true;
            }
        }
        
        // check trees to east
        for (int tr = (tree + 1); tr < rowSize; tr++)
        {
            // if tree is equal height or taller, break loop
            if (grid[row, tr] >= grid[row, tree])
            {
                break;
            }
            // if tree height of all trees to edge of grid are lower
            else if (tr == rowSize - 1)
            {
                return true;
            }
        }
        
        // check trees to south
        for (int rw = (row + 1); rw < columnSize; rw++)
        {
            // if tree is equal height or taller, break loop
            if (grid[rw, tree] >= grid[row, tree])
            {
                break;
            }
            // if tree height of all trees to edge of grid are lower
            else if (rw == columnSize - 1)
            {
                return true;
            }
        }
        
        // check trees to west
        for (int tr = (tree - 1); tr >= 0; tr--)
        {
            // if tree is equal height or taller, break loop
            if (grid[row, tr] >= grid[row, tree])
            {
                break;
            }
            // if tree height of all trees to edge of grid are lower
            else if (tr == 0)
            {
                return true;
            }
        }

        return false;
    }
    
    private static int ScenicScore(int row, int tree, int rowSize, int columnSize, int[,] grid)
    {
        int scenicScoreNorth = 0;
        int scenicScoreEast = 0;
        int scenicScoreSouth = 0;
        int scenicScoreWest = 0;
        int totalScenicScore = 0;
        
        // check scenic score to the north
        for (int rw = (row - 1); rw >= 0; rw--)
        {
            // if tree is equal height or taller, break loop
            if (grid[rw, tree] >= grid[row, tree])
            {
                scenicScoreNorth++;
                break;
            }
            // if tree is smaller
            {
                scenicScoreNorth++;
            }
        }
        
        // check scenic score to the east
        for (int tr = (tree + 1); tr < rowSize; tr++)
        {
            // if tree is equal height or taller, break loop
            if (grid[row, tr] >= grid[row, tree])
            {
                scenicScoreEast++;
                break;
            }
            // if tree is smaller
            else
            {
                scenicScoreEast++;
            }
        }
        
        // check scenic score to the south
        for (int rw = (row + 1); rw < columnSize; rw++)
        {
            // if tree is equal height or taller, break loop
            if (grid[rw, tree] >= grid[row, tree])
            {
                scenicScoreSouth++;
                break;
            }
            // if tree is smaller
            else
            {
                scenicScoreSouth++;
            }
        }
        
        // check scenic score to the west
        for (int tr = (tree - 1); tr >= 0; tr--)
        {
            // if tree is equal height or taller, break loop
            if (grid[row, tr] >= grid[row, tree])
            {
                scenicScoreWest++;
                break;
            }
            // if tree is smaller
            else
            {
                scenicScoreWest++;
            }
        }

        // calculate the total scenic score
        totalScenicScore = scenicScoreNorth * scenicScoreEast * scenicScoreSouth * scenicScoreWest;
        return totalScenicScore;
    }
}