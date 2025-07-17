using System.Net.Http;
using System;

namespace Modasyafasi.Webapp.ApiHelper
{
    public class ApiHelper
    {
        public HttpClient HttpClient(HttpClient httpClient)
        {
            var tutucu = new HttpClientHandler();
            tutucu.ClientCertificateOptions = ClientCertificateOption.Manual;
            tutucu.ServerCertificateCustomValidationCallback = (HttpRequestMessage, cert, certChain, errors) => { return true; };
            httpClient = new HttpClient(tutucu);
            httpClient.BaseAddress = new Uri("https://localhost:44397/");
            return httpClient;
        }

    }
}
