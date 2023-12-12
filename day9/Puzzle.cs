using System.Collections;

namespace day9;

public class Puzzle
{
    public void Solve()
    {
        List<History> histories = new List<History>();
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day9\input.txt");
        string line = sr.ReadLine();

        while (line != null)
        {
            int[] nums = line.Split(" ").Select(num => int.Parse(num)).ToArray();
            histories.Add(new History(nums));
            line = sr.ReadLine();
        }
        
        Console.WriteLine("The answer to puzzle1 is " + histories.Aggregate(0L, (i, history) => i + history.SolvePuzzle1() ));
        Console.WriteLine("The answer to puzzle2 is " + histories.Aggregate(0L, (i, history) => i + history.SolvePuzzle2() ));
    }
}

public class History
{
    public List<int[]> Values = new List<int[]>();

    public History(int[] startingValues)
    {
        Values.Add(startingValues);
        GenerateHistory();
    }
    
    private void GenerateHistory()
    {
        while (Values[^1].Any(num => num != 0))
        {
            int[] parentRow = Values[^1];
            int[] row = new int[parentRow.Length -1];
            for (int i = 0; i < row.Length; i++)
            {
                row[i] =  parentRow[i + 1] - parentRow[i] ;
            }
            Values.Add(row);
        }
    }

    public int SolvePuzzle1()
    {
        int currVal = 0;

        for (int i = Values.Count - 2; i >= 0; i--)
        {
            currVal += Values[i][^1];
        }

        return currVal;
    }
    
    public int SolvePuzzle2()
    {
        int currVal = 0;

        for (int i = Values.Count - 2; i >= 0; i--)
        {
            currVal = Values[i][0] - currVal;
        }

        return currVal;
    }

}