namespace day8;

public class Puzzle1
{
    public void Solve()
    {
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

            line = sr.ReadLine();
        } //NETWORK CREATED
        
        Console.WriteLine("It took " + CountSteps(nodeNetwork, navigations) + " steps to reach ZZZ");
    }

    private int CountSteps(Dictionary<string, Node> network, string instructions)
    {
        Node currNode = network["AAA"];
        bool completed = false;
        int steps = 0;

        while (completed != true)
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
                if (network["ZZZ"] == currNode)
                {
                    completed = true;
                    break;
                }
            }
        }

        return steps;
    }
}

public class Node
{
    public Node? LeftNode;
    public Node? RightNode;
}