using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FethlerV2
{
    public partial class formGorevliSec : Form
    {
        public formGorevliSec()
        {
            InitializeComponent();
        }

        FetihlerV2Entities1 db = new FetihlerV2Entities1();


        public void ara1 (DataGridView data3)
        {

            string gorevliAra = txtGorevliAd.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(gorevliAra))
            {
                var kelimeler = gorevliAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = db.tbl_Gorevliler
                    .Where(d1 => d1.Aktiflik == true && d1.Seç == false )
                    .ToList() // Belleğe çekiyoruz çünkü aşağıdaki işlemler EF tarafında çalışmaz
                    .Where(d1 =>
                    {
                        string fullName = (d1.GorevliAd + " " + d1.GorevliSoyAd).ToLower();
                        // Kelimeler sırasız bir şekilde ad ve soyad içinde bulunmalı
                        return kelimeler.All(kelime => fullName.Contains(kelime));
                    })
                    .Select(d1 => new
                    {
                        No = d1.GorevliNo,
                        AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                        Cinsiyet = d1.GorevliCinsiyet,
                    })
                    .ToList();

                data3.DataSource = query;
                data3.Columns[1].Visible = false;
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }


        }

        public void erkekara1(DataGridView data3)
        {

            string gorevliAra = txtGorevliAd.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(gorevliAra))
            {
                var kelimeler = gorevliAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = db.tbl_Gorevliler
                    .Where(d1 => d1.Aktiflik == true && d1.Seç == false && d1.GorevliCinsiyet == "Erkek")
                    .ToList() // Belleğe çekiyoruz çünkü aşağıdaki işlemler EF tarafında çalışmaz
                    .Where(d1 =>
                    {
                        string fullName = (d1.GorevliAd + " " + d1.GorevliSoyAd).ToLower();
                        // Kelimeler sırasız bir şekilde ad ve soyad içinde bulunmalı
                        return kelimeler.All(kelime => fullName.Contains(kelime));
                    })
                    .Select(d1 => new
                    {
                        No = d1.GorevliNo,
                        AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                        Cinsiyet = d1.GorevliCinsiyet,
                    })
                    .ToList();

                data3.DataSource = query;
                data3.Columns[1].Visible = false;
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }


        }

        public void erkekara2(DataGridView data4)
        {
            string gorevliAra = txtGorevliAd.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(gorevliAra))
            {
                var kelimeler = gorevliAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = db.tbl_Gorevliler
                    .Where(d1 => d1.Aktiflik == true && d1.Seç == true && d1.GorevliCinsiyet == "Erkek")
                    .ToList() // Belleğe çekiyoruz çünkü aşağıdaki işlemler EF tarafında çalışmaz
                    .Where(d1 =>
                    {
                        string fullName = (d1.GorevliAd + " " + d1.GorevliSoyAd).ToLower();
                        // Kelimeler sırasız bir şekilde ad ve soyad içinde bulunmalı
                        return kelimeler.All(kelime => fullName.Contains(kelime));
                    })
                    .Select(d1 => new
                    {
                        No = d1.GorevliNo,
                        AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                        Cinsiyet = d1.GorevliCinsiyet,
                    })
                    .ToList();

                data4.DataSource = query;
                data4.Columns[1].Visible = false;
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }

        }


        public void kadınara1(DataGridView data3)
        {

            string gorevliAra = txtGorevliAd.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(gorevliAra))
            {
                var kelimeler = gorevliAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = db.tbl_Gorevliler
                    .Where(d1 => d1.Aktiflik == true && d1.Seç == false && d1.GorevliCinsiyet == "Kadın")
                    .ToList()
                    .Where(d1 =>
                    {
                        string fullName = (d1.GorevliAd + " " + d1.GorevliSoyAd).ToLower();
                        // Kelimeler sırasız bir şekilde ad ve soyad içinde bulunmalı
                        return kelimeler.All(kelime => fullName.Contains(kelime));
                    })
                    .Select(d1 => new
                    {
                        No = d1.GorevliNo,
                        AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                        Cinsiyet = d1.GorevliCinsiyet,
                    })
                    .ToList();

                data3.DataSource = query;
                data3.Columns[1].Visible = false;
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }


        }

        public void kadınara2(DataGridView data4)
        {
            string gorevliAra = txtGorevliAd.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(gorevliAra))
            {
                var kelimeler = gorevliAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = db.tbl_Gorevliler
                    .Where(d1 => d1.Aktiflik == true && d1.Seç == true && d1.GorevliCinsiyet == "Kadın")
                    .ToList()
                    .Where(d1 =>
                    {
                        string fullName = (d1.GorevliAd + " " + d1.GorevliSoyAd).ToLower();
                        // Kelimeler sırasız bir şekilde ad ve soyad içinde bulunmalı
                        return kelimeler.All(kelime => fullName.Contains(kelime));
                    })
                    .Select(d1 => new
                    {
                        No = d1.GorevliNo,
                        AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                        Cinsiyet = d1.GorevliCinsiyet,
                    })
                    .ToList();

                data4.DataSource = query;
                data4.Columns[1].Visible = false;
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }



        }



        public void ara2(DataGridView data4)
        {
            string gorevliAra = txtGorevliAd.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(gorevliAra))
            {
                var kelimeler = gorevliAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = db.tbl_Gorevliler
                    .Where(d1 => d1.Aktiflik == true && d1.Seç == true )
                    .ToList() // Belleğe çekiyoruz çünkü aşağıdaki işlemler EF tarafında çalışmaz
                    .Where(d1 =>
                    {
                        string fullName = (d1.GorevliAd + " " + d1.GorevliSoyAd).ToLower();
                        // Kelimeler sırasız bir şekilde ad ve soyad içinde bulunmalı
                        return kelimeler.All(kelime => fullName.Contains(kelime));
                    })
                    .Select(d1 => new
                    {
                        No = d1.GorevliNo,
                        AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                        Cinsiyet = d1.GorevliCinsiyet,
                    })
                    .ToList();

                data4.DataSource = query;
                data4.Columns[1].Visible = false;
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }


        }
        public void List1(DataGridView data)
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == false
                        
                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                        };
            data.DataSource = query.OrderBy(x => x.AdSoyad).ToList();
            data.Columns[1].Visible = false;

        }

        public void ListErkek1(DataGridView data)
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == false
                        where d1.GorevliCinsiyet == "Erkek"

                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                        };
            data.DataSource = query.OrderBy(x => x.AdSoyad).ToList();
            data.Columns[1].Visible = false;

        }

        public void ListErkek2(DataGridView data)
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        where d1.GorevliCinsiyet == "Erkek"

                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                        };
            data.DataSource = query.OrderBy(x => x.AdSoyad).ToList();
            data.Columns[1].Visible = false;

        }


        public void ListKadın1(DataGridView data)
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == false
                        where d1.GorevliCinsiyet == "Kadın"

                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                        };
            data.DataSource = query.OrderBy(x => x.AdSoyad).ToList();
            data.Columns[1].Visible = false;

        }

        public void ListKadın2(DataGridView data)
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        where d1.GorevliCinsiyet == "Kadın"

                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                        };
            data.DataSource = query.OrderBy(x => x.AdSoyad).ToList();
            data.Columns[1].Visible = false;

        }

        public void List2(DataGridView data2)
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                        };
            data2.DataSource = query.OrderBy(x => x.AdSoyad).ToList();
            data2.Columns[1].Visible = false;

        }


        public void toplamGorevli()
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        
                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                            toplam=db.tbl_Gorevliler.Count(x=> x.Aktiflik ==true)
                        };

            var toplam = (from d1 in query select (int?) d1.toplam).Count();
            lblToplamGorevli.Text = toplam.ToString();
            
        }

        public void secilenGorevli()
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç==true

                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Cinsiyet = d1.GorevliCinsiyet,
                            toplam = db.tbl_Gorevliler.Count(x => x.Aktiflik == true && x.Seç==true)
                        };

            var toplam = (from d1 in query select (int?)d1.toplam).Count();
            lblSecilenGörevli.Text = toplam.ToString();
        }


        private void formGorevliSec_Load(object sender, EventArgs e)
        {
            toplamGorevli();
            secilenGorevli();
            ara1(bunifuCustomDataGrid1);
            ara2(bunifuCustomDataGrid2);

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblGorevliNo.Text = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                int gorevliNo = Convert.ToInt32(lblGorevliNo.Text);
                var g = db.tbl_Gorevliler.Find(gorevliNo);
                g.Seç = true;
                db.SaveChanges();

                ara1(bunifuCustomDataGrid1);
                ara2(bunifuCustomDataGrid2);
                toplamGorevli();
                secilenGorevli();
            }
            catch
            {


            }
        }

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblGorevliNo.Text = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[1].Value?.ToString();
                int gorevliNo = Convert.ToInt32(lblGorevliNo.Text);
                var g = db.tbl_Gorevliler.Find(gorevliNo);
                g.Seç = false;

                db.SaveChanges();
                ara1(bunifuCustomDataGrid1);
                ara2(bunifuCustomDataGrid2);

                toplamGorevli();
                secilenGorevli();
            }
            catch
            {


            }
        }

        private void txtGorevliAd_TextChanged(object sender, EventArgs e)
        {
            if ( chckErkek.Checked== false && chckKadın.Checked == false)
            {
                ara1(bunifuCustomDataGrid1);
                ara2(bunifuCustomDataGrid2);
            }

            if (chckErkek.Checked == true && chckKadın.Checked == false)
            {
                erkekara1(bunifuCustomDataGrid1);
                erkekara2(bunifuCustomDataGrid2);
            }

            if (chckErkek.Checked == false && chckKadın.Checked == true)
            {
                kadınara1(bunifuCustomDataGrid1);
                kadınara2(bunifuCustomDataGrid2);
            }



        }

        private void chckKadın_CheckedChanged(object sender, EventArgs e)
        {
            if (chckKadın.Checked == true)
            {
                chckErkek.Checked = false;
                ListKadın1(bunifuCustomDataGrid1);
                ListKadın2(bunifuCustomDataGrid2);
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }


        }

       

        private void chckErkek_CheckedChanged(object sender, EventArgs e)
        {
            if (chckErkek.Checked==true)
            {
                chckKadın.Checked = false;
                ListErkek1(bunifuCustomDataGrid1);
                ListErkek2(bunifuCustomDataGrid2);
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);

            }

        }

        private void btnallın_MouseClick(object sender, MouseEventArgs e)
        {

            // Önce tüm görevlileri al
            var gorevliList = db.tbl_Gorevliler.ToList();

            // Tüm Seç alanlarını önce sıfırla (hiçbiri seçili değil varsayımıyla)
            

            // Seçimlere göre filtreleme yap ve uygun olanları işaretle
            if (chckErkek.Checked && !chckKadın.Checked)
            {
                foreach (var g in gorevliList.Where(x => x.GorevliCinsiyet == "Erkek"))
                {
                    g.Seç = true;
                }
            }
            else if (!chckErkek.Checked && chckKadın.Checked)
            {
                foreach (var g in gorevliList.Where(x => x.GorevliCinsiyet == "Kadın"))
                {
                    g.Seç = true;
                }
            }
            else if (chckErkek.Checked && chckKadın.Checked)
            {
                foreach (var g in gorevliList)
                {
                    g.Seç = true;
                }
            }

            else if (!chckErkek.Checked && !chckKadın.Checked)
            {
                foreach (var g in gorevliList)
                {
                    g.Seç = true;
                }
            }

            // Güncel filtrelenmiş listeyle grid'leri güncelle
            ara1(bunifuCustomDataGrid1);
            ara2(bunifuCustomDataGrid2);

            // Toplam ve seçilen sayısını güncelle
            toplamGorevli();
            secilenGorevli();
            if (chckErkek.Checked == true)
            {
                chckKadın.Checked = false;
                ListErkek1(bunifuCustomDataGrid1);
                ListErkek2(bunifuCustomDataGrid2);
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);

            }

            if (chckKadın.Checked == true)
            {
                chckErkek.Checked = false;
                ListKadın1(bunifuCustomDataGrid1);
                ListKadın2(bunifuCustomDataGrid2);
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }

            // Değişiklikleri kaydet
            db.SaveChanges();

        }

        private void btnallout_MouseClick(object sender, MouseEventArgs e)
        {    // Önce tüm görevlileri al
            var gorevliList = db.tbl_Gorevliler.ToList();

            // Tüm Seç alanlarını önce sıfırla (hiçbiri seçili değil varsayımıyla)


            // Seçimlere göre filtreleme yap ve uygun olanları işaretle
            if (chckErkek.Checked && !chckKadın.Checked)
            {
                foreach (var g in gorevliList.Where(x => x.GorevliCinsiyet == "Erkek"))
                {
                    g.Seç = false;
                }
            }
            else if (!chckErkek.Checked && chckKadın.Checked)
            {
                foreach (var g in gorevliList.Where(x => x.GorevliCinsiyet == "Kadın"))
                {
                    g.Seç = false;
                }
            }
            else if (chckErkek.Checked && chckKadın.Checked)
            {
                foreach (var g in gorevliList)
                {
                    g.Seç = false;
                }
            }

            else if (!chckErkek.Checked && !chckKadın.Checked)
            {
                foreach (var g in gorevliList)
                {
                    g.Seç = false;
                }
            }
            ara1(bunifuCustomDataGrid1);
            ara2(bunifuCustomDataGrid2);

            if (chckErkek.Checked == true)
            {
                chckKadın.Checked = false;
                ListErkek1(bunifuCustomDataGrid1);
                ListErkek2(bunifuCustomDataGrid2);
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);

            }

            if (chckKadın.Checked == true)
            {
                chckErkek.Checked = false;
                ListKadın1(bunifuCustomDataGrid1);
                ListKadın2(bunifuCustomDataGrid2);
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }

            toplamGorevli();
            secilenGorevli();
            // Değişiklikleri kaydet
            db.SaveChanges();
        }
    }
}
