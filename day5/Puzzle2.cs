namespace day5;

public class Puzzle2
{
    //För varje seed intervall
    //  Hitta alla vägar till location intervall
    //  välj den som är minst
    //  Hitta den minsta srcen för den locationen
    //Ta minimum av alla minsta locations
    public void Solve()
    {
        
        int numOfReadLineBreaks = 0; //Keeps track of what value is read
        List<Int64> locations = new List<Int64>();

        List<RangeMapper> seedies = new List<RangeMapper>();
        List<RangeMapper> seedToSoil = new List<RangeMapper>();
        List<RangeMapper> soilToFert = new List<RangeMapper>();
        List<RangeMapper> fertToWater = new List<RangeMapper>();
        List<RangeMapper> waterToLight = new List<RangeMapper>();
        List<RangeMapper> lightToTemp = new List<RangeMapper>();
        List<RangeMapper> tempToHumid = new List<RangeMapper>();
        List<RangeMapper> humidToLoc = new List<RangeMapper>();
        
        List<RangeMapper[]> paths = new List<RangeMapper[]>();

        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day5\input.txt");
        string line;

        //Read seeds
        line = sr.ReadLine();
        string[] seedParts = line.Split(":")[1].Split(" ");
        
        for (int i = 0; i < seedParts.Length; i+=2)
        {
            seedies.Add(new RangeMapper(
                Int64.Parse(seedParts[i]),
                Int64.Parse(seedParts[i]),
                Int64.Parse(seedParts[i+1])));
        }
        

        line = sr.ReadLine();

        //Read mappings
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
            RangeMapper mapper = new RangeMapper(Int64.Parse(numParts[1]), Int64.Parse(numParts[0]), Int64.Parse(numParts[2]));

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
        
    }

    public void FindAllPathsForSeed(
        RangeMapper seedRange,
        List<RangeMapper> seedToSoil, 
        List<RangeMapper> soilToFert, 
        List<RangeMapper> fertToWater, 
        List<RangeMapper> waterToLight,
        List<RangeMapper> lightToTemp,
        List<RangeMapper> tempToHumid,
        List<RangeMapper> humidToLoc)
    {
        List<List<RangeMapper>> paths = new List<List<RangeMapper>>();

        foreach (var soilMapper in seedToSoil)
        {
            if (seedRange.MappingIsPossible(soilMapper))
            {
                paths.Add(new List<RangeMapper>{seedRange, soilMapper});
            }
        }

        foreach (var seed in seedRange)
        {
            
        }


    }
    
}

public class RangeMapper
{
    public Int64 src_index;
    public Int64 dest_value;
    public Int64 range;

    public RangeMapper(Int64 srcIndex, Int64 destIndex, Int64 range)
    {
        src_index = srcIndex;
        this.range = range;
        dest_value = destIndex - src_index;
    }

    public bool MappingIsPossible(RangeMapper target)
    {
        return true;
    }

    public void MapToMapper(RangeMapper target)
    {
        throw new NotImplementedException();
    }
    
}

public class TreeNode
{
     public List<TreeNode> Parents = new List<TreeNode>();
     public List<TreeNode> Children = new List<TreeNode>();
     public RangeMapper nodeVal
    
}