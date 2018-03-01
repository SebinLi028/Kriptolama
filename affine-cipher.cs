using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

string[] affine_alfabe = new string[] //Dizi 0-25
{"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};

private void affine_Click(object sender, EventArgs e)
{   //AFFINE CIPHER ENCRYPTION
    try
    {
        string metin = MetinTxt.Text;
        int a = int.Parse(akey.Text);
        int b = int.Parse(bkey.Text);
        int x = 0;
        int Ey = 0;
        string smetin = "";
        metin = metin.ToUpper(); //metni BÜYÜK HARF yap

        for (int i = 0; i < metin.Length; i++) //sıfırdan metin uzunluğuna kadar 1 artır
        {
            string pmetin = metin.Substring(i, 1); //metinden 1 eleman al

            //türkçe karakterleri ingilizce karakter yap
            if (pmetin == "Ç") pmetin = "C"; if (pmetin == "Ğ") pmetin = "G"; if (pmetin == "İ") pmetin = "I";
            if (pmetin == "Ö") pmetin = "O"; if (pmetin == "Ş") pmetin = "S"; if (pmetin == "Ü") pmetin = "U";

            for (int j = 0; j < affine_alfabe.Length; j++) //sıfırdan dizinin uzunluğuna kadar 1 artır
            {
                if (pmetin == affine_alfabe[j]) //eleman dizide varsa döngüden çık
                { break; }
                else //yoksa dizideki yerini bulmak için 1 artır
                { x++; }
            }
            if (pmetin == " ") //eğer metinin içerinde boşluk varsa
            {
                smetin += " "; //şifreli metine ekle ama boşluğu şifreleme
                x = 0; //dizideki yerini sıfırla
            }
            else if (char.Parse(pmetin) >= 48 && char.Parse(pmetin) <= 57) //ASCI kod tablosunda 0-9 arasındaki rakamlar
            {
                smetin += pmetin; //şifrelemeye dahil etmeden ekle
                x = 0; //dizideki yerini sıfırla
            }
            else //yoksa kaldığı yerden devam et
            {
                Ey = (a * x + b) % 26; //y=ax+b formülüne göre modunu alarak şifrele
                x = 0; //dizideki yerini sıfırla
                smetin += affine_alfabe[Ey]; //ve dizideki yerini bul değişkene at
            }
        }
        encTxt.Text = smetin; //oluşan şifreli metni yazdır
    }
    catch (Exception) //tüm oluşabilecek hataları yakala
    {
        MessageBox.Show("Hatalı Veri Girişi");
    }
}

private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{
    if (encTxt.Text != "") //şifreli metni textBox'a kopyala
    {
        Clipboard.SetText(encTxt.Text); //kopyala
        MetinTxt.Text = Clipboard.GetText(); //yapıştır
    }
}

private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{   //AFFINE CIPHER DECRYPTION
    try
    {
        string metin = MetinTxt.Text;
        int a = int.Parse(akey.Text);
        int b = int.Parse(bkey.Text);
        int x = 0, mters = 0;
        int Dy = 0;
        string cmetin = "";
        metin = metin.ToUpper(); //metni BÜYÜK HARF yap

        for (int i = 0; i < metin.Length; i++) //sıfırdan metin uzunluğuna kadar 1 artır
        {
            string pmetin = metin.Substring(i, 1); //metinden 1 eleman al

            for (int j = 0; j < affine_alfabe.Length; j++) //sıfırdan dizinin uzunluğuna kadar 1 artır
            {
                if (pmetin == affine_alfabe[j]) //eleman dizide varsa döngüden çık
                { break; }
                else //yoksa dizideki yerini bulmak için 1 artır
                { x++; }
            }
            if (pmetin == " ") //eğer metinin içerinde boşluk varsa
            {
                cmetin += " "; //şifreli metine ekle ama boşluğu şifreleme
                x = 0;
            }
            else if (char.Parse(pmetin) >= 48 && char.Parse(pmetin) <= 57) //ASCI kod tablosunda 0-9 arasındaki rakamlar
            {
                cmetin += pmetin; //şifrelemeye dahil etmeden ekle
                x = 0; //dizideki yerini sıfırla
            }
            else //yoksa kaldığı yerden devam et
            {
                for (int modters = 1; modters < 27; modters++) //modüler ters alma işlemi 3^-1 mod 26
                {
                    if ((a * modters) % 26 == 1) //kalan eğer 1'e eşitse o sayının modüler tersi modters değişkenindeki elemandır
                    {
                        mters = modters;
                        break;
                    }
                }
                if (mters != 0) //modüler tersi varsa işleme devam et
                {
                    if (x - b < 0) x += 26; //x-b sıfırdan küçük ise x'e 26 (mod 26 olduğu için) ekle
                    Dy = (mters * (x - b)) % 26; //y=a^-1*(x-b) formülüne göre modunu alarak şifre çöz
                    x = 0; //dizideki yerini sıfırla
                    cmetin += affine_alfabe[Dy]; //ve dizideki yerini bul değişkene at
                }
                else if (mters == 0) //modüler tersi sıfıra eşitse mesaj ver ve döngüden çık
                {
                    MessageBox.Show("a anahtarının modüler tersi olmadığı için çözme işlemi gerçekleştirilemiyor!\nBu yüzden a anahtarı tek sayı olmalıdır.", "Modüler Ters");
                    break;
                }
            }
        }
        decTxt.Text = cmetin; //oluşan şifreli metni yazdır
    }
    catch (Exception) //tüm oluşabilecek hataları yakala
    {
        MessageBox.Show("Hatalı Veri Girişi");
    }
}
