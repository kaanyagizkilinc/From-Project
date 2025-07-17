using Microsoft.AspNetCore.Mvc;
using Modasayfasi.Model.Entity;
using Modasayfasi.Model.Entity.vmmodel;
using System.Threading.Tasks;

namespace Modasaydasi.WebApi.Model.Interface
{
    public interface IKullaniciKayit
    {
        Task<Kullanici> KullaniciKaydet(Kullanici model);

        Task<object> MezuniyetKaydet(KullaniciOkulBilgileri model);
        Task<List<CografyaKutuphanesi>> UlkeGetir();
        Task<List<CografyaKutuphanesi>> SehirleriGetir(int id);

        Task<List<ViewModelListele>> İletisimListele();

        Task<List<CografyaKutuphanesi>> KonumVeriAl();

        Task<bool> VeriSil(int id);
        Task<bool> SilinenOkulGuncelle(int id);

        Task<bool> SilinenOkulGuncelleDiv(int id);

        Task<Kullanici> KullaniciGuncelle(Kullanici model);
        Task<RegisterLogin> RegisterKullanici(RegisterLogin model);

        Task<RegisterLogin> LoginKullanici(RegisterLogin model);

        Task<Kullanici> KullaniciGetir(int id);
        Task<List<Media>> MedyaKayit(List<IFormFile> mediaFiles, int id);

        Task<List<Media>> DiplomaKayit([FromForm] List<IFormFile> Diploma, int kullaniciId, [FromForm] int OkulId);
        Task<List<Media>> MediaGuncelle(List<IFormFile> mediaFiles, int id);

        Task<List<vmPrew>> MedyaGetir(int id);

        Task<List<vmOkullar>> OkulVeriGetir(int OkulId);

        Task<KullaniciOkulBilgileri> KullaniciOkulGuncelle(KullaniciOkulBilgileri model);
    }
}
