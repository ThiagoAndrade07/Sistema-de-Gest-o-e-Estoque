using SGEServer.Class;
using SGEServer.Data;
using SGEServer.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.InkML;
using System.Reflection.Metadata;


namespace SGEServer.Controllers
{
    public class ProdutosController
    {
        public string SalvarProduto(ProdutosModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "200", "Nome", dados.Item!),
                    new ParametroSql("input", "200", "Marca", dados.Marca!),
                    new ParametroSql("input", "200", "Ncm", dados.Ncm!),
                    new ParametroSql("input", "200", "Ca", dados.CaEpi!),
                    new ParametroSql("input", "50", "Categoria", dados.CodCategoria!),
                    new ParametroSql("input", "50", "Fornecedor", dados.Fornecedor!),
                    new ParametroSql("input", "", "EstoqueMinimo", dados.EstoqueMinimo),
                    new ParametroSql("input", "10", "EnderecoEstoque", dados.Endereco!),
                    new ParametroSql("input", "500", "Descricao", dados.Descricao!),
                    new ParametroSql("input", "", "UnidadeMedida", dados.CodUnidadeMedida!),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional001CadastroProdutos]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public List<DropDownModel> CarregaCategorias()
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<DropDownModel> dados = new List<DropDownModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("SpOperacional002CarregaCategorias", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            Codigo = int.Parse(It["Codigo"].ToString()!),
                            Descricao = It["Descricao"].ToString()!,
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

        public List<ProdutosModel> CarregaProdutosCadastrados(int CodEmpresa, string nome)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ProdutosModel> dados = new List<ProdutosModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "200", "Nome", nome),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa)
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("SpConsulta001RetornaProdutosCadastrados", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        var produto = new ProdutosModel();

                        if (int.TryParse(It["Codigo"].ToString(), out int codProduto))
                        {
                            produto.CodProduto = codProduto;
                        }

                        produto.Item = It["Nome"].ToString();
                        produto.Marca = It["Marca"].ToString();
                        produto.Referencia = It["Referencia"].ToString();
                        produto.Ncm = It["Ncm"].ToString();
                        produto.CaEpi = It["Ca"].ToString();
                        produto.UnidadeMedida = It["UnidadeMedida"].ToString();

                        if (int.TryParse(It["LucroVenda"].ToString(), out int lucroVenda))
                        {
                            produto.LucroVenda = lucroVenda;
                        }

                        if (decimal.TryParse(It["ValorVendaIndicado"].ToString(), out decimal valorVendaIndicado))
                        {
                            produto.ValorVendaIndicado = valorVendaIndicado;
                        }
                        if (int.TryParse(It["QuantidadeEstoque"].ToString(), out int quantidadeEstoque))
                        {
                            produto.QuantidadeEstoque = quantidadeEstoque;
                        }

                        produto.EnderecoEstoque = It["EnderecoEstoque"].ToString();

                        if (int.TryParse(It["EstoqueMinimo"].ToString(), out int estoqueMinimo))
                        {
                            produto.EstoqueMinimo = estoqueMinimo;
                        }

                        if (It["ValorUltimaCompra"] != DBNull.Value && decimal.TryParse(It["ValorUltimaCompra"].ToString(), out decimal valorUltimaCompra))
                        {
                            produto.ValorUltimaCompra = valorUltimaCompra;
                        }

                        if (It["ValorUltimaVenda"] != DBNull.Value && decimal.TryParse(It["ValorUltimaVenda"].ToString(), out decimal valorUltimaVenda))
                        {
                            produto.ValorUltimaVenda = valorUltimaVenda;
                        }

                        dados.Add(produto);
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }


        public string RemoverProduto(int codProduto)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "", "CodProduto", codProduto),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional003RemoverProdutoCadastrado]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string RemoverProdutoPrazo(int CodProdutoAPrazo, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "", "CodProdutoPendente", CodProdutoAPrazo),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("SpOperacional042RemoveProdutoAPrazo", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public ProdutosModel CarregaProdutoPorCod(int codProduto)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                ProdutosModel dados = new ProdutosModel();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodigoProduto", codProduto),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("SpOperacional004RetornaProdutoPorCod", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.CodProduto = int.Parse(It["Codigo"].ToString()!);
                        dados.Item = It["Nome"].ToString()!;
                        dados.Referencia = It["Referencia"].ToString()!;
                        dados.Marca = It["Marca"].ToString()!;
                        dados.Ncm = It["Ncm"].ToString()!;
                        dados.CaEpi = It["Ca"].ToString()!;
                        dados.CodCategoria = int.Parse(It["CodCategoria"].ToString()!);
                        dados.Fornecedor = It["Fornecedor"].ToString()!;
                        dados.EstoqueMinimo = int.Parse(It["EstoqueMinimo"].ToString()!);
                        dados.CodUnidadeMedida = int.Parse(It["CodUnidadeMedida"].ToString()!);
                        dados.Endereco = It["EnderecoEstoque"].ToString()!;
                        dados.Descricao = It["Descricao"].ToString()!;
                        dados.QuantidadeEstoque = int.Parse(It["QtdeEstoque"].ToString()!);
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string EditarProduto(ProdutosModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "", "CodProduto", dados.CodProduto),
                    new ParametroSql("input", "200", "Nome", dados.Item!),
                    new ParametroSql("input", "200", "Marca", dados.Marca!),
                    new ParametroSql("input", "200", "Ncm", dados.Ncm!),
                    new ParametroSql("input", "200", "Ca", dados.CaEpi!),
                    new ParametroSql("input", "50", "Categoria", dados.CodCategoria!),
                    new ParametroSql("input", "50", "Fornecedor", dados.Fornecedor!),
                    new ParametroSql("input", "", "EstoqueMinimo", dados.EstoqueMinimo),
                    new ParametroSql("input", "10", "EnderecoEstoque", dados.Endereco!),
                    new ParametroSql("input", "500", "Descricao", dados.Descricao!),
                    new ParametroSql("input", "", "CodUnidadeMedida", dados.CodUnidadeMedida!),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional005EditarProduto]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public List<DropDownModel> CarregaUnidadesMedida()
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<DropDownModel> dados = new List<DropDownModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("SpOperacional006CarregaUnidadesMedida", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            Codigo = int.Parse(It["Codigo"].ToString()!),
                            Descricao = It["Descricao"].ToString()!,
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

        public string RealizarEntradaEstoque(int codProduto, string Item, decimal valorUltimaCompra, int Quantidade, int LucroVenda, decimal ValorTotal, decimal Icms, decimal Ipi, int codEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> parametros = new()
            {
                new ParametroSql("input", "", "CodProduto", codProduto),
                new ParametroSql("input", "", "Nome", Item),
                new ParametroSql("input", "18,6", "ValorUltimaCompra", valorUltimaCompra),
                new ParametroSql("input", "", "Quantidade", Quantidade),
                new ParametroSql("input", "", "LucroVenda", LucroVenda),
                new ParametroSql("input", "18,6", "ValorVendaIndicado", ValorTotal),
                new ParametroSql("input", "18,6", "Icms", Icms),
                new ParametroSql("input", "18,6", "Ipi", Ipi),
                new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                new ParametroSql("output", "500", "Resp", 0),
            };

                return contexto.ExecutaComandoRetornoString("[SpOperacional008EntradaEstoque]", parametros);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }
        public string RealizaSaidaEstoque(int CodEmpresa, ProdutosModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> parametros = new()
            {
                new ParametroSql("input", "", "CodProduto", dados.CodProduto),
                new ParametroSql("input", "", "ValorUltimaVenda", dados.ValorUnitario),
                new ParametroSql("input", "", "ValorTotal", dados.ValorTotal),
                new ParametroSql("input", "200", "Nome", dados.Item),
                new ParametroSql("input", "", "Quantidade", dados.QtdeItens),
                new ParametroSql("input", "18", "Cnpj", dados.Cnpj),
                new ParametroSql("input", "200", "NomeFantasia", dados.NomeFantasia),
                new ParametroSql("input", "", "CodCliente", dados.CodCliente),
                new ParametroSql("input", "50", "RetiradoPor", dados.RetiradoPor),
                new ParametroSql("input", "10", "TipoMovimentacao", dados.TipoMovimentacao),
                new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                new ParametroSql("output", "500", "Resp", 0),
            };

                return contexto.ExecutaComandoRetornoString("SpOperacional009SaidaEstoque", parametros);

            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }


        public List<MovimentacaoModel> CarregaMovimentacaoEntrada(int CodEmpresa, string nome)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<MovimentacaoModel> dados = new List<MovimentacaoModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "200", "Nome", nome),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa)
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpConsulta003RetornaMovimentacaoEntrada]", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            CodProduto = int.Parse(It["CodProduto"].ToString()!),
                            IdMovimentacao = int.Parse(It["IdMovimentacao"].ToString()!),
                            Item = It["Nome"].ToString()!,
                            TipoMovimentacao = It["TipoMovimentacao"].ToString()!,
                            Quantidade = int.Parse(It["Quantidade"].ToString()!),
                            Icms = decimal.Parse(It["Icms"].ToString()!),
                            Ipi = decimal.Parse(It["Ipi"].ToString()!),
                            LucroVenda = int.Parse(It["LucroVenda"].ToString()!),
                            ValorUnitario = decimal.Parse(It["ValorMovimentacao"].ToString()!),
                            ValorTotal = decimal.Parse(It["ValorTotal"].ToString()!),
                            DataMovimentacao = DateTime.Parse(It["DataMovimentacao"].ToString()),
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

        public List<MovimentacaoModel> CarregaMovimentacaoSaida(int CodEmpresa, string nome)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<MovimentacaoModel> dados = new List<MovimentacaoModel>();

                List<ParametroSql> list = new()
        {
            new ParametroSql("input", "200", "Nome", nome),
            new ParametroSql("input", "", "CodEmpresa", CodEmpresa)
        };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpConsulta004RetornaMovimentacaoSaida]", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        var movimentacao = new MovimentacaoModel
                        {
                            Item = It["Nome"].ToString(),
                            TipoMovimentacao = It["TipoMovimentacao"].ToString(),
                            DataMovimentacao = DateTime.Parse(It["DataMovimentacao"].ToString()),
                            Cnpj = It["Cnpj"].ToString(),
                            NomeFantasia = It["NomeFantasia"].ToString(),
                            RetiradoPor = It["RetiradoPor"].ToString(),
                        };

                        if (int.TryParse(It["CodProduto"].ToString(), out int codProduto))
                            movimentacao.CodProduto = codProduto;

                        if (int.TryParse(It["IdMovimentacao"].ToString(), out int idMovimentacao))
                            movimentacao.IdMovimentacao = idMovimentacao;

                        if (int.TryParse(It["Quantidade"].ToString(), out int quantidade))
                            movimentacao.Quantidade = quantidade;

                        if (decimal.TryParse(It["ValorMovimentacao"].ToString(), out decimal valorUnitario))
                            movimentacao.ValorUnitario = valorUnitario;

                        if (decimal.TryParse(It["ValorTotal"].ToString(), out decimal valorTotal))
                            movimentacao.ValorTotal = valorTotal;

                        dados.Add(movimentacao);
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {

                Console.WriteLine(exp.ToString());
                throw;
            }
        }


        public List<ProdutosModel> CarregarProdutosAPrazoPorEmpresa(int codEmpresa, int CodCliente)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ProdutosModel> dados = new List<ProdutosModel>();

                List<ParametroSql> parametros = new List<ParametroSql>
                {
                    new ParametroSql("input", "", "CodEmpresa", codEmpresa),
                    new ParametroSql("input", "", "CodCliente", CodCliente),
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpConsulta008ProdutosAPrazoPorEmpresa]", parametros);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dados.Add(new ProdutosModel
                        {
                            CodProdutoAPrazo = int.Parse(row["Codigo"].ToString()),
                            CodCliente = int.Parse(row["CodCliente"].ToString()),
                            CodProduto = int.Parse(row["CodProduto"].ToString()),
                            Item = row["Item"].ToString(),
                            QtdeItens = int.Parse(row["Quantidade"].ToString()),
                            ValorUnitario = decimal.Parse(row["ValorUnitario"].ToString()),
                            ValorTotal = decimal.Parse(row["ValorTotal"].ToString()),
                            DataVenda = DateTime.Parse(row["DataVenda"].ToString()),
                            DataVencimento = DateTime.Parse(row["DataVencimento"].ToString()),
                            StatusPagamento = row["StatusPagamento"].ToString()
                        });
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {

                throw new Exception($"Erro ao carregar produtos a prazo: {exp.Message}");

            }
        }

        public string InserirValorAbater(decimal ValorAbater,int CodCliente, int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "ValorAbater", ValorAbater),
                    new ParametroSql("input", "", "CodCliente", CodCliente),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional040AdicionarCreditosAEmpresa]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }


    }


}



