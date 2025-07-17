using Microsoft.AspNetCore.Mvc;
using Modasaydasi.WebApi.Migrations;
using Modasaydasi.WebApi.Model.Interface;
using Modasayfasi.Model.Entity;
using Modasayfasi.Model.Entity.vmmodel;

[Route("api/[controller]")]
[ApiController]
public class KullaniciKaydetController : ControllerBase
{
    private readonly IKullaniciKayit _IkullaniciKayitService;

    public KullaniciKaydetController(IKullaniciKayit kullaniciKayitService)
    {
        _IkullaniciKayitService = kullaniciKayitService;
    }
    [Route("UlkelerGetir")]
    [HttpGet]
    public async Task<List<CografyaKutuphanesi>> UlkelerGetir()
    {
        try
        {
            var result = await _IkullaniciKayitService.UlkeGetir();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Cografya kutuphanesi getirme hatası: {ex.Message}");
        }
    }
    [HttpGet("KullaniciGetir")]
    public async Task<Kullanici> KullaniciGetir(int id)
    {
        try
        {
            var result = await _IkullaniciKayitService.KullaniciGetir(id);
            return result;
        }
        catch
        {
            return null;
        }
    }


    [HttpGet("SehirleriGetir")]
    public async Task<List<CografyaKutuphanesi>> SehirleriGetir([FromQuery] int id)
    {
        try
        {
            var result = await _IkullaniciKayitService.SehirleriGetir(id);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Sehirleri getirme hatası: {ex.Message}");
        }
    }

    [HttpPost("KullaniciKayit")]
    public async Task<IActionResult> KullaniciKayit([FromBody] Kullanici model)
    {
        if (model == null)
            return BadRequest("Model null geldi!");

        try
        {
            var result = await _IkullaniciKayitService.KullaniciKaydet(model);
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Sunucu hatası: {ex.Message}");
        }
    }

    [HttpPost("RegisterKullanici")]
    public async Task<IActionResult> RegisterKullanici([FromBody] RegisterLogin model)
    {
        if (model == null)
            return BadRequest("Model null geldi!");

        try
        {
            var result = await _IkullaniciKayitService.RegisterKullanici(model);
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Sunucu hatası: {ex.Message}");
        }
    }

    [HttpPost("LoginKullanici")]
    public async Task<RegisterLogin> LoginKullanici([FromBody] RegisterLogin model)
    {
        if (model == null)
            return null;

        try
        {
            var result = await _IkullaniciKayitService.LoginKullanici(model);
            return result;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    [HttpPost("MezuniyetKayit")]
    public async Task<IActionResult> MezuniyetKayit([FromBody] KullaniciOkulBilgileri okulmodel)
    {
        if (okulmodel == null)
            return BadRequest("Model null geldi!");
        try
        {
            var result = await _IkullaniciKayitService.MezuniyetKaydet(okulmodel);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Sunucu hatası: {ex.Message}");
        }
    }

    [Route("KullaniciOkulGuncelle")]
    [HttpPut]
    public async Task<IActionResult> KullaniciOkulGuncelle([FromBody] KullaniciOkulBilgileri okulmodel)
    {
        if (okulmodel == null)
            return BadRequest("Gönderilen model null");

        try
        {
            var result = await _IkullaniciKayitService.KullaniciOkulGuncelle(okulmodel);

            if (result == null)
                return NotFound("Kayıt bulunamadı veya güncellenemedi");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Hata oluştu: {ex.Message}");
        }
    }


    [Route("KullaniciGuncelle")]
    [HttpPut]
    public async Task<IActionResult> KullaniciGuncelle([FromBody] Kullanici model)
    {
        if (model == null)
            return BadRequest("Model null geldi!");

        try
        {
            var result = await _IkullaniciKayitService.KullaniciGuncelle(model);
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Sunucu hatası: {ex.Message}");
        }
    }

    [Route("IletisimListele")]
    [HttpGet]
    public async Task<List<ViewModelListele>> IletisimListele()
    {
        try
        {
            var result = await _IkullaniciKayitService.İletisimListele();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"İletişim listeleme hatası: {ex.Message}");
        }
    }

    [Route("KonumVeriAl")]
    [HttpGet]
    public async Task<List<CografyaKutuphanesi>> KonumVeriAl()
    {
        try
        {
            var result = await _IkullaniciKayitService.KonumVeriAl();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"İletişim listeleme hatası: {ex.Message}");
        }
    }

    [Route("VeriSil")]
    [HttpPost]
    public async Task<bool> VeriSil([FromQuery] int id)
    {
        try
        {
            var result = await _IkullaniciKayitService.VeriSil(id);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Veri silme hatası: {ex.Message}");
        }
    }


    //MEDİA
    [Route("MediaKayit")]
    [HttpPost]
    public async Task<ActionResult<List<Media>>> MediaKayit(List<IFormFile> mediaFiles, [FromForm] int id)
    {
        try
        {
            var result = await _IkullaniciKayitService.MedyaKayit(mediaFiles, id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Medya kayıt hatası: {ex.Message}" });
        }
    }

    //MEDİA
    [Route("DiplomaKayit")]
    [HttpPost]
    public async Task<ActionResult<List<OkulMedyalar>>> DiplomaKayit([FromForm] List<IFormFile> Diploma, [FromForm] int kullaniciId, [FromForm] int OkulId)
    {
        try
        {
            var result = await _IkullaniciKayitService.DiplomaKayit(Diploma, kullaniciId, OkulId);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Medya kayıt hatası: {ex.Message}" });
        }
    }

    [Route("MediaGuncelle")]
    [HttpPut]
    public async Task<ActionResult<List<Media>>> MediaGuncelle(List<IFormFile> mediaFiles, [FromForm] int id)
    {
        try
        {
            var result = await _IkullaniciKayitService.MediaGuncelle(mediaFiles, id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Medya kayıt hatası: {ex.Message}" });
        }
    }

    [HttpGet("MedyaGetir")]
    public async Task<List<vmPrew>> MedyaGetir(int id)
    {
        try
        {
            var result = await _IkullaniciKayitService.MedyaGetir(id);
            return result;
        }
        catch
        {
            return null;
        }
    }
    [Route("OkulVeriGetir/{kullaniciId}")]
    [HttpGet]
    public async Task<List<vmOkullar>> OkulVeriGetir( int kullaniciId)
    { 
        try
        {
            var result = await _IkullaniciKayitService.OkulVeriGetir(kullaniciId);
            return result;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    [HttpDelete("SilinenOkulGuncelle/{id}")]
    public async Task<bool> SilinenOkulGuncelle(int id)
    {
        var result = await _IkullaniciKayitService.SilinenOkulGuncelle(id);
        return true;
    }

    [HttpDelete("SilinenOkulGuncelleDiv/{id}")]
    public async Task<bool> SilinenOkulGuncelleDiv(int id)
    {
        var result = await _IkullaniciKayitService.SilinenOkulGuncelleDiv(id);
        return true;
    }
}
