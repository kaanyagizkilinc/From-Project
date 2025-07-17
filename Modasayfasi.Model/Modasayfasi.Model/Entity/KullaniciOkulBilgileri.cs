using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modasayfasi.Model.Entity
{
    public class KullaniciOkulBilgileri
    {
        [Key]
        public int Id { get; set; }//Okulmedya için Id eşleştirme
        public int KullaniciId { get; set; }
        [Column("OkulTürü")]
        public int OkulTuru { get; set; }//Tür Lise, Üni falan 1-2
        public string OkulAdi { get; set; }
        public DateOnly MezuniyetYili { get; set; }

    }
}
