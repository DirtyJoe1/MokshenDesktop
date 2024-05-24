using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MokshenDesktop.Resources.Services
{
    public abstract class HttpClientMK
    {
        private readonly Uri _baseUri;
        protected HttpClientMK()
        {
            _baseUri = new Uri("http://79.174.94.77:8080");
        }
        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = _baseUri;
            return client;
        }
    }
}
