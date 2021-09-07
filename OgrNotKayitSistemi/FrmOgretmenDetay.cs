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
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        void gecen()
        {
            baglanti.Open();
            SqlCommand komut0 = new SqlCommand("select count(OgrID) from TblDers where Durum='true' ", baglanti);
            SqlDataReader dr1 = komut0.ExecuteReader();
            if(dr1.Read())
            {
                LblGecenSayi.Text = dr1[0].ToString();
            }
            baglanti.Close();
        }
        void kalan()
        {
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("select count(OgrID) from TblDers where Durum='false' ", baglanti);
            SqlDataReader dr2 = komut6.ExecuteReader();
            if (dr2.Read())
            {
                LblKalanSayi.Text = dr2[0].ToString();
            }
            baglanti.Close();
        }
        void sinif()
        {
            baglanti.Open();
            SqlCommand komut7 = new SqlCommand("select avg(OgrOrtalama) from TblDers", baglanti);
            SqlDataReader dr = komut7.ExecuteReader();
            if (dr.Read())
            {
                LblOrtalama.Text = dr[0].ToString();
            }
            baglanti.Close();
        }
        void list()
        {
            this.tblDersTableAdapter1.Fill(this.dbNotKayitDataSet1.TblDers);
        }
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet1.TblDers' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TblDers' table. You can move, or remove it, as needed.
            //this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);
            list();
            sinif();
            gecen();
            kalan();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-OC5036T\MSSQLSERVER1;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            if (MsTxtOgrNum.Text.Trim() != "" && TxtAd.Text.Trim() != "" && TxtSoyad.Text.Trim() != "")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TblDers (OgrNum,OgrAd,OgrSoyad) values (@p1,@p2,@p3)", baglanti);
                komut.Parameters.AddWithValue("@p1", MsTxtOgrNum.Text);
                komut.Parameters.AddWithValue("@p2", TxtAd.Text);
                komut.Parameters.AddWithValue("@p3", TxtSoyad.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Öğrencinin Kaydı Yapıldı");
                list();
                gecen();
                kalan();
            }
            else
            {
                MessageBox.Show("Lütfen Öğrenci Bilgilerinin Eksiksiz Doldurunuz");
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label11.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            MsTxtOgrNum.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            LblOrt.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            string durum = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            if(durum=="True")
            {
                LblDurum.Text = "Geçti";
            }
            else
            {
                LblDurum.Text = "Kaldı";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MsTxtOgrNum.Text.Trim() != "" && TxtAd.Text.Trim() != "" && TxtSoyad.Text.Trim() != "" && label11.Text != "0")
            {
                double s1=0, s2=0, s3=0,ortalama;
                s1 = Convert.ToDouble(TxtSinav1.Text);
                s2 = Convert.ToDouble(TxtSinav2.Text);
                s3 = Convert.ToDouble(TxtSinav3.Text);
                ortalama = (s1 + s2 + s3) / 3;
                LblOrt.Text = Convert.ToString(ortalama);
                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("update TblDers set OgrNum=@p1,OgrAd=@p2,OgrSoyad=@p3,OgrS1=@p4,OgrS2=@p5,OgrS3=@p6,OgrOrtalama=@p7,Durum=@p9 where OgrID=@p8", baglanti);
                komut1.Parameters.AddWithValue("@p8", label11.Text);
                komut1.Parameters.AddWithValue("@p1", MsTxtOgrNum.Text);
                komut1.Parameters.AddWithValue("@p2", TxtAd.Text);
                komut1.Parameters.AddWithValue("@p3", TxtSoyad.Text);
                komut1.Parameters.AddWithValue("@p4", s1);
                komut1.Parameters.AddWithValue("@p5", s2);
                komut1.Parameters.AddWithValue("@p6", s3);
                komut1.Parameters.AddWithValue("@p7", ortalama);
                if(ortalama>=50)
                {
                    komut1.Parameters.AddWithValue("@p9", "true");
                }
                else
                {
                    komut1.Parameters.AddWithValue("@p9", "false");
                }
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme Yapıldı");
                list();
                sinif();
                gecen();
                kalan();
            }
            else
            {
                MessageBox.Show("Lütfen Girilen Değerleri Kontrol Ediniz");
            }
        }
    }
}
