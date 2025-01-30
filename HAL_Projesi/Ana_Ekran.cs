using DevExpress.XtraBars;
using HAL_Projesi.alım;
using HAL_Projesi.Ekleme;
using HAL_Projesi.geçmiş;
using HAL_Projesi.satış;
using System;
using System.Windows.Forms;

namespace HAL_Projesi
{
    public partial class Ana_Ekran : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public Ana_Ekran()
        {
            InitializeComponent();
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            Ürün_Ekleme ürün_Ekleme = new Ürün_Ekleme();
            this.Hide();
            ürün_Ekleme.Show();
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            Kasa_Ekleme kasa_Ekleme = new Kasa_Ekleme();
            this.Hide();
            kasa_Ekleme.Show();
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            Plaka_Ekleme plaka_Ekleme = new Plaka_Ekleme();
            this.Hide();
            plaka_Ekleme.Show();
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            Müsteri_Ekleme müsteri_Ekleme = new Müsteri_Ekleme();
            this.Hide();
            müsteri_Ekleme.Show();
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            Müstahsil_Ekleme müstahsil_Ekleme = new Müstahsil_Ekleme();
            this.Hide();
            müstahsil_Ekleme.Show();
        }

        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            Müstahsil_Alım müstahsil_Alım = new Müstahsil_Alım();
            this.Hide();
            müstahsil_Alım.Show();
        }

        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
            hal_içi_alım hal_İçi_Alım = new hal_içi_alım();
            this.Hide();
            hal_İçi_Alım.Show();
        }

        private void accordionControlElement14_Click(object sender, EventArgs e)
        {
            Sevkiyat sevkiyat = new Sevkiyat();
            this.Hide();
            sevkiyat.Show();
        }

        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            dükkan_önüSatış dükkan = new dükkan_önüSatış();
            this.Hide();
            dükkan.Show();
        }

        private void accordionControlElement17_Click(object sender, EventArgs e)
        {
            geçmiş_alım geçmiş_Alım = new geçmiş_alım();
            this.Hide();
            geçmiş_Alım.Show();
        }

        private void accordionControlElement26_Click(object sender, EventArgs e)
        {
            Sifre_Ekranı SifreGeçiş = new Sifre_Ekranı();
            this.Hide();
            SifreGeçiş.Show();
        }

        private void fluentDesignFormContainer1_Click(object sender, EventArgs e)
        {
        }

        private void accordionControlElement18_Click(object sender, EventArgs e)
        {
            geçmiş_satışlar geçmiş_Satışlar = new geçmiş_satışlar();
            this.Close();
            geçmiş_Satışlar.Show();
        }
    }
}
