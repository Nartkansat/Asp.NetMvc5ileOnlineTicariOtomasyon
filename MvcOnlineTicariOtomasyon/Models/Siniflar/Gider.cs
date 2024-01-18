using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Gider
    {
        [Key]
        public int Giderid { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }
        public Decimal Tutar { get; set; }
    }
}