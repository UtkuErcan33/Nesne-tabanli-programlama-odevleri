using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Kullanıcıdan grid boyutunu al
        Console.Write("Grid boyutunu (N x N) girin: ");
        int N = int.Parse(Console.ReadLine());

        // Grid'i tanımla ve kullanıcıdan elemanları al
        int[,] grid = new int[N, N];
        Console.WriteLine("Grid elemanlarını girin (1 veya 0):");
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write($"Grid[{i},{j}]: ");
                grid[i, j] = int.Parse(Console.ReadLine());
            }
        }

        // Robotların başlangıç pozisyonlarını kullanıcıdan al
        List<Tuple<int, int>> robotStartPositions = new List<Tuple<int, int>>();
        Console.Write("Kaç robot var? ");
        int robotSayisi = int.Parse(Console.ReadLine());

        for (int i = 0; i < robotSayisi; i++)
        {
            Console.Write($"Robot {i + 1} başlangıç satırı: ");
            int startRow = int.Parse(Console.ReadLine());
            Console.Write($"Robot {i + 1} başlangıç sütunu: ");
            int startCol = int.Parse(Console.ReadLine());
            robotStartPositions.Add(Tuple.Create(startRow, startCol));
        }

        // Robotların kaç düğüm kurtardığını hesapla
        int totalSavedNodes = RobotlarIleKurtar(grid, robotStartPositions);

        Console.WriteLine($"Toplam kurtarılan düğüm sayısı: {totalSavedNodes}");

        // Programın kapanmaması için bekle
        Console.WriteLine("Programı kapatmak için bir tuşa basın...");
        Console.ReadKey();  // Programın hemen kapanmasını engeller
    }

    // Yönler: yukarı, aşağı, sol, sağ
    static int[] dRow = { -1, 1, 0, 0 };
    static int[] dCol = { 0, 0, -1, 1 };

    // BFS ile düğümleri dolaşma
    static int BFS(int[,] grid, bool[,] visited, int startRow, int startCol)
    {
        int N = grid.GetLength(0);
        int savedNodes = 0;
        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        queue.Enqueue(Tuple.Create(startRow, startCol));
        visited[startRow, startCol] = true;

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            int row = node.Item1;
            int col = node.Item2;

            savedNodes++; // Bu düğümü kurtardık

            // 4 yöne git
            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dRow[i];
                int newCol = col + dCol[i];

                // Sınırların içinde mi ve ziyaret edilmemiş zarar görmemiş düğüm mü?
                if (newRow >= 0 && newRow < N && newCol >= 0 && newCol < N &&
                    grid[newRow, newCol] == 1 && !visited[newRow, newCol])
                {
                    queue.Enqueue(Tuple.Create(newRow, newCol));
                    visited[newRow, newCol] = true; // Ziyaret edildi olarak işaretle
                }
            }
        }

        return savedNodes;
    }

    // Robotların kaç düğüm kurtardığını hesapla
    static int RobotlarIleKurtar(int[,] grid, List<Tuple<int, int>> robotStartPositions)
    {
        int N = grid.GetLength(0);
        bool[,] visited = new bool[N, N]; // Ziyaret edilen düğümleri işaretleyeceğimiz bir grid
        int totalSavedNodes = 0;

        // Her robot için BFS çalıştır ve kurtarılan düğümleri topla
        foreach (var robotStart in robotStartPositions)
        {
            int startRow = robotStart.Item1;
            int startCol = robotStart.Item2;

            // Robotun başlangıç pozisyonu zarar görmemiş bir düğümdeyse BFS başlat
            if (grid[startRow, startCol] == 1 && !visited[startRow, startCol])
            {
                totalSavedNodes += BFS(grid, visited, startRow, startCol);
            }
        }

        return totalSavedNodes;
    }
}
