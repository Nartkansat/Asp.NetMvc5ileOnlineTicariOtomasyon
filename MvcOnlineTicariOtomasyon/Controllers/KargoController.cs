using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var kargolar = from x in c.KargoDetays select x;

            //paramatre boş değilse ürünü getir.
            if (!string.IsNullOrEmpty(p))
            {
                //Ürün adında p içeren değeri ürünler değişkenine aktar
                kargolar = kargolar.Where(y => y.TakipKodu.Contains(p));
            }

            return View(kargolar.ToList());
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L"};
            int k1, k2, k3;
            k1 = rnd.Next(0, karakterler.Length); // 0,1,2,3 değerlerini alabilir
            k2 = rnd.Next(0, karakterler.Length);
            k3 = rnd.Next(0, karakterler.Length);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000); // 10 -> 3 1 2 1 2 1
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);

            string kod = s1.ToString() + karakterler[k1] + s2.ToString() + karakterler[k2] + s3.ToString() + karakterler[k3];
            ViewBag.takipkod = kod;

            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay p)
        {
            c.KargoDetays.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult KargoDurumEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KargoDurumEkle(KargoTakip p)
        {
            c.KargoTakips.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}