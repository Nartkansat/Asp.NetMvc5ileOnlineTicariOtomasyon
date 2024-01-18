using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            //ürünleri seç
            var urunler = from x in c.Uruns select x; 

            //paramatre boş değilse ürünü getir.
            if(!string.IsNullOrEmpty(p))
            {
                //Ürün adında p içeren değeri ürünler değişkenine aktar
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }

            return View(urunler.ToList());
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult YeniUrun()
        {
            //drop down yapısı için. Text kısmı görününen kısım, Value kısmı işlemi tabi olan.
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "A")]
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var urun = c.Uruns.Find(id);

            return View("UrunGetir", urun);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urn = c.Uruns.Find(p.Urunid);
            urn.UrunAd = p.UrunAd;
            urn.Marka = p.Marka;
            urn.Stok = p.Stok;
            urn.AlisFiyat = p.AlisFiyat;
            urn.SatisFiyat = p.SatisFiyat;
            urn.Durum = p.Durum;
            urn.Kategoriid = p.Kategoriid;
            urn.UrunGorsel = p.UrunGorsel;

            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();

            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();
            
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;



            var deger3 = c.Uruns.Find(id);
            ViewBag.dgr3 = deger3.Urunid;
            ViewBag.dgr4 = deger3.SatisFiyat;

            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket s)
        {
            //Satış yapabilmek için stok sayısı kontrol edilmelidir ve satış yaptıkça düşmelidir.
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
        }

    }
}