namespace day2;

public class Puzzle1
{
    private Dictionary<string, int> _configuration;

    public Puzzle1(Dictionary<string, int> configuration)
    {
        _configuration = configuration;
    }

    public void Solve()
    {
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day2\input.txt");
        string line;
        int sum = 0;

        line = sr.ReadLine();

        while (line != null)
        {
            string[] strArr = line.Split(':');

            bool possible = IsGamePossible(_configuration, strArr[1]);
            if (possible)
            {
                sum += int.Parse(strArr[0].Split(' ')[1]);
            }

            line = sr.ReadLine();
        }

        Console.WriteLine("The sum was " + sum);
    }

    private bool IsGamePossible(Dictionary<string, int> configuration, string cubePicks)
    {
        Dictionary<string, int> cubePicksDict = new Dictionary<string, int>();
        string[] sets = cubePicks.Split(';');

        foreach (var set in sets)
        {
            string[] sr = set.Split(',');
            
            //Read the cube picks
            foreach (var pick in sr)
            {
                string[] parts = pick.Split(' ');

                bool isAdded = cubePicksDict.TryAdd(parts[2], int.Parse(parts[1]));
                if (isAdded == false) //The key already exist
                {
                    cubePicksDict[parts[2]] += int.Parse(parts[1]);
                }
            }

            //Compare with config
            foreach (var pair in cubePicksDict)
            {
                if (!configuration.ContainsKey(pair.Key)) return false;

                if (configuration[pair.Key] < pair.Value) return false;
            }
            
            cubePicksDict.Clear();
        }
        
        return true;
    }
}