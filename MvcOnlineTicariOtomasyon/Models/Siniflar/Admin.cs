using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Admin
    {
        [Key]
        public int Adminid { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string KullaniciAd { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string Sifre { get; set; }

        [Column(TypeName = "char")]
        [StringLength(1)]
        public string Yetki { get; set; }
    }
}