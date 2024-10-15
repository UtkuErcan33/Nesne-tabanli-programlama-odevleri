using System;
using System.Collections.Generic;

class Program
{
    // Yönler: yukarı, aşağı, sol, sağ
    static int[] dRow = { -1, 1, 0, 0 };
    static int[] dCol = { 0, 0, -1, 1 };

    static void Main()
    {
        // Kullanıcıdan labirent boyutunu al
        Console.Write("Labirentin boyutunu (N x N) girin: ");
        int N = int.Parse(Console.ReadLine());

        // Labirenti tanımla ve kullanıcıdan elemanları al
        int[,] labirent = new int[N, N];
        Console.WriteLine("Labirent elemanlarını girin (1 veya 0):");
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write($"Labirent[{i},{j}]: ");
                labirent[i, j] = int.Parse(Console.ReadLine());
            }
        }

        // En kısa yolu bul ve sonucu yazdır
        int adimSayisi = EnKisaYoluBul(labirent, N);
        if (adimSayisi == -1)
        {
            Console.WriteLine("Yol Yok");
        }
        else
        {
            Console.WriteLine($"En Kısa Yol: {adimSayisi} adım");
        }

        // Programın kapanmaması için bekle
        Console.WriteLine("Programı kapatmak için bir tuşa basın...");
        Console.ReadKey();  // Programın hemen kapanmasını engeller
    }

    // En kısa yolu bulmak için BFS algoritması
    static int EnKisaYoluBul(int[,] labirent, int N)
    {
        // Başlangıç veya bitiş noktasına ulaşılamazsa
        if (labirent[0, 0] == 0 || labirent[N - 1, N - 1] == 0)
        {
            return -1; // Yol yok
        }

        // Ziyaret edilen hücreleri işaretleyeceğimiz bir grid
        bool[,] visited = new bool[N, N];
        visited[0, 0] = true;

        // BFS kuyruğu
        Queue<Tuple<int, int, int>> queue = new Queue<Tuple<int, int, int>>();
        queue.Enqueue(Tuple.Create(0, 0, 1)); // (Satır, Sütun, Adım Sayısı)

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int row = current.Item1;
            int col = current.Item2;
            int steps = current.Item3;

            // Hazineye ulaştık mı? (N-1, N-1)
            if (row == N - 1 && col == N - 1)
            {
                return steps;
            }

            // 4 farklı yöne git
            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dRow[i];
                int newCol = col + dCol[i];

                // Yeni konum sınırların içinde mi, geçilebilir mi ve ziyaret edilmemiş mi?
                if (newRow >= 0 && newRow < N && newCol >= 0 && newCol < N &&
                    labirent[newRow, newCol] == 1 && !visited[newRow, newCol])
                {
                    queue.Enqueue(Tuple.Create(newRow, newCol, steps + 1));
                    visited[newRow, newCol] = true; // Ziyaret edildi olarak işaretle
                }
            }
        }

        // Hazineye ulaşılamazsa
        return -1; // Yol Yok
    }
}
