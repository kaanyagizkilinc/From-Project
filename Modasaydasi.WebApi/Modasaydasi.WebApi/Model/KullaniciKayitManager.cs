using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modasaydasi.WebApi.Model.Interface;
using Modasayfasi.Model.Entity;
using Modasayfasi.Model.Entity.vmmodel;
using System.Threading.Tasks;
using Modasaydasi.WebApi.Helper;

namespace Modasaydasi.WebApi.Model
{
    public class KullaniciKayitManager : IKullaniciKayit
    {
        private readonly ApplicationDbContext _context;

        public KullaniciKayitManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Kullanici> KullaniciKaydet(Kullanici model)
        {
            _context.Kullanici.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Kullanici> KullaniciGuncelle(Kullanici model)
        {
            var kullanici = await _context.Kullanici.FirstOrDefaultAsync(x => x.Id == model.Id);
            Console.WriteLine(kullanici);
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

        public async Task<List<CografyaKutuphanesi>> UlkeGetir()
        {
            try
            {
                var result = await _context.CografyaKutuphanesi.Where(x => x.Ustid == 0)
                    .OrderBy(x => x.Tanim)
                    .ToListAsync();
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

        public async Task<List<ViewModelListele>> İletisimListele()
        {
            try
            {
                var kullanicilar = await _context.Kullanici
                    .Where(x => x.SilindiMi == 0)
                    .ToListAsync();
                var viewModel = new List<ViewModelListele>();

                foreach (var kullanici in kullanicilar)
                {
                    var medyalar = await (
                     from km in _context.KullaniciMedia
                     join m in _context.Media on km.MediaId equals m.Id
                     where km.KullaniciId == kullanici.Id
                     select m
                    ).ToListAsync();

                    var mediavmKullanici = await _context.KullaniciMedia
                    .Where(km => km.KullaniciId == kullanici.Id)
                    .ToListAsync();


                    viewModel.Add(new ViewModelListele
                    {
                        Id = kullanici.Id,
                        Ad = kullanici.Ad,
                        Soyad = kullanici.Soyad,
                        DogumTarihi = kullanici.DogumTarihi,
                        Ulke = kullanici.Ulke,
                        Sehir = kullanici.Sehir,
                        Cinsiyet = kullanici.Cinsiyet,
                        Aciklama = kullanici.Aciklama,
                        SilindiMi = kullanici.SilindiMi,
                        Medivm = medyalar,
                        mediavmKullanici = mediavmKullanici
                    });
                }
                return viewModel;

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
                Console.WriteLine("Hata mesajı: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("İç hata: " + ex.InnerException.Message);
                throw new Exception($"Veri silme hatası: {ex.Message}");
            }
        }

        public async Task<List<Media>> DiplomaKayit([FromForm] List<IFormFile> Diploma, [FromForm] int kullaniciId, [FromForm] int OkulId)
        {
            var medyalar = new List<Media>();
            if (Diploma == null || Diploma.Count == 0)
                return null;

            var rootPath = @"C:\Users\yagiz\Desktop\KaydetmesqlProje1\Modasyafasi.Webapp\Modasyafasi.Webapp\wwwroot\MediaKutuphane";

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
                string sql = @"
                    DECLARE @MediaId INT
                    INSERT INTO Media (MediaAdı, MediaUrl)
                    VALUES (@MediaAdı, @MediaUrl);
                    SET @MediaId = SCOPE_IDENTITY();
                    INSERT INTO OkulMedyalar (OkulBilgiıd, MedyaId)
                    VALUES (@OkulBilgiId, @MediaId);";

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    _context.Database.OpenConnection();

                    var adParam = command.CreateParameter();
                    adParam.ParameterName = "@MediaAdı";
                    adParam.Value = dosyaAdi;
                    command.Parameters.Add(adParam);

                    var urlParam = command.CreateParameter();
                    urlParam.ParameterName = "@MediaUrl";
                    urlParam.Value = dosyaAdi + guid + uzanti;
                    command.Parameters.Add(urlParam);

                    var kullaniciIdParam = command.CreateParameter();
                    kullaniciIdParam.ParameterName = "@OkulBilgiıd";
                    kullaniciIdParam.Value = OkulId;
                    command.Parameters.Add(kullaniciIdParam);

                    await command.ExecuteNonQueryAsync();
                    _context.Database.CloseConnection();
                }

                medyalar.Add(new Media
                {
                    MediaAdı = dosyaAdi,
                    MediaUrl = "/MediaKutuphane/" + dosyaAdi + guid + uzanti,
                });
            }
            return medyalar;
        }



        //MEDİA
        public async Task<List<Media>> MedyaKayit(List<IFormFile> mediaFiles, int id)
        {
            try
            {
                List<Media> medyalar = new List<Media>();

                foreach (var file in mediaFiles)
                {
                    var rootPath = @"C:\Users\yagiz\Desktop\KaydetmesqlProje1\Modasyafasi.Webapp\Modasyafasi.Webapp\wwwroot\MediaKutuphane";
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

                    string sql = @"
                    DECLARE @MediaId INT
                    INSERT INTO Media (MediaAdı, MediaUrl)
                    VALUES (@MediaAdı, @MediaUrl);
                    SET @MediaId = SCOPE_IDENTITY();
                    INSERT INTO KullaniciMedia (KullaniciId, MediaId)
                    VALUES (@KullaniciId, @MediaId);";

                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = sql;
                        _context.Database.OpenConnection();

                        var adParam = command.CreateParameter();
                        adParam.ParameterName = "@MediaAdı";
                        adParam.Value = dosyaAdi;
                        command.Parameters.Add(adParam);

                        var urlParam = command.CreateParameter();
                        urlParam.ParameterName = "@MediaUrl";
                        urlParam.Value = dosyaAdi + guid + uzanti;
                        command.Parameters.Add(urlParam);

                        var kullaniciIdParam = command.CreateParameter();
                        kullaniciIdParam.ParameterName = "@KullaniciId";
                        kullaniciIdParam.Value = id;
                        command.Parameters.Add(kullaniciIdParam);

                        await command.ExecuteNonQueryAsync();
                        _context.Database.CloseConnection();
                    }

                    medyalar.Add(new Media
                    {
                        MediaAdı = dosyaAdi,
                        MediaUrl = "/MediaKutuphane/" + dosyaAdi + guid + uzanti,

                    });
                }

                return medyalar;
            }
            catch (Exception ex)
            {
                throw new Exception($"Medya kaydetme hatası: {ex.Message}");
            }
        }

        public async Task<List<Media>> MediaGuncelle(List<IFormFile> mediaFiles, int kullaniciId)
        {
            var sqlUPDATE = @"
            UPDATE m
            SET m.MediaAdı = @YeniMediaAdı, m.MediaUrl = @YeniMediaUrl
            OUTPUT INSERTED.Id, INSERTED.MediaAdı, INSERTED.MediaUrl
            FROM Media m
            INNER JOIN KullaniciMedia km ON m.Id = km.MediaId
            WHERE km.KullaniciId = @KullaniciId AND km.MediaId = @MediaId;
    ";

            var medyalar = new List<Media>();
            var rootPath = @"C:\Users\yagiz\Desktop\KaydetmesqlProje1\Modasyafasi.Webapp\Modasyafasi.Webapp\wwwroot\MediaKutuphane";
            var mediaIdList = await _context.KullaniciMedia
                                 .Where(km => km.KullaniciId == kullaniciId)
                                 .Select(km => km.MediaId)
                                 .ToListAsync();

            var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            try
            {
                for (int i = 0; i < mediaFiles.Count; i++)
                {
                    var file = mediaFiles[i];

                    if (i >= mediaIdList.Count)
                        break;

                    int mediaId = mediaIdList[i];

                    var guid = Guid.NewGuid().ToString();
                    var uzanti = Path.GetExtension(file.FileName);
                    var dosyaAdi = Path.GetFileNameWithoutExtension(file.FileName);
                    var yeniDosyaAdi = dosyaAdi + guid + uzanti;
                    var savePathApp = Path.Combine(rootPath, yeniDosyaAdi);

                    using (var stream = new FileStream(savePathApp, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    using var command = connection.CreateCommand();
                    command.CommandText = sqlUPDATE;

                    var adParam = command.CreateParameter();
                    adParam.ParameterName = "@YeniMediaAdı";
                    adParam.Value = dosyaAdi;
                    command.Parameters.Add(adParam);

                    var urlParam = command.CreateParameter();
                    urlParam.ParameterName = "@YeniMediaUrl";
                    urlParam.Value = yeniDosyaAdi;
                    command.Parameters.Add(urlParam);

                    var kullaniciIdParam = command.CreateParameter();
                    kullaniciIdParam.ParameterName = "@KullaniciId";
                    kullaniciIdParam.Value = kullaniciId;
                    command.Parameters.Add(kullaniciIdParam);

                    var mediaIdParam = command.CreateParameter();
                    mediaIdParam.ParameterName = "@MediaId";
                    mediaIdParam.Value = mediaId;
                    command.Parameters.Add(mediaIdParam);

                    using var reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        medyalar.Add(new Media
                        {
                            Id = reader.GetInt32(0),
                            MediaAdı = reader.GetString(1),
                            MediaUrl = reader.GetString(2),
                        });
                    }

                    await reader.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Medya güncelleme sırasında hata: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }

            return medyalar;
        }

        public async Task<object> MezuniyetKaydet(KullaniciOkulBilgileri model)
        {
            _context.KullaniciOkulBilgileri.Add(model);
            await _context.SaveChangesAsync();
            return new { id = model.Id, kullaniciId = model.KullaniciId };
        }

        public async Task<KullaniciOkulBilgileri> KullaniciOkulGuncelle(KullaniciOkulBilgileri model)
        {
            var okul = await _context.KullaniciOkulBilgileri.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (okul == null)
            {
                model.Id = 0;
                _context.KullaniciOkulBilgileri.Add(model);
                await _context.SaveChangesAsync();
                return model;
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
                    var okulmedyalar = await _context.OkulMedyalar.Where(om => om.OkulBilgiId == okul.Id).ToListAsync();
                    var medyaList = new List<Media>();
                    foreach( var media in okulmedyalar)
                    {
                        var medya = await _context.Media.FirstOrDefaultAsync(m => m.Id == media.MedyaId);
                        if(medya != null)
                            medyaList.Add(medya);
                    }
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
                        OkulBilgiVM = okulBilgiVMList
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Okul verisi getirme hatası: {ex.Message}");
            }
        }


        public async Task<List<vmPrew>> MedyaGetir(int id)
        {
            var Media = new List<vmPrew>();
            try
            {
                var medialist = await _context.KullaniciMedia
                    .Where(km => km.KullaniciId == id)
                    .Join(_context.Media, km => km.MediaId, m => m.Id, (km, m) => new vmPrew
                    {
                        Id = km.KullaniciId,
                        MediaId = km.MediaId,
                        Media = new Media
                        {
                            MediaUrl = m.MediaUrl,
                            MediaAdı = m.MediaAdı,
                        }
                    })
                    .ToListAsync();

                return medialist;
            }
            catch (Exception ex)
            {
                throw new Exception($"Medya getirme hatası: {ex.Message}");
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

        public async Task<RegisterLogin> RegisterKullanici(RegisterLogin model)
        {
            string salt = Helper.Hash_SaltHelper.HelperSalt();
            string hashPass = Helper.Hash_SaltHelper.HelperHash(model.KullaniciSifre, salt); 
            var kullanici = new RegisterLogin
            {
                KullaniciAdi = model.KullaniciAdi,
                KullaniciSifre = hashPass,
                Salt = salt,
            };

            _context.RegisterLogin.Add(kullanici);
            await _context.SaveChangesAsync();

            return kullanici;
        }

        public async Task<RegisterLogin> LoginKullanici(RegisterLogin model)
        {
            var Salt = await _context.RegisterLogin.Where(x => x.KullaniciAdi == model.KullaniciAdi)
                .Select(x => x.Salt).FirstOrDefaultAsync();
            var kullanici = await _context.RegisterLogin
                .FirstOrDefaultAsync(x => x.KullaniciAdi == model.KullaniciAdi);
            if (kullanici == null)
            {
                return null;
            }

            string HashGirilenPass = Helper.Hash_SaltHelper.HelperHash(model.KullaniciSifre, Salt);

            if( HashGirilenPass == kullanici.KullaniciSifre)
            {
                return kullanici;
            }
            return null;
        }
    }
}
