namespace day3;

public class Puzzle1
{
    public void Solve()
    {
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day3\input.txt");
        List<NumOnMap> numbersFound = new List<NumOnMap>();
        List<Coord> symbolCoords = new List<Coord>();

        Coord currCoord = new Coord { X = 0, Y = 0 };
        string line;
        line = sr.ReadLine();
        int sum = 0;

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
                    //Its a symbol
                    symbolCoords.Add(currCoord.Clone());
                    
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

        sum = CountSum(numbersFound, symbolCoords);
        
        Console.WriteLine("The sum was " + sum);
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

    private int CountSum(List<NumOnMap> numbersFound, List<Coord> symbolCoords)
    {
        int sum = 0;
        
        foreach (var num in numbersFound)
        {
            bool countNum = false;
            var nearbySymbols = symbolCoords.Where(c => int.Abs(num.Coords[0].Y - c.Y) < 2); // Symbols above, below, beside
            foreach (var numCoord in num.Coords)
            {
                if (nearbySymbols.Any(c => int.Abs(c.X - numCoord.X) < 2))
                {
                    countNum = true;
                    break;
                }
            }

            if (countNum)
            {
                sum += num.value;
            }
        }

        return sum;
    }
}

class NumOnMap
{
    public List<Coord> Coords;
    public int value;

    public NumOnMap(List<Coord> coords, int value)
    {
        Coords = coords;
        this.value = value;
    }
}

class Coord 
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coord Clone()
    {
        return new Coord
        {
            X = X,
            Y = Y
        };
    }
}