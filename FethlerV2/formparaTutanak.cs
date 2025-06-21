using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FethlerV2
{
    public partial class formparaTutanak : Form
    {
        FetihlerV2Entities1 db = new FetihlerV2Entities1();
        public void cmbDataLoad()
        {
            cmbKategori.DataSource = db.tbl_Kategoriler.Where(x => x.Aktiflik == true ).ToList();
            cmbKategori.ValueMember = "KategoriNo";
            cmbKategori.DisplayMember = "KategoriAdi";

            var queryKoy = from item in db.tbl_Koyler
                           where item.Aktiflik == true && item.Sec == true

                           orderby item.KoyAdi
                           select new
                           {
                               item.KoyNo,
                               item.KoyAdi,

                           };
            cmbKoy.DataSource = queryKoy.ToList();
            cmbKoy.ValueMember = "KoyNo";
            cmbKoy.DisplayMember = "KoyAdi";



            cmbBolge.DataSource = db.tbl_Bolgeler.Where(x => x.Aktiflik == true ).ToList();
            cmbBolge.ValueMember = "BolgeNo";
            cmbBolge.DisplayMember = "BolgeAdi";

            cmbDonem.DataSource = db.tbl_Donemler.Where(x => x.Aktiflik == true).ToList();
            cmbDonem.ValueMember = "DonemNo";
            cmbDonem.DisplayMember = "DonemAdi";

            
        }
        public void temizle()
        {
            cmbBolge.SelectedItem = null;
            cmbKategori.SelectedItem = null;
            cmbKoy.SelectedItem= null;
            cmbDonem.SelectedItem= null;
        }
        

        public formparaTutanak()
        {
            InitializeComponent();
        }

        private void formparaTutanak_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.tbl_Erzaklar' table. You can move, or remove it, as needed.
            



            cmbDataLoad();
            temizle();
            //TODO: This line of code loads data into the 'dataSet1.DataTable1' table. You can move, or remove it, as needed.
            /*this.dataSet1.EnforceConstraints = false;
           this.dataTable1TableAdapter.fillParaTutanagi(this.dataSet1.DataTable1);
            

            this.reportViewer1.RefreshReport();*/

            this.reportViewer1.RefreshReport();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (cmbKoy.SelectedItem != null)
            {
                
                this.dataSet1.EnforceConstraints = false;
               
                this.dataTable1TableAdapter.fillParaTutanagi(this.dataSet1.DataTable1, Convert.ToInt32(cmbBolge.SelectedValue), Convert.ToInt32(cmbKategori.SelectedValue), Convert.ToInt32(cmbKoy.SelectedValue),Convert.ToInt32(cmbDonem.SelectedValue));
                
                this.reportViewer1.RefreshReport();
                
            }
            else
            {
                
                this.dataSet1.EnforceConstraints = false;
               
                this.dataTable1TableAdapter.FillBy(this.dataSet1.DataTable1, Convert.ToInt32(cmbBolge.SelectedValue), Convert.ToInt32(cmbKategori.SelectedValue), Convert.ToInt32(cmbDonem.SelectedValue));
                this.reportViewer1.RefreshReport();
            }

            gunaAdvenceButton1.Enabled = true;

            /*this.dataSet1.EnforceConstraints = false;
            this.dataTable1TableAdapter.fillParaTutanagi(this.dataSet1.DataTable1, Convert.ToInt32(cmbBolge.SelectedValue),Convert.ToInt32(cmbKategori.SelectedValue),Convert.ToInt32(cmbKoy.SelectedValue) );
            this.reportViewer1.RefreshReport();*/
        }

        private void cmbBolge_SelectionChangeCommitted(object sender, EventArgs e)
        {
            gunaAdvenceButton1.Enabled = false;
        }

        private void cmbBolge_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                cmbBolge.SelectedItem = null;
                
                cmbKoy.SelectedItem = null;
                cmbBolge.SelectedItem = null;
                
                cmbKoy.SelectedItem = null;

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

        private void cmbKategori_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {


                cmbKategori.SelectedItem = null;
                cmbKategori.SelectedItem = null;
            }
        }

        private void cmbBolge_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            if (cmbBolge.SelectedItem != null)
            {
                var bolgeNo = Convert.ToInt32(cmbBolge.SelectedValue);

                var query = (from item in db.tbl_Koyler
                             where item.Bolge == bolgeNo
                             where item.Aktiflik == true
                             where item.Sec == true
                             orderby item.KoyAdi
                             select new
                             {
                                 item.KoyNo,
                                 item.KoyAdi,
                             }).ToList();
                cmbKoy.DataSource = query;
                cmbKoy.ValueMember = "KoyNo";
                cmbKoy.DisplayMember = "KoyAdi";
                gunaAdvenceButton1.Enabled = false;

            }
            else
            {


            }
            cmbKoy.SelectedItem = null;
        }

        
       
        

     

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (cmbBolge.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Bölge Seçiniz.");
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "Raporu yazdırmak istiyor musunuz?",
                    "Yazdırma Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    // Raporu PDF formatında oluştur
                    byte[] bytes = reportViewer1.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    // Geçici klasöre PDF dosyasını yaz
                    string pdfPath = Path.Combine(Path.GetTempPath(), "Rapor.pdf");
                    File.WriteAllBytes(pdfPath, bytes);

                    // PDF'yi varsayılan program ile yazdır
                    Process printProcess = new Process();
                    printProcess.StartInfo.FileName = pdfPath;
                    printProcess.StartInfo.Verb = "print";
                    printProcess.StartInfo.CreateNoWindow = true;
                    printProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    printProcess.Start();
                }
                else
                {
                    MessageBox.Show("Yazdırma işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cmbKoy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            gunaAdvenceButton1.Enabled = false;
        }

        private void cmbKategori_SelectionChangeCommitted(object sender, EventArgs e)
        {
            gunaAdvenceButton1.Enabled = false;
        }

        private void cmbDonem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            gunaAdvenceButton1.Enabled = false;
        }
    }
}
