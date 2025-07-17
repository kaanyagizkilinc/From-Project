using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Modasayfasi.Model;
using Modasayfasi.Model.Entity;
using Modasayfasi.Model.Entity.vmmodel;
using Modasyafasi.Webapp.Models.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Modasyafasi.Webapp.Service
{
    public class KullaniciService : IKullaniciService
    {
        private readonly HttpClient _httpClient;

        public KullaniciService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //
        public async Task<RegisterLogin> RegisterKullanici([FromBody]RegisterLogin model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("KullaniciService KullaniciKayit metodu çağrıldı. Model: " + json);
            Console.WriteLine(content);
            var response = await _httpClient.PostAsync("api/KullaniciKaydet/RegisterKullanici", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RegisterLogin>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<RegisterLogin> LoginKullanici(RegisterLogin model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("KullaniciService KullaniciKayit metodu çağrıldı. Model: " + json);
            Console.WriteLine(content);
            var response = await _httpClient.PostAsync("api/KullaniciKaydet/LoginKullanici", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RegisterLogin>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        //

        public async Task<Kullanici> KullaniciGetir(int id)
        {
            Console.WriteLine("KullaniciService KullaniciGetir metodu çağrıldı. Kullanıcı ID: " + id);
            var response = await _httpClient.GetAsync($"api/KullaniciKaydet/KullaniciGetir?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Kullanici>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<List<CografyaKutuphanesi>> SehirGetir(int id)
        {
            Console.WriteLine("KullaniciService SehirGetir metodu çağrıldı. Şehir ID: " + id);
            var response = await _httpClient.GetAsync($"api/KullaniciKaydet/SehirleriGetir?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<CografyaKutuphanesi>>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<List<CografyaKutuphanesi>> UlkeGetir()
        {
            var response = await _httpClient.GetAsync("api/KullaniciKaydet/UlkelerGetir");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response + " " + responseBody);
                var result = JsonConvert.DeserializeObject<List<CografyaKutuphanesi>>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<Kullanici> KullaniciKayit(Kullanici model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("KullaniciService KullaniciKayit metodu çağrıldı. Model: " + json);
            Console.WriteLine(content);
            var response = await _httpClient.PostAsync("api/KullaniciKaydet/KullaniciKayit", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Kullanici>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<KullaniciOkulBilgileri> MezuniyetKayit(KullaniciOkulBilgileri okulmodel)
        {

            var json = JsonConvert.SerializeObject(okulmodel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("KullaniciService MezuniyetKayit metodu çağrıldı. Model: " + json);
            Console.WriteLine(content);
            var response = await _httpClient.PostAsync("api/KullaniciKaydet/MezuniyetKayit", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KullaniciOkulBilgileri>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<KullaniciOkulBilgileri> KullaniciOkulGuncelle([FromBody] KullaniciOkulBilgileri model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("KullaniciService KullaniciOkulGuncelle metodu çağrıldı. Model: " + json);
            Console.WriteLine(content);
            var response = await _httpClient.PutAsync("api/KullaniciKaydet/KullaniciOkulGuncelle", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KullaniciOkulBilgileri>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<Kullanici> KullaniciGuncelle(Kullanici model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("KullaniciService KullaniciGüncelle metodu çağrıldı. Model: " + json);
            Console.WriteLine(content);
            var response = await _httpClient.PutAsync("api/KullaniciKaydet/KullaniciGuncelle", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Kullanici>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<List<ViewModelListele>> İletisimListele()
        {
            var response = await _httpClient.GetAsync("api/KullaniciKaydet/IletisimListele");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ViewModelListele>>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }

        }

        public async Task<List<CografyaKutuphanesi>> KonumVeriAl()
        {
            var response = await _httpClient.GetAsync("api/KullaniciKaydet/KonumVeriAl");
            if (response.IsSuccessStatusCode)
            {
                var responsebody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<CografyaKutuphanesi>>(responsebody);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> VeriSil(int id)
        {
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/KullaniciKaydet/VeriSil?id={id}", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //MEDİA

        public async Task<List<Media>> MediaKayit(List<IFormFile> file, int id)
        {
            var content = new MultipartFormDataContent();
            foreach (var f in file)
            {
                var streamContent = new StreamContent(f.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(f.ContentType);
                content.Add(streamContent, "mediaFiles", f.FileName);

            }
            content.Add(new StringContent(id.ToString()), "id");
            Console.WriteLine("KullaniciService MediaKayit metodu çağrıldı. Model: " + content);
            Console.WriteLine(content);
            var response = await _httpClient.PostAsync("api/KullaniciKaydet/MediaKayit", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Media>>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<List<OkulMedyalar>> DiplomaKayit(List<IFormFile> Diploma, int kullaniciId, int OkulId)
        {
            var content = new MultipartFormDataContent();
            foreach (var f in Diploma)
            {
                var streamContent = new StreamContent(f.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(f.ContentType);
                content.Add(streamContent, "Diploma", f.FileName);
            }
            content.Add(new StringContent(kullaniciId.ToString()), "kullaniciId");
            content.Add(new StringContent(OkulId.ToString()), "OkulId");
            Console.WriteLine("KullaniciService MediaKayit metodu çağrıldı. Model: " + content);
            Console.WriteLine("content :" + content);
            var response = await _httpClient.PostAsync("api/KullaniciKaydet/DiplomaKayit", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<OkulMedyalar>>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<List<Media>> MediaGuncelle(List<IFormFile> file, int id)
        {

            var content = new MultipartFormDataContent();
            foreach (var f in file)
            {
                var streamContent = new StreamContent(f.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(f.ContentType);
                content.Add(streamContent, "mediaFiles", f.FileName);
            }
            content.Add(new StringContent(id.ToString()), "id");
            var response = await _httpClient.PutAsync("api/KullaniciKaydet/MediaGuncelle", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Media>>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<List<vmPrew>> MedyaGetir(int id)
        {
            Console.WriteLine("KullaniciService medyagetir metodu çağrıldı. Kullanıcı ID: " + id);
            var response = await _httpClient.GetAsync($"api/KullaniciKaydet/MedyaGetir?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<vmPrew>>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<List<vmOkullar>> OkulVeriGetir(int kullaniciId)
        {
            Console.WriteLine("KullaniciService OkulVeriGetir metodu çağrıldı. Kullanıcı ID: " + kullaniciId);
            var response = await _httpClient.GetAsync($"api/KullaniciKaydet/OkulVeriGetir/{kullaniciId}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<vmOkullar>>(responseBody);
                return result;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Hatası: {response.StatusCode}, Detay: {errorBody}");
            }
        }

        public async Task<bool> SilinenOkulGuncelle(int id)
        {
            try
            {
                var respose = await _httpClient.DeleteAsync($"api/KullaniciKaydet/SilinenOkulGuncelle/{id}");
                if (respose.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorBody = await respose.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"API Hatası: {respose.StatusCode}, Detay: {errorBody}");
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SilinenOkulGuncelleDiv(int id)
        {
            try
            {
                var respose = await _httpClient.DeleteAsync($"api/KullaniciKaydet/SilinenOkulGuncelleDiv/{id}");
                if (respose.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorBody = await respose.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"API Hatası: {respose.StatusCode}, Detay: {errorBody}");
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
