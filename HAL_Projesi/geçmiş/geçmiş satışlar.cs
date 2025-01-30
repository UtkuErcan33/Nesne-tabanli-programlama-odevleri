using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HAL_Projesi
{
    public partial class geçmiş_satışlar : Form
    {
        public geçmiş_satışlar()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");

        private void geçmiş_satışlar_Load(object sender, EventArgs e)
        {
            try
            {
                // Bugünün tarihini ayarla
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;

                // DataGridView ayarlarını yap
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10F, FontStyle.Bold);
                dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 9F);
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
                dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.White;

                // Buton renklerini ayarla
                button1.BackColor = Color.FromArgb(41, 128, 185);
                button1.ForeColor = Color.White;
                button1.FlatStyle = FlatStyle.Flat;
                button1.FlatAppearance.BorderSize = 0;

                button2.BackColor = Color.FromArgb(46, 204, 113);
                button2.ForeColor = Color.White;
                button2.FlatStyle = FlatStyle.Flat;
                button2.FlatAppearance.BorderSize = 0;

                button3.BackColor = Color.FromArgb(231, 76, 60);
                button3.ForeColor = Color.White;
                button3.FlatStyle = FlatStyle.Flat;
                button3.FlatAppearance.BorderSize = 0;

                button4.BackColor = Color.FromArgb(52, 152, 219);
                button4.ForeColor = Color.White;
                button4.FlatStyle = FlatStyle.Flat;
                button4.FlatAppearance.BorderSize = 0;

                // Verileri yükle
                verileriGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void verileriGoster()
        {
            try
            {
                dataGridView1.DataSource = null;
                baglantı.Open();

                string sorgu = @"SELECT 
                    Ürün_Kodu as 'Ürün Kodu',
                    Ürün_Adi as 'Ürün Adı',
                    CAST(Ürün_KG as decimal(18,2)) as 'KG',
                    CAST(Ürün_TL as decimal(18,2)) as 'Birim Fiyat',
                    CAST(Ürün_Toplam as decimal(18,2)) as 'Toplam Tutar',
                    Araç_Plaka as 'Araç Plaka',
                    Satici as 'Satıcı',
                    Alici as 'Alıcı',
                    Tarih
                    FROM Satis_Gecmis 
                    ORDER BY Tarih DESC";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglantı);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Sütun formatlarını ayarla
                if (dataGridView1.Columns["Tarih"] != null)
                    dataGridView1.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy";

                if (dataGridView1.Columns["KG"] != null)
                    dataGridView1.Columns["KG"].DefaultCellStyle.Format = "N2";

                if (dataGridView1.Columns["Birim Fiyat"] != null)
                    dataGridView1.Columns["Birim Fiyat"].DefaultCellStyle.Format = "N2";

                if (dataGridView1.Columns["Toplam Tutar"] != null)
                    dataGridView1.Columns["Toplam Tutar"].DefaultCellStyle.Format = "N2";

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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("Başlangıç tarihi bitiş tarihinden büyük olamaz.");
                    return;
                }

                baglantı.Open();
                
                string query = @"SELECT 
                                Ürün_Kodu as 'Ürün Kodu',
                                Ürün_Adi as 'Ürün Adı',
                                CAST(Ürün_KG as decimal(18,2)) as 'KG',
                                CAST(Ürün_TL as decimal(18,2)) as 'Birim Fiyat',
                                CAST(Ürün_Toplam as decimal(18,2)) as 'Toplam Tutar',
                                Araç_Plaka as 'Araç Plaka',
                                Satici as 'Satıcı',
                                Alici as 'Alıcı',
                                Tarih
                                FROM Satis_Gecmis 
                                WHERE tarih >= @baslangicTarihi 
                                AND tarih < DATEADD(day, 1, @bitisTarihi)
                                ORDER BY tarih DESC";

                SqlCommand cmd = new SqlCommand(query, baglantı);
                cmd.Parameters.AddWithValue("@baslangicTarihi", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@bitisTarihi", dateTimePicker2.Value.Date);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Seçilen tarih aralığında kayıt bulunamadı.");
                }
                else
                {
                    decimal toplamKG = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["KG"]));
                    decimal toplamTutar = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Toplam Tutar"]));

                    string mesaj = $"Toplam {dt.Rows.Count} kayıt bulundu.\n\n" +
                                 $"Toplam KG: {toplamKG:N2}\n" +
                                 $"Toplam Tutar: {toplamTutar:N2} TL";

                    MessageBox.Show(mesaj, "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                baglantı.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri getirme sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglantı.State == ConnectionState.Open)
                    baglantı.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ana_Ekran anaForm = new Ana_Ekran();
            anaForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            verileriGoster();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Seçili kaydı silmek istediğinize emin misiniz?", "Onay", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int urunKodu = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Ürün Kodu"].Value);
                        string tarih = dataGridView1.SelectedRows[0].Cells["Tarih"].Value.ToString();

                        baglantı.Open();
                        SqlCommand komut = new SqlCommand(
                            "DELETE FROM Satis_Gecmis WHERE Ürün_Kodu = @urunKodu AND Tarih = @tarih", 
                            baglantı);

                        komut.Parameters.AddWithValue("@urunKodu", urunKodu);
                        komut.Parameters.AddWithValue("@tarih", tarih);
                        komut.ExecuteNonQuery();
                        baglantı.Close();

                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        verileriGoster();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silinecek kaydı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}