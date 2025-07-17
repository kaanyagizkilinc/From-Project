using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modasayfasi.Model.Entity.vmmodel
{

        public class OkulBilgiVM
        {
            public KullaniciOkulBilgileri OkulBilgisi { get; set; }
            public List<Media> Media { get; set; }
           public List<OkulMedyalar> OkulMedyalars { get; set; }
        }

        public class vmOkullar
        {
            public int OkulId { get; set; }
            public List<OkulBilgiVM> OkulBilgiVM { get; set; }
        }
}
