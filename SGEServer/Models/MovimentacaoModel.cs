namespace SGEServer.Models
{
    public class MovimentacaoModel
    {
        public int IdMovimentacao { get; set; }
        public int CodProduto { get; set; }
        public string Item { get; set; }
        public string? TipoMovimentacao { get; set; }
        public int Quantidade { get; set; }
        public decimal Icms { get; set; }
        public decimal Ipi { get; set; }
        public decimal ValorMovimentacao { get; set; }
        public int LucroVenda { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorUnitario { get; set; }
        public string? RefOrdemCompra { get; set; }
        public string? RetiradoPor { get; set; }
        public string? Cnpj { get; set; }
        public string? NomeFantasia { get; set; }

        
    }
}

