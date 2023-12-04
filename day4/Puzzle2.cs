namespace day4;

public class Puzzle2
{
    private const int Winnums = 10;
    private const int TotalNums = 35;
    
    public void Solve()
    {
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day4\input.txt");
        List<int[]> listOfCards = new List<int[]>();
        string line;
        int totalScratchCards;
        line = sr.ReadLine();

        while (line != null)
        {
            int[] numsInCard = new int[TotalNums];
            string[] parts = line.Split("|");
            string[] winnerNums = parts[0].Split(":")[1].Trim().Split(" ");
            string[] nums = parts[1].Trim().Split(" ");

            //Sanitize double-spaces
            winnerNums = winnerNums.Where(num => num != "").ToArray();
            nums = nums.Where(num => num != "").ToArray();

            for (int i = 0; i < Winnums; i++)
            {
                numsInCard[i] = int.Parse(winnerNums[i]);
            }

            for (int i = Winnums; i < TotalNums; i++)
            {
                numsInCard[i] = int.Parse(nums[i - Winnums]);
            }

            listOfCards.Add(numsInCard);
            line = sr.ReadLine();
        }
        sr.Close();

        totalScratchCards = CountTotalScratchCards(listOfCards.ToArray());
        Console.WriteLine("Total amount of scrathcards were " + totalScratchCards);
    }

    public int CountTotalScratchCards(int[][] arrOfCards)
    {
        int count = 0;

        for (int i = 0; i < arrOfCards.Length; i++)
        {
            count += CountScrathCardInCard(arrOfCards, i);
        }

        return count;
    }
    
    public int CountScrathCardInCard(int[][] arrOfCards, int currentCard)
    {
        int total = 1;

            int[] winnNums = arrOfCards[currentCard].Take(Winnums).ToArray();
            int[] otherNums = arrOfCards[currentCard].Skip(Winnums).Take(TotalNums-Winnums).ToArray();
            var intersection = otherNums.Intersect(winnNums);
            
            if (intersection.Any())
            {
                for (int i = 1; i < intersection.Count() + 1; i++)
                {
                    if (arrOfCards.Length > currentCard + i)
                    {
                        total += CountScrathCardInCard(arrOfCards, currentCard + i);
                    }
                }
            }
            //852412 = för lite. Finns det samma nummer på kort? NEJ.
            
        return total;
    }
}