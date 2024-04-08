using System;
using System.Text.Json.Serialization;

namespace SGEServer.Models
{
    public class CnpjModel
    {
        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("abertura")]
        public string DataAbertura { get; set; }

        [JsonPropertyName("nome")]
        public string RazaoSocial { get; set; }

        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("fantasia")]
        public string NomeFantasia { get; set; }

        [JsonPropertyName("natureza_juridica")]
        public string NaturezaJuridica { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("municipio")]
        public string Municipio { get; set; }

        [JsonPropertyName("uf")]
        public string Uf { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }

        [JsonPropertyName("situacao")]
        public string Situacao { get; set; }

        [JsonPropertyName("porte")]
        public string Porte { get; set; }

        [JsonPropertyName("capital_social")]
        public string CapitalSocial { get; set; }

        // Outras propriedades conforme a resposta da API
    }


}
