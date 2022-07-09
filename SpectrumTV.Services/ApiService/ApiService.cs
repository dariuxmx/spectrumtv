using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SpectrumTV.Services.ApiService
{
    public class ApiService : IApiService
    {

        private readonly JsonSerializerSettings _serializerSettings;
        public ApiService()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        // GET 
        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            HttpClient httpClient = CreateHttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        // POST
        public Task<TResult> PostAsync<TResult>(string uri, TResult data)
        {
            return PostAsync<TResult, TResult>(uri, data);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data)
        {
            HttpClient httpClient = CreateHttpClient();
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
            HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            string responseData = await response.Content.ReadAsStringAsync();
            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));

            return result;
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }


    }
}
