namespace day8;

//DOES NOT WORK. NEED LIKE A QUANTUM COMPUTER OR SOME SHIT
public class Puzzle2Brute
{
    private readonly int ITERATIONS = 90000000; //Defines how many times the instructions should be executed

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

        //start case
        Node first = startingNodes.First();
        endingIndexes = EndStepsToZZZ(first, navigations);
        startingNodes.Remove(first);

        //All other cases
        foreach (var node in startingNodes)
        {
            endingIndexes = UpdateEndIndexWithNode(endingIndexes, node, navigations);
        }

        if (endingIndexes.Count == 0)
        {
            Console.WriteLine("To few iterations, no paths hit simontainously. MORE!");
        }
        else
        {
            Console.WriteLine("It took " + endingIndexes.Min() + " steps to reach ZZZ for all paths");
        }
    }

    private HashSet<int> UpdateEndIndexWithNode(HashSet<int> currEndSteps, Node startingNode, string instructions)
    {
        HashSet<int> nodeEndSteps = EndStepsToZZZ(startingNode, instructions);

        //All endSteps that intersect with existing endSteps are kept
        var nodeEndStepsIntersect = currEndSteps.Intersect(nodeEndSteps);

        var res = nodeEndStepsIntersect.ToHashSet();

        return res;
    }

    private HashSet<int> EndStepsToZZZ(Node startingNode, string instructions)
    {
        HashSet<int> nodeEndSteps = new HashSet<int>();
        Node currNode = startingNode;
        int steps = 0;

        for (int i = 0; i < ITERATIONS; i++)
        {
            foreach (var instruction in instructions)
            {
                if (instruction == 'L')
                {
                    if (currNode.LeftNode != null) currNode = currNode.LeftNode;
                }

                if (instruction == 'R')
                {
                    if (currNode.RightNode != null) currNode = currNode.RightNode;
                }

                steps++;
                if (currNode.EndNode)
                {
                    nodeEndSteps.Add(steps);
                }
            }
        }

        return nodeEndSteps;
    }
}