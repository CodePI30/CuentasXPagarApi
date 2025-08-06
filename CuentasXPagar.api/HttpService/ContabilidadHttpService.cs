using CuentasXPagar.api.Endpoints;
using CuentasXPagar.api.Models;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;

namespace CuentasXPagar.api.HttpService
{
    public class ContabilidadHttpService
    {
        private readonly HttpClient _httpClient;
        public IConfiguration _Configuration;

        public ContabilidadHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _Configuration = configuration;
        }

        public async Task<CatalogoResponse> GetCatalogoCuentasAsync()
        {
            var url = $"{_Configuration["API_Contabilidad:Url"]}{EndpointsContabilidad.GetCatalogoCuentas}";
            var apiKey = _Configuration["API_Contabilidad:Api_Key"];

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("x-api-key", apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var resultado = JsonSerializer.Deserialize<CatalogoResponse>(json, options);

            return resultado;
        }

        public async Task<List<EntradaContable>> GetEntradasContablesAsync(DateTime fechaInicio, DateTime fechaFin, int cuentaId)
        {
            var apiKey = _Configuration["API_Contabilidad:Api_Key"];

            string endpoint = string.Format(EndpointsContabilidad.GetEntradasContables,
                fechaInicio.ToString("yyyy-MM-dd"),
                fechaFin.ToString("yyyy-MM-dd"),
                cuentaId);

            var url = $"{_Configuration["API_Contabilidad:Url"]}{endpoint}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("x-api-key", apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var resultado = JsonSerializer.Deserialize<ApiResponse<List<EntradaContable>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return resultado?.data ?? new List<EntradaContable>();
        }


        public async Task<ApiResponseModel> PostEntradaContableAsync(EntradaContableRequest requestData)
        {
            var apiKey = _Configuration["API_Contabilidad:Api_Key"];

            var url = $"{_Configuration["API_Contabilidad:Url"]}{EndpointsContabilidad.PostEntradaContable}";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("x-api-key", apiKey);

            request.Content = JsonContent.Create(requestData);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<ApiResponseModel>();
                return resultado;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al crear la entrada contable: {response.StatusCode} - {error}");
            }
        }

        public async Task<BalancesModel> GetBalancesCuentasAsync()
        {
            var url = $"{_Configuration["API_Contabilidad:Url"]}{EndpointsContabilidad.GetBalances}";
            var apiKey = _Configuration["API_Contabilidad:Api_Key"];

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("x-api-key", apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var resultado = JsonSerializer.Deserialize<BalancesModel>(json, options);

            return resultado;
        }
    }
}
