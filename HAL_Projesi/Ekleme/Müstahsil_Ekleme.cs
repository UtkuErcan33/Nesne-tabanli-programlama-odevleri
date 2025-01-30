using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HAL_Projesi
{
    public partial class Müstahsil_Ekleme : Form
    {
        public Müstahsil_Ekleme()
        {
            InitializeComponent();
            verilerigöster();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");

        private string YeniMustahsilKoduOlustur()
        {
            try
            {
                baglantı.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 mustahsil_kodu FROM müstahsil_ekleme ORDER BY mustahsil_kodu DESC", baglantı);
                object result = cmd.ExecuteScalar();
                baglantı.Close();

                if (result != null)
                {
                    string sonKod = result.ToString();
                    int sayi = int.Parse(sonKod.Substring(1)) + 1;
                    return "T" + sayi.ToString("D3");
                }
                return "T001";
            }
            catch
            {
                return "T001";
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

                string yeniKod = YeniMustahsilKoduOlustur();

                baglantı.Open();
                SqlCommand komut = new SqlCommand(
                    "INSERT INTO müstahsil_ekleme (mustahsil_kodu, mustahsil_adi, mustahsil_soyadi, mustahsil_tel, tarih) " +
                    "VALUES (@mustahsil_kodu, @mustahsil_adi, @mustahsil_soyadi, @mustahsil_tel, @tarih)",
                    baglantı);

                komut.Parameters.AddWithValue("@mustahsil_kodu", yeniKod);
                komut.Parameters.AddWithValue("@mustahsil_adi", textBox1.Text);
                komut.Parameters.AddWithValue("@mustahsil_soyadi", textBox2.Text);
                komut.Parameters.AddWithValue("@mustahsil_tel", textBox3.Text);
                komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);

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

                MessageBox.Show("Müstahsil başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müstahsil eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SqlCommand komut = new SqlCommand("SELECT * FROM müstahsil_ekleme ORDER BY mustahsil_kodu ASC", baglantı);
                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem(oku["mustahsil_kodu"].ToString());
                    ekle.SubItems.Add(oku["mustahsil_adi"].ToString());
                    ekle.SubItems.Add(oku["mustahsil_soyadi"].ToString());
                    ekle.SubItems.Add(oku["mustahsil_tel"].ToString());
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
                    MessageBox.Show("Lütfen silinecek müstahsili seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Seçili müstahsili silmek istediğinize emin misiniz?", "Onay", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string silinecekKod = listView1.SelectedItems[0].Text;
                    
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM müstahsil_ekleme WHERE mustahsil_kodu = @kod", baglantı);
                    komut.Parameters.AddWithValue("@kod", silinecekKod);
                    komut.ExecuteNonQuery();
                    baglantı.Close();

                    MessageBox.Show("Müstahsil başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void Müstahsil_Ekleme_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}