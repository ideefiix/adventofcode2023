namespace day3;

public class Puzzle2
{
    public void Solve()
    {
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day3\input.txt");
        List<NumOnMap> numbersFound = new List<NumOnMap>();
        List<Coord> gearCoords = new List<Coord>();

        Coord currCoord = new Coord { X = 0, Y = 0 };
        string line;
        line = sr.ReadLine();
        int score = 0;

        while (line != null)
        {
            string numRead = "";
            foreach (var c in line)
            {
                if (c == '.')
                {
                    if (numRead != "")
                    {
                        Coord lastCoordForNum = currCoord.Clone();
                        lastCoordForNum.X -= 1;
                        numbersFound.Add(CreateNumOnMap(numRead, lastCoordForNum));
                        numRead = "";
                    }
                }
                else if (char.IsNumber(c))
                {
                    numRead += c;
                }
                else
                {
                    //Its a symbol. Check for gear
                    if(c == '*') gearCoords.Add(currCoord.Clone());
                    
                    if (numRead != "")
                    {
                        Coord lastCoordForNum = currCoord.Clone();
                        lastCoordForNum.X -= 1;
                        numbersFound.Add(CreateNumOnMap(numRead, lastCoordForNum));
                        numRead = "";
                    }
                }

                currCoord.X += 1;
            }
            
            if (numRead != "") //Line ended with number
            {
                Coord lastCoordForNum = currCoord.Clone();
                lastCoordForNum.X -= 1;
                numbersFound.Add(CreateNumOnMap(numRead, lastCoordForNum));
            }

            currCoord.Y += 1;
            currCoord.X = 0;
            line = sr.ReadLine();
        }
        sr.Close();

        score = CountGearScore(numbersFound, gearCoords);
        
        Console.WriteLine("The score was " + score);
    }

    private NumOnMap CreateNumOnMap(string num, Coord endCoord)
    {
        List<Coord> coords = new List<Coord>();
        int val = int.Parse(num);

        for (int i = 0; i < num.Length; i++)
        {
            coords.Add(new Coord{X = endCoord.X-i, Y = endCoord.Y});
        }

        return new NumOnMap(coords, val);
    }

    private int CountGearScore(List<NumOnMap> numbersFound, List<Coord> gearCoords)
    {
        Dictionary<Coord, int> gearCoordValueDict = new Dictionary<Coord, int>();
        int score = 0;
        
        foreach (var num in numbersFound)
        {
            var nearbyGears = gearCoords.Where(c => int.Abs(num.Coords[0].Y - c.Y) < 2); // Gears above, below, beside
            foreach (var numCoord in num.Coords)
            {
                var gearCoord = nearbyGears.FirstOrDefault(c => int.Abs(c.X - numCoord.X) < 2);
                if (gearCoord != null)
                {
                    bool success = gearCoordValueDict.TryAdd(gearCoord, num.value);
                    if (!success)// The gear already exist. Add score and remove it 
                    {
                        score += (gearCoordValueDict[gearCoord] * num.value);
                        gearCoordValueDict.Remove(gearCoord);
                    }
                    break;
                }
            }
        }

        return score;
    }
}
