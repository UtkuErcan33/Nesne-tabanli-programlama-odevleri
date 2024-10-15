using System;

class Program
{
    static void Main()
    {
        // Kullanıcıdan N sayısını al
        Console.Write("Bir sayı girin (N): ");
        int N = int.Parse(Console.ReadLine());

        int toplam = 0; // Asal sayıların toplamı

        // 2'den N'ye kadar olan sayılar için asal kontrolü yap
        for (int i = 2; i <= N; i++)
        {
            if (AsalMi(i))
            {
                toplam += i; // Asal sayı ise toplamına ekle
            }
        }

        // Sonucu ekrana yazdır
        Console.WriteLine($"{N}'e kadar olan asal sayıların toplamı: {toplam}");

        // Programın hemen kapanmaması için bekle
        Console.WriteLine("Programı kapatmak için bir tuşa basın...");
        Console.ReadKey(); // Programı kapatmadan önce bir tuşa basılmasını bekler
    }

    // Bir sayının asal olup olmadığını kontrol eden fonksiyon
    static bool AsalMi(int sayi)
    {
        if (sayi < 2)
            return false;

        // Sayının 2'den sayının kareköküne kadar olan bölünürlüğü kontrol edilir
        for (int i = 2; i <= Math.Sqrt(sayi); i++)
        {
            if (sayi % i == 0)
                return false; // Bölünüyorsa asal değildir
        }

        return true; // Eğer hiçbir bölme olmadıysa asal sayıdır
    }
}
