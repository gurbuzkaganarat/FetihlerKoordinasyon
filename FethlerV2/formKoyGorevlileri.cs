using Bunifu.Framework.UI;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.DirectX.NativeInterop.Direct3D;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraRichEdit.API.Native.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Design.DXCollectionEditorBase;

namespace FethlerV2
{
    public partial class formKoyGorevlileri : Form
    {
        public formKoyGorevlileri()
        {
            InitializeComponent();
        }



        FetihlerV2Entities1 db = new FetihlerV2Entities1();

        List<int> secilenKoyler = new List<int>();
        List<int> secilerGorevliler = new List<int>();
        List<string> secilenKoyAdlari = new List<string>();
        List<string> secilenGorevliAdları= new List<string>();

        private void cmbDataLoad()
        {
            cmbBolge.DataSource = db.tbl_Bolgeler.Where(x => x.Aktiflik == true).OrderBy(x => x.BolgeAdi).ToList();
            cmbBolge.ValueMember = "BolgeNo";
            cmbBolge.DisplayMember = "BolgeAdi";

            cmbDagitimDonemi.DataSource = db.tbl_Donemler.Where(x => x.Aktiflik == true).OrderBy(x => x.DonemAdi).ToList();
            cmbDagitimDonemi.ValueMember = "DonemNo";
            cmbDagitimDonemi.DisplayMember = "DonemAdi";

        }


        public void temizle()
        {
            cmbBolge.SelectedItem = null;
            cmbDagitimDonemi.SelectedItem = null;

        }

        public void aracListesi(DataGridView data2)
        {
            var donemNo = Convert.ToInt32(cmbDagitimDonemi.SelectedValue);

            var query = from d1 in db.tbl_Araclar
                        join d3 in db.tbl_Gorevliler on d1.AracSahip equals d3.GorevliNo
                        from d2 in db.tbl_Donemler
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        where d1.Gorevlendir == false
                        where d2.DonemNo == donemNo
                        select new
                        {
                            No = d1.AracNo,
                            Araç = d1.AracAdi,
                            // burada ID yerine ad soyad gösteriyoruz
                            Kapasite = d1.AracKapasite / d2.PaketAgirligi,
                            Sahibi = d3.GorevliAd + " " + d3.GorevliSoyAd,
                        };

            data2.DataSource = query.OrderBy(x => x.Kapasite).ToList();
            data2.Columns[1].Visible = false;



        }

      

        public void gorevliListesi(DataGridView data3)
        {

            string gorevliAd = txtGorevliAd.Text.Trim();

            if (!string.IsNullOrEmpty(gorevliAd))
            {
                // Kullanıcının girdiği metni boşluklara göre parçalıyoruz
                var aramaParcalari = gorevliAd.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = from d1 in db.tbl_Gorevliler
                            where d1.Aktiflik == true
                               && d1.Seç == true
                               && d1.Gorevlendir == false
                               && aramaParcalari.All(p =>
                                    (d1.GorevliAd + " " + d1.GorevliSoyAd).Contains(p))
                            orderby d1.GorevliAd + " " + d1.GorevliSoyAd
                            select new
                            {
                                No = d1.GorevliNo,
                                Gorevli = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            };

                data3.DataSource = query.ToList();
            }
            else
            {
                // Arama boşsa tüm aktif, seçilmiş ve henüz görevlendirilmemiş kişileri getir
                var query = from d1 in db.tbl_Gorevliler
                            where d1.Aktiflik == true
                               && d1.Seç == true
                               && d1.Gorevlendir == false
                            orderby d1.GorevliAd + " " + d1.GorevliSoyAd
                            select new
                            {
                                No = d1.GorevliNo,
                                Gorevli = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            };

                data3.DataSource = query.ToList();
            }

            data3.Columns[2].Visible = false;

        }

        public void aracara(DataGridView data5)
        {
            string AracAd = txt_arac_ad.Text.Trim();
            var donemNo = Convert.ToInt32(cmbDagitimDonemi.SelectedValue);

            // Kullanıcı metin girdiyse parçala
            var aramaParcalari = AracAd.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var query = from d1 in db.tbl_Araclar
                        join d3 in db.tbl_Gorevliler on d1.AracSahip equals d3.GorevliNo
                        from d2 in db.tbl_Donemler
                        where d1.Aktiflik == true
                           && d1.Seç == true
                           && d1.Gorevlendir == false
                           && d2.DonemNo == donemNo
                        select new
                        {
                            No = d1.AracNo,
                            Araç = d1.AracAdi,
                            Kapasite = d1.AracKapasite / d2.PaketAgirligi,
                            Sahibi = d3.GorevliAd + " " + d3.GorevliSoyAd,
                        };

            // Arama kelimeleri varsa filtre uygula
            if (aramaParcalari.Length > 0)
            {
                query = query.Where(x => aramaParcalari.All(p => x.Araç.Contains(p)));
            }

            data5.DataSource = query.OrderBy(x => x.Kapasite).ToList();
            data5.Columns[1].Visible = false;
        }
        public void gorevListesi(DataGridView data4)
        {
            var bolgeNo = Convert.ToInt32(cmbBolge.SelectedValue);
            var query = from d1 in db.tbl_SeferGorev
                        join d2 in db.tbl_Araclar on d1.Arac_id equals d2.AracNo
                        join d3 in db.tbl_Gorevliler on d1.Sofor_id equals d3.GorevliNo
                        //  join d4 in db.tbl_Gorevliler on d1.Gorevli_id equals d4.GorevliNo
                        join d5 in db.tbl_Bolgeler on d1.Bölge equals d5.BolgeNo into d5list
                        from d5 in d5list.DefaultIfEmpty()
                        where d1.Bölge == bolgeNo                       
                        select new
                        {
                            GorevNo = d1.Gorev_id,
                            Arac = d2.AracAdi,
                            Sofor = d3.GorevliAd + " " + d3.GorevliSoyAd,
                            Yardımcı = d1.Gorevli,
                            YardımcıNo=d1.Gorevli_ids,
                            Koyler = d1.Köyler,
                            Bolge = d5.BolgeAdi,
                            KoyNo = d1.Koy_ids,
                            PaketSayisi = d1.PaketSayisi,
                            CikisSirasi = d1.cikisSirasi

                        };
            data4.DataSource = query.ToList();
            data4.Columns[1].Visible = false;
            data4.Columns[5].Visible = false;
            data4.Columns[8].Visible = false;

        }

        public void gorevListesibolgesiz(DataGridView data4)
        {
          //  var bolgeNo = Convert.ToInt32(cmbBolge.SelectedValue);
            var query = from d1 in db.tbl_SeferGorev
                        join d2 in db.tbl_Araclar on d1.Arac_id equals d2.AracNo
                        join d3 in db.tbl_Gorevliler on d1.Sofor_id equals d3.GorevliNo
                        //  join d4 in db.tbl_Gorevliler on d1.Gorevli_id equals d4.GorevliNo
                        join d5 in db.tbl_Bolgeler on d1.Bölge equals d5.BolgeNo 
                       // where d1.Bölge == bolgeNo
                       orderby d1.cikisSirasi
                        select new
                        {
                            GorevNo = d1.Gorev_id,
                            Arac = d2.AracAdi,
                            Sofor = d3.GorevliAd + " " + d3.GorevliSoyAd,
                            Yardımcı = d1.Gorevli,
                            YardımcıNo = d1.Gorevli_ids,
                            Koyler = d1.Köyler,
                            Bolge = d5.BolgeAdi,
                            KoyNo = d1.Koy_ids,
                            PaketSayisi = d1.PaketSayisi,
                            CikisSirasi = d1.cikisSirasi

                        };
            data4.DataSource = query.ToList().OrderBy(x => x.CikisSirasi).ToList();
            data4.Columns[1].Visible = false;
            data4.Columns[5].Visible = false;
            data4.Columns[8].Visible = false;

        }

        public void koyListesi(DataGridView data)
        {
            var bolgeNo = Convert.ToInt32(cmbBolge.SelectedValue);

            var query =
                         from d1 in db.tbl_Koyler
                         join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                         from d2 in d2list.DefaultIfEmpty()
                         join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list
                         where d1.Bolge == bolgeNo
                         where d1.Aktiflik == true
                         where d1.Sec == true
                         where d1.Gorevlendir == false
                         orderby d1.Güzergah
                         select new
                         {
                             No = d1.KoyNo,
                             KöyAdi = d1.KoyAdi,
                             KSayi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),
                             Güzergah= d1.Güzergah,


                         };
            data.DataSource = query.ToList();
            data.Columns[1].Visible = false;
        }



        private void formKoyGorevlileri_Load_1(object sender, EventArgs e)
        {
            cmbDataLoad();
            temizle();
            gorevliListesi(dataGridGorevli);
            secilenKoy();
            secilenKisi();
            secilenArac();
            secilenGorevli();
            dagıtımaCikilanPaketSayisi();
         //   gorevListesi(datagridGörevler);
            gorevListesibolgesiz(datagridGörevler);


        }

        private void cmbDagitimDonemi_SelectionChangeCommitted(object sender, EventArgs e)
        {
            aracListesi(dataGridArac);
            // dataGridArac.Columns[0].Visible = false;
                aracKapasite();
        }

        private void txtGorevliAd_TextChanged(object sender, EventArgs e)
        {
            gorevliListesi(dataGridGorevli);
        }


        private void dataGridGorevli_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (dataGridGorevli.Columns[e.ColumnIndex].HeaderText == "Söför")
            {
                int soforNo = Convert.ToInt32(dataGridGorevli.Rows[e.RowIndex].Cells[2].Value);

                // Yardımcı olarak atanmışsa, şoför yapılamaz
                if (secilerGorevliler.Contains(soforNo))
                {
                    MessageBox.Show("Bu kişi zaten yardımcı olarak atanmış!");
                    return;
                }

                lblşNo.Text = soforNo.ToString();
                txt_sofor.Text = dataGridGorevli.Rows[e.RowIndex].Cells[3].Value?.ToString();
                txt_sofor.ReadOnly = true;
            }

            if (dataGridGorevli.Columns[e.ColumnIndex].HeaderText == "Yardımcı")
            {
                int gorevliNo = Convert.ToInt32(dataGridGorevli.Rows[e.RowIndex].Cells[2].Value);
                string gorevliAdi = dataGridGorevli.Rows[e.RowIndex].Cells[3].Value?.ToString();

                // Eğer bu kişi şoför olarak atanmışsa, yardımcı olamaz
                if (lblşNo.Text == gorevliNo.ToString())
                {
                    MessageBox.Show("Bu kişi zaten şoför olarak atanmış!");
                    return;
                }

                if (!secilerGorevliler.Contains(gorevliNo))
                {
                    secilerGorevliler.Add(gorevliNo);
                }

               

                if (!secilenGorevliAdları.Contains(gorevliAdi))
                {
                    secilenGorevliAdları.Add(gorevliAdi);

                    rch_Yardimci.Focus();               // RichTextBox'a odaklan
                      // RichTextBox'a odaklan

                    // Mevcut köy adlarını al (RichTextBox'tan), virgüllerden ayır ve trimle
                    var mevcutYardımcılar = rch_Yardimci.Text
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(k => k.Trim())
                        .ToList();

                    // Eğer köy adı zaten listede yoksa ekleend
                    if (!mevcutYardımcılar.Contains(gorevliAdi))
                    {
                        mevcutYardımcılar.Add(gorevliAdi);
                    }

                    // Kullanıcının tıkladığı yeri (imlec) kontrol et
                    if (rch_Yardimci.SelectionStart == rch_Yardimci.Text.Length)
                    {
                        // Eğer imlec sondaysa, köyü sonuna ekle
                        rch_Yardimci.Text = string.Join(" , ", mevcutYardımcılar);
                    }
                    else
                    {
                        // Eğer araya ekleme yapılmışsa
                        var beforeText = rch_Yardimci.Text.Substring(0, rch_Yardimci.SelectionStart).Trim();
                        var afterText = rch_Yardimci.Text.Substring(rch_Yardimci.SelectionStart).Trim();

                        // Önceki ve sonraki metni virgüllerle ayır
                        var updatedText = string.Join(" , ", beforeText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                                         .Select(k => k.Trim())
                                                                         .Concat(new[] { gorevliAdi })
                                                                         .Concat(afterText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                                                         .Select(k => k.Trim())));

                        rch_Yardimci.Text = updatedText;
                    }
                }
            }



        }

        private void dataGridArac_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridArac.Columns[e.ColumnIndex].HeaderText == "Araç")
            {
                lblaNo.Text = dataGridArac.Rows[e.RowIndex].Cells[1].Value?.ToString();
                txt_arac.Text = dataGridArac.Rows[e.RowIndex].Cells[2].Value?.ToString();
                txt_arac.ReadOnly = true;

            }

        }

        int y = 0;
        private void dataGridKoy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dataGridKoy.Columns[e.ColumnIndex].HeaderText == "Köy")
                {
                    // Köy ID'si genelde birinci ya da ikinci sütunda bulunur, sütun indeksine göre ayarla.
                    int koyId = Convert.ToInt32(dataGridKoy.Rows[e.RowIndex].Cells[1].Value); // Köy ID'si
                    string koyAdi = dataGridKoy.Rows[e.RowIndex].Cells[2].Value?.ToString(); // Köy adı

                    // Köy ID'si listede değilse ekle
                    if (!secilenKoyler.Contains(koyId))
                    {
                        secilenKoyler.Add(koyId);
                        secilenkoydekiKisiSayisi(koyId);
                    }

                    // Köy adı listede değilse RichTextBox'a ve listeye ekle
                    if (!secilenKoyAdlari.Contains(koyAdi))
                    {
                        secilenKoyAdlari.Add(koyAdi);

                        rch_koyler.Focus(); // RichTextBox'a odaklan

                        var mevcutKoyler = rch_koyler.Text
                            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(k => k.Trim())
                            .ToList();

                        if (!mevcutKoyler.Contains(koyAdi))
                            mevcutKoyler.Add(koyAdi);

                        if (rch_koyler.SelectionStart == rch_koyler.Text.Length)
                        {
                            rch_koyler.Text = string.Join(" , ", mevcutKoyler);
                        }
                        else
                        {
                            var beforeText = rch_koyler.Text.Substring(0, rch_koyler.SelectionStart).Trim();
                            var afterText = rch_koyler.Text.Substring(rch_koyler.SelectionStart).Trim();

                            var updatedText = string.Join(" , ",
                                beforeText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(k => k.Trim())
                                .Concat(new[] { koyAdi })
                                .Concat(afterText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(k => k.Trim()))
                            );

                            rch_koyler.Text = updatedText;
                        }
                    }

                    int kisiSayisi = secilenkoydekiKisiSayisi(koyId); // Kişi sayısını al

                    // Mevcut toplamı al ve üzerine ekle
                    y += kisiSayisi;
                    tplmpktsys.Text = y.ToString();

                   
                }
            }
            catch
            {
                // Hata varsa sessizce geç
            }
            dataGridKoy.ClearSelection();
            dataGridKoy.CurrentCell = null;
        }

        private int secilenkoydekiKisiSayisi(int koyId)
        {
            var query = from d1 in db.tbl_Koyler
                        join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                        from d2 in d2list.DefaultIfEmpty()
                        join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list

                        where d1.Aktiflik == true
                        where d1.Sec == true
                        where d1.Gorevlendir == false
                        where d1.KoyNo == koyId


                        select new
                        {
                            KöyNo = d1.KoyNo,
                            Bolge = d2.BolgeAdi,
                            KöyAdi = d1.KoyAdi,
                            KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),


                        };
            var toplam = (from x in query select (int?)x.KisiSayisi).Sum() ?? 0;
            // Eğer sadece bir köyse tek bir sonuç alıyoruz.
            return toplam;



        }
                public void secilenKoy()
               {
                   var query = from d1 in db.tbl_Koyler
                              where d1.Aktiflik == true
                              where d1.Sec == true
                              where d1.Gorevlendir == false

                               select new
                              {
                                 secilenKoySayisi = db.tbl_Koyler.Count(x => x.Aktiflik == true && x.Sec == true)
                               };

                   var secilenKoy = (from d1 in query select (int?)d1.secilenKoySayisi).Count();

                   lbl_KoySayi.Text = secilenKoy.ToString();
                }

        public void secilenKisi()
        {
            var query = from d1 in db.tbl_Koyler
                        join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                        from d2 in d2list.DefaultIfEmpty()
                        join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list

                        where d1.Aktiflik == true
                        where d1.Sec == true
                        where d1.Gorevlendir == false


                        select new
                        {
                            KöyNo = d1.KoyNo,
                            Bolge = d2.BolgeAdi,
                            KöyAdi = d1.KoyAdi,
                            KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),


                        };
            var toplam = (from x in query select (int?)x.KisiSayisi).Sum() ?? 0;
            // (int?) null yapılabilen deger  ?? 0 ise baslangıctada null deger dondurebilir
            lbl_FakirSayisi.Text = toplam.ToString();

        }

        public void dagıtımaCikilanPaketSayisi()
        {
            var seferKoyIdleri = db.tbl_SeferGorev
                            .Where(sg => !string.IsNullOrEmpty(sg.Koy_ids))
                            .Select(sg => sg.Koy_ids)
                            .ToList();

            List<int> tumKoyIdleri = new List<int>();

            foreach (var koyIdString in seferKoyIdleri)
            {
                var idler = koyIdString
                            .Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(id => int.Parse(id.Trim()));

                tumKoyIdleri.AddRange(idler);
            }

            tumKoyIdleri = tumKoyIdleri.Distinct().ToList();

            // tbl_Koyler üzerinden başlıyoruz çünkü aktiflik ve sec alanı burada
            var query = from koy in db.tbl_Koyler
                        join kisi in db.tbl_Kisiler on koy.KoyNo equals kisi.Koy
                        where tumKoyIdleri.Contains(koy.KoyNo)
                              && koy.Aktiflik == true
                              && koy.Sec == true
                              && koy.Gorevlendir == true
                              && kisi.Aktiflik == true
                              && kisi.Kategori == 32
                        select kisi;

            int toplamKisi = query.Count();

            lblEklenenPaket.Text = toplamKisi.ToString();


        }

        public void secilenArac()
        {
            var query = from d1 in db.tbl_Araclar
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        where d1.Gorevlendir == false
                        select new
                        {
                            ToplamArac = db.tbl_Araclar.Count(x => x.Aktiflik == true && x.Seç == true)
                        };
            var toplam = (from d1 in query select (int?)d1.ToplamArac).Count();
            lbl_aracSayisi.Text = toplam.ToString();

        }

        public void aracKapasite()
        {
            var donemNo = Convert.ToInt32(cmbDagitimDonemi.SelectedValue);
            var query = from d1 in db.tbl_Araclar
                        from d2 in db.tbl_Donemler
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        where d2.DonemNo == donemNo
                        where d1.Gorevlendir == false
                        select new
                        {
                            Kapasite = d1.AracKapasite / d2.PaketAgirligi
                        };
            var toplam = (from d1 in query select (int?)d1.Kapasite).Sum();
            araç_Kapasite.Text = toplam.ToString();

        }
        public void secilenGorevli()
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        where d1.Gorevlendir == false

                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                            toplam = db.tbl_Gorevliler.Count(x => x.Aktiflik == true && x.Seç == true)
                        };

            var toplam = (from d1 in query select (int?)d1.toplam).Count();
            lbl_gorevliSayi.Text = toplam.ToString();
        }
        tbl_SeferGorev seferGorev = new tbl_SeferGorev();
        private void btnKaydet_Click(object sender, EventArgs e)
        {

            try
            {
                int aracNo = Convert.ToInt32(lblaNo.Text);
                int soforNo = Convert.ToInt32(lblşNo.Text);
                int bolgeNo = Convert.ToInt32(lblbNo.Text);

                // Koy adlarını birleştir
                string KoyAdları = string.Join(" , ", secilenKoyAdlari); // Koy adlarını virgülle ayırarak birleştiriyoruz
                string KoyNo = string.Join(", ", secilenKoyler);  // Koy numaralarını virgülle ayırarak birleştiriyoruz

                // Görevli adlarını ve numaralarını birleştir
                string GorevliAdlari = string.Join(" , ", secilenGorevliAdları); // Görevli adlarını virgülle ayırarak birleştiriyoruz
                string GorevliNo = string.Join(", ", secilerGorevliler);  // Görevli numaralarını virgülle ayırarak birleştiriyoruz

                // Boş olan değerleri kontrol et
                if (aracNo > 0 && soforNo > 0 && !string.IsNullOrEmpty(KoyAdları) && !string.IsNullOrEmpty(KoyNo)  && bolgeNo > 0)
                {
                    seferGorev.Arac_id = aracNo;
                    var aracG = db.tbl_Araclar.Find(aracNo);
                    aracG.Gorevlendir = true;

                    seferGorev.Sofor_id = soforNo;
                    var soforG = db.tbl_Gorevliler.Find(soforNo);
                    soforG.Gorevlendir = true;

                    seferGorev.Gorevli_ids = GorevliNo;
                    seferGorev.Gorevli = GorevliAdlari; // Görevli numaralarını virgülle ayrılmış şekilde kaydediyoruz

                    string[] gorevliIdArray = GorevliNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string id in gorevliIdArray)
                    {
                        int gorevliId = int.Parse(id);
                        var gorevliG = db.tbl_Gorevliler.Find(gorevliId);
                        gorevliG.Gorevlendir = true;
                    }

                    seferGorev.Bölge = bolgeNo;

                    seferGorev.Köyler = KoyAdları;  // Köy adlarını virgülle ayrılmış şekilde kaydediyoruz
                    seferGorev.Koy_ids = KoyNo;     // Köy numaralarını virgülle ayrılmış şekilde kaydediyoruz

                    string[] koyIdArray = KoyNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    int toplamKisiSayisi = 0;
                    foreach (string id in koyIdArray)
                    {
                        int koyId = int.Parse(id);
                        var koyG = db.tbl_Koyler.Find(koyId);
                        koyG.Gorevlendir = true;

                        int kisiSayisi = db.tbl_Kisiler.Count(k => k.Koy == koyId && k.Aktiflik == true && k.Kategori == 32);

                        toplamKisiSayisi += kisiSayisi;

                    }

                    seferGorev.PaketSayisi = toplamKisiSayisi;

                    if (string.IsNullOrWhiteSpace(txt_Cıkıs.Text))
                    {
                        MessageBox.Show("Lütfen çıkış sırası giriniz.");
                       
                    }

                    if (string.IsNullOrWhiteSpace(txt_Cıkıs.Text))
                    {
                        MessageBox.Show("Lütfen çıkış sırası giriniz.");
                        return;
                    }

                    int cikisSirasi = Convert.ToInt32(txt_Cıkıs.Text);

                    // Veritabanında aynı çıkış sırası kontrolü
                    bool cikisSirasiVarMi = db.tbl_SeferGorev.Any(sg => sg.cikisSirasi == cikisSirasi);
                    if (cikisSirasiVarMi)
                    {
                        MessageBox.Show("Bu çıkış sırası zaten kullanılıyor. Lütfen farklı bir çıkış sırası giriniz.");
                        return;
                    }

                    seferGorev.cikisSirasi = cikisSirasi;

                    seferGorev.cikisSirasi = Convert.ToInt32(txt_Cıkıs.Text);



                    // Veritabanına ekle
                    db.tbl_SeferGorev.Add(seferGorev);
                    db.SaveChanges();

                    // Görev listesini güncelle
                    gorevListesi(datagridGörevler);
                    aracListesi(dataGridArac);
                    gorevliListesi(dataGridGorevli);
                    koyListesi(dataGridKoy);


                    secilenGorevliAdları.Clear();
                    secilerGorevliler.Clear();
                    rch_Yardimci.Clear();

                    secilenKoyAdlari.Clear();
                    secilenKoyler.Clear();
                    rch_koyler.Clear();
                    gorevListesi(datagridGörevler);

                    lblyNo.Text = string.Empty;
                    txt_sofor.Text = string.Empty;
                    lblşNo.Text = string.Empty;
                    txt_arac.Text = string.Empty;
                    tplmpktsys.Text = string.Empty;
                    y = 0;

                    secilenKoy();
                    secilenKisi();
                    secilenArac();
                    secilenGorevli();
                    aracKapasite();
                    dagıtımaCikilanPaketSayisi();
                }
                else
                {
                    MessageBox.Show("Lütfen tüm gerekli alanları doldurun.");
                }
            }
            catch (FormatException ex)
            {
                
            }
           





        }

        private void txt_arac_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                txt_arac.Text=string.Empty;
                lblaNo.Text = string.Empty;

                if (lblgNo.Text != string.Empty)
                {

                  
                    int gorevNo = Convert.ToInt32(lblgNo.Text);
                    var x = db.tbl_SeferGorev.Find(gorevNo);

               

                    if (!string.IsNullOrEmpty(lblaNo.Text) && int.TryParse(lblaNo.Text, out int aracNo))
                    {
                        var aracG = db.tbl_Gorevliler.Find(aracNo);
                        if (aracG != null)
                        {
                            aracG.Gorevlendir = false;
                        }
                    }


                }

            }
        }

        private void txt_sofor_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                txt_sofor.Text=string.Empty;
                lblşNo.Text = string.Empty;
                if (lblgNo.Text != string.Empty)
                {
                    // Köyler
                    int gorevNo = Convert.ToInt32(lblgNo.Text);
                    var x = db.tbl_SeferGorev.Find(gorevNo);
                    var şNo = x.Sofor_id.ToString();
                    lblşNo.Text = şNo;
                    

                    // lblşNo.Text'in boş olmadığı ve geçerli bir sayıya dönüştürülebilir olup olmadığını kontrol et
                    if (!string.IsNullOrEmpty(lblşNo.Text) && int.TryParse(lblşNo.Text, out int soforNo))
                    {
                        var soforG = db.tbl_Gorevliler.Find(soforNo);
                        if (soforG != null)
                        {
                            soforG.Gorevlendir = false;
                        }
                    }
                  
                }

                if (lblgNo.Text != string.Empty)
                {

                    // Köyler
                    int gorevNo = Convert.ToInt32(lblgNo.Text);
                    var x = db.tbl_SeferGorev.Find(gorevNo);
                    var gorevliNo = x.Gorevli_ids.ToString();

                    string[] gorevliNoArray = gorevliNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string id in gorevliNoArray)
                    {
                        int gorevliıd = int.Parse(id);
                        var GorevliG = db.tbl_Gorevliler.Find(gorevliıd);
                        if (GorevliG != null) GorevliG.Gorevlendir = false;
                    }


                }
            }
        }

        private void txt_yardımcı_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {

                secilenGorevliAdları.Clear();
                secilerGorevliler.Clear();
                rch_Yardimci.Clear();

            }
        }

        private void rch_koyler_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                rch_koyler.Clear();
                secilenKoyAdlari.Clear();
                secilenKoyler.Clear();
                tplmpktsys.Text = string.Empty;
                y = 0;

                if(lblgNo.Text != string.Empty)
                {

                    // Köyler
                    int gorevNo = Convert.ToInt32(lblgNo.Text);
                    var x = db.tbl_SeferGorev.Find(gorevNo);
                    var koyNo = x.Koy_ids.ToString();

                    string[] koyIdArray = koyNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string id in koyIdArray)
                    {
                        int koyId = int.Parse(id);
                        var koyG = db.tbl_Koyler.Find(koyId);
                        if (koyG != null) koyG.Gorevlendir = false;
                    }
                    

                }



            }
        }

        private void datagridGörevler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && datagridGörevler.Columns[e.ColumnIndex].Name != "Sil")
            {
               
                lblgNo.Text = datagridGörevler.Rows[e.RowIndex].Cells[1].Value.ToString();
                int gorevNo = Convert.ToInt32(lblgNo.Text);
                var x = db.tbl_SeferGorev.Find(gorevNo);
                if (lblgNo.Text != string.Empty)
                {
                    btnKaydet.Enabled = false;
                    btngercekGuncelle.Enabled = true;
                }
                if (x !=null)
                {
                    DataGridViewRow row = datagridGörevler.Rows[e.RowIndex];
                    txt_arac.Text = row.Cells["Arac"].Value.ToString();
                    lblaNo.Text = x.Arac_id.ToString();
                    txt_sofor.Text = row.Cells["Sofor"].Value.ToString();
                    lblşNo.Text = x.Sofor_id.ToString();
                    
                    rch_Yardimci.Text =   row.Cells["Yardımcı"].Value.ToString()  ;
                    var gorevliNo = x.Gorevli_ids.ToString();
              secilerGorevliler = gorevliNo
                    .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(int.Parse)
                     .ToList();
                    rch_koyler.Text =row.Cells["Koyler"].Value.ToString() ;
                    var koyNo = x.Koy_ids.ToString();
                    secilenKoyler = koyNo
                    .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                     .ToList();
                    txt_Cıkıs.Text = x.cikisSirasi.ToString();
                    tplmpktsys.Text = x.PaketSayisi.ToString();


                }
                


            }



            // 1. Satır ve sütun kontrolü
            if (e.RowIndex >= 0 && datagridGörevler.Columns[e.ColumnIndex].Name == "Sil")
            {
                lblgNo.Text = datagridGörevler.Rows[e.RowIndex].Cells[1].Value.ToString();
             
                int gorevNo = Convert.ToInt32(lblgNo.Text);
                var x = db.tbl_SeferGorev.Find(gorevNo);

                if (x != null)
                {
                    lblaNo.Text = x.Arac_id.ToString();
                    lblşNo.Text = x.Sofor_id.ToString();
                    var gorevliNo = x.Gorevli_ids.ToString();
                    var koyNo = x.Koy_ids.ToString();

                    db.tbl_SeferGorev.Remove(x);

                    // Araç
                    int aracNo = Convert.ToInt32(lblaNo.Text);
                    var aracG = db.tbl_Araclar.Find(aracNo);
                    if (aracG != null) aracG.Gorevlendir = false;

                    // Şoför
                    int soforNo = Convert.ToInt32(lblşNo.Text);
                    var soforG = db.tbl_Gorevliler.Find(soforNo);
                    if (soforG != null) soforG.Gorevlendir = false;

                    // Yardımcı Görevliler
                    string[] gorevliNoArray = gorevliNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string no in gorevliNoArray)
                    {
                        int gorevliId = int.Parse(no);
                        var gorevliG = db.tbl_Gorevliler.Find(gorevliId);
                        if (gorevliG != null) gorevliG.Gorevlendir = false;
                    }

                    // Köyler
                    string[] koyIdArray = koyNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string id in koyIdArray)
                    {
                        int koyId = int.Parse(id);
                        var koyG = db.tbl_Koyler.Find(koyId);
                        if (koyG != null) koyG.Gorevlendir = false;
                    }

                    db.SaveChanges();

                    // Listeleri güncelle
                    gorevListesi(datagridGörevler);
                    aracListesi(dataGridArac);
                    gorevliListesi(dataGridGorevli);
                    koyListesi(dataGridKoy);

                    // Alanları temizle
                    secilenKoyAdlari.Clear();
                    secilenKoyler.Clear();
                    rch_koyler.Clear();

                    secilenGorevliAdları.Clear();
                    secilerGorevliler.Clear();
                    rch_Yardimci.Clear();

                    lblyNo.Text = string.Empty;
                    txt_sofor.Text = string.Empty;
                    lblşNo.Text = string.Empty;
                    txt_arac.Text = string.Empty;
                    tplmpktsys.Text = string.Empty;
                    lblgNo.Text = string.Empty;
                    if (lblgNo.Text == string.Empty)
                    {
                        btnKaydet.Enabled = true;
                        btngercekGuncelle.Enabled = false;
                    }
                    y = 0;

                    // Diğer kontrolleri sıfırla
                    secilenKoy();
                    secilenKisi();
                    secilenArac();
                    secilenGorevli();
                    aracKapasite();
                 //    gorevListesibolgesiz(datagridGörevler);
                    dagıtımaCikilanPaketSayisi();

                }
                else
                {
                    MessageBox.Show("Görev bulunamadı.");
                }
            }


        }

        private void rch_Yardimci_KeyDown(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {



                secilenGorevliAdları.Clear();
                secilerGorevliler.Clear();
                rch_Yardimci.Clear();

            }
            if (lblgNo.Text != string.Empty)
            {

                // Köyler
                int gorevNo = Convert.ToInt32(lblgNo.Text);
                var x = db.tbl_SeferGorev.Find(gorevNo);
                var gorevliNo = x.Gorevli_ids.ToString();

                string[] gorevliNoArray = gorevliNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string id in gorevliNoArray)
                {
                    int gorevliıd = int.Parse(id);
                    var GorevliG = db.tbl_Gorevliler.Find(gorevliıd);
                    if (GorevliG != null) GorevliG.Gorevlendir = false;
                }


            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtDagitimSifre.Text == "965478")
            {
                try
                {
                    // Listeyi sıfırlamak için kullanıcıdan onay al
                    DialogResult dialogResult = MessageBox.Show("Listeyi sıfırlamak istediğinizden emin misiniz?",
                                                                "Onay",
                                                                MessageBoxButtons.YesNo,
                                                                MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // 1. İlgili araçlar için "Gorevlendir" alanını false yap
                        var araclar = db.tbl_Araclar.ToList();
                        foreach (var arac in araclar)
                        {
                            arac.Gorevlendir = false;
                        }

                        // 2. İlgili şoförler için "Gorevlendir" alanını false yap
                        var gorevliler = db.tbl_Gorevliler.ToList();
                        foreach (var gorevli in gorevliler)
                        {
                            gorevli.Gorevlendir = false;
                        }

                        // 3. İlgili köyler için "Gorevlendir" alanını false yap
                        var koyler = db.tbl_Koyler.ToList();
                        foreach (var koy in koyler)
                        {
                            koy.Gorevlendir = false;
                        }
                        var seferGorevListesi = db.tbl_SeferGorev.ToList();
                        db.tbl_SeferGorev.RemoveRange(seferGorevListesi);


                        // 4. Tüm değişiklikleri veritabanına kaydet
                        db.SaveChanges();

                        MessageBox.Show("Tüm araçlar, görevliler ve köyler sıfırlandı.");

                        // 5. Listeleri güncelle
                        gorevListesi(datagridGörevler);
                        aracListesi(dataGridArac);
                        gorevliListesi(dataGridGorevli);
                        koyListesi(dataGridKoy);
                        txtDagitimSifre.Text = "";

                        secilenKoy();
                        secilenKisi();
                        secilenArac();
                        secilenGorevli();
                        aracKapasite();
                       // gorevListesibolgesiz(datagridGörevler);
                        dagıtımaCikilanPaketSayisi();
                    }
                    else
                    {
                        MessageBox.Show("Liste sıfırlama işlemi iptal edildi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
            else {
                MessageBox.Show("Lütfen Doğru Şifreyi Giriniz");
            }
            


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = true;

                var workBook = excelApp.Workbooks.Add(Type.Missing);
                var workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Sheets[1];

                int excelCol = 1;

                for (int colIndex = 0; colIndex < datagridGörevler.Columns.Count; colIndex++)
                {
                    string columnHeader = datagridGörevler.Columns[colIndex].HeaderText;
                    if (columnHeader == "GorevNo" || columnHeader == "YardımcıNo" || columnHeader == "KoyNo" || columnHeader == "Sil")
                        continue;

                    var cell = (Microsoft.Office.Interop.Excel.Range)workSheet.Cells[1, excelCol];
                    cell.Value = columnHeader;
                    cell.Font.Bold = true;
                    cell.Font.Size = 12;
                    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkOrange);
                    cell.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    cell.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    cell.WrapText = true; // Metni kaydır
                    cell.Orientation = 90;
                    excelCol++;
                }

                for (int rowIndex = 0; rowIndex < datagridGörevler.Rows.Count; rowIndex++)
                {
                    excelCol = 1;
                    for (int colIndex = 0; colIndex < datagridGörevler.Columns.Count; colIndex++)
                    {
                        string columnHeader = datagridGörevler.Columns[colIndex].HeaderText;
                        if (columnHeader == "GorevNo" || columnHeader == "YardımcıNo" || columnHeader == "KoyNo" || columnHeader == "Sil")
                            continue;

                        var value = datagridGörevler.Rows[rowIndex].Cells[colIndex].Value?.ToString() ?? "";

                        var cell = (Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex + 2, excelCol];
                        cell.Value = value;
                        cell.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        cell.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        cell.WrapText = true;

                        // 🔵 Alternatif satır renklendirme (açık mavi ve beyaz)
                        if ((rowIndex % 2) == 0)
                        {
                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 225, 242)); // Açık mavi
                        }
                        else
                        {
                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        }

                        excelCol++;
                    }
                }

                // 🌐 Sayfa yapısı: yatay, tek sayfa genişliğine sığsın
                workSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                workSheet.PageSetup.FitToPagesWide = 1;
                workSheet.PageSetup.FitToPagesTall = false;

                workSheet.Columns.AutoFit();
                workSheet.Rows.AutoFit();
                // Excel nesnelerini serbest bırak
                // workBook.Close(false);
                //  excelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }


        }

        private void cmbBolge_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
          

            
            
                koyListesi(dataGridKoy);
                var bolgeNo = Convert.ToInt32(cmbBolge.SelectedValue);
                lblbNo.Text = bolgeNo.ToString();

                secilenKoyAdlari.Clear();
                secilenKoyler.Clear();
                rch_koyler.Clear();
                gorevListesi(datagridGörevler);

                rch_Yardimci.Text = string.Empty;
                lblyNo.Text = string.Empty;
                txt_sofor.Text = string.Empty;
                lblşNo.Text = string.Empty;
                txt_arac.Text = string.Empty;
                lblaNo.Text = string.Empty;
                txt_Cıkıs.Text = string.Empty;
            tplmpktsys.Text = string.Empty;
            lblgNo.Text = string.Empty;
            if (lblgNo.Text == string.Empty)
            {
                btnKaydet.Enabled = true;
                btngercekGuncelle.Enabled = false;
            }
            y = 0;

         

        }

        private void btn_aracbosacıkar_Click(object sender, EventArgs e)
        {
            if (txtDagitimSifre.Text == "965478")
            {
                try
                {
                    // Listeyi sıfırlamak için kullanıcıdan onay al
                    DialogResult dialogResult = MessageBox.Show("Listeyi sıfırlamak istediğinizden emin misiniz?",
                                                                "Onay",
                                                                MessageBoxButtons.YesNo,
                                                                MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // 1. tbl_SeferGorev tablosundaki tüm verileri al
                        var seferGorevListesi = db.tbl_SeferGorev.ToList();

                        // 2. SeferGorev'deki her bir kaydı işleyip ilgili alanları sıfırla
                        foreach (var seferGorev in seferGorevListesi)
                        {
                            // 3. İlgili araçları "Gorevlendir" alanını false yap
                            var arac = db.tbl_Araclar.Find(seferGorev.Arac_id);
                            if (arac != null)
                            {
                                arac.Gorevlendir = false;
                            }

                            var sofor = db.tbl_Gorevliler.Find(seferGorev.Sofor_id);
                            if (sofor != null)
                            {
                                sofor.Gorevlendir = false;
                            }

                            // 4. İlgili köyleri "Gorevlendir" alanını false yap
                            string[] koyIdArray = seferGorev.Koy_ids.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string koyId in koyIdArray)
                            {
                                int koyIdInt = int.Parse(koyId);
                                var koy = db.tbl_Koyler.Find(koyIdInt);
                                if (koy != null)
                                {
                                    koy.Gorevlendir = null;
                                }
                            }

                            // 5. İlgili görevlileri "Gorevlendir" alanını false yap
                            string[] gorevliIdArray = seferGorev.Gorevli_ids.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string gorevliId in gorevliIdArray)
                            {
                                int gorevliIdInt = int.Parse(gorevliId);
                                var gorevli = db.tbl_Gorevliler.Find(gorevliIdInt);
                                if (gorevli != null)
                                {
                                    gorevli.Gorevlendir = false;
                                }
                            }





                        }
                        db.tbl_SeferGorev.Remove(seferGorev);

                        // 7. Tüm değişiklikleri veritabanına kaydet
                        db.SaveChanges();

                        MessageBox.Show("Tüm Araçlar ve Görevliler Boşa Çıkartıldı.");

                        // 8. Listeleri güncelle
                        gorevListesi(datagridGörevler);
                        aracListesi(dataGridArac);
                        gorevliListesi(dataGridGorevli);
                        koyListesi(dataGridKoy);
                    }
                    else
                    {
                        MessageBox.Show("Araç ve Görevli Boşa çıkarma işlemi iptal edildi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Doğru Şifreyi Giriniz");
            }
        }

        private void txt_arac_ad_TextChanged(object sender, EventArgs e)
        {
            aracara(dataGridArac);
        }

        private void cmbBolge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                gorevListesibolgesiz(datagridGörevler);
            }
        }

        private void txt_Cıkıs_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakamlar ve kontrol tuşlarına (örn. Backspace) izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Geçersiz karakteri engelle
            }
        }

        private void btngercekGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int aracNo = Convert.ToInt32(lblaNo.Text);
                int soforNo = Convert.ToInt32(lblşNo.Text);
                int bolgeNo = Convert.ToInt32(lblbNo.Text);

                string KoyAdları = rch_koyler.Text;
                string KoyNo = string.Join(", ", secilenKoyler);

                string GorevliAdlari = rch_Yardimci.Text;
                string GorevliNo = string.Join(", ", secilerGorevliler);

                int guncellenecekSeferId = Convert.ToInt32(lblgNo.Text); // Güncellemek istediğin seferin ID'sini saklamalısın
                var sefer = db.tbl_SeferGorev.Find(guncellenecekSeferId);
                if (sefer != null)
                {
                    // Boş olan değerleri kontrol et
                    if (aracNo > 0 && soforNo > 0 && !string.IsNullOrEmpty(KoyAdları) && !string.IsNullOrEmpty(KoyNo) && bolgeNo > 0)
                    {
                        sefer.Arac_id = aracNo;
                        var aracG = db.tbl_Araclar.Find(aracNo);
                        aracG.Gorevlendir = true;

                        sefer.Sofor_id = soforNo;
                        var soforG = db.tbl_Gorevliler.Find(soforNo);
                        soforG.Gorevlendir = true;

                        sefer.Gorevli_ids = GorevliNo;
                        sefer.Gorevli = GorevliAdlari; // Görevli numaralarını virgülle ayrılmış şekilde kaydediyoruz

                        string[] gorevliIdArray = GorevliNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string id in gorevliIdArray)
                        {
                            int gorevliId = int.Parse(id);
                            var gorevliG = db.tbl_Gorevliler.Find(gorevliId);
                            gorevliG.Gorevlendir = true;
                        }

                        sefer.Bölge = bolgeNo;

                        sefer.Köyler = KoyAdları;  // Köy adlarını virgülle ayrılmış şekilde kaydediyoruz
                        sefer.Koy_ids = KoyNo;     // Köy numaralarını virgülle ayrılmış şekilde kaydediyoruz

                        string[] koyIdArray = KoyNo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        int toplamKisiSayisi = 0;
                        foreach (string id in koyIdArray)
                        {
                            int koyId = int.Parse(id);
                            var koyG = db.tbl_Koyler.Find(koyId);
                            koyG.Gorevlendir = true;

                            int kisiSayisi = db.tbl_Kisiler.Count(k => k.Koy == koyId && k.Aktiflik == true && k.Kategori == 32);

                            toplamKisiSayisi += kisiSayisi;
                        }

                        seferGorev.PaketSayisi = toplamKisiSayisi;

                        if (string.IsNullOrWhiteSpace(txt_Cıkıs.Text))
                        {
                            MessageBox.Show("Lütfen çıkış sırası giriniz.");
                            return;
                        }

                        int cikisSirasi = Convert.ToInt32(txt_Cıkıs.Text);

                        // Eğer mevcut çıkış sırası güncellenmek istenen sefer ile aynıysa, mükerrer kontrol yapma
                        if (cikisSirasi != sefer.cikisSirasi)
                        {
                            // Veritabanında aynı çıkış sırası var mı diye kontrol et
                            bool cikisSirasiVarMi = db.tbl_SeferGorev.Any(sg => sg.cikisSirasi == cikisSirasi);
                            if (cikisSirasiVarMi)
                            {
                                MessageBox.Show("Bu çıkış sırası zaten kullanılıyor. Lütfen farklı bir çıkış sırası giriniz.");
                                return;
                            }
                        }

                        // Çıkış sırasını güncelle
                        seferGorev.cikisSirasi = cikisSirasi;

                        // Veritabanına kaydet
                        db.SaveChanges();

                        // Görev listesini güncelle
                        gorevListesi(datagridGörevler);
                        aracListesi(dataGridArac);
                        gorevliListesi(dataGridGorevli);
                        koyListesi(dataGridKoy);

                        // Temizleme işlemleri
                        secilenGorevliAdları.Clear();
                        secilerGorevliler.Clear();
                        rch_Yardimci.Clear();
                        secilenKoyAdlari.Clear();
                        secilenKoyler.Clear();
                        rch_koyler.Clear();

                        lblyNo.Text = string.Empty;
                        txt_sofor.Text = string.Empty;
                        lblşNo.Text = string.Empty;
                        txt_arac.Text = string.Empty;
                        tplmpktsys.Text = string.Empty;
                        y = 0;
                        lblgNo.Text = string.Empty;

                        if (lblgNo.Text == string.Empty)
                        {
                            btnKaydet.Enabled = true;
                            btngercekGuncelle.Enabled = false;
                        }

                        secilenKoy();
                        secilenKisi();
                        secilenArac();
                        secilenGorevli();
                        aracKapasite();
                        dagıtımaCikilanPaketSayisi();
                    }
                    else
                    {
                        MessageBox.Show("Lütfen tüm gerekli alanları doldurun.");
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }

        
    }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            gorevListesi(datagridGörevler);
            aracListesi(dataGridArac);
            gorevliListesi(dataGridGorevli);
            koyListesi(dataGridKoy);

            secilenGorevliAdları.Clear();
            secilerGorevliler.Clear();
            rch_Yardimci.Clear();

            secilenKoyAdlari.Clear();
            secilenKoyler.Clear();
            rch_koyler.Clear();
            gorevListesi(datagridGörevler);

            lblyNo.Text = string.Empty;
            txt_sofor.Text = string.Empty;
            lblşNo.Text = string.Empty;
            txt_arac.Text = string.Empty;
            tplmpktsys.Text = string.Empty;
            y = 0;
            lblgNo.Text = string.Empty;
            if(lblgNo.Text == string.Empty)
            {
                btnKaydet.Enabled = true;
                btngercekGuncelle.Enabled = false;
            }
        }
        int hoveredRowIndexGridArac = -1;
        private void dataGridArac_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != hoveredRowIndexGridArac)
            {
                if (hoveredRowIndexGridArac >= 0 && hoveredRowIndexGridArac < dataGridArac.Rows.Count)
                {
                    // Önceki hover satırını eski haline döndür
                    dataGridArac.Rows[hoveredRowIndexGridArac].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                }

                // Yeni hover satırı
                dataGridArac.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 110, 49);
                hoveredRowIndexGridArac = e.RowIndex;
            }
        }

        private void dataGridArac_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (hoveredRowIndexGridArac >= 0 && hoveredRowIndexGridArac < dataGridArac.Rows.Count)
            {
                dataGridArac.Rows[hoveredRowIndexGridArac].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                hoveredRowIndexGridArac = -1;
            }
        }
        int hoveredRowIndexGridGorevli = -1;
        private void dataGridGorevli_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != hoveredRowIndexGridGorevli)
            {
                if (hoveredRowIndexGridGorevli >= 0 && hoveredRowIndexGridGorevli < dataGridGorevli.Rows.Count)
                {
                    // Önceki hover satırını eski haline döndür
                    dataGridGorevli.Rows[hoveredRowIndexGridGorevli].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                }

                // Yeni hover satırı
                dataGridGorevli.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 110, 49);
                hoveredRowIndexGridGorevli = e.RowIndex;
            }
        }

        private void dataGridGorevli_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (hoveredRowIndexGridGorevli >= 0 && hoveredRowIndexGridGorevli < dataGridGorevli.Rows.Count)
            {
                dataGridGorevli.Rows[hoveredRowIndexGridGorevli].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                hoveredRowIndexGridGorevli = -1;
            }
        }
        int hoveredRowIndexGridKoy = -1;
        private void dataGridKoy_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != hoveredRowIndexGridKoy)
            {
                if (hoveredRowIndexGridKoy >= 0 && hoveredRowIndexGridKoy < dataGridKoy.Rows.Count)
                {
                    // Önceki hover satırını eski haline döndür
                    dataGridKoy.Rows[hoveredRowIndexGridKoy].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                }

                // Yeni hover satırı
                dataGridKoy.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 110, 49);
                hoveredRowIndexGridKoy = e.RowIndex;
            }
        }

        private void dataGridKoy_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (hoveredRowIndexGridKoy >= 0 && hoveredRowIndexGridKoy < dataGridKoy.Rows.Count)
            {
                dataGridKoy.Rows[hoveredRowIndexGridKoy].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                hoveredRowIndexGridKoy = -1;
            }

        }
        int hoveredRowIndexGridGorevler = -1;

        private void datagridGörevler_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != hoveredRowIndexGridGorevler)
            {
                if (hoveredRowIndexGridGorevler >= 0 && hoveredRowIndexGridGorevler < datagridGörevler.Rows.Count)
                {
                    // Önceki hover satırını eski haline döndür
                    datagridGörevler.Rows[hoveredRowIndexGridGorevler].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                }

                // Yeni hover satırı
                datagridGörevler.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 110, 49);
                hoveredRowIndexGridGorevler = e.RowIndex;
            }
        }

      

        private void datagridGörevler_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (hoveredRowIndexGridGorevler >= 0 && hoveredRowIndexGridGorevler < datagridGörevler.Rows.Count)
            {
                datagridGörevler.Rows[hoveredRowIndexGridGorevler].DefaultCellStyle.BackColor = Color.FromArgb(120, 120, 150);
                hoveredRowIndexGridGorevler = -1;
            }
        }

        private void dataGridArac_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridArac.ClearSelection();
            dataGridArac.ClearSelection();
            dataGridArac.CurrentCell = null;
            dataGridArac.RowHeadersVisible = false;
        }

        private void dataGridKoy_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!dataGridKoy.Columns.Contains("Güzergah")) return;

            int guzergahColIndex = dataGridKoy.Columns["Güzergah"].Index;

            foreach (DataGridViewRow row in dataGridKoy.Rows)
            {
                // Sabit lacivert arka plan + beyaz yazı
                row.Cells[guzergahColIndex].Style.BackColor = Color.FromArgb(20, 20, 60);
                row.Cells[guzergahColIndex].Style.ForeColor = Color.White;
                row.Cells[guzergahColIndex].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }

            dataGridKoy.ClearSelection();
            // Ayrıca CurrentCell'i null yaparak köşedeki hücre odak çizgisini de kaldır
            dataGridKoy.CurrentCell = null;
            dataGridKoy.RowHeadersVisible = false;
        }

        private void dataGridGorevli_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridGorevli.ClearSelection();
            dataGridGorevli.CurrentCell = null;
            dataGridGorevli.RowHeadersVisible = false;
        }
    }
}

