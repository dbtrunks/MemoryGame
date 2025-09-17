namespace MemoryGame
{
    internal static class Logic
    {
        internal static string[] RandomizeWithFisherYates(string[] array)
        {
            int count = array.Length;
            while (count > 1)
            {
                int i = Random.Shared.Next(count--);
                (array[i], array[count]) = (array[count], array[i]);
            }
            return array;
        }

        internal static bool CheckPoint(string[] point, string[,] tab)
        {
            if (point.Length != 2)
            {
                Console.WriteLine("Niepoprawny format. Użyj kombinacji: 'wiersz,kolumna'");
                return false;
            }

            int row, col;
            if (!int.TryParse(point[0], out row) || !int.TryParse(point[1], out col))
            {
                Console.WriteLine("Wprowadzone wartości nie są liczbami.");
                return false;
            }

            if (row < 1 || row > tab.GetLength(0) || col < 1 || col > tab.GetLength(1))
            {
                Console.WriteLine("Indeks poza zakresem tabeli. Spróbuj jeszcze raz.");
                return false;
            }

            return true;
        }
    }
}
