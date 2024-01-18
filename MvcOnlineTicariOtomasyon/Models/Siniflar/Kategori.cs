using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Kategori //vt tablosu aslında
    {
        // Property sütunlar

        [Key]
        public int KategoriID { get; set; }

        [Display(Name = "Kategori Adı")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string KategoriAd { get; set; }

        // Her Kategorinin içerisinde bir den fazla ürün vardır. // İlişkisel tablo. // 1'den Çoğa
        public ICollection<Urun> Uruns { get; set; }
    }
}