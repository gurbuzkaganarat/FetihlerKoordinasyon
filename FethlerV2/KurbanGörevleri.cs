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
    public partial class KurbanGörevleri : Form
    {
        public KurbanGörevleri()
        {
            InitializeComponent();
        }

        FetihlerV2Entities1 db = new FetihlerV2Entities1();

        public void List1(DataGridView data)
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        where d1.GorevliCinsiyet == "Erkek"
                        where d1.KurbanGörevi == null

                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Görevi = d1.KurbanGörevi
                        };
            data.DataSource = query.OrderBy(x => x.AdSoyad).ToList();
            data.Columns[0].Visible = false;
        }

        public void List2(DataGridView data)
        {
            var query = from d1 in db.tbl_Gorevliler
                        where d1.Aktiflik == true
                        where d1.Seç == true
                        where d1.GorevliCinsiyet == "Erkek"
                        where d1.KurbanGörevi != null

                        select new
                        {
                            No = d1.GorevliNo,
                            AdSoyad = d1.GorevliAd + " " + d1.GorevliSoyAd,
                            Görevi = d1.KurbanGörevi
                        };
            data.DataSource = query.OrderBy(x => x.AdSoyad).ToList();
            data.Columns[0].Visible = false;
        }

        public void erkekara1(DataGridView data3)
        {

            string gorevliAra = txtGorevliAra.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(gorevliAra))
            {
                var kelimeler = gorevliAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = db.tbl_Gorevliler
                    .Where(d1 => d1.Aktiflik == true && d1.Seç == true && d1.GorevliCinsiyet == "Erkek" && d1.KurbanGörevi == null )
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
                        Görevi = d1.KurbanGörevi
                    })
                    .ToList();

                data3.DataSource = query;
                data3.Columns[0].Visible = false;


            }

            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }
          


        }


        public void erkekara2(DataGridView data3)
        {

            string gorevliAra = txtGorevliAra.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(gorevliAra))
            {
                var kelimeler = gorevliAra.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var query = db.tbl_Gorevliler
                    .Where(d1 => d1.Aktiflik == true && d1.Seç == true && d1.GorevliCinsiyet == "Erkek" && d1.KurbanGörevi != null)
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
                        Görevi = d1.KurbanGörevi
                    })
                    .ToList();

                data3.DataSource = query;
                data3.Columns[0].Visible = false;
            }
            else
            {
                List1(bunifuCustomDataGrid1);
                List2(bunifuCustomDataGrid2);
            }



        }

        private void txtGorevliAra_TextChanged(object sender, EventArgs e)
        {
            erkekara1(bunifuCustomDataGrid1);
            erkekara2(bunifuCustomDataGrid2);
        }

        private void KurbanGörevleri_Load(object sender, EventArgs e)
        {
            erkekara1(bunifuCustomDataGrid1);
            erkekara2(bunifuCustomDataGrid2);
        }
    }
}
