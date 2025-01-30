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

namespace HAL_Projesi
{
    public partial class Sevkiyat : DevExpress.XtraEditors.XtraForm
    {
        public Sevkiyat()
        {
            InitializeComponent();
        }

        private void Sevkiyat_Load(object sender, EventArgs e)
        {
            try
            {
                // Kısıtlamaları geçici olarak devre dışı bırak
                this.giriş_kayıtlarıDataSet2.EnforceConstraints = false;
                
                // DataGridView'in veri kaynağını temizle
                dataGridView1.DataSource = null;
                
                // Dataset'i temizle ve yeniden doldur
                this.giriş_kayıtlarıDataSet2.Clear();
                this.satis_GecmisTableAdapter.Fill(this.giriş_kayıtlarıDataSet2.Satis_Gecmis);
                
                // DataGridView'e veri kaynağını bağla
                dataGridView1.DataSource = this.satisGecmisBindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme sırasında bir hata oluştu: " + ex.Message);
            }
        }
        SqlConnection baglantı = new SqlConnection("Data Source = UTKU\\MSSQLSERVER01; Initial Catalog = giriş_kayıtları; Integrated Security = True");

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void textEdit9_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // Yeni eklenmiş boş satırları atla
                {
                    string column1 = row.Cells[0].Value?.ToString() ?? ""; // 1. sütun
                    string column2 = row.Cells[1].Value?.ToString() ?? ""; // 2. sütun
                    string column3 = row.Cells[2].Value?.ToString() ?? ""; // 3. sütun
                    string column4 = row.Cells[3].Value?.ToString() ?? "";
                    string column5 = row.Cells[4].Value?.ToString() ?? "";
                    string column6 = row.Cells[5].Value?.ToString() ?? "";
                    string column7 = row.Cells[6].Value?.ToString() ?? "";
                    string column8 = row.Cells[7].Value?.ToString() ?? "";
                    string column9 = row.Cells[8].Value?.ToString() ?? "";

                    // Veritabanına kayıt sorgusu
                    string query = "INSERT INTO Satis_Gecmis (Ürün_Kodu,Ürün_Adi,Ürün_KG,Ürün_TL,Ürün_Toplam,Araç_Plaka,Satici,Alici,Tarih) VALUES (@ürün_kodu, @ürün_Adi, @ürün_KG, @ürün_TL, @ürün_Toplam,@Araç_Plaka, @satici, @alici, @tarih)";

                    using (SqlCommand command = new SqlCommand(query, baglantı))
                    {
                        command.Parameters.AddWithValue("@ürün_kodu", column1);
                        command.Parameters.AddWithValue("@ürün_Adi", column2);
                        command.Parameters.AddWithValue("@ürün_KG", column3);
                        command.Parameters.AddWithValue("@ürün_TL", column4);
                        command.Parameters.AddWithValue("@ürün_Toplam", column5);
                        command.Parameters.AddWithValue("@Araç_Plaka", column6);
                        command.Parameters.AddWithValue("@satici", column7);
                        command.Parameters.AddWithValue("@alici", column8);
                        command.Parameters.AddWithValue("@tarih", column9);

                        command.ExecuteNonQuery(); // Sorguyu çalıştır


                    }
                }
            }
            MessageBox.Show("Kayıt işlemi başarılı");

            baglantı.Close();


        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                dataGridView1.DataSource = null; // DataSource bağlantısını kaldır
            }

            // Kolonları manuel olarak ekleyin (eğer henüz eklenmediyse)
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("ÜrünKodu", "Ürün Kodu");
                dataGridView1.Columns.Add("ÜrünAdi", "Ürün Adı");
                dataGridView1.Columns.Add("ÜrünKG", "KG");
                dataGridView1.Columns.Add("ÜrünTL", "TL");
                dataGridView1.Columns.Add("ÜrünToplam", "Toplam");
                dataGridView1.Columns.Add("AraçPlaka", "Araç Plaka");
                dataGridView1.Columns.Add("Satici", "Satıcı");
                dataGridView1.Columns.Add("Alici", "Alıcı");
                dataGridView1.Columns.Add("Tarih", "Tarih");
            }

            // Yeni satır ekleyin
            dataGridView1.Rows.Add(
                textEdit1.Text,
                textEdit2.Text,
                textEdit6.Text,
                textEdit7.Text,
                textEdit8.Text,
                textEdit9.Text,
                textEdit3.Text,
                textEdit4.Text,
                textEdit5.Text
                
            );

            // TextEdit'leri temizle
            textEdit1.Text = "";
            textEdit2.Text = "";
            textEdit6.Text = "";
            textEdit7.Text = "";
            textEdit8.Text = "";
            textEdit9.Text = "";
            textEdit3.Text = "";
            textEdit4.Text = "";
            textEdit5.Text = "";
        }



        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int kg = int.Parse(textEdit6.Text);
            int tl = int.Parse(textEdit7.Text);
            textEdit8.Text = (kg * tl).ToString();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit1_DoubleClick(object sender, EventArgs e)
        {
            listView1.Items.Clear(); // Listeyi temizleyin, böylece tekrar tekrar ekleme yapmaz
            ürünverilerigöster();
            listView1.Visible = true; // `listView2`yi görünür hale getirin
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
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
                listView1.Items.Add(ekle);
            }

            baglantı.Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Seçilen öğenin `ürün_kodu` ve `ürün_adi` bilgilerini text kutularına aktarın
                textEdit1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textEdit2.Text = listView1.SelectedItems[0].SubItems[1].Text;

                // `listView2`yi gizleyin
                listView1.Visible = false;
            }
        }

        private void textEdit3_DoubleClick(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            müstahsilverilerigöster();
            listView2.Visible = true;
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
                ekle.SubItems.Add(oku["mustahsil_adi"].ToString());
                listView2.Items.Add(ekle);
            }

            baglantı.Close();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                // Seçilen öğenin `ürün_kodu` ve `ürün_adi` bilgilerini text kutularına aktarın
                textEdit3.Text = listView2.SelectedItems[0].SubItems[1].Text;


                // `listView2`yi gizleyin
                listView2.Visible = false;
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
                ekle.SubItems.Add(oku["musteri_tel"].ToString());
                listView3.Items.Add(ekle);
            }

            baglantı.Close();
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                // Seçilen öğenin `ürün_kodu` ve `ürün_adi` bilgilerini text kutularına aktarın
                textEdit4.Text = listView3.SelectedItems[0].SubItems[1].Text;


                // `listView2`yi gizleyin
                listView3.Visible = false;
            }

        }

        private void textEdit4_DoubleClick(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            müşteriverilerigöster();
            listView3.Visible = true;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                if (dataGridView1.DataSource == null)
                {
                    dataGridView1.Rows.RemoveAt(selectedRowIndex);
                }
                else if (dataGridView1.DataSource is BindingSource bindingSource)
                {
                    if (bindingSource.DataSource is DataTable dataTable)
                    {
                        if (selectedRowIndex >= 0 && selectedRowIndex < dataTable.Rows.Count)
                        {
                            dataTable.Rows[selectedRowIndex].Delete();
                        }
                    }
                    else
                    {
                        bindingSource.RemoveCurrent();
                    }
                }
                else
                {
                    MessageBox.Show("Veri kaynağı tanımlanamadı veya desteklenmiyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Ana_Ekran AnaEKranGeçiş = new Ana_Ekran();
            this.Hide();
            AnaEKranGeçiş.Show();

        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                // Seçilen öğenin `ürün_kodu` ve `ürün_adi` bilgilerini text kutularına aktarın
                textEdit9.Text = listView4.SelectedItems[0].SubItems[3].Text;


                // `listView2`yi gizleyin
                listView4.Visible = false;
            }
        }
        public void plakaGöster()
        {
            listView4.Items.Clear();
            baglantı.Open();

            // `sıra` sütununu da getiriyoruz
            SqlCommand komut = new SqlCommand("SELECT sıra, Arac_Sahibi, Arac_İsim, Araç_Plaka FROM Plaka_Ekleme", baglantı);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem(oku["sıra"].ToString()); // `sıra` sütununu ekle
                ekle.SubItems.Add(oku["Arac_Sahibi"].ToString());
                ekle.SubItems.Add(oku["Arac_İsim"].ToString());
                ekle.SubItems.Add(oku["Araç_Plaka"].ToString());
                listView4.Items.Add(ekle);
            }

            baglantı.Close();
        }
        private void textEdit9_DoubleClick(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            plakaGöster();
            listView4.Visible = true;
        }
    }
}