namespace day7;

public class Puzzle2
{
    LinkedList<Hand> hands = new LinkedList<Hand>();
    static char[] cards = { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

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

public class Hand
{
    public string hand;
    public int str;
    public int bid;

    public Hand(string hand, int bid)
    {
        this.hand = hand;
        this.bid = bid;
        str = InitHandStrength();
    }

    private int InitHandStrength()
    {
        int jokers = 0;

        foreach (var card in hand)
        {
            if (card == 'J') jokers++;
        }

        switch (jokers)
        {
            case 5:
                return 7;
            case 4:
                return 7;
            case 3:
                return CalcStrength3Jokers();
            case 2:
                return CalcStrength2Jokers();
            case 1:
                return CalcStrength1Jokers();
            default:
                return CalcStrength0Jokers();
        }
    }

    private int CalcStrength3Jokers()
    {
        var pair = hand.Where(c => c != 'J');
        if (pair.First() == pair.Last())
        {
            //5ofAKind
            return 7;
        }
        //4ofAKind
        return 6;
    }

    private int CalcStrength2Jokers()
    {
        foreach (var card in hand.Where(c => c != 'J'))
        {
            int kind = hand.Count(c => c == card);
            if (kind == 3) return 7;
            if (kind == 2) return 6;


            var pair = hand.Where(c => c != card && c != 'J');
            if (pair.First() == pair.Last())
            {
                //4ofAKind
                return 6;
            }

            //ThreeOfAKind
            return 4;
        }

        throw new Exception("Hand has no cards");
    }

    private int CalcStrength1Jokers()
    {
        foreach (var card in hand.Where(c => c != 'J'))
        {
            int kind = hand.Count(c => c == card);
            if (kind == 4) return 7;
            if (kind == 3) return 6;
            if (kind == 2)
            {
                var pair = hand.Where(c => c != card && c != 'J');
                if (pair.First() == pair.Last())
                {
                    //Fullhouse
                    return 5;
                }

                //ThreeOfAKind
                return 4;
            }


            var distinctsCards = hand.Where(c => c != card && c != 'J').Distinct().Count();
            if (distinctsCards == 1)
            {
                //4ofAKind
                return 6;
            }
            else if (distinctsCards == 2)
            {
                //3ofAKind
                return 4;
            }

            return 2;
        }

        throw new Exception("Hand has no cards");
    }

    private int CalcStrength0Jokers()
    {
        foreach (var card in hand)
        {
            int kind = hand.Count(c => c == card);
            if (kind == 5) return 7;
            if (kind == 4) return 6;
            if (kind == 3)
            {
                var pair = hand.Where(c => c != card);
                if (pair.First() == pair.Last())
                {
                    //Fullhouse
                    return 5;
                }

                //ThreeOfAKind
                return 4;
            }

            if (kind == 2)
            {
                var distinctsCards = hand.Where(c => c != card).Distinct().Count();
                if (distinctsCards == 1)
                {
                    //Full house
                    return 5;
                }
                else if (distinctsCards == 2)
                {
                    //Two pair
                    return 3;
                }

                return 2;
            }
        }

        return 1;
    }

    public bool IsStronger(Hand target, char[] cards)
    {
        if (str > target.str)
        {
            return true;
        }

        if (str < target.str)
        {
            return false;
        }

        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == target.hand[i]) continue;
            int myIndex = Array.IndexOf(cards, hand[i]);
            int targetIndex = Array.IndexOf(cards, target.hand[i]);
            if (myIndex < targetIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        throw new ArgumentException("The hands are the exact same. What to do sarge?");
    }
}