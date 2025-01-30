using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HAL_Projesi
{
    public partial class Sifre_Ekranı : DevExpress.XtraEditors.XtraForm
    {
        public Sifre_Ekranı()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");
 

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Sifre_Ekranı_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            baglantı.Open();
            Sifre_Ekranı SifreGeçiş = new Sifre_Ekranı();
            Ana_Ekran AnaEKranGeçiş = new Ana_Ekran();


            // SQL sorgusunu parametreli şekilde oluşturuyoruz.
            SqlCommand komut = new SqlCommand("SELECT * FROM kullanici_bilgileri WHERE kullanici_adi = @kullanici AND şifre = @sifre", baglantı);

            // Parametreleri ekliyoruz (güvenlik için).
            komut.Parameters.AddWithValue("@kullanici", textBox1.Text);
            komut.Parameters.AddWithValue("@sifre", textBox2.Text);

            SqlDataReader oku = komut.ExecuteReader();

            // Eğer kullanıcı adı ve şifre doğruysa, bir sonuç dönecektir.

            if (oku.Read())
            {
                // Giriş başarılı
                 this.Hide();
                AnaEKranGeçiş.Show();
               
                

            }
            else
            {
                // Giriş başarısız
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre.");

            }

            baglantı.Close();
        }
    }
}
