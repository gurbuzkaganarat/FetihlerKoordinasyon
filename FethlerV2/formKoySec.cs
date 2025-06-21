using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.CodeParser;
using DevExpress.XtraEditors.Filtering.Templates;
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
    public partial class formKoySec : Form
    {
        public formKoySec()
        {
            InitializeComponent();
        }
        FetihlerV2Entities1 db = new FetihlerV2Entities1();

        private void cmbDataLoad()
        {
            cmbBolgeAd.DataSource = db.tbl_Bolgeler.Where(x => x.Aktiflik == true).ToList();
            cmbBolgeAd.ValueMember = "BolgeNo";
            cmbBolgeAd.DisplayMember = "BolgeAdi";

        }

        public void List1 (DataGridView data)
        {
            string koyAra = txtKoyAra.Text.ToLower();

            var dataList = (from d1 in db.tbl_Koyler
                            join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                            from d2 in d2list.DefaultIfEmpty()
                            join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list
                            where d1.Aktiflik == true && d1.Sec == true
                            select new
                            {
                                KöyNo = d1.KoyNo,
                                Bolge = d2.BolgeAdi,
                                KöyAdi = d1.KoyAdi,
                                KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),
                                GuzergahSırası = d1.Güzergah
                            }).ToList();

            bool HarflerYeterinceVar(string kaynak, string aranan)
            {
                kaynak = kaynak.ToLower();
                var grup = aranan.ToLower().GroupBy(c => c);

                foreach (var harfGrubu in grup)
                {
                    int gerekenAdet = harfGrubu.Count();
                    int mevcutAdet = kaynak.Count(c => c == harfGrubu.Key);
                    if (mevcutAdet < gerekenAdet)
                        return false;
                }

                return true;
            }

            var filtered = dataList
                .Where(x => HarflerYeterinceVar(x.KöyAdi, koyAra))
                .OrderBy(x => x.KöyAdi)
                .ToList();

            data.DataSource = filtered;
            data.Columns[1].Visible = false;


        }

        public void List2 (DataGridView data2)
        {
            string koyAra = txtKoyAra.Text.ToLower();

            var dataList = (from d1 in db.tbl_Koyler
                            join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                            from d2 in d2list.DefaultIfEmpty()
                            join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list
                            where d1.Aktiflik == true && d1.Sec == false
                            select new
                            {
                                KöyNo = d1.KoyNo,
                                Bolge = d2.BolgeAdi,
                                KöyAdi = d1.KoyAdi,
                                KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),
                                GuzergahSırası = d1.Güzergah
                            }).ToList();

            bool HarflerYeterinceVar(string kaynak, string aranan)
            {
                kaynak = kaynak.ToLower();
                var grup = aranan.ToLower().GroupBy(c => c);

                foreach (var harfGrubu in grup)
                {
                    int gerekenAdet = harfGrubu.Count();
                    int mevcutAdet = kaynak.Count(c => c == harfGrubu.Key);
                    if (mevcutAdet < gerekenAdet)
                        return false;
                }

                return true;
            }

            var filtered = dataList
                .Where(x => HarflerYeterinceVar(x.KöyAdi, koyAra))
                .OrderBy(x => x.KöyAdi)
                .ToList();

            data2.DataSource = filtered;
            data2.Columns[1].Visible = false;
        }

        public void toplamKisi()
        {
            var query = from d1 in db.tbl_Koyler
                        join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                        from d2 in d2list.DefaultIfEmpty()
                        join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list
                        
                        where d1.Aktiflik == true
                        
                        

                        select new
                        {
                            KöyNo = d1.KoyNo,
                            Bolge = d2.BolgeAdi,
                            KöyAdi = d1.KoyAdi,
                            KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),
                            GuzergahSırası = d1.Güzergah


                        };
            var toplam = (from x in query select (int?)x.KisiSayisi).Sum();
            lblToplamFakir.Text = toplam.ToString();
        }

        public void secilenKisi()
        {
            var query = from d1 in db.tbl_Koyler
                        join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                        from d2 in d2list.DefaultIfEmpty()
                        join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list
                        
                        where d1.Aktiflik == true
                        where d1.Sec == true
                        

                        select new
                        {
                            KöyNo = d1.KoyNo,
                            Bolge = d2.BolgeAdi,
                            KöyAdi = d1.KoyAdi,
                            KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),
                            GuzergahSırası = d1.Güzergah


                        };
            var toplam = (from x in query select (int?)x.KisiSayisi).Sum() ?? 0;
            // (int?) null yapılabilen deger  ?? 0 ise baslangıctada null deger dondurebilir
            lblDagitimFakir.Text = toplam.ToString();

        }

        public void bolgeSec1(DataGridView data)
        {

            if (cmbBolgeAd.SelectedItem != null)
            {
                var bolgeNo = Convert.ToInt32(cmbBolgeAd.SelectedValue);
                string koyAra = txtKoyAra.Text;
                var query = 
                             from d1 in db.tbl_Koyler
                             join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                             from d2 in d2list.DefaultIfEmpty()
                             join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list
                             orderby d1.Güzergah
                             where d1.Bolge == bolgeNo
                             where d1.Aktiflik == true
                             where d1.Sec == true
                             where d1.KoyAdi.Contains(koyAra)


                select new
                {
                    KöyNo = d1.KoyNo,
                    Bolge = d2.BolgeAdi,
                    KöyAdi = d1.KoyAdi,
                    KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),
                    GuzergahSırası = d1.Güzergah


                };
                data.DataSource = query.ToList();
                data.Columns[1].Visible = false;

            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);

            }
        }

        public void bolgeSec2(DataGridView data2)
        {
            if (cmbBolgeAd.SelectedItem != null)
            {
                var bolgeNo = Convert.ToInt32(cmbBolgeAd.SelectedValue);
                string koyAra = txtKoyAra.Text;

                var query =
                             from d1 in db.tbl_Koyler
                             join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                             from d2 in d2list.DefaultIfEmpty()
                             join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list
                             orderby d1.Güzergah
                             where d1.Bolge == bolgeNo
                             where d1.Aktiflik == true
                             where d1.Sec == false
                             where d1.KoyAdi.Contains(koyAra)


                             select new
                             {
                                 KöyNo = d1.KoyNo,
                                 Bolge = d2.BolgeAdi,
                                 KöyAdi = d1.KoyAdi,
                                 KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),
                                 GuzergahSırası = d1.Güzergah


                             };
                data2.DataSource = query.ToList();
                data2.Columns[1].Visible = false;

            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);

            }

        }


        public void toplamKoy()
        {

            var query = from d1 in db.tbl_Koyler
                        where d1.Aktiflik == true
                        select new
                        {
                            toplamKoySayisi = db.tbl_Koyler.Count(x=> x.Aktiflik == true)
                        };

            var toplamKoy = (from d1 in query select (int?)d1.toplamKoySayisi).Count();

            lblToplamKoy.Text = toplamKoy.ToString();                             

        }

        public void bolgeyegoreToplamKoy() {

            if (cmbBolgeAd.SelectedItem != null)
            {
                var bolgeNo = Convert.ToInt32(cmbBolgeAd.SelectedValue);

                var query = from d1 in db.tbl_Koyler
                            where d1.Bolge == bolgeNo && d1.Aktiflik == true
                            where d1.Sec ==true
                            select new
                            {
                                toplamKoySayisi = db.tbl_Koyler.Count(x => x.Bolge == bolgeNo && x.Aktiflik == true && x.Sec == true)
                            };

                var toplamKoy = query.FirstOrDefault()?.toplamKoySayisi ?? 0;
                lbl_bolgekoysayısı.Text = toplamKoy.ToString();
            }
            else
            {
                toplamKoy();
            }

        }

        public void bolgeyegoreToplamFakirSayısı() {


            if (cmbBolgeAd.SelectedItem != null)
            {
                int bolgeNo = Convert.ToInt32(cmbBolgeAd.SelectedValue);

                var query = from d1 in db.tbl_Koyler
                            join d2 in db.tbl_Bolgeler on d1.Bolge equals d2.BolgeNo into d2list
                            from d2 in d2list.DefaultIfEmpty()
                            join d3 in db.tbl_Kisiler on d1.KoyNo equals d3.Koy into d3list

                            where d1.Aktiflik == true && d1.Bolge == bolgeNo  && d1.Sec == true

                            select new
                            {
                                KöyNo = d1.KoyNo,
                                Bolge = d2.BolgeAdi,
                                KöyAdi = d1.KoyAdi,
                                KisiSayisi = d3list.Count(x => x.Aktiflik == true && x.Kategori == 32),
                                GuzergahSırası = d1.Güzergah
                            };

                var toplam = (from x in query select (int?)x.KisiSayisi).Sum() ?? 0;

                secilenbolgekoylusayısı.Text = toplam.ToString();
            }
            else
            {
                toplamKoy();
            }

        }

        public void secilenKoy()
        {
            var query = from d1 in db.tbl_Koyler
                        where d1.Aktiflik == true
                        where d1.Sec==true

                        select new
                        {
                            secilenKoySayisi = db.tbl_Koyler.Count(x => x.Aktiflik == true && x.Sec == true)
                        };

            var secilenKoy = (from d1 in query select (int?)d1.secilenKoySayisi).Count();

            lblSecilenKoy.Text = secilenKoy.ToString();
        }
       

        private void deneme_Load(object sender, EventArgs e)
        {
            List1(bunifuCustomDataGrid1);
            List2(bunifuCustomDataGrid2);
            toplamKisi();
            secilenKisi();
            cmbDataLoad();
            cmbBolgeAd.SelectedItem = null;
            toplamKoy();
            secilenKoy();


        }

        private void cmbBolgeAd_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblKoyNo.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                int koyNo = Convert.ToInt32(lblKoyNo.Text);
                var g = db.tbl_Koyler.Find(koyNo);
                var koyad = g.KoyAdi;

                // Kullanıcıya onay sorusu soruluyor
                DialogResult result = MessageBox.Show(
                    $"{koyad} köyünü dağıtımdan çıkartmak istiyor musunuz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    g.Sec = false;
                    db.SaveChanges();
                    bolgeSec1(bunifuCustomDataGrid1);
                    bolgeSec2(bunifuCustomDataGrid2);
                    toplamKisi();
                    secilenKisi();
                    toplamKoy();
                    secilenKoy();
                    bolgeyegoreToplamKoy();
                    bolgeyegoreToplamFakirSayısı();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }

        }

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblKoyNo.Text = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[1].Value?.ToString();
                int koyNo = Convert.ToInt32(lblKoyNo.Text);
                var g = db.tbl_Koyler.Find(koyNo);
                var koyad = g.KoyAdi;

                // Kullanıcıya onay sorusu soruluyor
                DialogResult result = MessageBox.Show(
                    $"{koyad} köyünü dağıtıma eklemek istiyor musunuz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    g.Sec = true;
                    db.SaveChanges();
                    bolgeSec1(bunifuCustomDataGrid1);
                    bolgeSec2(bunifuCustomDataGrid2);
                    toplamKisi();
                    secilenKisi();
                    toplamKoy();
                    secilenKoy();
                    bolgeyegoreToplamKoy();
                    bolgeyegoreToplamFakirSayısı();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }

        }

        private void cmbBolgeAd_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                cmbBolgeAd.SelectedItem = null;              
                bolgeSec1(bunifuCustomDataGrid1);
                bolgeSec2(bunifuCustomDataGrid2);
                lbl_bolgekoysayısı.Text = Convert.ToString(0);
                
                toplamKisi();
                secilenKisi();
                toplamKoy();
                secilenKoy();
               

            }
        }

        private void txtKoyAra_TextChanged(object sender, EventArgs e)
        {
           
            
                bolgeSec1(bunifuCustomDataGrid1);
                bolgeSec2(bunifuCustomDataGrid2);
            
           
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (cmbBolgeAd.SelectedItem != null)
            {
                try
                {
                    // Excel uygulamasını başlat ve görünür yap
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Visible = true;  // Excel uygulamasının görünür olmasını sağla

                    // Yeni bir çalışma kitabı oluştur
                    Microsoft.Office.Interop.Excel.Workbook workBook = excelApp.Workbooks.Add(Type.Missing);

                    // İlk sayfayı seç
                    Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Sheets[1];

                    // *** Başlıkları (Kolon Adlarını) Yaz ***
                    int excelCol = 1; // Excel sütun indeksini takip eden değişken
                    for (int colIndex = 0; colIndex < bunifuCustomDataGrid1.Columns.Count; colIndex++)
                    {
                        string columnHeader = bunifuCustomDataGrid1.Columns[colIndex].HeaderText;

                        // **"Çıkar" ve "KöyNo" kolonlarını atla**
                        if (columnHeader == "Çıkar" || columnHeader == "KöyNo"|| columnHeader == "Dağıtımdan Çıkar")
                            continue;

                        // Başlık hücrelerine kalın font ve arka plan rengi ekle
                        workSheet.Cells[1, excelCol] = columnHeader;
                        workSheet.Cells[1, excelCol].Font.Bold = true;
                        workSheet.Cells[1, excelCol].Font.Bold = 12;
                        workSheet.Cells[1, excelCol].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        workSheet.Cells[1, excelCol].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkOrange); // Arka plan rengi
                        workSheet.Cells[1, excelCol].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        excelCol++;
                    }

                    // *** DataGrid içindeki veriyi Excel'e yaz ***
                    for (int rowIndex = 0; rowIndex < bunifuCustomDataGrid1.Rows.Count; rowIndex++)
                    {
                        excelCol = 1; // Her satır başında excel sütun indexini sıfırla

                        for (int colIndex = 0; colIndex < bunifuCustomDataGrid1.Columns.Count; colIndex++)
                        {
                            string columnHeader = bunifuCustomDataGrid1.Columns[colIndex].HeaderText;

                            // **"Çıkar" ve "KöyNo" kolonlarını atla**
                            if (columnHeader == "Çıkar" || columnHeader == "KöyNo" || columnHeader == "Dağıtımdan Çıkar")
                                continue;

                            if (bunifuCustomDataGrid1.Rows[rowIndex].Cells[colIndex].Value != null)
                            {
                                workSheet.Cells[rowIndex + 2, excelCol] = bunifuCustomDataGrid1.Rows[rowIndex].Cells[colIndex].Value.ToString();

                            }
                            var cell = (Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex + 2, excelCol];
                            cell.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // Kenarlık ekle
                            cell.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter; // Ortala

                            excelCol++; // **Burada sadece gerekli olan sütunlara ilerle**
                        }


                    }

                    // **Kolon genişliklerini 25 olarak sabitle**
                    workSheet.Columns.ColumnWidth = 25;

                    // Excel nesnelerini serbest bırak
                    workBook.Close();
                    excelApp.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }

            }
            else {

                MessageBox.Show("Lütfen Bölge Seçiniz! ");
            }
           




        }

        private void cmbBolgeAd_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            bolgeyegoreToplamKoy();
            bolgeyegoreToplamFakirSayısı();
            bolgeSec1(bunifuCustomDataGrid1);
            bolgeSec2(bunifuCustomDataGrid2);

        }

        private void cmbBolgeAd_KeyDown_1(object sender, KeyEventArgs e)
        {
              if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                cmbBolgeAd.SelectedItem = null;              
                bolgeSec1(bunifuCustomDataGrid1);
                bolgeSec2(bunifuCustomDataGrid2);
                lbl_bolgekoysayısı.Text = Convert.ToString(0);
                
                toplamKisi();
                secilenKisi();
                toplamKoy();
                secilenKoy();
                secilenbolgekoylusayısı.Text = "0";
               

            }
        }
    }

}


