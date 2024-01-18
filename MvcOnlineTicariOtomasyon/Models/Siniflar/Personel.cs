using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int Personelid { get; set; }

        [Display(Name ="Personel Adı")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string PersonelAd { get; set; }

        [Display(Name = "Personel Soyadı")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string PersonelSoyad { get; set; }

        [Display(Name = "Personel Görseli")]
        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string PersonelGorsel { get; set; }

        //
        public ICollection<SatisHareket> SatisHarekets { get; set; }

        // Bir personel sadece bir tane departmanda bulunabilir.
        public int Departmanid { get; set; }
        public virtual Departman Departman { get; set; }

        public bool Durum { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(14)]
        public string Telefon { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Adres { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Mail { get; set; }
    }
}