using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modasayfasi.Model.Entity
{
    public class OkulMedyalar
    {
        [Key]
        public int Id { get; set; }
        public int MedyaId { get; set; } // Okulmedya için Id eşleştirme
        public int OkulBilgiId { get; set; }
    }
}
