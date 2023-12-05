using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(@"C:\Users\zach.larsen\source\repos\AdventOfCode.2023.Day3\input.txt");

            Day3Solution solution = new Day3Solution(input);
            Console.WriteLine(solution.PartTwo());

        }

        class Day3Solution
        {
            string input;
            int length;

            public Day3Solution(string input)
            {
                this.input = input;
            }

            public int PartOne()
            {
                string[] lines = input.Split("\n");
                length = lines[0].Length;
                string pattern = @"[^a-zA-Z0-9.\r\n]";
                int total = 0;

                for (int i = 0; i < lines.Length; i++)
                {
                    HashSet<int> symbolIndex = new HashSet<int>();
                    MatchCollection symbols;
                    if (i != 0)
                    {
                        symbols = Regex.Matches(lines[i-1], pattern);
                        foreach (Match item in symbols)
                        {
                            symbolIndex.Add(item.Index);
                        }
                    }
                    symbols = Regex.Matches(lines[i], pattern);
                    foreach (Match item in symbols)
                    {
                        symbolIndex.Add(item.Index);
                    }
                    if (i != lines.Length-1)
                    {
                        symbols = Regex.Matches(lines[i + 1], pattern);
                        foreach (Match item in symbols)
                        {  
                            symbolIndex.Add(item.Index);
                        }
                    }

                    foreach (int x in symbolIndex)
                    {
                        Console.Write(x.ToString() + ", ");
                    }
                    MatchCollection numbers = Regex.Matches(lines[i], @"(\d+)");
                    foreach (Match number in numbers)
                    {
                        int start = number.Index;
                        int end = start + number.Length;

                        Console.WriteLine("Number: " + number.Value + " range: " + start + " - " + end);

                        bool isSymbolNear = Enumerable.Range(start-1, end-start +2).Any(num => symbolIndex.Contains(num));


                        if (isSymbolNear)
                        {
                            Console.WriteLine("ADDING " + number.Value + " At Index " + start + "-" + end);
                            total += int.Parse(number.Value);
                            //Console.WriteLine("");
                        }
                    }
                }
                return total;
            }

            public int PartTwo()
            {
                int total = 0;
                string[] lines = input.Split("\n");
                length = lines[0].Length;

                for (int i = 0; i < lines.Length; i++)
                {
                    MatchCollection matches = Regex.Matches(lines[i], @"\*");
                    foreach (Match match in matches)
                    {
                        if (match.Success)
                        {
                            List<int> numbers = new List<int>();
                            foreach (Match num in Regex.Matches(lines[i - 1], @"\d+"))
                            {
                                if (match.Index >= num.Index - 1 && num.Index + num.Length >= match.Index)
                                {
                                    numbers.Add(int.Parse(num.Value));
                                }
                            }
                            foreach (Match num in Regex.Matches(lines[i], @"\d+"))
                            {
                                if (match.Index >= num.Index - 1 && num.Index + num.Length >= match.Index)
                                {
                                    numbers.Add(int.Parse(num.Value));
                                }
                            }
                            foreach (Match num in Regex.Matches(lines[i + 1], @"\d+"))
                            {
                                if (match.Index >= num.Index - 1 && num.Index + num.Length >= match.Index)
                                {
                                    numbers.Add(int.Parse(num.Value));
                                }
                            }
                            foreach (int number in numbers)
                            {
                                Console.WriteLine(number);
                            }
                            if (numbers.Count == 2)
                            {
                                Console.WriteLine(numbers[0] + ", " + numbers[1]);
                                total += (numbers[0] * numbers[1]);
                            }
                        }
                    }
                }

                return total;
            }

        }
    }
}
