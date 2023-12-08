using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\test.txt");
            Day8Solution solution = new Day8Solution(input);
        }

        class Day8Solution
        {
            public string input;
            public string instructions;
            public Dictionary<string, Tuple<string, string>> nodes = new Dictionary<string, Tuple<string, string>>();
            public List<string> startingNodes = new List<string>();

            public Day8Solution(string input) 
            { 
                this.input = input;
                instructions = input.Split("\r\n\r\n")[0];
                parseNodes(input.Split("\r\n\r\n")[1]);
                Console.WriteLine(findPath());
            }

            private void parseNodes(string nodestring)
            {
                foreach (string node in nodestring.Split("\n"))
                {
                    MatchCollection matches = Regex.Matches(node, @"[A-Z0-9]{3}");
                    nodes[matches[0].Value] = new Tuple<string, string>(matches[1].Value, matches[2].Value);
                    if (matches[0].Value[2] == 'A')
                    {
                        startingNodes.Add(matches[0].Value);
                    }
                }
            }

            private double findPath()
            {
                bool found = false;
                int count = 0;
                //string currentNode = "AAA";
                List<string> currentNodes = startingNodes;
                List<double> results = new List<double>();
                
                foreach (string node in currentNodes)
                {
                    results.Add(getLast(node));
                }

                //while (!found)
                //{
                //    foreach (char c in instructions)
                //    {
                //        List<string> nextNodes = new List<string>();
                //        foreach (string node in currentNodes)
                //        {
                //            string nextNode = getNext(node, c);
                //            nextNodes.Add(nextNode);
                //        }
                //        found = nextNodes.All(node => node.EndsWith('Z'));
                //        currentNodes = nextNodes;
                //        count++;

                //    }
                //}

                return results.Aggregate(LCM);
            }

            private int getLast(string startNode)
            {
                HashSet<(string, int)> steps = new();
                var curr = (node: startNode, step: 0);
                while (steps.Add(curr))
                {
                    var nextNode = instructions[curr.step] == 'L' ? nodes[curr.node].Item1 : nodes[curr.node].Item2;
                    curr = (nextNode, (curr.step + 1) % instructions.Length);
                    
                }
                return steps.Count - curr.step;
            }

            private double LCM (double x, double y)
            {
                return x / GCD(x, y) * y;
            }

            private double GCD(double x, double y)
            {
               return y == 0 ? x : GCD(y, x % y);
            }
            private string getNext(string node, char c)
            {
                return c == 'L' ? nodes[node].Item1 : nodes[node].Item2;
            }
        }
    }
}
