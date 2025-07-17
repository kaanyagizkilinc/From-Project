using System;
using System.Threading.Tasks;
using Modasayfasi.Model.Entity;
using Modasaydasi.WebApi.Model.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Modasaydasi.WebApi.Model;
using Microsoft.EntityFrameworkCore;
using Modasayfasi.Model.Entity.vmmodel;

namespace Modasaydasi.WebApi.Service
{
    public class KullaniciKayitService : IKullaniciKayit
    {
        private readonly ApplicationDbContext _context;
        public KullaniciKayitService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RegisterLogin> LoginKullanici(RegisterLogin model)
        {
            var kullanici = await _context.RegisterLogin
                .FirstOrDefaultAsync(x => x.KullaniciAdi == model.KullaniciAdi && x.KullaniciSifre == model.KullaniciSifre);
            if (kullanici == null)
            {
                throw new Exception("Kullanıcı adı veya şifre yanlış.");
            }
            return null;
        }
        public async Task<RegisterLogin> RegisterKullanici(RegisterLogin model)
        {
            var kullanici = new RegisterLogin
            {
                KullaniciAdi = model.KullaniciAdi,
                KullaniciSifre = model.KullaniciSifre
            };

            _context.RegisterLogin.Add(kullanici);
            _context.SaveChangesAsync();

            return kullanici;
        }
        public async Task<bool> SilinenOkulGuncelleDiv(int id)
        {
            try
            {
                var Km = await _context.KullaniciOkulBilgileri.FirstOrDefaultAsync(x => x.Id == id);

                _context.KullaniciOkulBilgileri.Remove(Km);
                _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SilinenOkulGuncelle(int id)
        {
            try
            {
                var okulmedya = await _context.OkulMedyalar.FirstOrDefaultAsync(x => x.MedyaId == id);
                var media = await _context.Media.FirstOrDefaultAsync(x => x.Id == id);

                _context.OkulMedyalar.Remove(okulmedya);
                _context.Media.Remove(media);
                _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<KullaniciOkulBilgileri> KullaniciOkulGuncelle(KullaniciOkulBilgileri model)
        {
            var okul = await _context.KullaniciOkulBilgileri.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (okul == null)
            {
                throw new Exception("Kullanıcı okul bilgisi bulunamadı.");
            }
            okul.OkulTuru = model.OkulTuru;
            okul.OkulAdi = model.OkulAdi;
            okul.MezuniyetYili = model.MezuniyetYili;
            _context.KullaniciOkulBilgileri.Update(okul);
            await _context.SaveChangesAsync();
            return okul;
        }
        public async Task<List<vmOkullar>> OkulVeriGetir(int kullaniciId)
        {
            try
            {
                var okulbilgileri = await _context.KullaniciOkulBilgileri
                    .Where(x => x.KullaniciId == kullaniciId)
                    .ToListAsync();
                var okulBilgiVMList = new List<OkulBilgiVM>();

                foreach (var okul in okulbilgileri)
                {
                    var medyaIdlist = await _context.OkulMedyalar.Where(m => m.OkulBilgiId == kullaniciId)
                        .Select(m => m.MedyaId)
                        .ToListAsync();

                    var medyaList = await _context.Media
                        .Where(m => medyaIdlist.Contains(m.Id))
                        .ToListAsync();

                    okulBilgiVMList.Add(new OkulBilgiVM
                    {
                        OkulBilgisi = okul,
                        Media = medyaList
                    });
                }

                return new List<vmOkullar>
                {
                    new vmOkullar
                    {
                        OkulId = kullaniciId,
                        OkulBilgiVM = okulBilgiVMList
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Okul verisi getirme hatası: {ex.Message}");
            }
        }
        public async Task<Kullanici> KullaniciKaydet(Kullanici model)
        {
            _context.Kullanici.Add(model);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<Kullanici> KullaniciGuncelle(Kullanici model)
        {
            var kullanici = await _context.Kullanici.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (kullanici == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
            kullanici.Ad = model.Ad;
            kullanici.Soyad = model.Soyad;
            kullanici.DogumTarihi = model.DogumTarihi;
            kullanici.Ulke = model.Ulke;
            kullanici.Sehir = model.Sehir;
            kullanici.Cinsiyet = model.Cinsiyet;
            kullanici.Aciklama = model.Aciklama;
            _context.Kullanici.Update(kullanici);
            await _context.SaveChangesAsync();
            return kullanici;
        }
        public async Task<object> MezuniyetKaydet(KullaniciOkulBilgileri model)
        {
            _context.KullaniciOkulBilgileri.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<List<vmPrew>> MedyaGetir(int id)
        {
            var Media = new List<vmPrew>();
            try
            {
                var medialist = await _context.KullaniciMedia.Where(km => km.KullaniciId == id)
                    .Select(km => new vmPrew
                    {
                        Id = km.KullaniciId,
                        

                    })
                    .ToListAsync();

                return medialist;
            }
            catch (Exception ex)
            {
                throw new Exception($"Medya getirme hatası: {ex.Message}");
            }
        }

        public async Task<List<Media>> DiplomaKayit([FromForm] List<IFormFile> Diploma, [FromForm] int OkulmId, [FromForm] int OkulId)
        {
            if (Diploma == null || Diploma.Count == 0)
                return null;

            var rootPath = @"C:\Users\yagiz\Desktop\KaydetmesqlProje1\Modasyafasi.Webapp\Modasyafasi.Webapp\wwwroot\MediaKutuphane";
            var kayitlar = new List<OkulMedyalar>();

            foreach (var file in Diploma)
            {
                var guid = Guid.NewGuid().ToString();
                var uzanti = Path.GetExtension(file.FileName);
                var dosyaAdi = Path.GetFileNameWithoutExtension(file.FileName);
                var savePathApp = Path.Combine(rootPath, dosyaAdi + guid + uzanti);

                using (var stream = new FileStream(savePathApp, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var medya = new OkulMedyalar
                {   
                };
                _context.OkulMedyalar.Add(medya);
                kayitlar.Add(medya);
            }
            await _context.SaveChangesAsync();

            return null;
        }





        public async Task<List<CografyaKutuphanesi>> UlkeGetir()
        {
            try
            {
                var result = await _context.CografyaKutuphanesi.Where(x => x.Ustid == 0) // Corrected comparison to match string type
                    .OrderBy(x => x.Tanim)
                    .ToListAsync();
                Console.WriteLine("Ülkeler: " + result);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Cografya kutuphanesi getirme hatası: {ex.Message}");
            }
        }

        public async Task<List<CografyaKutuphanesi>> SehirleriGetir(int id)
        {
            try
            {
                var result = await _context.CografyaKutuphanesi
                    .Where(x => x.Ustid == id)
                    .OrderBy(x => x.Tanim)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception($"Sehirleri getirme hatası: {ex.Message}");
            }
        }
        public async Task<Kullanici> KullaniciGetir(int id)
        {
            var result = await _context.Kullanici.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<List<Media>> MedyaKayit(List<IFormFile> mediaFiles, int id)
        {
            try
            {
                List<Media> medyalar = new List<Media>();

                foreach (var file in mediaFiles)
                {
                    var guid = Guid.NewGuid().ToString();
                    var uzanti = Path.GetExtension(file.FileName);
                    var dosyaAdi = Path.GetFileName(file.FileName);
                    var savePath = Path.Combine("Media", guid + uzanti);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    Media medyakayit = new Media
                    {
                        MediaAdı = dosyaAdi,
                        MediaUrl = guid + uzanti
                    };

                    _context.Media.Add(medyakayit);
                    await _context.SaveChangesAsync();

                    var kMedya = new KullaniciMedia
                    {
                        KullaniciId = id,
                        MediaId = medyakayit.Id
                    };
                    _context.KullaniciMedia.Add(kMedya);
                    medyalar.Add(medyakayit);
                }

                await _context.SaveChangesAsync();
                return medyalar;
            }
            catch (Exception ex)
            {
                throw new Exception($"Medya kaydetme hatası: {ex.Message}");
            }
        }
        public async Task<List<ViewModelListele>> İletisimListele()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"İletişim listeleme hatası: {ex.Message}");
            }
        }

        public async Task<List<CografyaKutuphanesi>> KonumVeriAl()
        {
            try
            {
                var result = await _context.CografyaKutuphanesi.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Konum verisi alma hatası: {ex.Message}");
            }
        }

        public async Task<bool> VeriSil(int id)
        {
            try
            {
                var veri = await _context.Kullanici.FindAsync(id);
                if (veri == null)
                {
                    return false;
                }
                veri.SilindiMi = 1;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Veri silme hatası: {ex.Message}");
            }
        }
        public async Task<List<Media>> MediaGuncelle(List<IFormFile> mediaFiles, int id)
        {
            var sqlUPDATE = @"UPDATE m
                SET m.MediaAdı = @YeniMediaAdı, m.MediaUrl = @YeniMediaUrl
                FROM Media m
                INNER JOIN KullaniciMedia km ON m.Id = km.MediaId
                WHERE km.KullaniciId = @KullaniciId;";
            List<Media> medyalar = new List<Media>();
            var rootPath = @"C:\Users\yagiz\Desktop\KaydetmesqlProje1\Modasyafasi.Webapp\Modasyafasi.Webapp\wwwroot\MediaKutuphane";
            try
            {
                foreach (var file in mediaFiles)
                {

                    var guid = Guid.NewGuid().ToString();
                    var uzanti = Path.GetExtension(file.FileName);
                    var dosyaAdi = Path.GetFileNameWithoutExtension(file.FileName);
                    var savePath = Path.Combine("Media", guid + uzanti);
                    var savePathApp = Path.Combine(rootPath, dosyaAdi + guid + uzanti);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    using (var stream = new FileStream(savePathApp, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var connection = _context.Database.GetDbConnection();
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }
                    using var command = connection.CreateCommand();

                    command.CommandText = sqlUPDATE;

                    var adParam = command.CreateParameter();
                    adParam.ParameterName = "@YeniMediaAdı";
                    adParam.Value = dosyaAdi;

                    var urlParam = command.CreateParameter();
                    urlParam.ParameterName = "@YeniMediaUrl";
                    urlParam.Value = dosyaAdi + guid + uzanti;

                    var kullaniciIdParam = command.CreateParameter();
                    kullaniciIdParam.ParameterName = "@KullaniciId";
                    kullaniciIdParam.Value = id;
                    command.Parameters.Add(adParam);

                    int affectedRows = await command.ExecuteNonQueryAsync();

                    if (affectedRows > 0)
                    {
                        medyalar.Add(new Media
                        {
                            MediaAdı = dosyaAdi,
                            MediaUrl = "/MediaKutuphane/" + dosyaAdi + guid + uzanti
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Medya güncelleme sırasında hata: " + ex.Message);
            }

            return medyalar;
        }
    }
}
