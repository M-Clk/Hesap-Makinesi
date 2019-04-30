using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HesapMakinesi_163301053_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        decimal sonuc = 0;
        bool hesapYapiliyorMu = false;
        char islemTuru='=', oncekiIslemTuru='=';
        private void NumberClick(object sender, EventArgs e)
        {
            
            Button btnNumber = (Button)sender;

            if (btnNumber.Name.Equals(btnVirgul.Name) )
            {
                if (txtSonuc.Text.Equals("") || hesapYapiliyorMu)
                {
                    txtSonuc.Text = "0,";
                    hesapYapiliyorMu = false;
                   ;
                }
                else if(txtSonuc.Text.IndexOf(',')==-1)
                {
                    txtSonuc.Text += ",";
                }
                return;
            }

            int number = int.Parse(btnNumber.Text);
            if (number == 0 && txtSonuc.Text.Equals(""))
                return;

            if (hesapYapiliyorMu)
            {
               // lblHesapDurumu.Text += sonuc + " " + islemTuru.ToString() + " ";
                txtSonuc.Clear();
            }
            txtSonuc.Text += btnNumber.Text;
            hesapYapiliyorMu = false;
        }

        private void btnEsittir_Click(object sender, EventArgs e)
        {
            oncekiIslemTuru = islemTuru;
            IslemDurumuDegistir(false);
            IslemYap();
           
        }
        
        void IslemTuruDegistir(string btnName)
        {
            if (btnName == btnArti.Name)
                islemTuru = '+';
            else if (btnName == btnEksi.Name)
                islemTuru = '-';
            else if (btnName == btnCarpi.Name)
                islemTuru = '*';
            else if (btnName == btnBolu.Name)
                islemTuru = '/';

            hesapYapiliyorMu = true;
        }
        private void OperatorClick(object sender, EventArgs e)
        {
            if (txtSonuc.Text.Equals("") && islemTuru=='=') return;
            Button btn = (Button)sender;
            if(hesapYapiliyorMu)
            {
                
                IslemTuruDegistir(btn.Name);
                IslemDurumuDegistir(true);
                return;
            }
            oncekiIslemTuru = islemTuru;
            IslemTuruDegistir(btn.Name);
            IslemDurumuDegistir(false);
            IslemYap();
            
            
        }
        void IslemDurumuDegistir(bool degistir)
        {
            if (degistir)
            {
                lblHesapDurumu.Text = lblHesapDurumu.Text.Remove((lblHesapDurumu.Text.Length - 1), 1) + islemTuru;
                return;
            }
            if (txtSonuc.Text[txtSonuc.Text.Length - 1] == ',')
                txtSonuc.Text = txtSonuc.Text.Remove(txtSonuc.Text.Length - 1, 1);
            if ((islemTuru == '/' || islemTuru == '*') )
                lblHesapDurumu.Text = "(" + lblHesapDurumu.Text + txtSonuc.Text + ") " + islemTuru; 
            else if (decimal.Parse(txtSonuc.Text)<0)
                lblHesapDurumu.Text += " (" + txtSonuc.Text + ") " + islemTuru;
            else
                lblHesapDurumu.Text += " "+txtSonuc.Text + " " + islemTuru;
            
               
        }
        void IslemYap()
        {
            if (txtSonuc.Text[txtSonuc.Text.Length - 1] == '-')
                txtSonuc.Text = txtSonuc.Text.Remove(txtSonuc.Text.Length-1, 1);
           
            if (!txtSonuc.Text.Equals(""))
            {
                decimal simdikiSayi = decimal.Parse(txtSonuc.Text);
                if (txtSonuc.Text[txtSonuc.Text.Length - 1] == ',')
                    txtSonuc.Text = txtSonuc.Text.Remove(txtSonuc.Text.Length - 1, 1);
                
                    switch (oncekiIslemTuru)
                {
                    case '+':
                        sonuc += simdikiSayi;
                        break;
                    case '-':
                        sonuc -= simdikiSayi;
                        break;
                    case '*':
                        sonuc *= simdikiSayi;
                        break;
                    case '/':
                        sonuc /= simdikiSayi;
                        break;
                    case '=':
                        sonuc = simdikiSayi;
                        break;
                }
                Int64 tempSayi = (Int64)sonuc;
                if (sonuc - tempSayi > 0) txtSonuc.Text = sonuc.ToString();
                else txtSonuc.Text = tempSayi.ToString(); 
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSonuc.Clear();
            islemTuru = '=';
            sonuc = 0;
            hesapYapiliyorMu = false;
            lblHesapDurumu.Text = "";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                NumberClick(this.Controls["btnNumber" + e.KeyChar], new EventArgs());
            }
            catch (Exception)
            {
                
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if(!txtSonuc.Text.Equals(""))
            {
                txtSonuc.Text = txtSonuc.Text.Remove(txtSonuc.Text.Length - 1, 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSonuc.Text.Equals("") || hesapYapiliyorMu)
                return;
            if(txtSonuc.Text.IndexOf('-')==-1)
            {
                txtSonuc.Text = "-" + txtSonuc.Text;
            }
            else
            {
                txtSonuc.Text = txtSonuc.Text.Remove(0, 1);
            }
            
        }
    }
}
