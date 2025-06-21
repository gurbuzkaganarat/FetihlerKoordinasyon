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
    public partial class formKoyTanim : Form
    {
        public formKoyTanim()
        {
            InitializeComponent();
        }
        FetihlerV2Entities1 db = new FetihlerV2Entities1();


        private void cmbDataLoad()
        {

            var sıralı = from item in db.tbl_Bolgeler
                         where item.Aktiflik == true
                         orderby item.BolgeAdi
                         select new
                         {
                             item.BolgeNo,
                             item.BolgeAdi,

                         };


            cmbBolgeAd.DataSource = sıralı.ToList();
            cmbBolgeAd.ValueMember = "BolgeNo";
            cmbBolgeAd.DisplayMember = "BolgeAdi";

        }
        private void listele()
        {

            var query = from d1 in db.tbl_Koyler
                        join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list from d2 in d2list.DefaultIfEmpty()  // Önemli !!!  İlişkili Tabloda null değerleri listeler

                        where d1.Aktiflik == true

                        select new
                        {
                            KöyNo = d1.KoyNo,
                            Bolge = d2.BolgeAdi,
                            KöyAdi = d1.KoyAdi,
                            MuhtarAdi = d1.MuhtarAdi,
                            MuhtarTel = d1.MuhtarCepTel,
                            YardimciAdi = d1.YardimciAdi,
                            YardimciTel = d1.YardimciTel,
                            Köyİhtiyac = d1.KoyDetay,
                            GüzergahSırası = d1.Güzergah,
                            KonumKoordinat = d1.Konum

                        };
            bunifuCustomDataGrid1.DataSource = query.ToList();
            bunifuCustomDataGrid1.Columns[0].Visible = false;
            bunifuCustomDataGrid1.ClearSelection();






        }
        private void formKoyTanim_Load(object sender, EventArgs e)
        {

            listele();
            cmbDataLoad();
            temizle();


        }
        private void temizle()
        {
            txtKoyAd.Text = "";
            txtMuhtarAd.Text = "";
            txtMuhtarTelefon.Text = "";
            txtYardimciTelefon.Text = "";
            txtYardimciAd.Text = "";
            rchKoyDetay.Text = "";
            lblKoyNo.Text = "";
            txtKoyAd.Focus();
            cmbBolgeAd.SelectedItem = null;
            cmbBolgeAd.SelectedItem = null;
            txtKonumKoor.Text = "";
            txt_guzergah.Text = "";
            btnGuncelle.Enabled = false;
            btnKaydet.Enabled = true;
            btnSil.Enabled = false;
            bunifuCustomDataGrid1.ClearSelection();
            if (txtBolgeAra.Text == string.Empty && txtKoyAra.Text == string.Empty)
            {
                listele();
            }
            else {
                koyara(bunifuCustomDataGrid1);
                bolgeara(bunifuCustomDataGrid1);
            }
           // listele();

        }





        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                lblKoyNo.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[0].Value?.ToString();
                cmbBolgeAd.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                txtKoyAd.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[2].Value?.ToString();
                txtMuhtarAd.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[3].Value?.ToString();
                txtMuhtarTelefon.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[4].Value?.ToString();
                txtYardimciAd.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[5].Value?.ToString();
                txtYardimciTelefon.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[6].Value?.ToString();
                rchKoyDetay.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[7].Value?.ToString();
                txt_guzergah.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[8].Value?.ToString();
                txtKonumKoor.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[9].Value?.ToString();
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
            var query = from d1 in db.tbl_Koyler
                        where d1.Aktiflik == true
                        where d1.KoyAdi == txtKoyAd.Text
                        select new
                        {
                            d1.KoyAdi
                        };
            var koyad = (from d1 in query select d1.KoyAdi).FirstOrDefault();
            var koyad2 = txtKoyAd.Text;
            if (koyad == koyad2)
            {
                MessageBox.Show("Aynı Köy Adına Sahip Kayıt Bulunmaktadır!");
            }
            else
            {



                if (lblKoyNo.Text == "")
                {
                    tbl_Koyler koyTanim = new tbl_Koyler();
                    if (string.IsNullOrEmpty(txtKoyAd.Text))
                    {
                        MessageBox.Show("Lütfen Eksik Alanları Doldurunuz.");
                    }
                    else
                    {
                        koyTanim.KoyAdi = txtKoyAd.Text;

                        int bolgeNo = Convert.ToInt32(cmbBolgeAd.SelectedValue);
                        koyTanim.Bolge = bolgeNo;
                        koyTanim.MuhtarAdi = txtMuhtarAd.Text;
                        koyTanim.MuhtarCepTel = txtMuhtarTelefon.Text;
                        koyTanim.YardimciAdi = txtYardimciAd.Text;
                        koyTanim.YardimciTel = txtYardimciTelefon.Text;
                        koyTanim.KoyDetay = rchKoyDetay.Text;
                        koyTanim.Aktiflik = true;
                        koyTanim.Sec = false;
                        koyTanim.Güzergah = Convert.ToInt32( txt_guzergah.Text);
                        koyTanim.Konum = txtKonumKoor.Text;

                        db.tbl_Koyler.Add(koyTanim);
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Köy Başarıyla Kayıt Edildi");

                            listele();
                            temizle();
                        }
                        catch
                        {
                            MessageBox.Show("Lütfen Eksik  Bilgileri Giriniz");

                        }
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

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            if (lblKoyNo.Text != "")
            {
                DialogResult onay;
                onay = MessageBox.Show("Kayıt silme işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    try
                    {
                        int koyNo = Convert.ToInt32(lblKoyNo.Text);
                        var x = db.tbl_Koyler.Find(koyNo);



                        x.Aktiflik = false;

                        var kisiler = db.tbl_Kisiler.Where(a => a.Koy == koyNo).ToList();
                        foreach (var kisi in kisiler)
                        {
                            kisi.Aktiflik = false;
                        }

                        db.SaveChanges();
                        MessageBox.Show("Kayıt Başarıyla Silindi");
                        temizle();
                        listele();
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

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            try
            {
                tbl_Koyler koyTanim = new tbl_Koyler();
                int koyNo = Convert.ToInt32(lblKoyNo.Text);
                var g = db.tbl_Koyler.Find(koyNo);
                g.KoyAdi = txtKoyAd.Text;
                int bolgeNo = Convert.ToInt32(cmbBolgeAd.SelectedValue);
                g.Bolge = bolgeNo;
                g.MuhtarAdi = txtMuhtarAd.Text;
                g.MuhtarCepTel = txtMuhtarTelefon.Text;
                g.YardimciAdi = txtYardimciAd.Text;
                g.YardimciTel = txtYardimciTelefon.Text;
                g.KoyDetay = rchKoyDetay.Text;
                g.Güzergah = Convert.ToInt32(txt_guzergah.Text);
                g.Konum = txtKonumKoor.Text;
                db.SaveChanges();
                MessageBox.Show("Kayıt Başarıyla Güncellendi");

            }
            catch
            {
                MessageBox.Show("Güncellenicek Kayıt Seçilmedi");

            }
            temizle();
           // listele();
        }

        private void btnTemizle_Click_1(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        public void bolgeara(DataGridView data2)
        {
            string bolgeAra = txtBolgeAra.Text;
            string koyAra = txtKoyAra.Text;

            if (String.IsNullOrEmpty(koyAra))
            {
                var query = from d1 in db.tbl_Koyler
                            join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                            from d2 in d2list.DefaultIfEmpty()  // Önemli !!!  İlişkili Tabloda null değerleri listeler

                            where d1.Aktiflik == true
                            where d2.BolgeAdi.Contains(bolgeAra)
                            select new
                            {
                                KöyNo = d1.KoyNo,
                                Bolge = d2.BolgeAdi,
                                KöyAdi = d1.KoyAdi,
                                MuhtarAdi = d1.MuhtarAdi,
                                MuhtarTel = d1.MuhtarCepTel,
                                YardimciAdi = d1.YardimciAdi,
                                YardimciTel = d1.YardimciTel,
                                Köyİhtiyac = d1.KoyDetay,
                                GüzergahSırası = d1.Güzergah,
                                KonumKoordinat = d1.Konum,

                            };

                data2.DataSource = query.ToList();
                data2.Columns[0].Visible = false;
            }
            else
            {
                var query = from d1 in db.tbl_Koyler
                            join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                            from d2 in d2list.DefaultIfEmpty()  // Önemli !!!  İlişkili Tabloda null değerleri listeler

                            where d1.Aktiflik == true
                            where d2.BolgeAdi.Contains(bolgeAra) && d1.KoyAdi.Contains(koyAra)

                            select new
                            {
                                KöyNo = d1.KoyNo,
                                Bolge = d2.BolgeAdi,
                                KöyAdi = d1.KoyAdi,
                                MuhtarAdi = d1.MuhtarAdi,
                                MuhtarTel = d1.MuhtarCepTel,
                                YardimciAdi = d1.YardimciAdi,
                                YardimciTel = d1.YardimciTel,
                                Köyİhtiyac = d1.KoyDetay,
                                GüzergahSırası = d1.Güzergah,
                                KonumKoordinat = d1.Konum,

                            };


                data2.DataSource = query.ToList();
                data2.Columns[0].Visible = false;
            }
        }

        private void txtBolgeAra_TextChanged(object sender, EventArgs e)
        {
            bolgeara(bunifuCustomDataGrid1);

        }

        public void koyara(DataGridView data3) {
            string bolgeAra = txtBolgeAra.Text;
            string koyAra = txtKoyAra.Text;
            if (String.IsNullOrEmpty(bolgeAra))
            {
                var query = from d1 in db.tbl_Koyler
                            join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                            from d2 in d2list.DefaultIfEmpty()  // Önemli !!!  İlişkili Tabloda null değerleri listeler

                            where d1.Aktiflik == true
                            where d1.KoyAdi.Contains(koyAra)

                            select new
                            {
                                KöyNo = d1.KoyNo,
                                Bolge = d2.BolgeAdi,
                                KöyAdi = d1.KoyAdi,
                                MuhtarAdi = d1.MuhtarAdi,
                                MuhtarTel = d1.MuhtarCepTel,
                                YardimciAdi = d1.YardimciAdi,
                                YardimciTel = d1.YardimciTel,
                                Köyİhtiyac = d1.KoyDetay,
                                GüzergahSırası = d1.Güzergah,
                                KonumKoordinat = d1.Konum,

                            };

                data3.DataSource = query.ToList();
                data3.Columns[0].Visible = false;
            }
            else
            {
                var query = from d1 in db.tbl_Koyler
                            join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                            from d2 in d2list.DefaultIfEmpty()  // Önemli !!!  İlişkili Tabloda null değerleri listeler

                            where d1.Aktiflik == true
                            where d2.BolgeAdi.Contains(bolgeAra) && d1.KoyAdi.Contains(koyAra)

                            select new
                            {
                                KöyNo = d1.KoyNo,
                                Bolge = d2.BolgeAdi,
                                KöyAdi = d1.KoyAdi,
                                MuhtarAdi = d1.MuhtarAdi,
                                MuhtarTel = d1.MuhtarCepTel,
                                YardimciAdi = d1.YardimciAdi,
                                YardimciTel = d1.YardimciTel,
                                Köyİhtiyac = d1.KoyDetay,
                                GüzergahSırası = d1.Güzergah,
                                KonumKoordinat = d1.Konum,

                            };

                data3.DataSource = query.ToList();
                data3.Columns[0].Visible = false;
            }

        }

    
        private void txtKoyAra_TextChanged(object sender, EventArgs e)
        {
            koyara(bunifuCustomDataGrid1);

        }

        private void cmbBolgeAd_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {

                cmbBolgeAd.SelectedItem = null;
                cmbBolgeAd.SelectedItem = null;

            }
        }
        int hoveredRowIndex = -1;
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
           
        }
    }
}
