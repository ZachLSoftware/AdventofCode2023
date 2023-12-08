using System.ComponentModel.DataAnnotations;

namespace AdventOfCode._2023.Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\input.txt");
            Day7Solution solution = new Day7Solution(input);
            solution.PartOne();
        }

        class Day7Solution
        {
            public string input;
            public Dictionary<string,double> cardBets = new Dictionary<string,double>();
            public List<string> cards = new List<string>();
            public Dictionary<char, int> cardValues = new Dictionary<char, int>
            {
                { 'A', 14 },
                { 'K', 13 },
                { 'Q', 12 },
                { 'J', 1 },
                { 'T', 10 },
                { '9', 9 },
                { '8', 8 },
                { '7', 7 },
                { '6', 6 },
                { '5', 5 },
                { '4', 4 },
                { '3', 3 },
                { '2', 2 },

            };
            public Dictionary<string, int> handValues = new Dictionary<string, int>
            {
                { "five_of_a_kind", 7 },
                { "four_of_a_kind", 6 },
                { "full_house", 5 },
                { "three_of_a_kind", 4 },
                { "two_pair", 3 },
                { "one_pair", 2 },
                { "high_card", 1 }
            };

            public Day7Solution(string input) 
            { 
                this.input = input;
                parseCardBets();
            }

            private void parseCardBets() 
            {
                foreach (string line in input.Split("\n")){
                    string card = line.Split(" ")[0];
                    int bet = int.Parse(line.Split(" ")[1]);
                    cardBets[card] = bet;
                    cards.Add(card);
                }
            }

            public int customHandSort(string card1, string card2)
            {
                int hand1 = getHand(card1);
                int hand2 = getHand(card2);
                if (hand1 > hand2)
                {
                    return 1;
                }
                else if (hand1 < hand2)
                {
                    return -1;
                }
                else
                {
                    for (int i = 0; i < card1.Length; i++)
                    {
                        if (cardValues[card1[i]] > cardValues[card2[i]])
                        {
                            return 1;
                        }
                        else if (cardValues[card1[i]] < cardValues[card2[i]])
                        {
                            return -1;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return 0;
            }

            public int getHand(string card)
            {
                Dictionary<char,int> freq = new Dictionary<char,int>();
                foreach (char ch in card)
                {
                    if (freq.ContainsKey(ch))
                    {
                        freq[ch] += 1;
                    }
                    else { freq[ch] = 1; }
                }

                //Added for Part 2
                if (freq.ContainsKey('J') && freq.Count!=1)
                {
                    int jFreq = freq['J'];
                    freq.Remove('J');
                    int max = freq.Values.Max();
                    List<char> keys = freq.Where(pair => pair.Value == max).Select(pair => pair.Key).ToList();
                    if (keys.Count == 1)
                    {
                        freq[keys[0]] += jFreq;
                    }
                    else
                    {
                        keys.Sort(singleCardSort);
                        freq[keys[0]] += jFreq;
                    }
                }

                switch (freq.Count)
                {
                    case (1):
                        return 7;
                    case (2):
                        if (freq.ElementAt(0).Value==1 || freq.ElementAt(0).Value == 4)
                        {
                            return 6;
                        }
                        else
                        {
                            return 5;
                        }
                    case (3):
                        if (freq.ContainsValue(3))
                        {
                            return 4;
                        }
                        else { return 3; }
                    case (4):
                        return 2;
                    default: return 1;

                }
            }

            private int singleCardSort(char c1, char c2)
            {

                    if (cardValues[c1] > cardValues[c2])
                    {
                        return 1;
                    }
                    else if (cardValues[c1] < cardValues[c2])
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                
            }

            private double getWinnings()
            {
                double winnings = 0;
                for (int i = 0; i<cards.Count; i++)
                {
                    winnings += (cardBets[cards[i]] * (i + 1));
                }
                return winnings;
            }
            public void PartOne()
            {
                cards.Sort(customHandSort);
                foreach (string card in cards)
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine(getWinnings());
            }
        }
    }
}
