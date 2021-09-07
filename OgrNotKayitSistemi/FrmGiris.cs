using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrNotKayitSistemi
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text != "" && maskedTextBox1.Text.Length>3)
            {

                FrmOgrDetay fr = new FrmOgrDetay();
                fr.num = maskedTextBox1.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen Boş Alanı Doldurunuz");
            }
            if (maskedTextBox1.Text == "1111")
            {
                FrmOgretmenDetay fr = new FrmOgretmenDetay();
                fr.Show();
                this.Hide();
            }

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        
    }
}
