using System;

namespace AdventOfCode2022;

public static class AocDay2
{
    static readonly char OpponentMoveRock = 'A';
    static readonly char OpponentMovePaper = 'B';
    static readonly char OpponentMoveScissors = 'C';
    static readonly char MyMoveRock = 'X';
    static readonly char MyMovePaper = 'Y';
    static readonly char MyMoveScissors = 'Z';
    public static void RockPaperScissors()
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input2.txt");
        string[] inputSplit = input.Split("\r\n");
        
        // variable to hold total points
        int totalPointsPart1 = 0;
        int totalPointsPart2 = 0;

        foreach (string match in inputSplit)
        {
            totalPointsPart1 += MatchResultsPart1(match[0], match[2]);
            totalPointsPart2 += MatchResultsPart2(match[0], match[2]);
        }
        
        Console.WriteLine("Based on the instructions, you find the the total points would be " + totalPointsPart1 + ".");
        Console.WriteLine("Once you get the proper instructions you find the total points will be " + totalPointsPart2);
    }

    private static int MatchResultsPart1(char opponentsMove, char myMove)
    {
        // initialize variables
        int winPoints = 6;
        int drawPoints = 3;
        int movePoints = 0;
        int totalPoints;
        
        // set move points based on move
        if (myMove == MyMoveRock)
            movePoints = 1;
        else if (myMove == MyMovePaper)
            movePoints = 2;
        else if (myMove == MyMoveScissors)
            movePoints = 3;
        
        // winning moves
        if ((opponentsMove == OpponentMoveRock && myMove == MyMovePaper) ||
            (opponentsMove == OpponentMovePaper && myMove == MyMoveScissors) ||
            (opponentsMove == OpponentMoveScissors && myMove == MyMoveRock) )
        {
            totalPoints = movePoints + winPoints;
        }
        // draws
        else if ((opponentsMove == OpponentMoveRock && myMove == MyMoveRock) ||
                 (opponentsMove == OpponentMovePaper && myMove == MyMovePaper) ||
                 (opponentsMove == OpponentMoveScissors && myMove == MyMoveScissors))
        {
            totalPoints = movePoints + drawPoints;
        }
        // lost round
        else
        {
            totalPoints = movePoints;
        }

        return totalPoints;
    }
    
    private static int MatchResultsPart2(char opponentsMove, char intendedResult)
    {
        // initialize variables
        char drawRound = 'Y';
        char winRound = 'Z';
        char myMove;

        // need to win round
        if (intendedResult == winRound)
        {
            if (opponentsMove == OpponentMoveRock)
                myMove = MyMovePaper;
            else if (opponentsMove == OpponentMovePaper)
                myMove = MyMoveScissors;
            else
                myMove = MyMoveRock;
        }
        // need round to be a draw
        else if (intendedResult == drawRound)
        {
            if (opponentsMove == OpponentMoveRock)
                myMove = MyMoveRock;
            else if (opponentsMove == OpponentMovePaper)
                myMove = MyMovePaper;
            else
                myMove = MyMoveScissors;
        }
        // need to loose round
        else
        {
            if (opponentsMove == OpponentMoveRock)
                myMove = MyMoveScissors;
            else if (opponentsMove == OpponentMovePaper)
                myMove = MyMoveRock;
            else
                myMove = MyMovePaper;
        }
        
        // calculate points
        int totalPoints = MatchResultsPart1(opponentsMove, myMove);

        return totalPoints;
    }
}