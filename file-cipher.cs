using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cryptolama
{
    class EncryptAndDecrypt
    {
        int key = 0;

        public EncryptAndDecrypt(int NewKey) //Constructor (Yapıcı Metot)
        {
            key = NewKey;
        }

        public string Encrypt(string txt)
        {
            string EncryptTxt = “”;
            for (int i = 0; i < txt.Length; i++)
            {
                char chrTxt = Convert.ToChar(txt.Substring(i, 1)); //Metin dosyasındaki şeyi Substring ile tek parça haline getirip char veri tipine dönüştürülüyor.
                int intTxt = (int)chrTxt; //ASCII Kod tablosundaki integer (sayısal) değeri alınıyor.
                intTxt += key; //Girilen keye toplama yapılıp şifreleme yapılıyor. Decrypt te ise tam tersi yapılarak işlem uygulanıyor.
                EncryptTxt += (char)intTxt; //Toplanmış olan sayıda tekrar char tipine dönüştürülere ASCII Kod tablosundaki değeri EncryptTxt string veri tipi ile birleştirilip yeni metin oluşmuş oluyor.
            }
            return EncryptTxt;
        }

        public string Decrypt(string txt)
        {
            string DecryptTxt = “”;
            for (int i = 0; i < txt.Length; i++)
            {
                char chrTxt = Convert.ToChar(txt.Substring(i, 1));
                int intTxt = (int)chrTxt;
                intTxt -= key;
                DecryptTxt += (char)intTxt;
            }
            return DecryptTxt;
        }

        public void CreateOriginalFile()
        {
            StreamWriter OrgFile = new StreamWriter(“OriginalText.txt”); //Metin belgesi oluşturuluyor.
            OrgFile.WriteLine(“SebinLi028 – İbrahim YAVUZ”); //Metin belgesine değerler
            OrgFile.WriteLine(“Computer Engineering”); //yazdırılıyor.
            OrgFile.Close(); //Bağlantı kapatılıyor.
        }

        public void EncryptFile()
        {
            if (File.Exists(“OriginalText.original”)) //Bu isimde dosya varmı?
            {
                Console.Write(“\n\n’OriginalText.original’ dosyası zaten oluşturulmuş!!!tekrar encrypt edilemez!!!\n\n\n”);
                File.Delete(“OriginalText.txt”); //Varsa üsteki mesajı yaz ve txt uzantılı dosyayı sil
            }
            else
            {
                File.Delete(“OriginalText – Decrypted.txt”); //Yoksa bu isimdeki dosyayı sil
                StreamWriter OrgFileEncrypt = new StreamWriter(“OriginalText – Encrypted.txt”);
                StreamReader OrgFileRead = File.OpenText(“OriginalText.txt”); //Bu dosyayı oku
                string row = OrgFileRead.ReadLine(); //Dosyadaki satırlar
                Console.WriteLine(“\n\n\n * **Encrypt Edilmiş Metin * **\n”);
                while (row != null) //Satır boş değil ise işleme devam et
                {
                    row = Encrypt(row); //Okunan satırı şifrele ve tekrar row değişkenine ata
                    OrgFileEncrypt.WriteLine(row); //Okunan satırları “OriginalText – Encrypted.txt” dosyaya yazdır
                    Console.WriteLine(row); //Konsolda yazdır
                    row = OrgFileRead.ReadLine(); //Sonraki satırı row değişkenine ata
                }
                OrgFileEncrypt.Close();
                OrgFileRead.Close();
                Console.Write(“\nEncrypt İşlemi Tamamlandı\n\n\n\n”);
                File.Move(“OriginalText.txt”, “OriginalText.original”); //Dosyayı isim ve uzantı olarak değiştir
            }
        }

        public void DecryptFile()
        {
            if (File.Exists(“OriginalText.original”))
            {
                File.Move(“OriginalText.original”, “OriginalText – Decrypted.txt”);
                if (File.Exists(“OriginalText – Decrypted.txt”))
                {
                    File.Delete(“OriginalText.original”);
                    StreamWriter OrgFileDecrypt = new StreamWriter(“OriginalText – Decrypted.txt”);
                    StreamReader OrgFileEncryptRead = File.OpenText(“OriginalText – Encrypted.txt”);
                    string row = OrgFileEncryptRead.ReadLine();
                    Console.WriteLine(“\n\n\n * **Decrypt Edilmiş Metin * **\n\n”);
                    while (row != null)
                    {
                        row = Decrypt(row);
                        OrgFileDecrypt.WriteLine(row);
                        Console.WriteLine(row);
                        row = OrgFileEncryptRead.ReadLine();
                    }
                    OrgFileDecrypt.Close();
                    OrgFileEncryptRead.Close();
                    Console.Write(“\nDecrypt İşlemi Tamamlandı\n\n\n\n”);
                }
            }
            else Console.Write(“\n\n’OriginalText.original’ dosyası bulunamadı!!!tekrar decrypt edilemez!!!\n\n\n”);
        }

        public void Sil()
        {
            if (File.Exists(“OriginalText.original”) || File.Exists(“OriginalText – Encrypted.txt”) ||
            File.Exists(“OriginalText – Decrypted.txt”))
            {
                File.Delete(“OriginalText.original”);
                File.Delete(“OriginalText – Encrypted.txt”);
                File.Delete(“OriginalText – Decrypted.txt”);
                Console.Write(“\n\nDosyalar Silindi\n\n\n”);
            }
            else Console.Write(“\n\nSilinecek Dosyalar Bulunamadı\n\n\n”);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = “Cryptolama(========>>>> SebinLi028 <<<<========)”;
            Console.ForegroundColor = ConsoleColor.White;
            EncryptAndDecrypt Crypto = new EncryptAndDecrypt(28); //Key 28 olarak sınıfa gönderiliyor
            int choice;
            do
            {
                try
                {
                    Console.WriteLine(“***Crypto * **\n”);
                    Console.WriteLine(“1.Encryption\n2.Decryption\n3.Dosyaları Sil\n4.Ekranı Temizle”);
                    Console.Write(“\n\nSeçim:”); choice = Convert.ToInt16(Console.ReadLine());
                    switch (choice)
                    {
                        case 1: Crypto.CreateOriginalFile(); Crypto.EncryptFile(); break;
                        case 2: Crypto.DecryptFile(); break;
                        case 3: Crypto.Sil(); break;
                        case 4: Console.Clear(); break;
                    }
                }
                catch { Console.WriteLine(“\n\n\nSeçim Yapmadınız.\n\n\n”); }
            } while (true);
        }
    }
}
