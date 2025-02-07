﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLAPPY_BİRDS
{
    public partial class anaEkran : Form
    {
        // bu bölgede öncelikle yer çekimini yani kuşun yere düşme hızını ,boruların geçme hızını ve skoru tanımlıyoruz
        int boruHızı = 8;
        int yerÇekimi = 10;
        int skor = 0;

        public anaEkran()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            //bu satırda kuşun ağırlığını azalt yani yere düşmesini sağlamış oluyoruz 
            if (e.KeyCode == Keys.Space)
            {
                yerÇekimi = -10;
            }
        }
        //bu fonksiyonumuzun içinde timer koydumuz oyunun içeirssinde saniyeler içerisinde sürekli yenilemesini sağlıyoruz ve kuşun yüksekliğini ayarlıyoruz,boruların konumunu ayarlıyoruz,skoru arttırıyoruz ve kuşun body si ile kolonların bodysi çarpışırsa veya zemine değer veya çok üste çıkarsa oyunu bitirmesini sağlıyoruz
        private void gametimerEvent(object sender, EventArgs e)
        {
            bird.Top += yerÇekimi;
            alt_boru.Left -= boruHızı;
            üst_boru.Left -= boruHızı;
            Skor_text.Text = "Skor =" + skor;
            if (alt_boru.Left < -150)
            {
                alt_boru.Left = 800;
                skor++;
            }
            if (üst_boru.Left < -180)
            {
                üst_boru.Left = 950;
                skor++;
            }
            if(bird.Bounds.IntersectsWith(alt_boru.Bounds)|| bird.Bounds.IntersectsWith(üst_boru.Bounds)|| bird.Bounds.IntersectsWith(ground.Bounds))
            {
                EndGame();
            }
            if (skor > 5)
            {
                boruHızı = 10;
            }
            if (skor > 10)
            {
                boruHızı = 15;
            }
            if (bird.Top < -25)
            {
                EndGame();
            }

        }
        //yerçekimini yükselterek yükselmesii sağlıyoruz
        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                yerÇekimi = 10;
            }
        }
        //oyunun sonlandırıyoruz
        void EndGame()
        {
            gametimer.Stop();
            Skor_text.Text = "GAME OVER !!!!!!!!";
        }
    }
}
