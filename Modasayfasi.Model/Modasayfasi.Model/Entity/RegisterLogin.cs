using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modasayfasi.Model.Entity
{
    [Table("RegisterLogin")]
    public class RegisterLogin
    {
        [Key]
        public int Id { get; set; }
        
        public string KullaniciAdi { get; set; }
        public string KullaniciSifre { get; set; }
        public string Salt { get; set; }
    }
}
