private void ceaser_Click(object sender, EventArgs e)
{
    string[] ceaser_alfabe = new string[] //0-50
    {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
    "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

    string Enc = "", Dec = "";
    int shift = 0;

    //ENCRYPTION
    string metin = Interaction.InputBox("Şifrelenecek Metin Giriniz.", "Ceaser's Cipher Encryption");
    int key = Math.Abs(int.Parse(Interaction.InputBox("Şifreleme Anahtarınız Giriniz.\nNegatif Sayı Girmeyiniz.", "Ceaser's Cipher Encryption")));
    char[] pmetin = metin.ToCharArray(); //metni parçalara böl ve char tipinde dizi oluştur

    foreach (char c in pmetin)
    {
        //türkçe karakterleri ingilizce karakter yap
        string strc = "";
        strc = c.ToString().Replace("Ç", "C").Replace("ç", "c").Replace("Ğ", "G").Replace("ğ", "g").
            Replace("İ", "I").Replace("ı", "i").Replace("Ö", "O").Replace("ö", "o").Replace("Ş", "S").Replace("ş", "s").
            Replace("Ü", "U").Replace("ü", "u");

        if (char.Parse(strc) == 32) Enc += " "; //boşluğu ekle ama şifreleme
        else if (char.Parse(strc) >= 48 && char.Parse(strc) <= 57) Enc += strc; //ASCII 0-9
        else //kaldığı yerden devam et
        {
            shift = Array.IndexOf(ceaser_alfabe, strc) + key; //dizideki yerini bul ve şifrele
            if (shift >= ceaser_alfabe.Length) shift = shift % ceaser_alfabe.Length; //shift mod 26
            Enc += ceaser_alfabe[shift]; //dizideki yerini bul
        }
    }

    MessageBox.Show(Enc, "Encrypted Text");
    Clipboard.SetText(Enc); //değişkendeki veriyi kopyala
    MessageBox.Show("Encrypt Edilmiş Metin Kopyalandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

    //DECRYPTION
    metin = Interaction.InputBox("Şifrelenmiş Metni Giriniz.", "Ceaser's Cipher Decryption");
    Math.Abs(int.Parse(Interaction.InputBox("Şifreleme Anahtarınız Giriniz.\nNegatif Sayı Girmeyiniz.", "Ceaser's Cipher Decryption")));
    pmetin = metin.ToCharArray(); //metni parçalara böl ve char tipinde dizi oluştur
   
    foreach (char c in pmetin)
    {
        if (c == 32) Dec += " ";
        else if (c >= 48 && c <= 57) Dec += c;
        else
        {
            shift = Array.IndexOf(ceaser_alfabe, c.ToString()) - key; //dizideki yerini bu ve şifreyi çöz
            if (shift < 0) shift = (Math.Abs(ceaser_alfabe.Length + shift)) % ceaser_alfabe.Length; //eğer negatif ise dizi boyutundan çıkar
            Dec += ceaser_alfabe[shift]; //dizideki yerini bul
        }
    }
    MessageBox.Show(Dec, "Decrypted Text");
    Clipboard.SetText(Dec); //değişkendeki veriyi kopyala
    MessageBox.Show("Decrypt Edilmiş Metin Kopyalandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
