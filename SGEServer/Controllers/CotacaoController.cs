using SGEServer.Class;
using SGEServer.Data;
using SGEServer.Models;
using System.Data;

namespace SGEServer.Controllers
{
    public class CotacaoController
    {
        public List<CotacaoModel> ListarCotacoesPendentes(int codEmpresa, string? numCotacao, string? NomeFantasia, string? OrdemCompra)
        {
            if (string.IsNullOrEmpty(numCotacao))
            {
                numCotacao = null;
            }
            if (string.IsNullOrEmpty(NomeFantasia))
            {
                NomeFantasia = null;
            }
            if (string.IsNullOrEmpty(OrdemCompra))
            {
                OrdemCompra = null;
            }

            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<CotacaoModel> dados = new List<CotacaoModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "12", "NumCotacao", numCotacao!),
                    new ParametroSql("input", "200", "NomeFantasia", NomeFantasia!),
                    new ParametroSql("input", "12", "OrdemCompra", OrdemCompra!),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("SpOperacional032ListarCotacoesPendentes", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            CodCotacao = int.Parse(It["Codigo"].ToString()!),
                            NumCotacao = It["NumCotacao"].ToString()!,
                            DataCriacao = DateTime.Parse(It["DataCriacao"].ToString()!),
                            CodCliente = int.Parse(It["CodCliente"].ToString()!),
                            NomeFantasia = It["NomeFantasia"].ToString()!,
                            Cnpj = It["Cnpj"].ToString()!,
                            ValorTotal = decimal.Parse(It["ValorTotalCotacao"].ToString()!),
                            OrdemCompra = It["OrdemCompra"].ToString()!,
                            CodStatus = int.Parse(It["CodStatus"].ToString()!)
                        });
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public List<CotacaoModel> ListarCotacoesFinalizadas(int codEmpresa, string? numCotacao, string? NomeFantasia, string? OrdemCompra)
        {
            if (string.IsNullOrEmpty(numCotacao))
            {
                numCotacao = null;
            }
            if (string.IsNullOrEmpty(NomeFantasia))
            {
                NomeFantasia = null;
            }
            if (string.IsNullOrEmpty(OrdemCompra))
            {
                OrdemCompra = null;
            }

            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<CotacaoModel> dados = new List<CotacaoModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "12", "NumCotacao", numCotacao!),
                    new ParametroSql("input", "200", "NomeFantasia", NomeFantasia!),
                    new ParametroSql("input", "12", "OrdemCompra", OrdemCompra!),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpOperacional039ListarCotacoesFinalizadas]", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            CodCotacao = int.Parse(It["Codigo"].ToString()!),
                            NumCotacao = It["NumCotacao"].ToString()!,
                            DataCriacao = DateTime.Parse(It["DataCriacao"].ToString()!),
                            CodCliente = int.Parse(It["CodCliente"].ToString()!),
                            NomeFantasia = It["NomeFantasia"].ToString()!,
                            Cnpj = It["Cnpj"].ToString()!,
                            ValorTotal = decimal.Parse(It["ValorTotalCotacao"].ToString()!),
                            OrdemCompra = It["OrdemCompra"].ToString()!,
                            CodStatus = int.Parse(It["CodStatus"].ToString()!)
                        });
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public List<ProdutosModel> ListarProdutosCotacao(int codEmpresa, int CodCotacao)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ProdutosModel> dados = new List<ProdutosModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "", "CodCotacao", CodCotacao!),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("SpOperacional033ListarProdutosCotacao", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            CodProdutoCotacao = int.Parse(It["Codigo"].ToString()!),
                            CodProduto = int.Parse(It["CodProduto"].ToString()!),
                            CodCliente = int.Parse(It["CodCliente"].ToString()!),
                            Ncm = It["Ncm"].ToString()!,
                            CaEpi = It["Ca"].ToString()!,
                            UnidadeMedida = It["UnMedida"].ToString()!,
                            Item = It["Item"].ToString()!,
                            QtdeItens = int.Parse(It["Quantidade"].ToString()!),
                            ValorUnitario = decimal.Parse(It["ValorUnitario"].ToString()!),
                            ValorTotal = decimal.Parse(It["ValorTotalProduto"].ToString()!),
                            DataVenda = DateTime.Parse(It["DataVenda"].ToString()!),
                            DataVencimento = DateTime.Parse(It["DataVencimento"].ToString()!),
                            CodStatus = int.Parse(It["CodStatus"].ToString()!)
                        });
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public ProdutosModel RetornarDadosCotacao(int codEmpresa, int CodCotacao, ProdutosModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "", "CodCotacao", CodCotacao!),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("SpOperacional034RetornarDadosCotacao", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.NomeFantasia = It["Empresa"].ToString()!;
                        dados.Cnpj = It["Cnpj"].ToString()!;
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string RemoverItemCotacao(int codEmpresa, int CodCotacao, ProdutosModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "", "CodCotacao", CodCotacao!),
                    new ParametroSql("input", "", "CodProdutoCotacao", dados.CodProdutoCotacao),
                };

                var resp = contexto.ExecutaComandoRetornoString("SpOperacional035RemoverProdutoCotacao", list);

                return resp;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string EntregarItemCotacao(int codEmpresa, int CodCotacao, ProdutosModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "", "CodCotacao", CodCotacao!),
                    new ParametroSql("input", "", "CodProdutoCotacao", dados.CodProdutoCotacao),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                var resp = contexto.ExecutaComandoRetornoString("SpOperacional036EntregarItemCotacao", list);

                return resp;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string CancelarCotacao(int codEmpresa, CotacaoModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "12", "CodCotacao", dados.CodCotacao!),
                };

                var resp = contexto.ExecutaComandoRetornoString("SpOperacional037ApagarCotacao", list);

                return resp;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string FecharOrdemCompra(int codEmpresa, OrdemCompraModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "", "NumOrdemCompra", dados.NumOrdemCompra!),
                };

                var resp = contexto.ExecutaComandoRetornoString("SpOperacional038FecharCotacao", list);

                return resp;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public decimal CotacaoPuxaUltimosValores(int CodCliente, int CodProduto, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> parametros = new()
                {
                    new ParametroSql("input", "", "CodCliente", CodCliente),
                    new ParametroSql("input", "", "CodProduto", CodProduto),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpConsulta010UltimoValorVendaCotacao]", parametros);

                decimal valorUltimaVenda = 0;

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    decimal.TryParse(row["ValorUltimaVenda"].ToString(), out valorUltimaVenda);
                }

                return valorUltimaVenda;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao buscar o último valor de venda: {exp.Message}");
            }
        }

        public decimal CotacaoPuxaValorEntradaProduto(int CodProduto, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> parametros = new()
                {
                    new ParametroSql("input", "", "CodProduto", CodProduto),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpConsulta012UltimoValorCompradoItem]", parametros);

                decimal valorUltimaCompra = 0;

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    decimal.TryParse(row["ValorUltimaCompra"].ToString(), out valorUltimaCompra);
                }

                return valorUltimaCompra;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao buscar o último valor de venda: {exp.Message}");
            }
        }




        public string BuscaUltimoNumeroCotacao(int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ParametroSql> parametros = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("SpOperacional029GerarProximoNumeroCotacao", parametros);


                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["ProximoNumeroCotacao"].ToString();
                }

                return "000000000001";
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao buscar o último número de cotação: {exp.Message}");
            }
        }




        public string CriarCotacao(int CodCliente, decimal ValorTotalCotacao, string NomeFantasia, string Cnpj, int PrazoEntregaRetornado, int FaturamentoRetornado, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodCliente", CodCliente),
                    new ParametroSql("input", "18,6", "ValorTotalCotacao", ValorTotalCotacao),
                    new ParametroSql("input", "200", "NomeFantasia", NomeFantasia),
                    new ParametroSql("input", "18", "Cnpj", Cnpj),
                    new ParametroSql("input", "", "PrazoEntregaRetornado", PrazoEntregaRetornado),
                    new ParametroSql("input", "", "FaturamentoRetornado", FaturamentoRetornado),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "32", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional020CriarCotacao]", list).Split("|")[0];
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string AtualizaNcmCa(ProdutosModel dados, int CodEmpresa)
        {
            if (string.IsNullOrEmpty(dados.CaEpi))
            {
                dados.CaEpi = null;
            }
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodProduto", dados.CodProduto!),
                    new ParametroSql("input", "", "Ca", dados.CaEpi!),
                    new ParametroSql("input", "", "Ncm", dados.Ncm!),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "32", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional041AtualizaNcmCaProduto]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");

            }
        }

        public string InserirItensCotacao(ProdutosModel dados, int CodEmpresa, int CodCotacao)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodCotacao", CodCotacao!),
                    new ParametroSql("input", "200", "Item", dados.Item!),
                    new ParametroSql("input", "", "CodCliente", dados.CodCliente!),
                    new ParametroSql("input", "", "CodProduto", dados.CodProduto!),
                    new ParametroSql("input", "200", "Ncm", dados.Ncm!),
                    new ParametroSql("input", "200", "Ca", dados.CaEpi!),
                    new ParametroSql("input", "200", "UnMedida", dados.UnidadeMedida),
                    new ParametroSql("input", "", "Quantidade", dados.QtdeItens!),
                    new ParametroSql("input", "18,6", "ValorUnitario", dados.ValorUnitario!),
                    new ParametroSql("input", "18,6", "ValorTotalProduto", dados.ValorTotalProduto!),
                    new ParametroSql("input", "", "DataVenda", dados.DataVenda!),
                    new ParametroSql("input", "", "DataVencimento", dados.DataVencimento!),
                    //new ParametroSql("input", "Pendente", "StatusPagamento", dados.StatusPagamento!),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional021AdicionarProdutoACotacao]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");

            }
        }


        public List<CotacaoModel> CarregarCotacoes()
        {
            List<CotacaoModel> listaCotacoes = new List<CotacaoModel>();
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                DataTable dt = contexto.ExecutaComandoRetornoData("SpConsulta013RetornasCotacoesSalvas", new List<ParametroSql>());

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CotacaoModel cotacao = new CotacaoModel
                        {
                            NumCotacao = row["NumCotacao"].ToString(),
                            CodCotacao = TryParseInt(row["CodCotacao"].ToString()),
                            NomeFantasia = row["NomeFantasia"].ToString(),
                            Cnpj = row["Cnpj"].ToString(),
                            CodCliente = TryParseInt(row["CodCliente"].ToString()),
                            PrazoEntregaRetornado = TryParseInt(row["DiasPrazoEntrega"].ToString()),
                            FaturamentoRetornado = TryParseInt(row["DiasFaturamento"].ToString()),
                            DataCriacao = TryParseDateTime(row["DataCriacao"].ToString()),
                            ValorTotal = decimal.Parse(row["ValorTotal"].ToString())
                        };
                        listaCotacoes.Add(cotacao);
                    }
                }
            }
            catch (Exception exp)
            {
                // Considerar logar o erro exp para diagnóstico sem lançar exceção que pode interromper o fluxo do usuário
                Console.WriteLine($"Erro ao carregar cotações: {exp.Message}");
            }

            return listaCotacoes;
        }

        private int TryParseInt(string value)
        {
            int.TryParse(value, out int result);
            return result;
        }

        private decimal TryParseDecimal(string value)
        {
            decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal result);
            return result;
        }

        private DateTime TryParseDateTime(string value)
        {
            DateTime.TryParse(value, out DateTime result);
            return result;
        }




        public List<ProdutosModel> CarregarItensCotacao(int CodCotacao, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ProdutosModel> itensCotacao = new List<ProdutosModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodCotacao", CodCotacao),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa)
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpOperacional022BuscarItensPorNumCotacao]", list);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ProdutosModel item = new ProdutosModel
                        {
                            CodProduto = Convert.ToInt32(row["CodProduto"]),
                            CodCliente = Convert.ToInt32(row["CodCliente"]),
                            Ncm = row["Ncm"].ToString(),
                            CaEpi = row["Ca"].ToString(),
                            UnidadeMedida = row["UnMedida"].ToString(),
                            Item = row["Item"].ToString(),
                            QtdeItens = Convert.ToInt32(row["Quantidade"]),
                            ValorUnitario = Convert.ToDecimal(row["ValorUnitario"]),
                            ValorTotalProduto = Convert.ToDecimal(row["ValorTotalProduto"]),
                            DataVenda = Convert.ToDateTime(row["DataVenda"]),
                            DataVencimento = Convert.ToDateTime(row["DataVencimento"]),
                            StatusPagamento = row["StatusPagamento"].ToString()
                        };

                        itensCotacao.Add(item);
                    }

                }

                return itensCotacao;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao carregar itens da cotação: {exp.Message}");
            }
        }

        public string RemoverItensCotacao(int CodCotacao, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodCotacao", CodCotacao),
                    new ParametroSql("input", "200", "CodEmpresa", CodEmpresa),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional023RemoverItemCotacao]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro: {exp.Message}");
            }
        }

        public string AtualizaValorTotalCotacaoEditar(int CodCotacao, decimal ValorTotalCotacao, int FaturamentoRetornadoNovo, int PrazoEntregaRetornadoNovo, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "12", "CodCotacao", CodCotacao),
                    new ParametroSql("input", "18,6", "ValorTotalCotacao", ValorTotalCotacao),
                    new ParametroSql("input", "18,6", "FaturamentoRetornadoNovo", FaturamentoRetornadoNovo),
                    new ParametroSql("input", "18,6", "PrazoEntregaRetornadoNovo", PrazoEntregaRetornadoNovo),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional024AtualizaDadosCotacao]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string UpdateNumeroOrdemCompra(string NumCotacao, string NumOrdemCompra, int CodEmpresa)
        {
            try
            {

                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "12", "NumCotacao", NumCotacao),
                    new ParametroSql("input", "", "NumOrdemCompra", NumOrdemCompra),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0)

                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional025InsereNumOrdemCompra]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }


        public bool VerificaDadosEmpresaParaOrdemCompra(string NumCotacao, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ParametroSql> parametros = new List<ParametroSql>
                {
                    new ParametroSql("input", "12", "NumCotacao", NumCotacao),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa)
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpOperacional027VerificaDadosParaOrdemCompra]", parametros);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return (bool)dt.Rows[0][0];
                }

                return false;
            }

            catch (Exception exp)
            {
                throw new Exception($"Erro ao verificar dados para ordem de compra: {exp.Message}");
            }
        }

        public string VerificaCotacaoAtrelada(string NumCotacao, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ParametroSql> parametros = new List<ParametroSql>
                {
                    new ParametroSql("input", "12", "NumCotacao", NumCotacao),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa)
                };


                DataTable dt = contexto.ExecutaComandoRetornoData("[SpConsulta017RetornaOrdemCompraPorCotacao]", parametros);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }

                return "Nenhum resultado encontrado.";
            }
            catch (Exception exp)
            {
                // Retorna a mensagem de erro
                return $"Erro ao verificar dados para ordem de compra: {exp.Message}";
            }
        }




        public string CriarOrdemCompra(int CodCliente, decimal ValorTotal, string NomeFantasia, string Cnpj, string NumOrdemCompra, int CodCotacao, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodCliente", CodCliente),
                    new ParametroSql("input", "18,6", "ValorTotal", ValorTotal),
                    new ParametroSql("input", "12", "CodCotacao", CodCotacao),
                    new ParametroSql("input", "200", "NomeFantasia", NomeFantasia),
                    new ParametroSql("input", "", "NumOrdemCompra", NumOrdemCompra),
                    new ParametroSql("input", "18", "Cnpj", Cnpj),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),

                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional028CriaOrdemCompra]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public CotacaoModel CarregarCotacaoEspecifica(string NumCotacao, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);


                List<ParametroSql> parametros = new List<ParametroSql>
                {
                    new ParametroSql("input", "12", "NumCotacao", NumCotacao),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa)
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpConsulta015RetornaCotacaoEspecifica]", parametros);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new CotacaoModel
                    {
                        NumCotacao = row["NumCotacao"].ToString(),
                        NomeFantasia = row["NomeFantasia"].ToString(),
                        Cnpj = row["Cnpj"].ToString(),
                        CodCliente = int.Parse(row["CodCliente"].ToString()),
                        CodCotacao = int.Parse(row["CodCotacao"].ToString()),
                        DataCriacao = DateTime.Parse(row["DataCriacao"].ToString()),
                        ValorTotal = decimal.Parse(row["ValorTotal"].ToString())
                    };
                }
                return null;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao carregar a cotação: {exp.Message}");
            }
        }


        public List<OrdemCompraModel> CarregaOrdemCompras()
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<OrdemCompraModel> listOrdemCompra = new List<OrdemCompraModel>();

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpConsulta016RetornaOrdemCompraSalvas]", new List<ParametroSql>());

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        OrdemCompraModel ordemCompra = new OrdemCompraModel
                        {
                            NumOrdemCompra = row["NumOrdemCompra"].ToString(),
                            NumCotacao = row["NumCotacao"].ToString(),
                            NomeFantasia = row["NomeFantasia"].ToString(),
                            Cnpj = row["Cnpj"].ToString(),
                            CodCliente = int.Parse(row["CodCliente"].ToString()),
                            ValorTotal = decimal.Parse(row["ValorTotal"].ToString()),
                            StatusPagamento = row["StatusPagamento"].ToString(),
                            CodStatus = int.Parse(row["CodStatus"].ToString()!),
                            DataCriacao = DateTime.Parse(row["DataCriacao"].ToString()!)
                        };
                        listOrdemCompra.Add(ordemCompra);
                    }
                }

                return listOrdemCompra;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao carregar cotações: {exp.Message}");
            }
        }
    }
}
