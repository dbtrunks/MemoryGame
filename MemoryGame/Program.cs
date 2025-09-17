namespace MemoryGame
{
    internal class Program
    {
        const int _matrixSize = 4;
        static string[,] tab = new string[_matrixSize, _matrixSize];
        static string[,] tabToShow = new string[_matrixSize, _matrixSize];
        static string first;
        static string second;
        static int[] firstId;
        static int[] secondId;
        static bool play = true;
        static string charCard = "#";
        static void Main(string[] args)
        {
            Console.WriteLine("Memory Game.");

            SetMemory();
            while (play)
            {
                Play();
            }
        }

        private static void Play()
        {
            string chose = Console.ReadLine();
            var point = chose.Split(',', StringSplitOptions.TrimEntries);
            if (Logic.CheckPoint(point, tab))
            {
                var value = tab[Convert.ToInt32(point[0]) - 1, Convert.ToInt32(point[1]) - 1];
                tabToShow[Convert.ToInt32(point[0]) - 1, Convert.ToInt32(point[1]) - 1] = value;
                Console.WriteLine($"Wybrałeś: {value}");
                Thread.Sleep(1200);

                if (string.IsNullOrEmpty(first))
                {
                    first = value;
                    firstId = [Convert.ToInt32(point[0]) - 1, Convert.ToInt32(point[1]) - 1];
                }
                else
                {
                    second = value;
                    secondId = [Convert.ToInt32(point[0]) - 1, Convert.ToInt32(point[1]) - 1];
                }

                if (first.Equals(second) && !Enumerable.SequenceEqual(firstId, secondId))
                {
                    tabToShow[firstId[0], firstId[1]] = first;
                    tabToShow[secondId[0], secondId[1]] = second;
                }
                else if (!string.IsNullOrEmpty(second))
                {
                    tabToShow[firstId[0], firstId[1]] = charCard;
                    tabToShow[secondId[0], secondId[1]] = charCard;
                }
                PrintTab(tabToShow);
            }

            if (!string.IsNullOrEmpty(second))
            {

                first = second = "";
                IsWin();
            }
        }

        private static void IsWin()
        {
            var win = true;
            for (int i = 0; i < tabToShow.GetLength(0); i++)
            {
                for (int j = 0; j < tabToShow.GetLength(1); j++)
                {
                    if (tabToShow[i, j].Equals(charCard))
                    {
                        win = false; break;
                    }
                }
            }

            if (win)
            {
                Console.WriteLine("Wygrałeś!!! Gratulacje....");
                play = false;
            }
        }

        private static void SetMemory()
        {
            if (_matrixSize % 2 != 0)
                throw new Exception("Wymiary tablicy musi być liczbą parzystą.");

            var pairs = new List<string>();
            for (int i = 1; i <= (_matrixSize * _matrixSize) / 2; i++)
            {
                pairs.Add(i.ToString());
                pairs.Add(i.ToString());
            }

            var random = new Random();
            pairs = pairs.OrderBy(x => random.Next()).ToList();

            int index = 0;
            for (int r = 0; r < _matrixSize; r++)
            {
                for (int c = 0; c < _matrixSize; c++)
                {
                    tab[r, c] = pairs[index++];
                    tabToShow[r, c] = charCard;
                }
            }

            PrintTab(tabToShow);
        }

        private static void PrintTab(string[,] tab)
        {
            Console.Clear();
            Console.Write("   \t");
            for (int i = 1; i <= tab.GetLength(0); i++)
            {
                Console.Write("[{0}]\t", i);
            }

            Console.Write("\n");
            for (int r = 0; r < tab.GetLength(0); r++)
            {
                Console.Write("[{0}]\t", r + 1);
                for (int c = 0; c < tab.GetLength(1); c++)
                {
                    Console.Write("{0}\t", tab[r, c]);
                }
                Console.Write("\n\n");
            }
        }
    }
}
