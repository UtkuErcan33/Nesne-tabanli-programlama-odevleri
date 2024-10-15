using System;

namespace ndt_ders
{
    class Program
    {
        static void Main()
        {
            Console.Write("Matrisin boyutunu girin (NxN): ");
            int n = int.Parse(Console.ReadLine());

            // Matris
            int[,] matris = new int[n, n];
            SpiralDoldur(matris, n);
            MatrisYazdir(matris, n);

            // Programın hemen kapanmasını önlemek için kullanıcıdan bir tuşa basmasını iste
            Console.WriteLine("Programı kapatmak için bir tuşa basın...");
            Console.ReadKey(); // Konsolun kapanmasını engeller
        }

        static void SpiralDoldur(int[,] matris, int n)
        {
            int deger = 1;
            int sol = 0, sag = n - 1, yukari = 0, asagi = n - 1;

            while (deger <= n * n)
            {
                // Yukarıdan sağa doğru
                for (int i = sol; i <= sag; i++)
                {
                    matris[yukari, i] = deger++;
                }
                yukari++;

                // Sağdan aşağıya doğru
                for (int i = yukari; i <= asagi; i++)
                {
                    matris[i, sag] = deger++;
                }
                sag--;

                // Aşağıdan sola doğru
                for (int i = sag; i >= sol; i--)
                {
                    matris[asagi, i] = deger++;
                }
                asagi--;

                // Soldan yukarıya doğru
                for (int i = asagi; i >= yukari; i--)
                {
                    matris[i, sol] = deger++;
                }
                sol++;
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
}
