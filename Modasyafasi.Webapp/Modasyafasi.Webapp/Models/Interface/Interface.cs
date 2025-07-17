using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modasayfasi.Model;
using Modasayfasi.Model.Entity;
using Modasayfasi.Model.Entity.vmmodel;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Modasyafasi.Webapp.Models.Interface
{
    public interface IFileUploadService
    {
        //Task<Class1> Class1(Class1 class1);
    }

    public interface IKullaniciService
    {
        Task<Kullanici> KullaniciKayit(Kullanici model);

        Task<KullaniciOkulBilgileri> MezuniyetKayit(KullaniciOkulBilgileri okulmodel);
        Task<List<CografyaKutuphanesi>> SehirGetir(int id);
        Task<List<CografyaKutuphanesi>> UlkeGetir();
        Task<List<ViewModelListele>> İletisimListele();
        Task<List<CografyaKutuphanesi>> KonumVeriAl();
        Task<bool> VeriSil(int id);
        Task<bool> SilinenOkulGuncelle(int id);
        Task<bool> SilinenOkulGuncelleDiv(int id);
        Task<Kullanici> KullaniciGuncelle(Kullanici model);
        Task<KullaniciOkulBilgileri> KullaniciOkulGuncelle(KullaniciOkulBilgileri model);
        Task<Kullanici> KullaniciGetir(int id);

        Task<List<Media>> MediaKayit(List<IFormFile> file, int id);
        Task<List<OkulMedyalar>> DiplomaKayit(List<IFormFile> Diploma, int okulmId, int OkulId);
        Task<List<Media>> MediaGuncelle(List<IFormFile> file, int id);
        Task<List<vmPrew>> MedyaGetir(int id);

        Task<List<vmOkullar>> OkulVeriGetir(int kullaniciId);
        //Login-Register
        Task<RegisterLogin> RegisterKullanici(RegisterLogin model);
        Task<RegisterLogin> LoginKullanici(RegisterLogin model);

    }
}
