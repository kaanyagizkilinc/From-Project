using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modasayfasi.Model.Entity.vmmodel
{
    public class ViewModelListele
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateOnly DogumTarihi { get; set; }
        public string Ulke { get; set; }
        public string Sehir { get; set; }
        public string Cinsiyet { get; set; }
        public string Aciklama { get; set; }
        public int SilindiMi { get; set; }

        public List<Media> Medivm { get; set; }
    public List<KullaniciMedia> mediavmKullanici { get; set; }
    }
}
