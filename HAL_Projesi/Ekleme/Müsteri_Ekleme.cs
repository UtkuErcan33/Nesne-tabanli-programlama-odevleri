using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HAL_Projesi
{
    public partial class Müsteri_Ekleme : Form
    {
        public Müsteri_Ekleme()
        {
            InitializeComponent();
            verilerigöster();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");

        private string YeniMusteriKoduOlustur()
        {
            try
            {
                baglantı.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 musteri_kodu FROM Müsteri_Ekleme ORDER BY musteri_kodu DESC", baglantı);
                object result = cmd.ExecuteScalar();
                baglantı.Close();

                if (result != null)
                {
                    string sonKod = result.ToString();
                    int sayi = int.Parse(sonKod.Substring(1)) + 1;
                    return "M" + sayi.ToString("D3");
                }
                return "M001";
            }
            catch
            {
                return "M001";
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                    baglantı.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string yeniKod = YeniMusteriKoduOlustur();

                baglantı.Open();
                SqlCommand komut = new SqlCommand(
                    "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Müsteri_Ekleme') " +
                    "CREATE TABLE Müsteri_Ekleme (" +
                    "musteri_kodu VARCHAR(10) PRIMARY KEY, " +
                    "musteri_adi VARCHAR(50), " +
                    "musteri_soyadi VARCHAR(50), " +
                    "musteri_tel VARCHAR(15), " +
                    "tarih DATE" +
                    "); " +
                    "INSERT INTO Müsteri_Ekleme (musteri_kodu, musteri_adi, musteri_soyadi, musteri_tel, tarih) " +
                    "VALUES (@musteri_kodu, @musteri_adi, @musteri_soyadi, @musteri_tel, @tarih)",
                    baglantı);

                komut.Parameters.AddWithValue("@musteri_kodu", yeniKod);
                komut.Parameters.AddWithValue("@musteri_adi", textBox1.Text);
                komut.Parameters.AddWithValue("@musteri_soyadi", textBox2.Text);
                komut.Parameters.AddWithValue("@musteri_tel", textBox3.Text);
                komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.Date);

                komut.ExecuteNonQuery();
                baglantı.Close();

                ListViewItem yeniSatir = new ListViewItem(yeniKod);
                yeniSatir.SubItems.Add(textBox1.Text);
                yeniSatir.SubItems.Add(textBox2.Text);
                yeniSatir.SubItems.Add(textBox3.Text);
                yeniSatir.SubItems.Add(dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                listView1.Items.Add(yeniSatir);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                dateTimePicker1.Value = DateTime.Now;

                MessageBox.Show("Müşteri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri eklenirken bir hata oluştu: " + ex.Message + "\nHata detayı: " + ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                    baglantı.Close();
            }
        }

        void verilerigöster()
        {
            try
            {
                listView1.Items.Clear();
                baglantı.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM Müsteri_Ekleme ORDER BY musteri_kodu ASC", baglantı);
                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem(oku["musteri_kodu"].ToString());
                    ekle.SubItems.Add(oku["musteri_adi"].ToString());
                    ekle.SubItems.Add(oku["musteri_soyadi"].ToString());
                    ekle.SubItems.Add(oku["musteri_tel"].ToString());
                    ekle.SubItems.Add(Convert.ToDateTime(oku["tarih"]).ToString("dd.MM.yyyy"));
                    listView1.Items.Add(ekle);
                }
                baglantı.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                    baglantı.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            verilerigöster();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Lütfen silinecek müşteriyi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Seçili müşteriyi silmek istediğinize emin misiniz?", "Onay", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string silinecekKod = listView1.SelectedItems[0].Text;
                    
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Müsteri_Ekleme WHERE musteri_kodu = @kod", baglantı);
                    komut.Parameters.AddWithValue("@kod", silinecekKod);
                    komut.ExecuteNonQuery();
                    baglantı.Close();

                    MessageBox.Show("Müşteri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    dateTimePicker1.Value = DateTime.Now;

                    verilerigöster();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                    baglantı.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Ana_Ekran anaForm = new Ana_Ekran();
            anaForm.Show();
            this.Hide();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[3].Text;
                dateTimePicker1.Value = Convert.ToDateTime(listView1.SelectedItems[0].SubItems[4].Text);
            }
        }

        private void Müsteri_Ekleme_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}