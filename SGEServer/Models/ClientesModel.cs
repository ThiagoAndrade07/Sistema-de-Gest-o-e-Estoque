namespace SGEServer.Models
{
    public class ClientesModel
    {
        public int CodEmpresa { get; set; }
        public int CodCliente { get; set; }
        public string? NomeFantasia { get; set; }
        public string? Cnpj { get; set; }
        public string? RazaoSocial { get; set; }
        public string? InscricaoEstadual { get; set; }
        public bool? Isento { get; set; }
        public string? Cep { get; set; }
        public string? Setor { get; set; }
        public string? Numero { get; set; }
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? Estado { get; set; }
        public string? NomePessoa { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Cpf { get; set; }
        public string? Genero { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public byte[] Imagem { get; set; }
        public decimal Creditos { get; set; }
        public decimal ValorTotalEmpresa { get; set; }

    }
}