namespace day8;

public class Puzzle2
{

    public void Solve()
    {
        List<Node> startingNodes = new List<Node>();
        HashSet<int> endingIndexes = new HashSet<int>();

        string navigations;
        Dictionary<string, Node> nodeNetwork = new Dictionary<string, Node>();
        StreamReader sr = new StreamReader(@"C:\Users\flind\Desktop\adventofcode2023\day8\input.txt");
        string line;

        navigations = sr.ReadLine();
        sr.ReadLine();
        line = sr.ReadLine();

        while (line != null)
        {
            string[] parts = line.Replace(" ", "").Split("=");
            string leftStrNode = parts[1].Split(",")[0].Replace("(", "");
            string rightStrNode = parts[1].Split(",")[1].Replace(")", "");
            Node? leftNode = null;
            Node? rightNode = null;

            if (leftStrNode != parts[0])
            {
                if (nodeNetwork.TryGetValue(leftStrNode, out var value1))
                {
                    leftNode = value1;
                }
                else
                {
                    leftNode = new Node();
                    nodeNetwork.Add(leftStrNode, leftNode);
                }
            }

            if (rightStrNode != parts[0])
            {
                if (nodeNetwork.TryGetValue(rightStrNode, out var value))
                {
                    rightNode = value;
                }
                else
                {
                    rightNode = new Node();
                    nodeNetwork.Add(rightStrNode, rightNode);
                }
            }


            if (nodeNetwork.TryGetValue(parts[0], out var n))
            {
                n.RightNode = rightNode;
                n.LeftNode = leftNode;
            }
            else
            {
                Node newNode = new Node();
                newNode.RightNode = rightNode;
                newNode.LeftNode = leftNode;
                nodeNetwork.Add(parts[0], newNode);
            }

            if (parts[0][2] == 'A')
            {
                startingNodes.Add(nodeNetwork[parts[0]]);
            }
            else if (parts[0][2] == 'Z')
            {
                Node no = nodeNetwork[parts[0]];
                no.EndNode = true;
            }

            line = sr.ReadLine();
        } //NETWORK CREATED

        sr.Close();
        
        //Calculate steps X until endNode for all startNodes
        //They will reach the end node in cycles of length X
        //Find the LCM of all X

        HashSet<int> primes = new HashSet<int>();

        foreach (var node in startingNodes)
        {
            AddPrimeFactors(StepsToFirstZ(node, navigations), primes);
        }

        Int128 res = primes.Aggregate(1L, (curr, next) => curr * next); 
        
        Console.WriteLine("All startNodes will hit endNodes after " + res + " steps");

    }
    
    private void AddPrimeFactors(int n, HashSet<int> primes)
    {
        
        while (n % 2 == 0)
        {
            primes.Add(2);
            n /= 2;
        }

        // n must be odd at this point, so we can skip one element
        for (int i = 3; i <= Math.Sqrt(n); i += 2)
        {
            // While i divides n, print i and divide n
            while (n % i == 0)
            {
                primes.Add(i);
                n /= i;
            }
        }

        primes.Add(n);
    }

    private int StepsToFirstZ(Node n, string instructions)
    {
        Node currNode = n;
        int steps = 0;
        bool completed = false;

        while (!completed)
        {
            char instruction = instructions[steps % instructions.Length];
            
            if (instruction == 'L')
            {
                if (currNode.LeftNode != null) currNode = currNode.LeftNode;
            }
            else if (instruction == 'R')
            {
                if (currNode.RightNode != null) currNode = currNode.RightNode;
            }

            steps++;
            if (currNode.EndNode)
            {
                completed = true;
            }
        }

        return steps;
    }
}