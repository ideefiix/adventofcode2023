namespace day7;

public class Puzzle1
{
    LinkedList<Hand> hands = new LinkedList<Hand>();
    static char[] cards = {'A','K','Q','J','T','9','8','7','6','5','4','3','2'};
    
    public void Solve()
    {
        Int64 totalWinning = 0;
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day7\input.txt");
        string line;

        line = sr.ReadLine();

        while (line != null)
        {
            string[] parts = line.Split(" ");
            Hand hand = new Hand(parts[0], int.Parse(parts[1]));

            InsertHandInSortedList(hand);
            
            line = sr.ReadLine();
        }
        sr.Close();

        var currHand = hands.First;
        for (int i = hands.Count; i > 0; i--)
        {
            totalWinning += currHand.Value.bid * i;
            currHand = currHand.Next;
        }
        
        Console.WriteLine("The total winnings are " + totalWinning);
    }

    private void InsertHandInSortedList(Hand hand)
    {
        var current = hands.First;

        while (current != null)
        {
            if (hand.IsStronger(current.Value, cards))
            {
                hands.AddBefore(current, hand);
                return;
            }

            current = current.Next;
        }

        hands.AddLast(hand);
    }
}

//Just for clarification
public enum HandStrength
{
    FiveOfAKind = 7,
    FourOfAKind = 6,
    FullHouse = 5,
    ThreeOfAKind = 4,
    TwoPair = 3,
    Pair = 2,
    Nothing = 1,
}