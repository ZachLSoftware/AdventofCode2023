using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(@"C:\Users\zach.larsen\source\repos\AdventOfCode.Day4\input.txt");
            Day4Solution solution = new Day4Solution(input);
            Console.WriteLine(solution.PartTwo());

        }

        class Day4Solution
        {
            string input;
            string[] splitInput;
            int total = 0;
            Dictionary<string, int> cards = new Dictionary<string, int>();
            public Day4Solution(string input) {

                this.input = input;

                splitInput = input.Split('\n');

            }


            public int PartOne()
            {
                foreach (string line in splitInput)
                {
                    total += parseCard(line);
                }
                return total;
            }


            public int PartTwo()
            {
                getCardDict();
                for (int i = 0; i < splitInput.Length; i++)
                {
                    int x = parseCard(splitInput[i]);
                    int multiplyer = cards[cards.ElementAt(i).Key];

                    for (int j = i+1; j <= x+i; j++)
                    {
                        cards[cards.ElementAt(j).Key] += 1*multiplyer;
                    }
                }

                foreach (KeyValuePair<string, int> pair in cards)
                {
                    total += pair.Value;
                }
                
                return total;

            }

            public void getCardDict()
            {
                foreach (string line in splitInput)
                {
                    cards[line.Split(":")[0]] = 1;
                }
            }
            public int parseCard (string line){
                string card = line.Split(":")[0];
                string winningNumbers = line.Split(":")[1].Split("|")[0];
                string myNumbers = line.Split(":")[1].Split("|")[1];
                //return new string[] {card, winningNumbers, myNumbers};
                return getWinners2(winningNumbers, myNumbers);
            }

            public int getWinners(string winning, string numbers) { 
                int winners = 0;
                List<int> winNums = getNumbers(winning);
                List<int> myNums = getNumbers(numbers);
                foreach (int i in myNums)
                {
                    if (winNums.Contains(i))
                    {
                        if (winners == 0)
                        {
                            winners = 1;
                        }
                        else
                        {
                            winners *= 2;
                        }
                    }
                }
                return winners;
            }

            public void parseCard2(string line)
            {
                string card = line.Split(":")[0];

                string winningNumbers = line.Split(":")[1].Split("|")[0];
                string myNumbers = line.Split(":")[1].Split("|")[1];
                //return new string[] {card, winningNumbers, myNumbers};
                cards[card] = getWinners2(winningNumbers, myNumbers);
            }
            public int getWinners2(string winning, string numbers)
            {
                int winners = 0;
                List<int> winNums = getNumbers(winning);
                List<int> myNums = getNumbers(numbers);
                foreach (int i in myNums)
                {
                    if (winNums.Contains(i))
                    {
                        winners += 1;
                    }
                }
                return winners;
            }

            public List<int> getNumbers(string numbers)
            {
                List<int> result = new List<int>();
                MatchCollection matches = Regex.Matches(numbers, @"\d+");
                foreach (Match match in matches)
                {
                    result.Add(int.Parse(match.Value));
                }
                return result;
            }

            public void printInput()
            {
                string st = "";
                foreach (string line in splitInput)
                {
                    Console.WriteLine(line);
                }
            }

        }
    }
}
