using DevExpress.CodeParser;
using DevExpress.DirectX.NativeInterop.Direct2D;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FethlerV2
{
    public partial class formKisiTanim : Form
    {
        public formKisiTanim()
        {
            InitializeComponent();
        }

        FetihlerV2Entities1 db = new FetihlerV2Entities1();



        public void cmbDataLoad()
        {

            var queryKoy = from item in db.tbl_Koyler
                           where item.Aktiflik == true
                           orderby item.KoyAdi
                           select new
                           {
                               item.KoyNo,
                               item.KoyAdi,

                           };
            cmbKoy.DataSource = queryKoy.ToList();
            cmbKoy.ValueMember = "KoyNo";
            cmbKoy.DisplayMember = "KoyAdi";

            cmbHastalik.DataSource = db.tbl_Hastaliklar.Where(x => x.Aktiflik == true).ToList();
            cmbHastalik.ValueMember = "HastalikNo";
            cmbHastalik.DisplayMember = "HastalikAdi";

            cmbKategori.DataSource = db.tbl_Kategoriler.Where(x => x.Aktiflik == true).ToList();
            cmbKategori.ValueMember = "KategoriNo";
            cmbKategori.DisplayMember = "KategoriAdi";

            cmbMaas.DataSource = db.tbl_Maaslar.Where(x => x.Aktiflik == true).ToList();
            cmbMaas.ValueMember = "MaasNo";
            cmbMaas.DisplayMember = "MaasAdi";

            var data = from item in db.tbl_Gorevliler
                       where item.Aktiflik == true
                       orderby item.GorevliAd + " " + item.GorevliSoyAd
                       select new
                       {
                           item.GorevliNo,
                           AdSoyad = item.GorevliAd + " " + item.GorevliSoyAd,
                       };
            cmbReferans.DataSource = data.ToList();
            cmbReferans.ValueMember = "GorevliNo";
            cmbReferans.DisplayMember = "AdSoyad";


        }
        public void listele()
        {
            var query = from d1 in db.tbl_Kisiler
                        join d2 in db.tbl_Koyler on d1.Koy equals d2.KoyNo into d2list
                        from d2 in d2list.DefaultIfEmpty()
                        join d3 in db.tbl_Hastaliklar on d1.Hastalik equals d3.HastalikNo into d3list
                        from d3 in d3list.DefaultIfEmpty()
                        join d4 in db.tbl_Kategoriler on d1.Kategori equals d4.KategoriNo into d4list
                        from d4 in d4list.DefaultIfEmpty()
                        join d5 in db.tbl_Maaslar on d1.Maas equals d5.MaasNo into d5list
                        from d5 in d5list.DefaultIfEmpty()
                        join d6 in db.tbl_Gorevliler on d1.Referans equals d6.GorevliNo into d6list
                        from d6 in d6list.DefaultIfEmpty()
                        where d1.Aktiflik == true

                        select new
                        {
                            No = d1.KisiNo,
                            Köy = d2.KoyAdi,
                            Ad = d1.KisiAd,
                            Soyad = d1.KisiSoyAd,
                            Yas = DateTime.Now.Year - d1.DogumTarihi,
                            TC = d1.TcNo,
                            d1.MedeniHal,
                            d1.Cinsiyet,

                            Kategori = d4.KategoriAdi,
                            Hastalik = d3.HastalikAdi,
                            Ev = d1.EvDurumu,
                            Arazi = d1.AraziDurumu,
                            Küçükbaş = d1.KücükBas,
                            Büyükbaş = d1.BüyükBas,
                            İş = d1.IsDurumu,
                            Maaş = d5.MaasAdi,
                            Detay = d1.EkBilgi,
                            Referans = d6.GorevliAd + " " + d6.GorevliSoyAd,
                            Adres = d1.Adres,

                        };
            bunifuCustomDataGrid1.DataSource = query.ToList();
            bunifuCustomDataGrid1.Columns[0].Visible = false;
           
            bunifuCustomDataGrid1.ClearSelection();

        }
        private void temizle()
        {
            lblKisiNo.Text = "";
            txtAd.Text = "";
            txtSoyAd.Text = "";
            txt_yas.Text = "";


            cmbMedeniHal.SelectedItem = null;
            rdoErkek.Checked = false;
            rdoKadın.Checked = false;
            cmbKoy.SelectedItem = null;
            cmbKoy.SelectedItem = null;
            cmbKategori.SelectedItem = null;
            cmbKategori.SelectedItem = null;
            cmbHastalik.SelectedItem = null;
            cmbHastalik.SelectedItem = null;
            rdoEvVar.Checked = false;
            rdoEvYok.Checked = false;
            rdoAraziVar.Checked = false;
            rdoAraziYok.Checked = false;
            txtKücükBas.Text = "";
            txtBüyükBas.Text = "";
            rdoCalisabilir.Checked = false;
            rdoCalisamaz.Checked = false;
            cmbMaas.SelectedItem = null;
            cmbMaas.SelectedItem = null;
            rchEkBilgi.Text = "";
            txtTcNo.Text = "";
            cmbReferans.SelectedItem = null;
            cmbReferans.SelectedItem = null;
            rchAileYakınlari.Text = "";
            btnGuncelle.Enabled = false;
            btnKaydet.Enabled = true;
            btnSil.Enabled = false;



        }

        private void formKisiTanim_Load(object sender, EventArgs e)
        {
            
            cmbDataLoad();
            listele();          
            temizle();
            bunifuCustomDataGrid1.ClearSelection();

        }



        private void txtKücükBas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBüyükBas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void rdoErkek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoErkek.Checked == true)
            {
                rdoKadın.Checked = false;
            }
        }

        private void rdoKadın_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoKadın.Checked == true)
            {
                rdoErkek.Checked = false;
            }
        }

        private void rdoEvVar_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoEvVar.Checked == true)
            {
                rdoEvYok.Checked = false;
            }
        }

        private void rdoEvYok_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoEvYok.Checked == true)
            {
                rdoEvVar.Checked = false;
            }
        }

        private void rdoAraziVar_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAraziVar.Checked == true)
            {
                rdoAraziYok.Checked = false;
            }
        }

        private void rdoAraziYok_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAraziYok.Checked == true)
            {
                rdoAraziVar.Checked = false;
            }
        }

        private void rdoBakimMuhtac_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBakimMuhtac.Checked == true)
            {
                rdoBakımDegil.Checked = false;
            }
        }

        private void rdoBakımDegil_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBakımDegil.Checked == true)
            {
                rdoBakimMuhtac.Checked = false;
            }
        }

        private void rdoCalisabilir_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCalisabilir.Checked == true)
            {
                rdoCalisamaz.Checked = false;
            }
        }

        private void rdoCalisamaz_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCalisamaz.Checked == true)
            {
                rdoCalisabilir.Checked = false;
            }
        }

        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtSoyAd_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        void mükerrer()
        {
            if (lblKisiNo.Text == "") {
                var query = from d1 in db.tbl_Kisiler
                            join d2 in db.tbl_Koyler on d1.Koy equals d2.KoyNo into d2list
                            from d2 in d2list.DefaultIfEmpty()
                            where d1.Aktiflik == true
                            where d1.KisiAd == txtAd.Text
                            where d1.KisiSoyAd == txtSoyAd.Text
                            select new
                            {
                                adsoyad = d1.KisiAd + " " + d1.KisiSoyAd,
                                koyNo = d2.KoyNo

                            };
                var adsoyad = (from d1 in query select d1.adsoyad).FirstOrDefault();
                var ad = txtAd.Text;
                var soyad = txtSoyAd.Text;
                var adsoyad2 = ad + soyad;
                var  koynum = (from d1 in query select d1.koyNo).FirstOrDefault();
                var koynum2 = Convert.ToInt32(cmbKoy.SelectedValue);
               
                // Ad soyad kombinasyonu kontrolü
                if (adsoyad == adsoyad2 && koynum == koynum2)
                {
                    MessageBox.Show("Aynı Ad Soyada Sahip Kayıt Bulunmaktadır");
                    return;  // Aynı ad soyad varsa fonksiyondan çık
                }

                // Zorunlu alan kontrolü
                string eksikAlanlar = "";
                if (string.IsNullOrEmpty(txtAd.Text))
                    eksikAlanlar += "Ad, ";
               
                if (cmbKategori.SelectedItem == null)
                    eksikAlanlar += "Kategori, ";
                if (cmbKoy.SelectedItem == null)
                    eksikAlanlar += "Köy, ";
                if (!(rdoErkek.Checked || rdoKadın.Checked))
                    eksikAlanlar += "Cinsiyet, ";

                // Eğer eksik alan varsa uyarı ver
                if (!string.IsNullOrEmpty(eksikAlanlar))
                {
                    MessageBox.Show("Lütfen bu alanları doldurunuz: " + eksikAlanlar.TrimEnd(',', ' '));
                    return;
                }

                // Diğer alanlar kontrol edilecek
                try
                {
                    tbl_Kisiler kisiTanim = new tbl_Kisiler();
                    kisiTanim.KisiAd = txtAd.Text;
                    kisiTanim.KisiSoyAd = txtSoyAd.Text;

                    kisiTanim.MedeniHal = cmbMedeniHal.Text;
                    var koyNo = Convert.ToInt32(cmbKoy.SelectedValue);
                    kisiTanim.Koy = koyNo;
                    var kategoriNo = Convert.ToInt32(cmbKategori.SelectedValue);
                    kisiTanim.Kategori = kategoriNo;

                    if (cmbHastalik.SelectedItem != null)
                    {
                        var hastalikNo = Convert.ToInt32(cmbHastalik.SelectedValue);
                        kisiTanim.Hastalik = hastalikNo;
                    }

                    if (cmbMaas.SelectedItem != null)
                    {
                        var maasNo = Convert.ToInt32(cmbMaas.SelectedValue);
                        kisiTanim.Maas = maasNo;
                    }

                    if (cmbReferans.SelectedItem != null)
                    {
                        var referansNo = Convert.ToInt32(cmbReferans.SelectedValue);
                        kisiTanim.Referans = referansNo;
                    }

                    if (txt_yas.Text != null && txt_yas.Text != string.Empty)
                    {
                        var currentYear = DateTime.Now.Year;
                        var dogumTarihi = currentYear - Convert.ToInt32(txt_yas.Text);
                        kisiTanim.DogumTarihi = dogumTarihi;
                    }
                    else { kisiTanim.DogumTarihi = Convert.ToInt32(txt_yas.Text); }

                    // Cinsiyet
                    if (rdoErkek.Checked == true)
                    {
                        kisiTanim.Cinsiyet = rdoErkek.Text;
                    }
                    else if (rdoKadın.Checked == true)
                    {
                        kisiTanim.Cinsiyet = rdoKadın.Text;
                    }

                    // Diğer alanlar için aynı kontroller
                    kisiTanim.TcNo = txtTcNo.Text;
                    kisiTanim.BüyükBas = txtBüyükBas.Text;
                    kisiTanim.KücükBas = txtKücükBas.Text;
                    kisiTanim.EkBilgi = rchEkBilgi.Text;
                    kisiTanim.Adres = rchAileYakınlari.Text;

                    // Diğer zorunlu alanlar varsa burada eklenebilir

                    // Kaydetme işlemi
                    kisiTanim.Aktiflik = true;
                    db.tbl_Kisiler.Add(kisiTanim);
                    db.SaveChanges();
                    MessageBox.Show("Kisi Başarıyla Kayıt Edildi");
                    listele();
                    temizle();
                }
                catch
                {
                    MessageBox.Show("Lütfen Eksik Bilgileri Giriniz");
                }
                               
            }
            else
            {
                MessageBox.Show("Kayıt Tekrarı Yapılamaz.");
            }
                
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {

            mükerrer();


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lblKisiNo.Text != "")
            {
                DialogResult onay;
                onay = MessageBox.Show("Kayıt silme işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    try
                    {
                        int kisiNo = Convert.ToInt32(lblKisiNo.Text);
                        var x = db.tbl_Kisiler.Find(kisiNo);
                        x.Aktiflik = false;
                        db.SaveChanges();
                        temizle();
                        listele();
                        MessageBox.Show("Kayıt Başarıyla Silindi");
                    }
                    catch
                    {
                        MessageBox.Show("Kayıt Silinemedi");

                    }
                }
                else
                {
                    MessageBox.Show("Silinecek Kayıt Seçilmedi");

                }
            }



        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int kisiNo = Convert.ToInt32(lblKisiNo.Text);
                var g = db.tbl_Kisiler.Find(kisiNo);

                // Kayıt var mı kontrolü
                if (g == null)
                {
                    MessageBox.Show("Güncellenicek Kayıt Seçilmedi");
                    return;
                }



                var ad = txtAd.Text.Trim().ToLower();
                var soyad = txtSoyAd.Text.Trim().ToLower();
                var adsoyad2 = ad + " " + soyad;

                var koynum2 = Convert.ToInt32(cmbKoy.SelectedValue);

                // Veritabanında eşleşen aktif kayıt var mı kontrolü
                var kayitVarMi = (from d1 in db.tbl_Kisiler
                                  join d2 in db.tbl_Koyler on d1.Koy equals d2.KoyNo into d2list
                                  from d2 in d2list.DefaultIfEmpty()
                                  where d1.Aktiflik == true
                                  && d1.KisiAd.ToLower().Trim() == ad
                                  && d1.KisiSoyAd.ToLower().Trim() == soyad
                                  && d2.KoyNo == koynum2
                                  && d1.Kategori == 32
                                  && d1.KisiNo != kisiNo
                                  select d1).Any();

                if (kayitVarMi)
                {
                    MessageBox.Show("Aynı Ad Soyada ve Köye Sahip Kayıt Bulunmaktadır");
                    return; // Aynı kayıt varsa işlemi durdur
                }

                // Alanları güncelle
                g.KisiAd = string.IsNullOrEmpty(txtAd.Text) ? null : txtAd.Text;
                g.KisiSoyAd = string.IsNullOrEmpty(txtSoyAd.Text) ? null : txtSoyAd.Text;
                g.MedeniHal = string.IsNullOrEmpty(cmbMedeniHal.Text) ? null : cmbMedeniHal.Text;

                // Koy ve Kategori No
                var koyNo = Convert.ToInt32(cmbKoy.SelectedValue);
                g.Koy = koyNo;
                var kategoriNo = Convert.ToInt32(cmbKategori.SelectedValue);
                g.Kategori = kategoriNo;

                // Hastalık, Maaş ve Referans bilgileri kontrolü
                if (cmbHastalik.SelectedItem != null)
                {
                    var hastalikNo = Convert.ToInt32(cmbHastalik.SelectedValue);
                    g.Hastalik = hastalikNo;
                }
                else
                {
                    g.Hastalik = null;
                }

                if (cmbMaas.SelectedItem != null)
                {
                    var maasNo = Convert.ToInt32(cmbMaas.SelectedValue);
                    g.Maas = maasNo;
                }
                else
                {
                    g.Maas = null;
                }

                if (cmbReferans.SelectedItem != null)
                {
                    var referansNo = Convert.ToInt32(cmbReferans.SelectedValue);
                    g.Referans = referansNo;
                }
                else
                {
                    g.Referans = null;
                }

                // Diğer Bilgiler
                g.TcNo = string.IsNullOrEmpty(txtTcNo.Text) ? null : txtTcNo.Text;
                g.BüyükBas = string.IsNullOrEmpty(txtBüyükBas.Text) ? null : txtBüyükBas.Text;
                g.KücükBas = string.IsNullOrEmpty(txtKücükBas.Text) ? null : txtKücükBas.Text;
                g.EkBilgi = string.IsNullOrEmpty(rchEkBilgi.Text) ? null : rchEkBilgi.Text;
                g.Adres = string.IsNullOrEmpty(rchAileYakınlari.Text) ? null : rchAileYakınlari.Text;

                // Yaş kontrolü ve doğum tarihi hesaplama
                if (string.IsNullOrEmpty(txt_yas.Text))
                {
                    g.DogumTarihi = null;  // Yaş boş ise doğum tarihini null yap
                }
                else
                {
                    var currentYear = DateTime.Now.Year;
                    var dogumTarihi = currentYear - Convert.ToInt32(txt_yas.Text);
                    g.DogumTarihi = dogumTarihi;
                }

                // Cinsiyet kontrolü
                if (rdoErkek.Checked)
                {
                    g.Cinsiyet = rdoErkek.Text;
                }
                else if (rdoKadın.Checked)
                {
                    g.Cinsiyet = rdoKadın.Text;
                }

                // Ev Durumu
                if (rdoEvVar.Checked)
                {
                    g.EvDurumu = rdoEvVar.Text;
                }
                else if (rdoEvYok.Checked)
                {
                    g.EvDurumu = rdoEvYok.Text;
                }

                // Arazi Durumu
                if (rdoAraziVar.Checked)
                {
                    g.AraziDurumu = rdoAraziVar.Text;
                }
                else if (rdoAraziYok.Checked)
                {
                    g.AraziDurumu = rdoAraziYok.Text;
                }

                // Çalışabilirlik
                if (rdoCalisabilir.Checked)
                {
                    g.IsDurumu = rdoCalisabilir.Text;
                }
                else if (rdoCalisamaz.Checked)
                {
                    g.IsDurumu = rdoCalisamaz.Text;
                }

                // Bakım Durumu
                if (rdoBakimMuhtac.Checked)
                {
                    g.BakımDurumu = rdoBakimMuhtac.Text;
                }
                else if (rdoBakımDegil.Checked)
                {
                    g.BakımDurumu = rdoBakımDegil.Text;
                }

                // Veritabanı güncellemesi
                db.SaveChanges();
                MessageBox.Show("Kayıt Başarıyla Güncellendi");

                // Listeleme ve temizleme işlemleri
                listele();
                temizle();
            }
            catch
            {
                MessageBox.Show("Kayıt Güncellenemedi");
            }

        }


        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            try
            {
                temizle();

                lblKisiNo.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[0].Value?.ToString();
                cmbKoy.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                txtAd.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[2].Value?.ToString();
                txtSoyAd.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[3].Value?.ToString();
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[4].Value != null)
                {
                    int dogumYili;

                    // Değeri int'e çevirmeye çalış (hata olursa önlemek için TryParse kullan)
                    if (int.TryParse(bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString(), out dogumYili))
                    {
                        string yas = dogumYili.ToString();
                        txt_yas.Text = yas;
                    }

                }

                txtTcNo.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[5].Value?.ToString();
                cmbMedeniHal.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[6].Value?.ToString();
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[7].Value?.ToString() == rdoErkek.Text)
                {
                    rdoErkek.Checked = true;


                }
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[7].Value?.ToString() == rdoKadın.Text)
                {
                    rdoKadın.Checked = true;

                }
                cmbKategori.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[8].Value?.ToString();
                cmbHastalik.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[9].Value?.ToString();
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[10].Value?.ToString() == rdoEvVar.Text)
                {
                    rdoEvVar.Checked = true;


                }
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[10].Value?.ToString() == rdoEvYok.Text)
                {
                    rdoEvYok.Checked = true;

                }
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[11].Value?.ToString() == rdoAraziVar.Text)
                {
                    rdoAraziVar.Checked = true;


                }
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[11].Value?.ToString() == rdoAraziYok.Text)
                {
                    rdoAraziYok.Checked = true;

                }
                txtKücükBas.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[12].Value?.ToString();
                txtBüyükBas.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[13].Value?.ToString();
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[14].Value?.ToString() == rdoCalisabilir.Text)
                {
                    rdoCalisabilir.Checked = true;


                }
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[14].Value?.ToString() == rdoCalisamaz.Text)
                {
                    rdoCalisamaz.Checked = true;

                }
                cmbMaas.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[15].Value?.ToString();
                rchEkBilgi.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[16].Value?.ToString();
                cmbReferans.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[17].Value?.ToString();
                rchAileYakınlari.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[18].Value?.ToString();
                btnGuncelle.Enabled = true;
                btnKaydet.Enabled = false;
                btnSil.Enabled = true;
            }
            catch
            {


            }

        }

        private void txtAdAra_TextChanged(object sender, EventArgs e)
        {
            string adAra = txtAdAra.Text.Trim();
            string soyadAra = txtSoyAd.Text;

            // Kullanıcı arama metni boş değilse
            if (!string.IsNullOrEmpty(adAra))
            {
                // Kullanıcının girdiği metni parçalara ayırıyoruz
                var aramaParcalari = adAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = from d1 in db.tbl_Kisiler
                            join d2 in db.tbl_Koyler on d1.Koy equals d2.KoyNo into d2list
                            from d2 in d2list.DefaultIfEmpty()
                            join d3 in db.tbl_Hastaliklar on d1.Hastalik equals d3.HastalikNo into d3list
                            from d3 in d3list.DefaultIfEmpty()
                            join d4 in db.tbl_Kategoriler on d1.Kategori equals d4.KategoriNo into d4list
                            from d4 in d4list.DefaultIfEmpty()
                            join d5 in db.tbl_Maaslar on d1.Maas equals d5.MaasNo into d5list
                            from d5 in d5list.DefaultIfEmpty()
                            join d6 in db.tbl_Gorevliler on d1.Referans equals d6.GorevliNo into d6list
                            from d6 in d6list.DefaultIfEmpty()
                            where d1.Aktiflik == true
                            // Arama koşulunda her kelimeyi hem Ad hem Soyad üzerinde arıyoruz
                            where aramaParcalari.All(p =>
                                (d1.KisiAd.Contains(p) || d1.KisiSoyAd.Contains(p))
                            )
                            select new
                            {
                                No = d1.KisiNo,
                                Köy = d2.KoyAdi,
                                Ad = d1.KisiAd,
                                Soyad = d1.KisiSoyAd,
                                Yas = DateTime.Now.Year - d1.DogumTarihi,
                                TC = d1.TcNo,
                                d1.MedeniHal,
                                d1.Cinsiyet,
                                Kategori = d4.KategoriAdi,
                                Hastalik = d3.HastalikAdi,
                                Ev = d1.EvDurumu,
                                Arazi = d1.AraziDurumu,
                                Küçükbaş = d1.KücükBas,
                                Büyükbaş = d1.BüyükBas,
                                İş = d1.IsDurumu,
                                Maaş = d5.MaasAdi,
                                Detay = d1.EkBilgi,
                                Referans = d6.GorevliAd + " " + d6.GorevliSoyAd, // Görevli Ad Soyad
                                Adres = d1.Adres
                            };

                bunifuCustomDataGrid1.DataSource = query.ToList();
            }
            else
            {
                // Eğer arama metni boşsa tüm kayıtları göster
                var query = from d1 in db.tbl_Kisiler
                            join d2 in db.tbl_Koyler on d1.Koy equals d2.KoyNo into d2list
                            from d2 in d2list.DefaultIfEmpty()
                            join d3 in db.tbl_Hastaliklar on d1.Hastalik equals d3.HastalikNo into d3list
                            from d3 in d3list.DefaultIfEmpty()
                            join d4 in db.tbl_Kategoriler on d1.Kategori equals d4.KategoriNo into d4list
                            from d4 in d4list.DefaultIfEmpty()
                            join d5 in db.tbl_Maaslar on d1.Maas equals d5.MaasNo into d5list
                            from d5 in d5list.DefaultIfEmpty()
                            join d6 in db.tbl_Gorevliler on d1.Referans equals d6.GorevliNo into d6list
                            from d6 in d6list.DefaultIfEmpty()
                            where d1.Aktiflik == true
                            select new
                            {
                                No = d1.KisiNo,
                                Köy = d2.KoyAdi,
                                Ad = d1.KisiAd,
                                Soyad = d1.KisiSoyAd,
                                Yas = DateTime.Now.Year - d1.DogumTarihi,
                                TC = d1.TcNo,
                                d1.MedeniHal,
                                d1.Cinsiyet,
                                Kategori = d4.KategoriAdi,
                                Hastalik = d3.HastalikAdi,
                                Ev = d1.EvDurumu,
                                Arazi = d1.AraziDurumu,
                                Küçükbaş = d1.KücükBas,
                                Büyükbaş = d1.BüyükBas,
                                İş = d1.IsDurumu,
                                Maaş = d5.MaasAdi,
                                Detay = d1.EkBilgi,
                                Referans = d6.GorevliAd + " " + d6.GorevliSoyAd, // Görevli Ad Soyad
                                Adres = d1.Adres
                            };

                bunifuCustomDataGrid1.DataSource = query.ToList();
            }
        }


    

       

        private void cmbReferans_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {

                cmbReferans.SelectedItem = null;
                cmbReferans.SelectedItem = null;
            }
        }

        private void cmbKoy_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {

                cmbKoy.SelectedItem = null;
                cmbKoy.SelectedItem = null;
            }
        }

        private void cmbMedeniHal_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {

                cmbMedeniHal.SelectedItem = null;
                cmbMedeniHal.SelectedItem = null;
            }
        }

        private void cmbKategori_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {

                cmbKategori.SelectedItem = null;
                cmbKategori.SelectedItem = null;
            }
        }

        private void cmbMaas_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {

                cmbMaas.SelectedItem = null;
                cmbMaas.SelectedItem = null;
            }

        }

        private void cmbHastalik_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {

                cmbHastalik.SelectedItem = null;
                cmbHastalik.SelectedItem = null;
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
            bunifuCustomDataGrid1.ClearSelection();
        }
    }
    }

    
    

