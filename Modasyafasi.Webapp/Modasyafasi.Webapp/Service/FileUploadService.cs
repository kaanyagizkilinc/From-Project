using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Modasayfasi.Model;
using Modasayfasi.Model.Entity;
using Modasyafasi.Webapp.ApiHelper;
using Modasyafasi.Webapp.Models.Interface;

namespace Modasyafasi.Webapp.Service
{
    public class FileUploadService : IFileUploadService 
    {
        private readonly HttpClient _httpClient;
        public FileUploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Class1> Class1(Class1 class1)
        {
            throw null;
        }
    }
}
