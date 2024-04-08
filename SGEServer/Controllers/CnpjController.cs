using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using SGEServer.Models;

namespace SGEServer.Controllers
{
    public class CnpjService
    {
        private readonly HttpClient _httpClient;

        public CnpjService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CnpjModel> ConsultarCNPJAsync(string cnpj)
        {
            string token = "dec42cfdf7b6e321d4be5e851676a1f86dd325615e2c1d987b3e45b0dd0d3d5c";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.GetAsync($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CnpjModel>(responseBody)!;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro na requisição: {e.Message}");
                return null;
            }
        }
    }
}

