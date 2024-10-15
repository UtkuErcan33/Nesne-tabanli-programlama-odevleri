using System;

class MatrisCarpimi
{
    static void Main()
    {
        Console.Write("Matris boyutunu girin (NxN): ");
        int n = int.Parse(Console.ReadLine());

        int[,] matris1 = new int[n, n];
        int[,] matris2 = new int[n, n];
        int[,] sonucMatris = new int[n, n];

        // İlk matrisi kullanıcıdan al
        Console.WriteLine("Birinci matrisin elemanlarını girin:");
        MatrisOku(matris1, n);

        // İkinci matrisi kullanıcıdan al
        Console.WriteLine("İkinci matrisin elemanlarını girin:");
        MatrisOku(matris2, n);

        // İki matrisi çarp
        MatrisCarp(matris1, matris2, sonucMatris, n);

        // Sonucu ekrana yazdır
        Console.WriteLine("İki matrisin çarpımı sonucu:");
        MatrisYazdir(sonucMatris, n);

        // Programın hemen kapanmasını önlemek için kullanıcıdan girdi bekle
        Console.WriteLine("Programı kapatmak için bir tuşa basın...");
        Console.ReadKey(); // Bir tuşa basılana kadar bekler
    }

    static void MatrisOku(int[,] matris, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"Matris[{i},{j}] = ");
                matris[i, j] = int.Parse(Console.ReadLine());
            }
        }
    }

    static void MatrisCarp(int[,] matris1, int[,] matris2, int[,] sonucMatris, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sonucMatris[i, j] = 0;
                for (int k = 0; k < n; k++)
                {
                    sonucMatris[i, j] += matris1[i, k] * matris2[k, j];
                }
            }
        }
    }

    static void MatrisYazdir(int[,] matris, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matris[i, j].ToString().PadLeft(4));
            }
            Console.WriteLine();
        }
    }
}
