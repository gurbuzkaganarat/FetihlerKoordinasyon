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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var degerler = db.TBL_NOTLAR.Where(x => x.SINAV1 < 50);
                //list kullanımında column kapatılmıyor ...
                dataGridView1.DataSource = degerler.ToList();
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
            }
            if (radioButton2.Checked == true)
            {
                var degerler = db.TBL_OGRENCİ.Where(x => x.AD == "ali");
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton3.Checked == true)
            {
                var degerler = db.TBL_OGRENCİ.Where(x => x.AD == textBox1.Text || x.SOYAD == textBox1.Text);
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton4.Checked == true)
            {
                var degerler = db.TBL_OGRENCİ.Select(x => new { soyadı = x.SOYAD });
                dataGridView1.DataSource = degerler.ToList();

            }
            //select anonymous type
            if (radioButton5.Checked == true)
            {
                var degerler = db.TBL_OGRENCİ.Select(x => 
                new { Ad = x.AD.ToUpper(),
                    Soyad = x.SOYAD.ToLower() });
                dataGridView1.DataSource = degerler.ToList();

            }



            //şarlı seçim
            if (radioButton6.Checked == true)
            {
                var degerler = db.TBL_OGRENCİ.Select(x =>
                new {
                    Ad = x.AD.ToUpper(),
                    Soyad = x.SOYAD.ToLower()
                }).Where(x=>x.Ad!="ali");
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton7.Checked == true)
            {
                var degerler = db.TBL_NOTLAR.Select(x =>

                new
                {
                    OgrenciAd=x.OGR,
                    Ortalaması=x.ORTALAMA,
                    



                }
                
                
                
                
                );



                

            }


            if (radioButton8.Checked == true)
            {
                var degerler = db.TBL_NOTLAR.SelectMany(x => db.TBL_OGRENCİ.Where(y => y.ID == x.OGR),(x,y)=>new
                {
                    y.AD,
                    x.ORTALAMA



                }
                );
                dataGridView1.DataSource = degerler.ToList();

            }

        }
    }
}
