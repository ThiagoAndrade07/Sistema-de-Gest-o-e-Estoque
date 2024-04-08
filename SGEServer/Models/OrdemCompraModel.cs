namespace SGEServer.Models
{
    public class OrdemCompraModel
    {

        
        public decimal ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? Cnpj { get; set; }
        public string? NomeFantasia { get; set; }
        public string? NumCotacao { get; set; }
        public string? NumOrdemCompra { get; set; }
        public int CodCliente { get; set; }
        public string? StatusPagamento { get; set; }
        public int CodStatus {  get; set; }
    }
}
