namespace SGEServer.Models
{
    public class CotacaoModel
    {
        public int CodCotacao { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Status { get; set; }
        public int CodStatus { get; set; }
        public string? NumCotacao { get; set; }
        public string? Empresa { get; set; }
        public decimal ValorMonetario { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? NomeFantasia { get; set; }
        public string? Cnpj { get; set; }
        public int CodCliente {  get; set; }
        public string? OrdemCompra { get; set; }
        public int PrazoEntregaRetornado { get; set; }
        public int FaturamentoRetornado { get; set; }
        public int PrazoEntregaRetornadoNovo { get; set; }
        public int FaturamentoRetornadoNovo { get; set; }
    }
}
