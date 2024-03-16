using System;
using System.Collections.Generic;
using System.Linq;
internal class Program
{
    private static void Main(string[] args)
    {
        List<List<int>> square = new List<List<int>>(){
            new List<int>{9,0,0,4,0,0,0,0,0},
            new List<int>{0,0,0,6,0,0,1,0,7},
            new List<int>{0,8,7,9,1,5,3,0,2},

            new List<int>{4,0,0,0,0,2,0,8,0},
            new List<int>{0,6,0,5,0,0,7,0,0,},
            new List<int>{5,0,0,7,6,0,0,1,0},

            new List<int>{0,0,5,0,0,0,0,2,0},
            new List<int>{0,0,0,0,0,9,8,0,1},
            new List<int>{0,9,0,2,0,0,0,0,3},
        };

        if (!IsSolvable(square))
        {
            System.Console.WriteLine("It is not solvable");
        }
        else
        {
            var numberCount = InitialNumberCount(square);
            PlaceOnePossibles(square, numberCount, FillPossibilites(square, numberCount));
            Backtracking(square, FillPossibilites(square, numberCount));
            //BacktrackingFromZero(square);

            foreach (var item in square)
            {
                foreach (var i in item)
                {
                    System.Console.Write(i + " ");
                }
                System.Console.WriteLine();
            }
        }
    }

    public static void PlaceOnePossibles(List<List<int>> square, List<int> numberCount, Dictionary<int, List<int>> possibilities)
    {
        int size = square.Count;
        foreach (var item in possibilities)
        {
            if (item.Value.Count == 1)
            {
                int i = item.Key / size;
                int j = item.Key % size;
                square[i][j] = item.Value[0];
                numberCount[item.Value[0] - 1] = numberCount[item.Value[0] - 1] - 1;
                PlaceOnePossibles(square, numberCount, FillPossibilites(square, numberCount));
            }
        }
    }
    private static void Backtracking(List<List<int>> square, Dictionary<int, List<int>> possibilities)
    {
        int size = square.Count;
        List<bool> valid = new List<bool>();
        List<int> keys = possibilities.Keys.ToList();
        for (int n = 0; n < keys.Count; n++)
        {
            valid.Add(false);
        }
        int f = 0;
        while (!IsAllValid(valid))
        {
            int key = keys[f];
            int i = key / size;
            int j = key % size;
            int index = 0;
            if (possibilities[key].IndexOf(square[i][j]) != -1)
            {
                index = possibilities[key].IndexOf(square[i][j]) + 1;
            }
            while (index < possibilities[key].Count)
            {
                if (IsValid(square, i, j, possibilities[key][index]))
                {
                    square[i][j] = possibilities[key][index];
                    valid[f] = true;
                    f++;
                    break;
                }
                index++;
            }
            if (index == possibilities[key].Count)
            {
                square[i][j] = 0;
                valid[f] = false;
                if (f > 0)
                {
                    f--;
                }
            }
            foreach (var item in square)
            {
                foreach (var l in item)
                {
                    System.Console.Write(l + " ");
                }
                System.Console.WriteLine();
            }
            System.Console.Clear();
        }
    }
    private static void BacktrackingFromZero(List<List<int>> square)
    {
        List<int> pos = new List<int>();
        int size = square.Count;
        for (int i = 1; i <= size; i++)
        {
            pos.Add(i);
        }
        Dictionary<int, List<int>> possibilities = new Dictionary<int, List<int>>();
        for (int i = 0; i < size * size; i++)
        {
            int n = i / size;
            int m = i % size;
            if (square[n][m] == 0)
            {
                possibilities[i] = pos;
            }
        }

        List<bool> valid = new List<bool>();
        List<int> keys = possibilities.Keys.ToList();
        for (int n = 0; n < keys.Count; n++)
        {
            valid.Add(false);
        }
        int f = 0;
        while (!IsAllValid(valid))
        {
            int key = keys[f];
            int i = key / size;
            int j = key % size;
            int index = 0;
            if (possibilities[key].IndexOf(square[i][j]) != -1)
            {
                index = possibilities[key].IndexOf(square[i][j]) + 1;
            }
            while (index < possibilities[key].Count)
            {
                if (IsValid(square, i, j, possibilities[key][index]))
                {
                    square[i][j] = possibilities[key][index];
                    valid[f] = true;
                    f++;
                    break;
                }
                index++;
            }
            if (index == possibilities[key].Count)
            {
                square[i][j] = 0;
                valid[f] = false;
                if (f > 0)
                {
                    f--;
                }
            }
            foreach (var item in square)
            {
                foreach (var l in item)
                {
                    System.Console.Write(l + " ");
                }
                System.Console.WriteLine();
            }
            System.Console.Clear();
        }

    }
    private static bool IsSolvable(List<List<int>> square)
    {
        int size = square.Count;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (square[i][j] != 0)
                {
                    if (!IsValid(square, i, j, square[i][j]))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    private static bool IsAllValid(List<bool> valid)
    {
        foreach (var item in valid)
        {
            if (item == false)
            {
                return false;
            }
        }
        return true;
    }
    private static bool IsValid(List<List<int>> square, int i, int j, int num)
    {
        int size = square.Count;
        for (int k = 0; k < size; k++)
        {
            if (square[i][k] == num && k != j)
            {
                return false;
            }
            if (square[k][j] == num && k != i)
            {
                return false;
            }
        }
        int sqrt = Convert.ToInt32(Math.Sqrt(size));
        int bigi = (i / sqrt) * sqrt;
        int bigj = (j / sqrt) * sqrt;
        for (int l = bigi; l < bigi + sqrt; l++)
        {
            for (int m = bigj; m < bigj + sqrt; m++)
            {
                if (square[l][m] == num && l != i && m != j)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private static Dictionary<int, List<int>> FillPossibilites(List<List<int>> square, List<int> numberCount)
    {
        int size = square.Count;
        Dictionary<int, List<int>> possibilities = new Dictionary<int, List<int>>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (square[i][j] == 0)
                {
                    possibilities.Add(i * size + j, GetPossibleDigits(square, numberCount, i, j));
                }
            }
        }
        return possibilities;
    }
    private static List<int> GetPossibleDigits(List<List<int>> square, List<int> numberCount, int i, int j)
    {
        if (square[i][j] != 0)
        {
            return new List<int>() { square[i][j] };
        }

        List<int> poss = new List<int>();
        int size = square.Count;
        for (int l = 1; l <= size; l++)
        {
            poss.Add(l);
        }
        List<int> runOutNumbers = GetRunOutNumbers(numberCount);

        foreach (var ron in runOutNumbers)
        {
            poss.Remove(ron);
        }
        int sqrt = Convert.ToInt32(Math.Sqrt(size));
        int bigi = (i / sqrt) * sqrt;
        int bigj = (j / sqrt) * sqrt;
        for (int k = 0; k < size; k++)
        {
            poss.Remove(square[i][k]);
            poss.Remove(square[k][j]);
        }
        for (int l = bigi; l < bigi + sqrt; l++)
        {
            for (int m = bigj; m < bigj + sqrt; m++)
            {
                poss.Remove(square[l][m]);
            }
        }
        return poss;
    }
    private static List<int> InitialNumberCount(List<List<int>> square)
    {
        List<int> numberCount = new List<int>();
        int size = square.Count;
        for (int i = 1; i <= size; i++)
        {
            numberCount.Add(size);
        }

        foreach (var i in square)
        {
            foreach (var item in i)
            {
                if (item != 0)
                {
                    numberCount[item - 1] = numberCount[item - 1] - 1;
                }

            }
        }
        return numberCount;
    }
    private static List<int> GetRunOutNumbers(List<int> numberCount)
    {
        List<int> runOutNumbers = new List<int>();
        for (int i = 0; i < numberCount.Count; i++)
        {
            if (numberCount[i] == 0)
            {
                runOutNumbers.Add(i + 1);
            }
        }
        return runOutNumbers;
    }
}
