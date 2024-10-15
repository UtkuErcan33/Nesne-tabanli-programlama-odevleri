using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Tahmin edilecek kelimeler listesi
        List<string> kelimeler = new List<string> { "programlama", "bilgisayar", "algoritma", "yazilim", "teknoloji" };

        // Rastgele bir kelime seç
        Random rastgele = new Random();
        string secilenKelime = kelimeler[rastgele.Next(kelimeler.Count)];

        // Kelimenin tahmin edilmesi için saklanacak hali (örn: "_ _ _ _ _")
        char[] gizliKelime = new string('_', secilenKelime.Length).ToCharArray();
        HashSet<char> dogruTahminler = new HashSet<char>();
        HashSet<char> yanlisTahminler = new HashSet<char>();

        int kalanHak = 6;  // Oyuncunun toplam 6 yanlış yapma hakkı var

        // Oyun döngüsü
        while (kalanHak > 0 && new string(gizliKelime) != secilenKelime)
        {
            Console.Clear();
            Console.WriteLine("Adam Asmaca Oyununa Hoş Geldiniz!");
            Console.WriteLine($"Kalan Hakkınız: {kalanHak}");
            Console.WriteLine("Gizli Kelime: " + string.Join(" ", gizliKelime));
            Console.WriteLine("Yanlış Tahminler: " + string.Join(", ", yanlisTahminler));

            // Oyuncudan harf tahmini al
            Console.Write("Bir harf tahmin edin: ");
            string giris = Console.ReadLine().ToLower();  // Giriş string tipinde

            if (string.IsNullOrEmpty(giris) || giris.Length != 1 || !char.IsLetter(giris[0]))
            {
                Console.WriteLine("Geçersiz giriş, lütfen tek bir harf girin!");
                continue;
            }

            char tahmin = giris[0];  // İlk karakteri char olarak al

            if (dogruTahminler.Contains(tahmin) || yanlisTahminler.Contains(tahmin))
            {
                Console.WriteLine($"'{tahmin}' harfini zaten tahmin ettiniz.");
                continue;
            }

            // Tahmin doğru mu?
            if (secilenKelime.Contains(tahmin.ToString()))
            {
                dogruTahminler.Add(tahmin);
                for (int i = 0; i < secilenKelime.Length; i++)
                {
                    if (secilenKelime[i] == tahmin)
                    {
                        gizliKelime[i] = tahmin;  // Doğru tahmini gizli kelimeye yerleştir
                    }
                }
            }
            else
            {
                yanlisTahminler.Add(tahmin);
                kalanHak--;  // Yanlış tahmin hak azaltılır
            }
        }

        // Oyun bittiğinde sonuçları yazdır
        Console.Clear();
        if (new string(gizliKelime) == secilenKelime)
        {
            Console.WriteLine("Tebrikler! Kelimeyi doğru tahmin ettiniz: " + secilenKelime);
        }
        else
        {
            Console.WriteLine("Maalesef, hakkınız bitti! Doğru kelime: " + secilenKelime);
        }

        Console.WriteLine("Oyunu kapatmak için bir tuşa basın...");
        Console.ReadKey();
    }
}
