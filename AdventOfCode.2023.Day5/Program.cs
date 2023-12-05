using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day5
{
   internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\input.txt");

            Day5Solution solution = new Day5Solution(input);
            //solution.setDict();
            Console.WriteLine(solution.getClosestLocationFromSeed());
        }

        class Day5Solution
        {
            string input;
            string[] maps;
            List<double> seeds = new List<double>();
            Dictionary<string, string> allMaps = new Dictionary<string, string>();
            Dictionary<string, SortedDictionary<double, Tuple<double, double>>> mapToMap = new Dictionary<string, SortedDictionary<double, Tuple<double, double>>>();
            SortedDictionary<double, Tuple<double,double>> seedToSoil = new SortedDictionary<double, Tuple<double, double>>();
            SortedDictionary<double, Tuple<double, double>> soilToFertilizer = new SortedDictionary<double, Tuple<double, double>>();
            SortedDictionary<double, Tuple<double, double>> fertilizerToWater = new SortedDictionary<double, Tuple<double, double>>();
            SortedDictionary<double, Tuple<double, double>> waterToLight = new SortedDictionary<double, Tuple<double, double>>();
            SortedDictionary<double, Tuple<double, double>> lightToTemperature = new SortedDictionary<double, Tuple<double, double>>();
            SortedDictionary<double, Tuple<double, double>> temperatureToHumidity = new SortedDictionary<double, Tuple<double, double>>();
            SortedDictionary<double, Tuple<double, double>> humidityToLocation = new SortedDictionary<double, Tuple<double, double>>();

            public Day5Solution(string input) {
                this.input = input;
                maps = input.Split("\n\r");
                foreach (string s in maps)
                {
                    string key = s.Split(":")[0].Replace("\n","");
                    allMaps[key] = s.Split(":")[1].Replace("\r", "");

                    switch (key)
                    {
                        case ("seed-to-soil map"):
                            mapToMap[key] = seedToSoil;
                            break;
                        case ("soil-to-fertilizer map"):
                            mapToMap[key] = soilToFertilizer;
                            break;
                        case ("fertilizer-to-water map"):
                            mapToMap[key] = fertilizerToWater;
                            break;
                        case ("water-to-light map"):
                            mapToMap[key] = waterToLight;
                            break;
                        case ("light-to-temperature map"):
                            mapToMap[key] = lightToTemperature;
                            break;
                        case ("temperature-to-humidity map"):
                            mapToMap[key] = temperatureToHumidity;
                            break;
                        case ("humidity-to-location map"):
                            mapToMap[key] = humidityToLocation;
                            break;
                        default:
                            break;
                    }

                }
                parseMaps();

            }

            public void parseSeedList()
            {
                MatchCollection matches = Regex.Matches(allMaps["seeds"], @"\d+");

                for (int i = 0; i < matches.Count; i+=2) {
                    double start = double.Parse(matches[i].Value);
                    double end = start + double.Parse(matches[i + 1].Value);
                    for (double j = start; j < end; j++)
                    {
                        seeds.Add(j);
                    }
                }
            }

            public double getClosestLocationFromSeed()
            {

                double closest = 0;
                /*
                List<double> seeds = new List<double>();

                foreach (string s in allMaps["seeds"].Split(" "))
                {
                    if (s != "")
                    {
                        seeds.Add(double.Parse(s));
                    }
                }
                */

                MatchCollection matches = Regex.Matches(allMaps["seeds"], @"\d+");

                for (int i = 0; i < matches.Count; i += 2)
                {
                    double start = double.Parse(matches[i].Value);
                    double end = start + double.Parse(matches[i + 1].Value);
                    for (double j = start; j < end; j++)
                    {
                        double location = getLocationFromSeed(j);
                        if (location < closest || closest == 0)
                        {
                            closest = location;
                        }
                    }
                }

                /*
                double closest = getLocationFromSeed(seeds[0]);
                foreach (double seed in seeds)
                {
                    double location = getLocationFromSeed(seed);
                    if (location < closest)
                    {
                        closest = location;
                    }
                }
                */
                return closest;
            }

            public double getLocationFromSeed(double seed)
            {
                double location=-1;
                double soil;
                double fert;
                double water;
                double light;
                double temp;
                double humidity;

                soil = getTranslation(seedToSoil, seed);
                fert = getTranslation(soilToFertilizer, soil);
                water = getTranslation(fertilizerToWater, fert);
                light = getTranslation(waterToLight, water);
                temp = getTranslation(lightToTemperature, light);
                humidity = getTranslation(temperatureToHumidity, temp);
                location = getTranslation(humidityToLocation, humidity);

                return location;
            }

            public double getTranslation(SortedDictionary<double, Tuple<double, double>> dict, double seed)
            {
                double key = -1;

                foreach (KeyValuePair<double, Tuple<double, double>> kvp in dict)
                {
                    double max = kvp.Key + kvp.Value.Item2;
                    if (seed >= kvp.Key && seed <= max)
                    {
                        double shift = seed - kvp.Key;
                        key = kvp.Value.Item1 + shift;
                    }
                }
                if (key == -1)
                {
                    key = seed;
                }

                return key;
            }

            public void parseMaps()
            {
                double maxKey = 0;
                foreach (KeyValuePair<string, string> pair in allMaps)
                {
                    if (pair.Key == "seeds")
                    {
                        continue;
                    }
                    SortedDictionary<double, Tuple<double, double>> map = mapToMap[pair.Key];
                    foreach (string line in pair.Value.Split("\n")) {

                        if (line != "")
                        {
                            string[] numbers = line.Split(" ");
                            double dest = double.Parse(numbers[0]);
                            double source = double.Parse(numbers[1]);
                            double range = double.Parse(numbers[2]);

                            if (!map.ContainsKey(source))
                            {
                                map[source] = new Tuple<double, double>(dest, range);
                            }

                        }

                    }

                }
            }

        }

        

    }
}
