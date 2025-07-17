using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modasayfasi.Model.Entity
{
    public class Kullanici
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateOnly DogumTarihi { get; set; }
        public string Ulke { get; set; }
        public string Sehir { get; set; }
        public string Cinsiyet { get; set; }
        public string Aciklama { get; set; }
        public int SilindiMi { get; set; }

    }
}
