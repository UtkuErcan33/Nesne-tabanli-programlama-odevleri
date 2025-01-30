using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace HAL_Projesi.alım
{
    public partial class Müstahsil_Alım : DevExpress.XtraEditors.XtraForm
    {
        public Müstahsil_Alım()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");

        private void Müstahsil_Alım_Load_1(object sender, EventArgs e)
        {
            try
            {
                // DataGridView sütunlarını manuel olarak ekle
                dataGridView1.AutoGenerateColumns = false;
                
                if (dataGridView1.Columns.Count == 0)
                {
                    dataGridView1.Columns.Add("ürün_kodu", "Ürün Kodu");
                    dataGridView1.Columns.Add("ürün_adi", "Ürün Adı");
                    dataGridView1.Columns.Add("ürün_KG", "KG");
                    dataGridView1.Columns.Add("ürün_TL", "TL");
                    dataGridView1.Columns.Add("ürün_toplam", "Toplam");
                    dataGridView1.Columns.Add("satici", "Satıcı");
                    dataGridView1.Columns.Add("alici", "Alıcı");
                    dataGridView1.Columns.Add("tarih", "Tarih");
                }

                // Alım geçmişi verilerini yükle
            this.alım_gecmisiTableAdapter.Fill(this.giriş_kayıtlarıDataSet1.Alım_gecmisi);
                dataGridView1.DataSource = this.alımgecmisiBindingSource;
                
                // ListView'leri hazırla
                listView2.Columns.Clear();
                listView2.Columns.Add("Ürün Kodu", 100);
                listView2.Columns.Add("Ürün Adı", 100);

                listView3.Columns.Clear();
                listView3.Columns.Add("Müstahsil Kodu", 100);
                listView3.Columns.Add("Müstahsil Adı", 100);
                listView3.Columns.Add("Müstahsil Pozisyon", 100);

                listView4.Columns.Clear();
                listView4.Columns.Add("Müşteri Kodu", 100);
                listView4.Columns.Add("Müşteri Adı", 100);
                listView4.Columns.Add("Müşteri Pozisyon", 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(ürün_kodu_text.Text) ||
                    string.IsNullOrWhiteSpace(ürün_adi_text.Text) ||
                    string.IsNullOrWhiteSpace(kg_text.Text) ||
                    string.IsNullOrWhiteSpace(tl_text.Text) ||
                    string.IsNullOrWhiteSpace(toplam_text.Text) ||
                    string.IsNullOrWhiteSpace(satici_text.Text) ||
                    string.IsNullOrWhiteSpace(alici_text.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                baglantı.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Alım_gecmisi (ürün_kodu,ürün_adi,ürün_KG,ürün_TL,ürün_toplam,satici,alici,tarih) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglantı);
                komut.Parameters.AddWithValue("@p1", ürün_kodu_text.Text);
                komut.Parameters.AddWithValue("@p2", ürün_adi_text.Text);
                komut.Parameters.AddWithValue("@p3", kg_text.Text);
                komut.Parameters.AddWithValue("@p4", tl_text.Text);
                komut.Parameters.AddWithValue("@p5", toplam_text.Text);
                komut.Parameters.AddWithValue("@p6", satici_text.Text);
                komut.Parameters.AddWithValue("@p7", alici_text.Text);
                komut.Parameters.AddWithValue("@p8", tarih_text.DateTime);

                komut.ExecuteNonQuery();
                baglantı.Close();

                MessageBox.Show("Kayıt Başarılı");

                // TextEdit'leri temizle
                ürün_kodu_text.Text = "";
                ürün_adi_text.Text = "";
                kg_text.Text = "";
                tl_text.Text = "";
                toplam_text.Text = "";
                satici_text.Text = "";
                alici_text.Text = "";
                tarih_text.DateTime = DateTime.Now; // Tarihi şimdiki zaman olarak ayarla

                // Verileri yenile
                verileriYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        private void verileriYenile()
        {
            try
            {
                // DataGridView'ı temizle
                if (dataGridView1.DataSource != null)
                {
                    dataGridView1.DataSource = null;
                }

                // Verileri yeniden yükle
                this.alım_gecmisiTableAdapter.Fill(this.giriş_kayıtlarıDataSet1.Alım_gecmisi);
                dataGridView1.DataSource = this.alımgecmisiBindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yenilenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen silinecek satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Seçili satırı silmek istediğinize emin misiniz?", "Onay", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    
                    baglantı.Open();
                    string deleteQuery = @"DELETE FROM Alım_gecmisi 
                        WHERE ürün_kodu = @p1 
                        AND ürün_adi = @p2 
                        AND ürün_KG = @p3 
                        AND ürün_TL = @p4 
                        AND ürün_toplam = @p5 
                        AND satici = @p6 
                        AND alici = @p7 
                        AND CONVERT(VARCHAR(10), tarih, 120) = @p8";

                    using (SqlCommand komut = new SqlCommand(deleteQuery, baglantı))
                    {
                        komut.Parameters.AddWithValue("@p1", selectedRow.Cells[0].Value.ToString());
                        komut.Parameters.AddWithValue("@p2", selectedRow.Cells[1].Value.ToString());
                        komut.Parameters.AddWithValue("@p3", selectedRow.Cells[2].Value.ToString());
                        komut.Parameters.AddWithValue("@p4", selectedRow.Cells[3].Value.ToString());
                        komut.Parameters.AddWithValue("@p5", selectedRow.Cells[4].Value.ToString());
                        komut.Parameters.AddWithValue("@p6", selectedRow.Cells[5].Value.ToString());
                        komut.Parameters.AddWithValue("@p7", selectedRow.Cells[6].Value.ToString());
                        komut.Parameters.AddWithValue("@p8", Convert.ToDateTime(selectedRow.Cells[7].Value).ToString("yyyy-MM-dd"));

                        int affectedRows = komut.ExecuteNonQuery();
                        
                        if (affectedRows > 0)
                        {
                            // TextEdit'leri temizle
                            ürün_kodu_text.Text = "";
                            ürün_adi_text.Text = "";
                            kg_text.Text = "";
                            tl_text.Text = "";
                            toplam_text.Text = "";
                            satici_text.Text = "";
                            alici_text.Text = "";
                            tarih_text.Text = "";

                            // Verileri yenile
                            verileriYenile();

                            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı veya silinemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int kg = int.Parse(kg_text.Text);
            int tl = int.Parse(tl_text.Text);
            toplam_text.Text = (kg * tl).ToString();
        }

        private void ürün_kodu_text_DoubleClick(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            ürünverilerigöster();
            listView2.Visible = true;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void ürünverilerigöster()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Ürün_Ekleme", baglantı);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["urun_kodu"].ToString();
                ekle.SubItems.Add(oku["urun_adi"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                listView2.Items.Add(ekle);
            }

            baglantı.Close();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                ürün_kodu_text.Text = listView2.SelectedItems[0].SubItems[0].Text;
                ürün_adi_text.Text = listView2.SelectedItems[0].SubItems[1].Text;
                listView2.Visible = false;
            }
        }

        private void satici_text_DoubleClick(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            müstahsilverilerigöster();
            listView3.Visible = true;
        }
        public void müstahsilverilerigöster()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Müstahsil_Ekleme", baglantı);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["mustahsil_kodu"].ToString();
                ekle.SubItems.Add(oku["mustahsil_adi"].ToString());
                ekle.SubItems.Add(oku["mustahsil_soyadi"].ToString());
                listView3.Items.Add(ekle);
            }

            baglantı.Close();
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                satici_text.Text = listView3.SelectedItems[0].SubItems[1].Text;
                listView3.Visible = false;
            }
        }
        private void müşteriverilerigöster()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Müsteri_Ekleme", baglantı);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["musteri_kodu"].ToString();
                ekle.SubItems.Add(oku["musteri_adi"].ToString());
                ekle.SubItems.Add(oku["musteri_soyadi"].ToString());
                listView4.Items.Add(ekle);
            }

            baglantı.Close();
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                alici_text.Text = listView4.SelectedItems[0].SubItems[1].Text;
                listView4.Visible = false;
            }
        }

        private void alici_text_DoubleClick(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            müşteriverilerigöster();
            listView4.Visible = true;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Ana_Ekran AnaEKranGeçiş = new Ana_Ekran();
            this.Hide();
            AnaEKranGeçiş.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ürün_kodu_text.Text = row.Cells[0].Value?.ToString();
                ürün_adi_text.Text = row.Cells[1].Value?.ToString();
                kg_text.Text = row.Cells[2].Value?.ToString();
                tl_text.Text = row.Cells[3].Value?.ToString();
                toplam_text.Text = row.Cells[4].Value?.ToString();
                satici_text.Text = row.Cells[5].Value?.ToString();
                alici_text.Text = row.Cells[6].Value?.ToString();
                
                // Tarih değerini DateEdit kontrolüne atama
                if (DateTime.TryParse(row.Cells[7].Value?.ToString(), out DateTime tarih))
                {
                    tarih_text.DateTime = tarih;
                }
            }
        }
    }
}
