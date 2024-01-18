using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cariler
    {
        [Key]
        public int Cariid { get; set; }

        [Display(Name = "Cari Adı")]
        [Column(TypeName = "varchar")]
        [StringLength(30,ErrorMessage ="En Fazla 30 Karakter Girilebilir.")]
        public string CariAd { get; set; }

        [Display(Name = "Cari Soyadı")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu alanı boş geçemezsiniz!")]
        public string CariSoyad { get; set; }

        [Display(Name = "Şehir")]
        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string CariSehir { get; set; }

        [Display(Name = "Mail")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string CariMail { get; set; }

        [Display(Name = "Şifre")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string CariSifre { get; set; }

        //
        public ICollection<SatisHareket> SatisHarekets { get; set; }

        public bool Durum { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(14)]
        public string Telefon { get; set; }
    }
}