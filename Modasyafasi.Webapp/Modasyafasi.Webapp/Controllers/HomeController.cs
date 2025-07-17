using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Modasayfasi.Model.Entity;
using Modasyafasi.Webapp.Models;
using Modasyafasi.Webapp.Models.Interface;
using Modasyafasi.Webapp.Service;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Modasyafasi.Webapp.Controllers
{

    public class HomeController : Controller
    {
        private readonly IKullaniciService _KullaniciService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IKullaniciService kullaniciService)
        {
            _logger = logger;
            _KullaniciService = kullaniciService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        //Geçersiz yönlendirme için

        public IActionResult AccessDenied()
        {
            return Content("Eriþim reddedildi");
        }

        //
        [HttpPost]
        public async Task<RegisterLogin> RegisterKullanici([FromBody]RegisterLogin model)
        {
            var result = await _KullaniciService.RegisterKullanici(model);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> LoginKullanici([FromBody]RegisterLogin model)
        {
            var result = await _KullaniciService.LoginKullanici(model);
            if (result == null)
            {
                return null;
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.KullaniciAdi)
                };

                var claimIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var authencationProp = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(
                    "MyCookieAuth",
                    new ClaimsPrincipal(claimIdentity),
                    authencationProp);
                
                return Ok(new { success = true, message = "Giriþ baþarýlý" });
            }
        }
        //logOut
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListeGuncelle(int id)
        {

            var result = await _KullaniciService.KullaniciGetir(id);
            ViewBag.Konumlar = await _KullaniciService.KonumVeriAl();
            return View(result);
        }
        [Authorize]
        public IActionResult iletisimList()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Authorize]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult iletisim()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UlkeGetir()
        {
            try
            {
                var result = await _KullaniciService.UlkeGetir();
                Console.WriteLine("Dönen veri:" + result);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Coðrafya kütüphanesi servisine istek atýlýrken hata oluþtu.");
                return StatusCode(500, "Servis hatasý oluþtu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SehirGetir(int id)
        {
            try
            {
                var result = await _KullaniciService.SehirGetir(id);
                Console.WriteLine("Dönen veri:" + result);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Coðrafya kütüphanesi servisine istek atýlýrken hata oluþtu.");
                return StatusCode(500, "Servis hatasý oluþtu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<Kullanici> KullaniciGuncelle([FromBody] Kullanici model)
        {
            try
            {
                var result = await _KullaniciService.KullaniciGuncelle(model);
                Console.WriteLine("Dönen veri:" + result);
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Kullanýcý servisine istek atýlýrken hata oluþtu.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return null;
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<Kullanici> KullaniciKayit([FromBody] Kullanici model)
        {
            try
            {
                var result = await _KullaniciService.KullaniciKayit(model);
                Console.WriteLine("Dönen veri:" + result);
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Kullanýcý servisine istek atýlýrken hata oluþtu.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return null;
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<KullaniciOkulBilgileri> MezuniyetKayit([FromBody] KullaniciOkulBilgileri okulmodel)
        {
            try
            {
                var result = await _KullaniciService.MezuniyetKayit(okulmodel);
                Console.WriteLine("Dönen veri:" + result);
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Kullanýcý servisine istek atýlýrken hata oluþtu.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return null;
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> IletisimListesGET()
        {
            try
            {
                var result = await _KullaniciService.ÝletisimListele();
                Console.WriteLine("Dönen veri:" + result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanýcý kayýt sayfasý yüklenirken hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> KonumVeriAl()
        {
            try
            {
                var result = await _KullaniciService.KonumVeriAl();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanýcý Liste sayfasý yüklenirken hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<bool> VeriSil(int id)
        {
            try
            {
                var result = await _KullaniciService.VeriSil(id);
                return result;
            }
            catch
            {
                return false;
            }
        }
        [Authorize]
        //MEDÝA
        [HttpPost]
        public async Task<IActionResult> DiplomaKayit([FromForm] List<IFormFile> Diploma, [FromForm] int kullaniciId, [FromForm] int OkulId)
        {
            try
            {
                var result = await _KullaniciService.DiplomaKayit(Diploma, kullaniciId, OkulId);
                Console.WriteLine("Dönen veri:" + result);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Media servisine istek atýlýrken hata oluþtu.");
                return StatusCode(500, "Servis hatasý oluþtu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MediaKayit([FromForm] List<IFormFile> mediaFiles, [FromForm] int id)
        {
            try
            {
                var result = await _KullaniciService.MediaKayit(mediaFiles, id);
                Console.WriteLine("Dönen veri:" + result);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Media servisine istek atýlýrken hata oluþtu.");
                return StatusCode(500, "Servis hatasý oluþtu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> MediaGuncelle([FromForm] List<IFormFile> mediaFiles, [FromForm] int id)
        {
            try
            {
                var result = await _KullaniciService.MediaGuncelle(mediaFiles, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Media Güncellerken servise istek gitmedi");
                return null;
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MedyaGetir(int id)
        {
            try
            {
                var result = await _KullaniciService.MedyaGetir(id);
                Console.WriteLine("Dönen veri:" + result);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Coðrafya kütüphanesi servisine istek atýlýrken hata oluþtu.");
                return StatusCode(500, "Servis hatasý oluþtu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> OkulVeriGetir(int kullaniciId)
        {
            try
            {
                var result = await _KullaniciService.OkulVeriGetir(kullaniciId);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Media servisine istek atýlýrken hata oluþtu.");
                return StatusCode(500, "Servis hatasý oluþtu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<KullaniciOkulBilgileri> KullaniciOkulGuncelle([FromBody] KullaniciOkulBilgileri model)
        {
            try
            {
                var result = await _KullaniciService.KullaniciOkulGuncelle(model);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanýcý okul bilgileri güncellenirken hata oluþtu.");
                return null;
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> SilinenOkulGuncelle(int id)
        {
            try
            {
                var result = await _KullaniciService.SilinenOkulGuncelle(id);
                if (result)
                {
                    return Ok(new { message = "Okul bilgisi baþarýyla silindi." });
                }
                else
                {
                    return NotFound(new { message = "Okul bilgisi bulunamadý." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Okul bilgisi silinirken hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> SilinenOkulGuncelleDiv(int id)
        {
            try
            {
                var result = await _KullaniciService.SilinenOkulGuncelleDiv(id);
                if (result)
                {
                    return Ok(new { message = "Okul bilgisi baþarýyla silindi." });
                }
                else
                {
                    return NotFound(new { message = "Okul bilgisi bulunamadý." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Okul bilgisi silinirken hata oluþtu.");
                return StatusCode(500, "Bilinmeyen bir hata oluþtu.");
            }
        }
    }
}
