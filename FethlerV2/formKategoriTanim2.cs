using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FethlerV2
{
    public partial class formKategoriTanim2 : Form
    {
        public formKategoriTanim2()
        {
            InitializeComponent();
        }
        FetihlerV2Entities1 db = new FetihlerV2Entities1();
        private void formKategoriTanim2_Load(object sender, EventArgs e)
        {
            listele();
            

        }
        public void listele()
        {
            var query = from item in db.tbl_Kategoriler
                        where item.Aktiflik == true
                        select new
                        {
                            item.KategoriNo,
                            item.KategoriAdi,

                        };
            bunifuCustomDataGrid1.DataSource = query.ToList();
            bunifuCustomDataGrid1.Columns[0].Visible = false;
            bunifuCustomDataGrid1.ClearSelection();
        }
        public void temizle()
        {
            txtKategoriAdi.Text = "";
            lblKategoriNo.Text = "";
            btnGuncelle.Enabled = false;
            btnKaydet.Enabled = true;
            btnSil.Enabled = false;
            txtKategoriAdi.Focus();
            listele();
        }

      

        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                lblKategoriNo.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[0].Value?.ToString();
                txtKategoriAdi.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                btnGuncelle.Enabled = true;
                btnKaydet.Enabled = false;
                btnSil.Enabled = true;


            }
            catch
            {


            }
        }

        
        void mükerrer()
        {
            var query = from d1 in db.tbl_Kategoriler
                        where d1.Aktiflik == true
                        where d1.KategoriAdi == txtKategoriAdi.Text
                        select new
                        {
                            d1.KategoriAdi
                        };

            var kategoriAdi = (from d1 in query select d1.KategoriAdi).FirstOrDefault();
            var kategoriAdi2 = txtKategoriAdi.Text;
            if (kategoriAdi==kategoriAdi2)
            {
                MessageBox.Show("Aynı Kategori Adına Sahip Kayıt Bulunmaktadır");
            }
            else
            {
                if (lblKategoriNo.Text == "")
                {
                    tbl_Kategoriler kategoriTanim = new tbl_Kategoriler();
                    if (string.IsNullOrEmpty(txtKategoriAdi.Text))
                    {
                        MessageBox.Show("Lütfen Eksik Alanları Doldurunuz");
                    }
                    else
                    {
                        kategoriTanim.KategoriAdi = txtKategoriAdi.Text;
                        kategoriTanim.Aktiflik = true;
                        db.tbl_Kategoriler.Add(kategoriTanim);
                        db.SaveChanges();
                        MessageBox.Show("Kategori Başarıyla Kayıt Edildi");
                        listele();
                        temizle();
                    }

                }
                else
                {
                    MessageBox.Show("Kayıt Tekrarı Yapılamaz");
                }
            }
        }
        

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {

            mükerrer();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lblKategoriNo.Text !="")
            {
                DialogResult onay;
                onay = MessageBox.Show("Kayıt silme işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    try
                    {
                        int kategoriNo = Convert.ToInt32(lblKategoriNo.Text);
                        var x = db.tbl_Kategoriler.Find(kategoriNo);
                        x.Aktiflik = false;
                        db.SaveChanges();
                        MessageBox.Show("Kayıt Başarıyla Silindi");
                        listele();
                        temizle();
                    }
                    catch
                    {
                        MessageBox.Show("Kayıt Silinemedi");

                    }

                }
            }
            else
            {
                MessageBox.Show("Silinecek Kayıt Seçilmedi");
            }
           
                
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int kategoriNo = Convert.ToInt32(lblKategoriNo.Text);
                var g = db.tbl_Kategoriler.Find(kategoriNo);
                g.KategoriAdi = txtKategoriAdi.Text;
                db.SaveChanges();
                MessageBox.Show("Kayıt Başarıyla Güncellendi");

            }
            catch
            {

                MessageBox.Show("Güncellenicek Kayıt Seçilmedi");
            }
            listele();
            temizle();

        }

        int hoveredRowIndex = -1;
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void bunifuCustomDataGrid1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != hoveredRowIndex)
            {
                if (hoveredRowIndex >= 0 && hoveredRowIndex < bunifuCustomDataGrid1.Rows.Count)
                {
                    // Önceki hover satırını eski haline döndür
                    bunifuCustomDataGrid1.Rows[hoveredRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                }

                // Yeni hover satırı
                bunifuCustomDataGrid1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 110, 49);
                hoveredRowIndex = e.RowIndex;
            }
        }

        private void bunifuCustomDataGrid1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (hoveredRowIndex >= 0 && hoveredRowIndex < bunifuCustomDataGrid1.Rows.Count)
            {
                bunifuCustomDataGrid1.Rows[hoveredRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                hoveredRowIndex = -1;
            }
        }

        private void bunifuCustomDataGrid1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            bunifuCustomDataGrid1.ClearSelection();
        }
    }
}
