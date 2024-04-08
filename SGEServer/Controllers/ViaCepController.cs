using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SGEServer.Models;

namespace SGEServer.Controllers
{
    public class ViaCEPService
    {
        private readonly HttpClient _httpClient;

        public ViaCEPService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepModel> ConsultarCEPAsync(string cep)
        {
            var response = await _httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            return JsonSerializer.Deserialize<ViaCepModel>(response)!;
        }
    }

}