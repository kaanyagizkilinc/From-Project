using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modasayfasi.Model.Entity
{
    public  class Media
    {
        [Key]
        public int Id { get; set; }
        public string MediaAdı { get; set; }
        public string MediaUrl { get; set; }
    }
}
