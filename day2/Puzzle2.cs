namespace day2;

public class Puzzle2
{
    public void Solve()
    {
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day2\input.txt");
        string line;
        int sum = 0;

        line = sr.ReadLine();

        while (line != null)
        {
            string[] strArr = line.Split(':');

            int pwr = CountGamePwr(strArr[1]);
            sum += pwr;
            
            line = sr.ReadLine();
        }

        Console.WriteLine("The sum was " + sum);
    }
    
    private int CountGamePwr(string cubePicks)
    {
        Dictionary<string, int> highestColorPicks = new Dictionary<string, int>();
        string[] sets = cubePicks.Split(';');

        foreach (var set in sets)
        {
            string[] sr = set.Split(',');
            
            //Read the cube picks
            foreach (var pick in sr)
            {
                string[] parts = pick.Split(' ');

                bool isAdded = highestColorPicks.TryAdd(parts[2], int.Parse(parts[1]));
                if (isAdded == false) //The key already exist
                {
                    if (highestColorPicks[parts[2]] < int.Parse(parts[1]))
                    {
                        highestColorPicks[parts[2]] = int.Parse(parts[1]);
                    }
                }
            }
        }

        int pwr = 0;
        foreach (var value in highestColorPicks.Values)
        {
            if (pwr == 0)
            {
                pwr = value;
            }
            else
            {
                pwr *= value;
            }
        }
        
        return pwr;
    }
}