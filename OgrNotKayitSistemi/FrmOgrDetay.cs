using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OgrNotKayitSistemi
{
    public partial class FrmOgrDetay : Form
    {
        public FrmOgrDetay()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmGiris fr = new FrmGiris();
            fr.Show();
            this.Close();
        }
        public string num;
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-OC5036T\MSSQLSERVER1;Initial Catalog=DbNotKayit;Integrated Security=True");
        private void FrmOgrDetay_Load(object sender, EventArgs e)
        {

            LblNum.Text = num;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TblDers where OgrNum=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", num);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                LblSinav1.Text = dr[4].ToString();
                LblSinav2.Text = dr[5].ToString();
                LblSinav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                string durum = dr[8].ToString();
                if(durum=="True")
                {
                    LblDurum.Text = "Geçti";
                }
                else
                {
                    LblDurum.Text = "Kaldı";
                }
            }
            baglanti.Close();
        
        }
            
       
    
        }
    }

