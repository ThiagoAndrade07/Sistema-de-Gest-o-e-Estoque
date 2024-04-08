namespace SGEServer.Models
{
    public class ProdutosModel
    {
        public DateTime DataVencimento { get; set; }
        public DateTime DataVenda { get; set; } 
        public DateTime DataCriacao { get; set; }
        public string? StatusPagamento { get; set; }
        public int CodProduto { get; set; }
        public int CodCliente { get; set; }
        public int CodProdutoCotacao { get; set; }
        public string? Cnpj { get; set; }
        public string? RetiradoPor { get; set; }
        public string? NomeFantasia { get; set; }
        public string? TipoMovimentacao { get; set; }
        public string? Item { get; set; }
        public string? Referencia { get; set; }
        public string? Marca { get; set; }
        public int EstoqueMinimo { get; set; }
        public int QtdeItens { get; set; }
        public int LucroVenda { get; set; }
        public decimal ValorVendaIndicado { get; set; }
        public decimal ValorUnitario { get; set; }
        public string? EnderecoEstoque { get; set; }
        public decimal? ValorUltimaCompra { get; set; }
        public decimal? ValorUltimaVenda { get; set; }
        public int? QuantidadeUltimaVenda { get; set; }
        public int? QuantidadeUltimaCompra { get; set; }
        public decimal? ValorUnitarioComprado { get; set; }
        public int CodUnidadeMedida { get; set; }
        public string? UnidadeMedida { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Endereco { get; set; }
        public string? Ncm { get; set; }
        public string? CaEpi {  get; set; }
        public string? CnpjCpf { get; set; }
        public int? QuantidadeEstoque { get; set; }
        public string? Data { get; set; }
        public string? NumCotacao { get; set; }
        public int CodCotacao { get; set; }
        public decimal ValorTotalProduto { get; set; }
        public decimal ValorTotalCotacao { get; set; }
        public string? RazaoSocial { get; set; }
        public string? Fornecedor { get; set; }
        public int TbOperacional005Codigo { get; set; }
        public string? Categoria { get; set; }
        public int CodCategoria { get; set; }
        public string? Descricao { get; set; }
        public int CodStatus { get; set; }

        public int CodProdutoAPrazo { get; set; }

    }
}
