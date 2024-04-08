//using Newtonsoft.Json;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace SGEServer.Controllers
//{
//    public class MercadoLivreController
//    {
//        private readonly HttpClient _httpClient;
//        private string _accessToken;

//        public MercadoLivreController(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task AuthenticateAsync(string clientId, string clientSecret, string authorizationCode)
//        {
//            var requestBody = new Dictionary<string, string>
//            {
//                {"grant_type", "authorization_code"},
//                {"client_id", clientId},
//                {"client_secret", clientSecret},
//                {"code", authorizationCode},
//                {"redirect_uri", "YOUR_REDIRECT_URI"}
//            };

//            var response = await _httpClient.PostAsync("https://api.mercadolibre.com/oauth/token", new FormUrlEncodedContent(requestBody));
//            response.EnsureSuccessStatusCode();

//            var responseContent = await response.Content.ReadAsStringAsync();
//            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

//            _accessToken = tokenResponse.AccessToken;
//        }

//        public async Task<Product> GetProductByBarcodeAsync(string barcode)
//        {
//            var requestUrl = $"API_ENDPOINT_FOR_BARCODE_SEARCH?barcode={barcode}&access_token={_accessToken}";
//            var response = await _httpClient.GetAsync(requestUrl);
//            response.EnsureSuccessStatusCode();

//            var responseContent = await response.Content.ReadAsStringAsync();
//            return JsonConvert.DeserializeObject<Product>(responseContent);
//        }

//        public class TokenResponse
//        {
//            [JsonProperty("access_token")]
//            public string AccessToken { get; set; }
//        }
//    }

//    public class Product
//    {
//        // Propriedades do produto (nome, preço, etc.)
//    }
//}
