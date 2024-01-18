using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Urun
    {
        [Key]
        public int Urunid { get; set; }

        [Display(Name = "Ürün Adı")]
        [Column (TypeName ="varchar")]   // Kısıtlama yapıyoruz VT aktarmak için. String Çok yer kaplar.
        [StringLength(30)]
        public string UrunAd { get; set; }

        [Display(Name = "Ürün Markası")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string Marka { get; set; }

        public short Stok { get; set; }

        [Display(Name = "Alış Fiyatı")]
        public decimal AlisFiyat { get; set; }

        [Display(Name = "Satış Fiyatı")]
        public decimal SatisFiyat { get; set; }

        public bool Durum { get; set; }

        [Display(Name = "Ürün Görseli")]
        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string UrunGorsel { get; set; }

        // ürünlere kategori eklediğimiz zaman, kategori tablosuna otomatik olarak arttırarak devam ediyor.
        // onun önün geçmek için kendimiz yeni prop. oluşturduk. 
        public int Kategoriid { get; set; }

        //Bir ürünün bir kategori karşılığı olabilir
        public virtual Kategori Kategori { get; set; } //virtual eklemek zorundayız. Ürünler kısmında kategorinin adına ulaşamıyorduk.

        //
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}