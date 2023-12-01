using System.Text;

namespace day1;

public class Puzzle2
{
    public readonly Dictionary<string, string> NumberStrings = new Dictionary<string, string>
    {
        { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" },
        { "five", "5" }, { "six", "6" },{ "seven", "7" }, { "eight", "8" },{ "nine", "9" }
    };

    public void Solve()
    {
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day1\input.txt");
        StreamWriter sw = new StreamWriter(@"C:\Users\flind\Desktop\adventofcode2023\day1\modifiedInput.txt");

        string line;
        string digitLine;
        int sum = 0;

        line = sr.ReadLine();
        //Continue to read until you reach end of file
        while (line != null)
        {
            digitLine = ConvertLineToDigits(line);
            //Write the line
            sw.WriteLine(digitLine);


            //Read the next line
            line = sr.ReadLine();
        }

        //close the files
        sr.Close();
        sw.Close();
        //Console.WriteLine("Sum was " + sum);
    }

    private string ConvertLineToDigits(string line)
    {
        StringBuilder sb = new StringBuilder();
        int currIndex = 0;
        int lastIndex = line.Length - 1;

        while (currIndex <= lastIndex)
        {
            if (Char.IsNumber(line[currIndex]))
            {
                sb.Append(line[currIndex]);
                currIndex++;
                continue;
            }
            
            if (currIndex + 2 <= lastIndex && NumberStrings.ContainsKey(line.Substring(currIndex, 3)))
            {
                sb.Append(NumberStrings[line.Substring(currIndex, 3)]);
                currIndex++;
                continue;
            }
            
            if (currIndex + 3 <= lastIndex && NumberStrings.ContainsKey(line.Substring(currIndex, 4)))
            {
                sb.Append(NumberStrings[line.Substring(currIndex, 4)]);
                currIndex++;
                continue;
            }
            
            if (currIndex + 4 <= lastIndex && NumberStrings.ContainsKey(line.Substring(currIndex, 5)))
            {
                sb.Append(NumberStrings[line.Substring(currIndex, 5)]);
                currIndex++;
                continue;
            }

            currIndex++;
        }

        return sb.ToString();
    }
}
    