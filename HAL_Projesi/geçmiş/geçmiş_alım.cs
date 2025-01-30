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

namespace HAL_Projesi.geçmiş
{
    public partial class geçmiş_alım : Form
    {
        public geçmiş_alım()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            // Ana forma dön
            Ana_Ekran anaForm = new Ana_Ekran();
            anaForm.Show();
            this.Hide();
        }

        private void geçmiş_alım_Load(object sender, EventArgs e)
        {
            try
            {
                // Kısıtlamaları geçici olarak devre dışı bırak
                this.giriş_kayıtlarıDataSet1.EnforceConstraints = false;
                
                // Bugünün tarihini ayarla
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;

                // DataGridView ayarlarını yap
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "ürün_kodu", HeaderText = "Ürün Kodu", Width = 100 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "ürün_adi", HeaderText = "Ürün Adı", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "ürün_KG", HeaderText = "KG", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "ürün_TL", HeaderText = "TL", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "ürün_toplam", HeaderText = "Toplam", Width = 100 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "satici", HeaderText = "Satıcı", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "alici", HeaderText = "Alıcı", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "tarih", HeaderText = "Tarih", Width = 150 });

                // Verileri yükle
                this.alım_gecmisiTableAdapter.Fill(this.giriş_kayıtlarıDataSet1.Alım_gecmisi);
                dataGridView1.DataSource = this.alımGecmisiBindingSource;

                // Görünüm ayarları
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10F, FontStyle.Bold);
                dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 10F);
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
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
                                ürün_kodu,
                                ürün_adi,
                                ürün_KG,
                                ürün_TL,
                                ürün_toplam,
                                satici,
                                alici,
                                tarih
                                FROM Alım_gecmisi 
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
                    double toplamKG = dt.AsEnumerable().Sum(row => Convert.ToDouble(row["ürün_KG"]));
                    double toplamTutar = dt.AsEnumerable().Sum(row => Convert.ToDouble(row["ürün_toplam"]));
                    MessageBox.Show($"Toplam {dt.Rows.Count} kayıt bulundu.\nToplam KG: {toplamKG:N2}\nToplam Tutar: {toplamTutar:N2} TL");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void alımGecmisiBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}