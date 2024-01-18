using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        Context c = new Context();

        //Statik olarak view kısmında chart oluşturma
        public ActionResult Index()
        {
            return View();
        }

        //Statik olarak Controllerda C# ile görsel olarak chart oluşturma
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori & Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler",
                xValue: new[] { "Beyaz Eşya", "Telefon", "Küçük Ev Aletleri", "Bilgisayar" },
                yValues: new[] { "250", "44", "500", "89" }
                ).Write();

            return File(grafikciz.ToWebImage().GetBytes(),"image/jpeg");
        }

        //Dinamik olarak Controllerda chart oluşturma, (VT kullanarak)
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();

            var sonuclar = c.Uruns.ToList();

            // her bir ürün için xvalue'nun içerisine ürün adlarını ekle
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            //stok
            sonuclar.ToList().ForEach(x => yvalue.Add(x.Stok));

            var grafik = new Chart(1000, 1000);
            grafik.AddTitle("Stoklar").
            AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue).Write();

            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        //Statik olarak Google chart oluşturma
        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult()
        {

            return Json(Urunlistesi(),JsonRequestBehavior.AllowGet);
        }
        public List<sinif1> Urunlistesi()
        {
            List<sinif1> snf = new List<sinif1>();
            snf.Add(new sinif1()
            {
                urunad = "Bilgisayar",
                stok = 120
            });
            snf.Add(new sinif1()
            {
                urunad = "Beyaz eşya",
                stok = 150
            });
            snf.Add(new sinif1()
            {
                urunad = "Mobilya",
                stok = 70
            });
            snf.Add(new sinif1()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 180
            });
            snf.Add(new sinif1()
            {
                urunad = "Mobil Cihazlar",
                stok = 90
            });

            return snf;
            
        }

        //Dinamik olarak Google chart oluşturma, (VT kullanarak) ASIL BÖLÜM.
        public ActionResult Index5()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        {

            return Json(Urunlistesi2(), JsonRequestBehavior.AllowGet);
        }

        public List<sinif2> Urunlistesi2()
        {
            List<sinif2> snf = new List<sinif2>();
            using( var c = new Context())
            {
                snf = c.Uruns.Select(x => new sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }
           
            return snf;

        }

        //Admin LTE
        public ActionResult Index6()
        {
            var deger1 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d1 = deger1;

            var deger2 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d2 = deger2;

            var deger3 = c.Uruns.Where(u => u.Urunid == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d3 = deger3;

            var deger4 = c.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d4 = deger4;


            // Bu sorgu için bir sinif oluşturmak gerekiyor yoksa hata veriyor. 
            var sorgu = from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());

        }
        public ActionResult GetPieChartData()
        {
            List<sinif2> snf = new List<sinif2>();
            using (var c = new Context())
            {
                snf = c.Uruns.Select(x => new sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }

            return Json(snf, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPieChartData2()
        {
            List<SinifGrup3> snf = new List<SinifGrup3>();
            using (var c = new Context())
            {
                snf = c.Uruns
                    .GroupBy(x => x.Marka)
                    .Select(g => new SinifGrup3
                    {
                        Marka = g.Key,
                        Sayi = g.Count()
                    })
                    .ToList();
            }

            return Json(snf, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Partial1()
        {
            var sorgu5 = from x in c.Uruns
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             Marka = g.Key,
                             Sayi = g.Count()
                         };

            return PartialView(sorgu5.ToList());
        }

    }
}