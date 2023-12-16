using System.Linq.Expressions;

namespace day10;

public class Puzzle1
{
    public void solve()
    {
        char[,] map;
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day10\input.txt");
        //string lol = sr.ReadToEnd();
        string[] lines = sr.ReadToEnd().Split("\r\n");
        sr.Close();
        map = new char[lines[0].Length, lines.Length]; //All rows have same length
        int xStart = -1, yStart = -1;

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                if (lines[i][j] == 'S')
                {
                    xStart = j;
                    yStart = i;
                }

                map[j, i] = lines[i][j];
            }
        }

        int[] cycleLengths = new int[5] { 0, 0, 0, 0, 0 };
        //Try the 4 directions
        for (int directionIndex = 1; directionIndex < 5; directionIndex++)
        {
            cycleLengths[directionIndex] = GetCycleLengthFromStart((Direction)directionIndex, xStart, yStart, map);
        }

        Console.WriteLine("The furthest away you could come in any cycle is " + cycleLengths.Max() / 2 + " steps");
    }

    // Return 0 if no cycle is found
    private int GetCycleLengthFromStart(
        Direction startingDirection,
        int xStart,
        int yStart,
        char[,] map)
    {
        Direction? currDirection = startingDirection;
        int stepsFromStart = 0;
        int x = xStart;
        int y = yStart;

        while (true) //Step to goal
        {
            switch (currDirection)
            {
                case Direction.NORTH:
                    y -= 1;
                    break;
                case Direction.EAST:
                    x += 1;
                    break;
                case Direction.SOUTH:
                    y += 1;
                    break;
                case Direction.WEST:
                    x -= 1;
                    break;
            }

            Console.WriteLine("Arrived in coordinate: " + x + " " + y);
            stepsFromStart++;

            //If we step outside map, catch exception.
            try
            {
                if (map[x, y] == 'S')
                {
                    Console.WriteLine("FOUND START AGIAN");
                    return stepsFromStart;
                }

                currDirection = Step((Direction)currDirection, map[x, y]);
                if (currDirection == null) return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }

    //Steps to a target and returns new direction
    //Returns null on fail step
    private Direction? Step(Direction dir, char target)
    {
        switch (dir)
        {
            case Direction.NORTH:
                switch (target)
                {
                    case '|':
                        return Direction.NORTH;
                    case '7':
                        return Direction.WEST;
                    case 'F':
                        return Direction.EAST;
                    default:
                        return null;
                }
            case Direction.EAST:
                switch (target)
                {
                    case '-':
                        return Direction.EAST;
                    case 'J':
                        return Direction.NORTH;
                    case '7':
                        return Direction.SOUTH;
                    default:
                        return null;
                }
            case Direction.SOUTH:
                switch (target)
                {
                    case '|':
                        return Direction.SOUTH;
                    case 'L':
                        return Direction.EAST;
                    case 'J':
                        return Direction.WEST;
                    default:
                        return null;
                }
            case Direction.WEST:
                switch (target)
                {
                    case '-':
                        return Direction.WEST;
                    case 'L':
                        return Direction.NORTH;
                    case 'F':
                        return Direction.SOUTH;
                    default:
                        return null;
                }
            default:
                throw new Exception("Invalid Direction");
        }
    }
}

public enum Direction
{
    NORTH = 1,
    EAST = 2,
    SOUTH = 3,
    WEST = 4
}