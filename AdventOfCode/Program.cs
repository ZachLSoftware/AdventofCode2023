using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string input = File.ReadAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\test.txt");
            Solution solution = new Solution(input, 13, 14, 12);
            Console.WriteLine(solution.PartOne());
        }

        class Solution {
            string input;
            Dictionary<string, int> maxValues = new Dictionary<string, int>();
            int total = 0;
            public Solution(string input, int green, int blue, int red)
            {
                this.input = input;
                maxValues.Add("green", green);
                maxValues.Add("red", red);
                maxValues.Add("blue", blue);

            }
            public int PartOne() {
                foreach (string line in input.Split('\n'))
                {
                   total += parse(line);
                }
                return total; 
            }

            public int PartTwo()
            {
                foreach (string line in input.Split('\n'))
                {
                    total += parse(line);
                }
                return total;
            }

            private int parse(string line) {
                Match match = Regex.Match(line, @"Game \d+");
                if (match.Success)
                {
                    int game;
                    int red;
                    int green;
                    int blue;
                    bool success = int.TryParse(match.Value.Split(" ")[1], out game);

                    if (success)
                    {
                        red = parseInt(Regex.Matches(line, @"(\d+) red"));
                        green = parseInt(Regex.Matches(line, @"(\d+) green"));
                        blue = parseInt(Regex.Matches(line, @"(\d+) blue"));

                        return red * green * blue;
                        /*
                        if (red <= maxValues["red"] && green <= maxValues["green"] && blue <= maxValues["blue"])
                        {
                            return game;
                        }
                        else
                        {
                            return 0;
                        }*/

                    }

                    
                }
                return 0;
            }

            private int parseInt(MatchCollection mc)
            {
                List<int> result = new List<int>();
                foreach (Match m in mc)
                {
                    result.Add(int.Parse(Regex.Match(m.Value.ToString(), @"\d+").Value));
                }
                return result.Max();
            }
        }
    }
}
