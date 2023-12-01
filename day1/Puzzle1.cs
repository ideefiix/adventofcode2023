namespace day1;

public class Puzzle1
{
    public void Solve(string filePath)
    {
        StreamReader sr = new StreamReader(filePath);
        string line;
        int sum = 0;

        line = sr.ReadLine();
        //Continue to read until you reach end of file
        while (line != null)
        {
            char firstDigit = '0', lastDigit = '0';

            foreach (var character in line)
            {
                if (Char.IsNumber(character))
                {
                    firstDigit = character;
                    break;
                }
            }

            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (Char.IsNumber(line[i]))
                {
                    lastDigit = line[i];
                    break;
                }
            }

            int numToAdd = int.Parse(new string(new[] { firstDigit, lastDigit }));
            sum += numToAdd;
            
            //Read the next line
            line = sr.ReadLine();
        }

        //close the file
        sr.Close();
        Console.WriteLine("Sum was " + sum);
    }
}