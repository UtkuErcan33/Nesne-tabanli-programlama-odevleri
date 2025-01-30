using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HAL_Projesi.Ekleme
{
    public partial class Kasa_Ekleme : Form
    {
        public Kasa_Ekleme()
        {
            InitializeComponent();
            verileriGöster();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");

        private int BosNumaraBul()
        {
            try
            {
                baglantı.Open();
                // Mevcut numaraları sıralı şekilde al
                SqlCommand cmd = new SqlCommand(
                    "SELECT kasa_kodu FROM kasa_ekleme ORDER BY kasa_kodu", 
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

        private void Kasa_Ekleme_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
        }

        private void görüntüle_butonu_Click(object sender, EventArgs e)
        {
            verileriGöster();
        }

        private void kaydet_butonu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(kasaAdi_textBox.Text))
                {
                    MessageBox.Show("Lütfen kasa adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int yeniKasaKodu = BosNumaraBul();

                baglantı.Open();
                // IDENTITY_INSERT'i aç
                SqlCommand identityCmd = new SqlCommand("SET IDENTITY_INSERT kasa_ekleme ON", baglantı);
                identityCmd.ExecuteNonQuery();

                // Veriyi ekle
                SqlCommand komut = new SqlCommand(
                    "INSERT INTO kasa_ekleme (kasa_kodu, kasa_adi, tarih) " +
                    "VALUES (@kasa_kodu, @kasa_adi, @tarih)", 
                    baglantı);

                komut.Parameters.AddWithValue("@kasa_kodu", yeniKasaKodu);
                komut.Parameters.AddWithValue("@kasa_adi", kasaAdi_textBox.Text);
                komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.Date);

                komut.ExecuteNonQuery();

                // IDENTITY_INSERT'i kapat
                identityCmd = new SqlCommand("SET IDENTITY_INSERT kasa_ekleme OFF", baglantı);
                identityCmd.ExecuteNonQuery();

                baglantı.Close();

                MessageBox.Show("Kasa başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                kasaAdi_textBox.Clear();
                dateTimePicker1.Value = DateTime.Now;

                verileriGöster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kasa eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                {
                    // Hata durumunda IDENTITY_INSERT'i kapatmayı unutma
                    try
                    {
                        SqlCommand identityCmd = new SqlCommand("SET IDENTITY_INSERT kasa_ekleme OFF", baglantı);
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
                SqlCommand komut = new SqlCommand("SELECT * FROM kasa_ekleme ORDER BY kasa_kodu ASC", baglantı);
                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem(oku["kasa_kodu"].ToString());
                    ekle.SubItems.Add(oku["kasa_adi"].ToString());
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

        private void sil_butonu_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Lütfen silinecek kasayı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Seçili kasayı silmek istediğinize emin misiniz?", "Onay", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string silinecekKod = listView1.SelectedItems[0].Text;
                    
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM kasa_ekleme WHERE kasa_kodu = @kod", baglantı);
                    komut.Parameters.AddWithValue("@kod", silinecekKod);
                    komut.ExecuteNonQuery();
                    baglantı.Close();

                    MessageBox.Show("Kasa başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    kasaAdi_textBox.Clear();
                    dateTimePicker1.Value = DateTime.Now;

                    verileriGöster();
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

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                kasaAdi_textBox.Text = listView1.SelectedItems[0].SubItems[1].Text;
                dateTimePicker1.Value = Convert.ToDateTime(listView1.SelectedItems[0].SubItems[2].Text);
            }
        }

        private void Önceki_sayfa_Click(object sender, EventArgs e)
        {
            Ana_Ekran anaForm = new Ana_Ekran();
            anaForm.Show();
            this.Hide();
        }
    }
}