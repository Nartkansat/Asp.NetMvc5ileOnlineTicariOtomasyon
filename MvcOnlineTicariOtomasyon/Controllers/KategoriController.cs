using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges(); //vt kaydet
            ViewBag.AlertMessage = "Kategori başarıyla eklendi.";
            ViewBag.RedirectUrl = Url.Action("Index", "Kategori"); // ControllerName'i kendi controller adınızla değiştirin
            return View();
        }
        [Authorize(Roles = "A")]
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            if (ktg == null)
            {
                // Kategori bulunamadı, isteğe bağlı olarak hata sayfasına yönlendirme yapabilirsiniz
                return HttpNotFound();
            }

            c.Kategoris.Remove(ktg);
            c.SaveChanges();

            TempData["AlertMessage"] = "Kategori başarılı bir şekilde silindi.";
            return RedirectToAction("Index", "Kategori");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);

            return View("KategoriGetir", kategori);
        }
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var kategori = c.Kategoris.Find(k.KategoriID);
            kategori.KategoriAd = k.KategoriAd;
            c.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}