namespace day4;

public class Puzzle1
{
    public void Solve(int numOfWinningNumbers, int numOfGuessNumbers)
    {
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day4\input.txt");
        List<int[]> listOfCards = new List<int[]>();
        string line;
        int score;
        line = sr.ReadLine();

        while (line != null)
        {
            int[] numsInCard = new int[numOfWinningNumbers + numOfGuessNumbers];
            string[] parts = line.Split("|");
            string[] winnerNums = parts[0].Split(":")[1].Trim().Split(" ");
            string[] nums = parts[1].Trim().Split(" ");

            //Sanitize double-spaces
            winnerNums = winnerNums.Where(num => num != "").ToArray();
            nums = nums.Where(num => num != "").ToArray();

            for (int i = 0; i < numOfWinningNumbers; i++)
            {
                numsInCard[i] = int.Parse(winnerNums[i]);
            }

            for (int i = numOfWinningNumbers; i < numOfWinningNumbers + numOfGuessNumbers; i++)
            {
                numsInCard[i] = int.Parse(nums[i - numOfWinningNumbers]);
            }

            listOfCards.Add(numsInCard);
            line = sr.ReadLine();
        }
        sr.Close();

        score = CountScore(listOfCards, numOfWinningNumbers, numOfGuessNumbers);
        Console.WriteLine("The score is " + score);
    }

    public int CountScore(List<int[]> listOfCards, int numOfWinningNumbers, int numOfGuessNumbers)
    {
        int score = 0;
        foreach (var numArr in listOfCards)
        {
            int[] winnNums = numArr.Take(numOfWinningNumbers).ToArray();
            int[] otherNums = numArr.Skip(numOfWinningNumbers).Take(numOfGuessNumbers).ToArray();
            var intersection = otherNums.Intersect(winnNums);
            if (intersection.Any())
            {
                score += (int)(Math.Pow(2, intersection.Count() - 1));
            }
        }

        return score;
    }
}