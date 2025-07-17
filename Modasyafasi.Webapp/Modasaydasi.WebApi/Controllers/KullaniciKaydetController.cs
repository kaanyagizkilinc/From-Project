using Microsoft.AspNetCore.Mvc;
using Modasaydasi.WebApi.Model.Interface;
using Modasayfasi.Model.Entity;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class KullaniciKaydetController : ControllerBase
{
    private readonly IKullaniciKayit _IkullaniciKayitService;

    public KullaniciKaydetController(IKullaniciKayit kullaniciKayitService)
    {
        _IkullaniciKayitService = kullaniciKayitService;
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
            // Hata detayýný loglayýn
            return StatusCode(500, $"Sunucu hatasý: {ex.Message}");
        }
    }
}
