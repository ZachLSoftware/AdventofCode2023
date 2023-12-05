using System.Text.RegularExpressions;

namespace advent_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int part1() {

                Console.WriteLine("TESTING");
                int total = 0;
                var pattern1 = @"\d";
                var pattern2 = @"one|two|three|four|five|six|seven|eight|nine|[0-9]";
                Regex regex = new Regex(pattern2, RegexOptions.IgnoreCase);
                string line;
                try
                {
                    StreamReader sr = new StreamReader(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\test.txt");
                    line= sr.ReadLine();

                    while (line != null)
                    {
  
                        var first = Regex.Match(line, pattern2);
                        var last = Regex.Match(line, pattern2, RegexOptions.RightToLeft);
                        int x = stringToInt(first.Value) * 10;
                        int y = stringToInt(last.Value);
                        total += x + y;

                        line = sr.ReadLine();
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }


                return total;


            }

            int stringToInt(string s)
            {
                int i;

                bool parse = int.TryParse(s, out i);

                if (parse) {
                    return i;
                }
                else { 
                    switch (s)
                    {
                        case "one":
                            return 1;
                        case "two":
                            return 2;
                        case "three":
                            return 3;
                        case "four":
                            return 4;
                        case "five":
                            return 5;
                        case "six":
                            return 6;
                        case "seven":
                            return 7;
                        case "eight":
                            return 8;
                        case "nine":
                            return 9;
                        default:
                            return 0;
                    }
                }

                
            }

            Console.WriteLine(part1());
        }
    }
}
