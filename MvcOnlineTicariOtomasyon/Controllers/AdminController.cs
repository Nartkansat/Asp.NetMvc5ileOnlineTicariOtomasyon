using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Context c = new Context();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            p.Yetki = "B";
            c.Admins.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult AdminGetir()
        {
            var admin = c.Admins.ToList();
            return View("AdminGetir", admin);
        }
        [Authorize(Roles = "A")]
        public ActionResult AdminGuncelle(int id)
        {
            var admin = c.Admins.Find(id);

            return View("AdminGuncelle", admin);
        }
        public ActionResult AdminGuncelle2(Admin p)
        {
            var admin = c.Admins.Find(p.Adminid);
            admin.Yetki = p.Yetki;
            c.SaveChanges();
            return RedirectToAction("AdminGetir");
        }
        public ActionResult AdminSettings()
        {
            string kullaniciadi = (string)Session["KullaniciAd"];
            var deger = c.Admins.FirstOrDefault(x => x.KullaniciAd == kullaniciadi);
            return View(deger);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string newPassword)
        {
            string kullaniciadi = (string)Session["KullaniciAd"];
            var admin = c.Admins.FirstOrDefault(x => x.KullaniciAd == kullaniciadi);

            if (admin != null)
            {
                // Yeni şifreyi güncelle
                admin.Sifre = newPassword;

                // Veritabanına kaydet
                c.SaveChanges();

                // Şifre değiştirme işlemi tamamlandıktan sonra AdminSettings eylemine yönlendir
                return RedirectToAction("AdminSettings");
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}