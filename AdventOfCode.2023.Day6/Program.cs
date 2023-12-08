using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\input.txt");

            Day6Solution solution = new Day6Solution(input);
            solution.PartOne();
        }

        class Day6Solution
        {
            public string input;
            public List<Tuple<double, double>> races = new List<Tuple<double, double>>();

            public Day6Solution (string input)
            {
                this.input = input;
                parseRaces();
                //foreach (var race in races)
                //{
                 //   Console.WriteLine("Time: " + race.Item1 + ", Distance: " + race.Item2);
                //}
            }

            private void parseRaces()
            {
                string[] split = input.Split("\n");
                MatchCollection times = Regex.Matches(split[0].Split(":")[1].Replace(" ",""), @"\d+");
                MatchCollection distances = Regex.Matches(split[1].Split(":")[1].Replace(" ", ""), @"\d+");

                for (int i = 0; i < times.Count; i++)
                {
                    races.Add(new Tuple<double, double>(double.Parse(times[i].Value), double.Parse(distances[i].Value)));
                }

            }

            public void PartOne()
            {
                double total = 1;
                foreach (var race in races)
                {
                    total *= possibleTimes(race).Count;
                    //foreach (var pair in results)
                    //{
                    //    Console.WriteLine("Time: " + pair.Item1 + ", Distance: " + pair.Item2);
                    //}
                }
                Console.WriteLine(total);
            }

            public List<Tuple<double,double>> possibleTimes(Tuple<double,double> race)
            {
                List<Tuple<double,double>> raceOptions = new List<Tuple<double,double>>();

                double time = race.Item1;
                double distance = race.Item2;

                for (double i = 0; i < time+1; i++)
                {
                    double dist = i * (time - i);
                    if (dist > distance) { raceOptions.Add(new Tuple<double, double>(i, dist)); }
                }

                return raceOptions;

            }
        }
    }
}
