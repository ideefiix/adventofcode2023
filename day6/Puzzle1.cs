namespace day6;

public class Puzzle1
{
    public void Solve()
    {
        List<Race> races = new List<Race>();
        Int128 answer;
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day6\input.txt");
        string line;

        line = sr.ReadLine(); //Time
        Int128[] times = line.Split(":")[1].Trim().Split(" ")
            .Where(s => Int128.TryParse(s, out Int128 res))
            .Select(s => { return Int128.Parse(s); })
            .ToArray();

        line = sr.ReadLine(); // Distance
        Int128[] distance = line.Split(":")[1].Trim().Split(" ")
            .Where(s => Int128.TryParse(s, out Int128 res))
            .Select(s => { return Int128.Parse(s); })
            .ToArray();
        sr.Close();

        for (int i = 0; i < times.Length; i++)
        {
            Race race = new Race(times[i], distance[i]);
            races.Add(race);
        }

        answer = races.Select(r => r.WaysToBeatDistance()).Aggregate((current, next) => current * next);
        Console.WriteLine("The answer is " + answer);
    }
}

public class Race
{
    private Int128 time;
    private Int128 distance;

    public Race(Int128 time, Int128 distance)
    {
        this.time = time;
        this.distance = distance;
    }

    public Int128 WaysToBeatDistance()
    {
        Int128 count = 0;
        for (Int128 i = 1; i <= time; i++)
        {
            if ((time - i) * i > distance) count++;
        }

        return count;
    }
}