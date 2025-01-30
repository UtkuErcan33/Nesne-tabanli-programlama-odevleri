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
    public partial class Plaka_Ekleme : Form
    {
        public Plaka_Ekleme()
        {
            InitializeComponent();
            verileriGöster();
        }

        private void Plaka_Ekleme_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler
        }

        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");
      
        private string YeniPlakaKoduOlustur()
        {
            try
            {
                baglantı.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 sıra FROM Plaka_Ekleme ORDER BY sıra DESC", baglantı);
                object result = cmd.ExecuteScalar();
                baglantı.Close();

                if (result != null)
                {
                    string sonKod = result.ToString();
                    int sayi = int.Parse(sonKod) + 1;
                    return "P" + sayi.ToString("D3");
                }
                return "P001";
            }
            catch
            {
                return "P001";
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                    baglantı.Close();
            }
        }

        private int BosNumaraBul()
        {
            try
            {
                baglantı.Open();
                // Mevcut numaraları sıralı şekilde al
                SqlCommand cmd = new SqlCommand(
                    "SELECT sıra FROM Plaka_Ekleme ORDER BY sıra", 
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

        private void Kaydet_Butonu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comboBox1.Text) || 
                    string.IsNullOrWhiteSpace(TextBox1.Text) || 
                    string.IsNullOrWhiteSpace(TextBox2.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int yeniSiraNo = BosNumaraBul();

                baglantı.Open();
                // IDENTITY_INSERT'i aç
                SqlCommand identityCmd = new SqlCommand("SET IDENTITY_INSERT Plaka_Ekleme ON", baglantı);
                identityCmd.ExecuteNonQuery();

                // Veriyi ekle
                SqlCommand komut = new SqlCommand(
                    "INSERT INTO Plaka_Ekleme (sıra, Arac_Sahibi, Arac_İsim, Araç_Plaka) " +
                    "VALUES (@sira, @Arac_Sahibi, @Arac_İsim, @Arac_Plaka)",
                    baglantı);

                komut.Parameters.AddWithValue("@sira", yeniSiraNo);
                komut.Parameters.AddWithValue("@Arac_Sahibi", comboBox1.Text);
                komut.Parameters.AddWithValue("@Arac_İsim", TextBox1.Text);
                komut.Parameters.AddWithValue("@Arac_Plaka", TextBox2.Text);

                komut.ExecuteNonQuery();

                // IDENTITY_INSERT'i kapat
                identityCmd = new SqlCommand("SET IDENTITY_INSERT Plaka_Ekleme OFF", baglantı);
                identityCmd.ExecuteNonQuery();

                baglantı.Close();

                ListViewItem yeniSatir = new ListViewItem(yeniSiraNo.ToString());
                yeniSatir.SubItems.Add(comboBox1.Text);
                yeniSatir.SubItems.Add(TextBox1.Text);
                yeniSatir.SubItems.Add(TextBox2.Text);
                listView1.Items.Add(yeniSatir);

                comboBox1.SelectedIndex = -1;
                TextBox1.Clear();
                TextBox2.Clear();

                MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                verileriGöster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                {
                    // Hata durumunda IDENTITY_INSERT'i kapatmayı unutma
                    try
                    {
                        SqlCommand identityCmd = new SqlCommand("SET IDENTITY_INSERT Plaka_Ekleme OFF", baglantı);
                        identityCmd.ExecuteNonQuery();
                    }
                    catch { }
                    baglantı.Close();
                }
            }
        }

        private void verileriGöster()
        {
            try
            {
                listView1.Items.Clear();
                baglantı.Open();
           
                SqlCommand komut = new SqlCommand(
                    "SELECT sıra, Arac_Sahibi, Arac_İsim, Araç_Plaka " +
                    "FROM Plaka_Ekleme " +
                    "ORDER BY sıra ASC", baglantı);
                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem(oku["sıra"].ToString());
                    ekle.SubItems.Add(oku["Arac_Sahibi"].ToString());
                    ekle.SubItems.Add(oku["Arac_İsim"].ToString());
                    ekle.SubItems.Add(oku["Araç_Plaka"].ToString());
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Lütfen silinecek kaydı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Seçili kaydı silmek istediğinize emin misiniz?", "Onay", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int silinecekId = int.Parse(listView1.SelectedItems[0].Text);
                    
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Plaka_Ekleme WHERE sıra = @id", baglantı);
                    komut.Parameters.AddWithValue("@id", silinecekId);
                    komut.ExecuteNonQuery();
                    baglantı.Close();

                    verileriGöster();
                    
                    // Alanları temizle
                    comboBox1.SelectedIndex = -1;
                    TextBox1.Clear();
                    TextBox2.Clear();

                    MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Ana_Ekran anaForm = new Ana_Ekran();
            anaForm.Show();
            this.Hide();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                comboBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                TextBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;
                TextBox2.Text = listView1.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            verileriGöster();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}