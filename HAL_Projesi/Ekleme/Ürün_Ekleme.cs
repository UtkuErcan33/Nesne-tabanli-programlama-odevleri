using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HAL_Projesi
{
    public partial class Ürün_Ekleme : Form
    {
        public Ürün_Ekleme()
        {
            InitializeComponent();
            verilerigöster();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");

        private int BosNumaraBul()
        {
            try
            {
                baglantı.Open();
                // Mevcut numaraları sıralı şekilde al
                SqlCommand cmd = new SqlCommand(
                    "SELECT urun_kodu FROM Ürün_Ekleme ORDER BY urun_kodu", 
                    baglantı);
                
                SqlDataReader reader = cmd.ExecuteReader();
                int beklenenNumara = 1;
                
                // Numaraları kontrol et ve ilk boş numarayı bul
                while (reader.Read())
                {
                    int mevcutNumara = reader.GetInt32(0);
                    if (mevcutNumara > beklenenNumara)
                    {
                        // Boşluk bulundu
                        return beklenenNumara;
                    }
                    beklenenNumara = mevcutNumara + 1;
                }
                
                // Boşluk bulunamadıysa son numaradan sonrakini döndür
                return beklenenNumara;
            }
            catch
            {
                return 1; // Hata durumunda 1'den başla
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
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Lütfen ürün adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int yeniUrunKodu = BosNumaraBul();

                baglantı.Open();
                // IDENTITY_INSERT'i aç
                SqlCommand identityCmd = new SqlCommand("SET IDENTITY_INSERT Ürün_Ekleme ON", baglantı);
                identityCmd.ExecuteNonQuery();

                // Veriyi ekle
                SqlCommand komut = new SqlCommand(
                    "INSERT INTO Ürün_Ekleme (urun_kodu, urun_adi, tarih) " +
                    "VALUES (@urun_kodu, @urun_adi, @tarih)",
                    baglantı);

                komut.Parameters.AddWithValue("@urun_kodu", yeniUrunKodu);
                komut.Parameters.AddWithValue("@urun_adi", textBox1.Text);
                komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.Date);

                komut.ExecuteNonQuery();

                // IDENTITY_INSERT'i kapat
                identityCmd = new SqlCommand("SET IDENTITY_INSERT Ürün_Ekleme OFF", baglantı);
                identityCmd.ExecuteNonQuery();

                baglantı.Close();

                ListViewItem yeniSatir = new ListViewItem(yeniUrunKodu.ToString());
                yeniSatir.SubItems.Add(textBox1.Text);
                yeniSatir.SubItems.Add(dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                listView1.Items.Add(yeniSatir);

                textBox1.Clear();
                dateTimePicker1.Value = DateTime.Now;

                MessageBox.Show("Ürün başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                {
                    // Hata durumunda IDENTITY_INSERT'i kapatmayı unutma
                    try
                    {
                        SqlCommand identityCmd = new SqlCommand("SET IDENTITY_INSERT Ürün_Ekleme OFF", baglantı);
                        identityCmd.ExecuteNonQuery();
                    }
                    catch { }
                    baglantı.Close();
                }
            }
        }

        void verilerigöster()
        {
            try
            {
                listView1.Items.Clear();
                baglantı.Open();
                SqlCommand komut = new SqlCommand("SELECT urun_kodu, urun_adi, tarih FROM Ürün_Ekleme ORDER BY urun_kodu ASC", baglantı);
                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem(oku["urun_kodu"].ToString());
                    ekle.SubItems.Add(oku["urun_adi"].ToString());
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
                    MessageBox.Show("Lütfen silinecek ürünü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Seçili ürünü silmek istediğinize emin misiniz?", "Onay", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int silinecekId = int.Parse(listView1.SelectedItems[0].Text);
                    
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Ürün_Ekleme WHERE urun_kodu = @id", baglantı);
                    komut.Parameters.AddWithValue("@id", silinecekId);
                    komut.ExecuteNonQuery();
                    baglantı.Close();

                    MessageBox.Show("Ürün başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
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
                dateTimePicker1.Value = Convert.ToDateTime(listView1.SelectedItems[0].SubItems[2].Text);
            }
        }

        private void Ürün_Ekleme_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}