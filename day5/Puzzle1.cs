namespace day5;

public class Puzzle1
{
    public void Solve()
    {
        int numOfReadLineBreaks = 0; //Keeps track of what value is read
        List<Int64> locations = new List<Int64>();
        List<Int64> seeds = new List<Int64>();
        List<Mapper> seedToSoil = new List<Mapper>();
        List<Mapper> soilToFert = new List<Mapper>();
        List<Mapper> fertToWater = new List<Mapper>();
        List<Mapper> waterToLight = new List<Mapper>();
        List<Mapper> lightToTemp = new List<Mapper>();
        List<Mapper> tempToHumid = new List<Mapper>();
        List<Mapper> humidToLoc = new List<Mapper>();

        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day5\input.txt");
        string line;

        //Read seeds
        line = sr.ReadLine();
        string[] seedParts = line.Split(":")[1].Split(" ");
        foreach (var seed in seedParts)
        {
            if (seed == "") continue;
            seeds.Add(Int64.Parse(seed));
        }

        line = sr.ReadLine();

        //Read map values
        while (line != null)
        {
            if (line == "")
            {
                numOfReadLineBreaks++;
                sr.ReadLine();
                line = sr.ReadLine();
                continue;
            }

            string[] numParts = line.Split(" ");
            Mapper mapper = new Mapper(Int64.Parse(numParts[1]), Int64.Parse(numParts[0]), Int64.Parse(numParts[2]));

            if (numOfReadLineBreaks == 1)
            {
                seedToSoil.Add(mapper);
            }
            else if (numOfReadLineBreaks == 2)
            {
                soilToFert.Add(mapper);
            }
            else if (numOfReadLineBreaks == 3)
            {
                fertToWater.Add(mapper);
            }
            else if (numOfReadLineBreaks == 4)
            {
                waterToLight.Add(mapper);
            }
            else if (numOfReadLineBreaks == 5)
            {
                lightToTemp.Add(mapper);
            }
            else if (numOfReadLineBreaks == 6)
            {
                tempToHumid.Add(mapper);
            }
            else if (numOfReadLineBreaks == 7)
            {
                humidToLoc.Add(mapper);
            }

            line = sr.ReadLine();
        }

        sr.Close();

        foreach (var seed in seeds)
        {
            Int64 location = seed;
            location = MapValueUsingListOfMappers(location, seedToSoil);
            location = MapValueUsingListOfMappers(location, soilToFert);
            location = MapValueUsingListOfMappers(location, fertToWater);
            location = MapValueUsingListOfMappers(location, waterToLight);
            location = MapValueUsingListOfMappers(location, lightToTemp);
            location = MapValueUsingListOfMappers(location, tempToHumid);
            location = MapValueUsingListOfMappers(location, humidToLoc);
            locations.Add(location);
        }
        
        Console.WriteLine("The lowest location is " + locations.Min());
    }

    private Int64 MapValueUsingListOfMappers(Int64 val, List<Mapper> mapperList)
    {
        Int64? mapRes;
        foreach (var mapper in mapperList)
        {
            mapRes = mapper.MapNum(val);
            if (mapRes != null)
            {
                return (Int64)mapRes;
            }
        }
        //Default map: X -> X
        return val;
    }
}

public class Mapper
{
    private Int64 src_index;
    private Int64 map_value;
    private Int64 range;

    public Mapper(Int64 srcIndex, Int64 destIndex, Int64 range)
    {
        src_index = srcIndex;
        this.range = range;
        map_value = destIndex - src_index;
    }

    public Int64? MapNum(Int64 src_num)
    {
        if (src_num >= src_index && src_num < src_index + range)
        {
            return src_num + map_value;
        }

        return null;
    }
    
}