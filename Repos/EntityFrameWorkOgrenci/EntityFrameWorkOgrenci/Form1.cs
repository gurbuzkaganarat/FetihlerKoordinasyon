using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameWorkOgrenci
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        // sınıf olusturduk
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();

        private void Btn_OgrenciListele_Click(object sender, EventArgs e)
        {  
            
            dataGridView1.DataSource=db.TBL_OGRENCİ.ToList();

            //Secili İndex Kolonu Gizledik
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;

        }

        private void Btn_NotListesi_Click(object sender, EventArgs e)
        {

            // query(sorgu) adında değiişken olusturduk.
            // from item in db.tbl notlar ---  hangi tablodan kolon alıcagımızı seçtik
            // select new ---  kolonlarımızı sectik..
            var query = from item in db.TBL_NOTLAR
                        select new { item.NOTID,item.TBL_OGRENCİ.AD, item.OGR, item.DERS, item.SINAV1, item.SINAV2, item.SIvaNAV3 };
            dataGridView1.DataSource=query.ToList();

            // item secerken item.tabloadı.kolon adı yaparak join yapabiliriz.....!!!!

        }

        private void Btn_Kaydet_Click(object sender, EventArgs e)
        {

            // veri kaydetme...
            TBL_OGRENCİ  Ogr = new TBL_OGRENCİ();
            Ogr.AD = Txt_AD.Text;
            Ogr.SOYAD = Txt_Soyad.Text;
            db.TBL_OGRENCİ.Add(Ogr);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Listeye Eklenmiştir");

        }

        private void Btn_Sil_Click(object sender, EventArgs e)
        {

            // İD  ADINDA BİR DEGİSKEN OLUSTURUP-- TXTOGRENCİID TEXTİ İNT DEGERE CEVİRİP DEGISKENE ATADIK
            
            int id = Convert.ToInt32(Txt_OgrenciID.Text);

            //X degiskeni olusturup == tablodan ıd degerını buldurduk ve o degerın karsılıgındakı verileri X'e atadık
            var x = db.TBL_OGRENCİ.Find(id);
            db.TBL_OGRENCİ.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Veri Silindi");


        }

        private void Btn_Güncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Txt_OgrenciID.Text);
            var g = db.TBL_OGRENCİ.Find(id);
            g.AD = Txt_AD.Text;
            g.SOYAD = Txt_Soyad.Text;
            g.FOTOGRAF = Txt_Fotograf.Text;
            db.SaveChanges();
            MessageBox.Show("Basarıyla Güncellenmistir");

        }

        private void Btn_Prosedur_Click(object sender, EventArgs e)
        {

            // prosedur yansıtma...
            dataGridView1.DataSource = db.NOTLISTESI();

        }

        private void Btn_Bul_Click(object sender, EventArgs e)
        {

            // & ve işareti
            // | veya işareti
            // datagridwiew datasource == tablo sectik where kullanıp parametre verip parametreye deger atadık tolist ile listeledik...
            dataGridView1.DataSource = db.TBL_OGRENCİ.Where(x => x.AD == Txt_AD.Text | x.SOYAD==Txt_Soyad.Text).ToList();

        }

        private void Txt_AD_TextChanged(object sender, EventArgs e)
        {

            // string aranan diye  degisken olusturup textboxtan gelen degeri atadık
            //  degerler diye degisken olusturup  tablo ıcınde sectıgımız kolonu contains metoduyla aranan parametresini dondurduk
            // aranan   degerıne uygun ıtemleri degerler degiskenine aktardık
            // datagride tolist metoduyla yansıttık ...
            string aranan = Txt_AD.Text;
            var degerler = from item in db.TBL_OGRENCİ
                           where item.AD.Contains(aranan)
                           select new { item.ID,item.AD,item.SOYAD };
            dataGridView1.DataSource = degerler.ToList();
        }

        private void Btn_LinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {

                 

               
                
                // a-dan z ye sıraladımmm
                
                var sıralı   = from item in db.TBL_OGRENCİ
                               orderby item.AD
                               select new {item.ID,item.AD,item.SOYAD};









                dataGridView1.DataSource = sıralı.ToList();
                
                
                
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked==true)
            {
                

                var terssıralı = from item in db.TBL_OGRENCİ
                                 orderby  item.AD descending
                                 select new {item.ID,item.AD,item.SOYAD};
                dataGridView1.DataSource=terssıralı.ToList();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked==true)
            {
                List<TBL_OGRENCİ> liste3 = db.TBL_OGRENCİ.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }

            if (radioButton4.Checked==true)
            {

                int ıd = Convert.ToInt32(Txt_OgrenciID.Text);
                List<TBL_OGRENCİ> liste4 = db.TBL_OGRENCİ.Where(p => p.ID == ıd).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked == true)
            {
            
                // başlayanlar startswith
                //bitenler endwith
                List<TBL_OGRENCİ> liste5 = db.TBL_OGRENCİ.Where(p => p.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;

            }

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            bool deger = db.TBL_OGRENCİ.Any();
            MessageBox.Show(deger.ToString());
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                //count saydır sum topla  max mın avg 
                int toplam = db.TBL_OGRENCİ.Count();
                MessageBox.Show(toplam.ToString(), "toplam ogrenci sayısı");

            }
        }

        private void Btn_SınavNotuGüncelle_Click(object sender, EventArgs e)
        {
            // join islemleri
            var sorgu = from d1 in db.TBL_NOTLAR
                        join d2 in db.TBL_OGRENCİ
                        on d1.OGR equals d2.ID
                        join d3 in db.TBL_DERS
                        on d1.DERS equals d3.DERSID
                        select new
                        {
                            ogrenci=d2.AD + " "+ d2.SOYAD,
                            ders=d3.DERSAD,
                            sınav1=d1.SINAV1,
                            sınav2=d1.SINAV2,
                            ortalama=d1.ORTALAMA,


                        };
            dataGridView1.DataSource = sorgu.ToList();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
