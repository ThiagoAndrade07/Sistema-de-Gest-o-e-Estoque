using SGEServer.Class;
using SGEServer.Data;
using SGEServer.Models;
using System.Data;
using System.Data.SqlClient;

namespace SGEServer.Controllers
{
    public class ClientesController
    {
        public string SalvarCliente(ClientesModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "200", "RazaoSocial", dados.RazaoSocial!),
                    new ParametroSql("input", "200", "NomeFantasia", dados.NomeFantasia!),
                    new ParametroSql("input", "18", "Cnpj", dados.Cnpj!),
                    new ParametroSql("input", "50", "InscricaoEstadual", dados.InscricaoEstadual!),
                    new ParametroSql("input", "9", "Cep", dados.Cep!),
                    new ParametroSql("input", "100", "Setor", dados.Setor!),
                    new ParametroSql("input", "10", "Numero", dados.Numero!),
                    new ParametroSql("input", "250", "Endereco", dados.Endereco!),
                    new ParametroSql("input", "100", "Cidade", dados.Cidade!),
                    new ParametroSql("input", "100", "Bairro", dados.Bairro!),
                    new ParametroSql("input", "150", "Complemento", dados.Complemento!),
                    new ParametroSql("input", "2", "Estado", dados.Estado!),
                    new ParametroSql("input", "200", "NomePessoa", dados.NomePessoa!),
                    new ParametroSql("input", "", "DataNascimento", dados.DataNascimento!),
                    new ParametroSql("input", "14", "Cpf", dados.Cpf!),
                    new ParametroSql("input", "50", "Genero", dados.Genero!),
                    new ParametroSql("input", "14", "Telefone", dados.Telefone!),
                    new ParametroSql("input", "16", "Celular", dados.Celular!),
                    new ParametroSql("input", "1000", "Email", dados.Email!),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional007CadastroClientes]", list);

            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }
        
        public List<ClientesModel> CarregaClientesFisicosCadastrados(int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ClientesModel> dados = new List<ClientesModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpConsulta005RetornaClientesFisicosCadastrados]", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            CodCliente = int.Parse(It["Codigo"].ToString()!),
                            Cpf = It["Cpf"].ToString()!,
                            NomePessoa = It["NomePessoa"].ToString()!,
                            Celular = It["Celular"].ToString()!,
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

        public List<ClientesModel> CarregaClientesCadastrados(int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ClientesModel> dados = new List<ClientesModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpConsulta006RetornaClientesCadastrados]", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            CodCliente = int.Parse(It["Codigo"].ToString()!),
                            Cpf = It["Cpf"].ToString()!,
                            NomePessoa = It["NomePessoa"].ToString()!,
                            Celular = It["Celular"].ToString()!,
                            CodEmpresa = int.Parse(It["Codigo"].ToString()!),
                            Cnpj = It["Cnpj"].ToString()!,
                            RazaoSocial = It["RazaoSocial"].ToString()!,
                            NomeFantasia = It["NomeFantasia"].ToString()!,
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

        public List<ClientesModel> CarregaClientesJuridicosCadastrados(int CodCliente)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ClientesModel> dados = new List<ClientesModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", CodCliente),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpConsulta002RetornaClientesJuricosCadastrados]", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new()
                        {
                            CodCliente = int.Parse(It["Codigo"].ToString()!),
                            Cnpj = It["Cnpj"].ToString()!,
                            RazaoSocial = It["RazaoSocial"].ToString()!,
                            NomeFantasia = It["NomeFantasia"].ToString()!,
                            InscricaoEstadual = It["InscricaoEstadual"].ToString()!,
                            Cep = It["Cep"].ToString()!,
                            Setor = It["Setor"].ToString()!,
                            Endereco = It["Endereco"].ToString()!,
                            Numero = It["Numero"].ToString()!,
                            Cidade = It["Cidade"].ToString()!,
                            Bairro = It["Bairro"].ToString()!,
                            Complemento = It["Complemento"].ToString()!,
                            Estado = It["Estado"].ToString()!,
                            Telefone = It["Telefone"].ToString()!,
                            Celular = It["Celular"].ToString()!,
                            Email = It["Email"].ToString()!,
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

        // PUXA OS DADOS DA EMPRESA (NÃO PUXA OS DADOS DOS CLIENTES)
        public string SalvaDadosEmpresa(int CodEmpresa, ClientesModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "200", "RazaoSocial", dados.RazaoSocial!),
                    new ParametroSql("input", "200", "NomeFantasia", dados.NomeFantasia!),
                    new ParametroSql("input", "18", "Cnpj", dados.Cnpj!),
                    new ParametroSql("input", "50", "InscricaoEstadual", dados.InscricaoEstadual!),
                    new ParametroSql("input", "9", "Cep", dados.Cep!),
                    new ParametroSql("input", "100", "Setor", dados.Setor!),
                    new ParametroSql("input", "10", "Numero", dados.Numero!),
                    new ParametroSql("input", "250", "Endereco", dados.Endereco!),
                    new ParametroSql("input", "100", "Cidade", dados.Cidade!),
                    new ParametroSql("input", "100", "Bairro", dados.Bairro!),
                    new ParametroSql("input", "150", "Complemento", dados.Complemento!),
                    new ParametroSql("input", "2", "Estado", dados.Estado!),
                    new ParametroSql("input", "14", "Telefone", dados.Telefone!),
                    new ParametroSql("input", "16", "Celular", dados.Celular!),
                    new ParametroSql("input", "1000", "Email", dados.Email!),
                    new ParametroSql("input", "", "Imagem", dados.Imagem),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional010CadastraDadosEmpresa]", list);

            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public bool VerificaDadosEmpresaExistem(int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ParametroSql> parametros = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("[spOperacional011VerificaDadosEmpresa]", parametros);

                return dt != null && dt.Rows.Count > 0;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao verificar dados da empresa: {exp.Message}");
            }
        }

        public ClientesModel ObterDadosEmpresa(int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                ClientesModel dados = new ClientesModel();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa)
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpOperacional013RetornaDadosEmpresa]", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        bool IsentoBanco = false;
                        if (It["Isento"].ToString()! == "1")
                        {
                            IsentoBanco = true;
                        }
                        else
                        {
                            IsentoBanco = false;
                        }
                            dados.CodCliente = int.Parse(It["Codigo"].ToString()!);
                            dados.Cnpj = It["Cnpj"].ToString()!;
                            dados.NomeFantasia = It["NomeFantasia"].ToString()!;
                            dados.RazaoSocial = It["RazaoSocial"].ToString()!;
                            dados.InscricaoEstadual = It["InscricaoEstadual"].ToString()!;
                            dados.Cep = It["Cep"].ToString()!;
                            dados.Setor = It["Setor"].ToString()!;
                            dados.Isento = IsentoBanco;
                            dados.Endereco = It["Endereco"].ToString()!;
                            dados.Numero = It["Numero"].ToString()!;
                            dados.Cidade = It["Cidade"].ToString()!;
                            dados.Bairro = It["Bairro"].ToString()!;
                            dados.Complemento = It["Complemento"].ToString()!;
                            dados.Estado = It["Estado"].ToString()!;
                            dados.Telefone = It["Telefone"].ToString()!;
                            dados.Celular = It["Celular"].ToString()!;
                            dados.Email = It["Email"].ToString()!;
                        
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string EditaDadosEmpresa(int CodEmpresa, ClientesModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "200", "RazaoSocial", dados.RazaoSocial!),
                    new ParametroSql("input", "200", "NomeFantasia", dados.NomeFantasia!),
                    new ParametroSql("input", "18", "Cnpj", dados.Cnpj!),
                    new ParametroSql("input", "50", "InscricaoEstadual", dados.InscricaoEstadual!),
                    new ParametroSql("input", "9", "Cep", dados.Cep!),
                    new ParametroSql("input", "100", "Setor", dados.Setor!),
                    new ParametroSql("input", "10", "Numero", dados.Numero!),
                    new ParametroSql("input", "250", "Endereco", dados.Endereco!),
                    new ParametroSql("input", "100", "Cidade", dados.Cidade!),
                    new ParametroSql("input", "100", "Bairro", dados.Bairro!),
                    new ParametroSql("input", "150", "Complemento", dados.Complemento!),
                    new ParametroSql("input", "2", "Estado", dados.Estado!),
                    new ParametroSql("input", "14", "Telefone", dados.Telefone!),
                    new ParametroSql("input", "16", "Celular", dados.Celular!),
                    new ParametroSql("input", "1000", "Email", dados.Email!),
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("SpOperacional012EditarDadosEmpresa", list);

            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public List<ClientesModel> CarregaEmpresasAPrazo(int CodEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ClientesModel> dados = new List<ClientesModel>();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpConsulta007RetornaEmpresasAPrazo]", list);

                if (Dt != null)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.Add(new ClientesModel
                        {
                            CodCliente = int.Parse(It["CodCliente"].ToString()),
                            Creditos = decimal.Parse(It["Creditos"].ToString()),
                            Cnpj = It["Cnpj"].ToString(),
                            RazaoSocial = It["RazaoSocial"].ToString(),
                            NomeFantasia = It["NomeFantasia"].ToString(),
                            ValorTotalEmpresa = decimal.Parse(It["ValorTotal"].ToString()),
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

        public ClientesModel CarregaClientePorCod(int CodCliente)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                ClientesModel dados = new ClientesModel();

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodCliente", CodCliente),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                };

                DataTable Dt = contexto.ExecutaComandoRetornoData("[SpOperacional016RetornaClientePorCod]", list);

                if (Dt != null && Dt.Rows.Count > 0)
                {
                    foreach (DataRow It in Dt.Rows)
                    {
                        dados.CodCliente = int.Parse(It["Codigo"].ToString()!);
                        dados.Cnpj = It["Cnpj"].ToString()!;
                        dados.NomeFantasia = It["NomeFantasia"].ToString()!;
                        dados.RazaoSocial = It["RazaoSocial"].ToString()!;
                        dados.InscricaoEstadual = It["InscricaoEstadual"].ToString()!;
                        dados.Cep = It["Cep"].ToString()!;
                        dados.Endereco = It["Endereco"].ToString()!;
                        dados.Setor = It["Setor"].ToString()!;
                        dados.Numero = It["Numero"].ToString()!;
                        dados.Cidade = It["Cidade"].ToString()!;
                        dados.Bairro = It["Bairro"].ToString()!;
                        dados.Complemento = It["Complemento"].ToString()!;
                        dados.NomePessoa = It["NomePessoa"].ToString()!;
                        if (DateTime.TryParse(It["DataNascimento"].ToString(), out DateTime dataNascimento))
                        {
                            dados.DataNascimento = dataNascimento;
                        }
                        else
                        {
                            dados.DataNascimento = null; 
                        }
                        dados.Estado = It["Estado"].ToString()!;
                        dados.Cpf = It["Cpf"].ToString()!;
                        dados.Genero = It["Genero"].ToString()!;
                        dados.Telefone = It["Telefone"].ToString()!;
                        dados.Celular = It["Celular"].ToString()!;
                        dados.Email = It["Email"].ToString()!;
                    }
                }

                return dados;
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }


        public string EditarCliente(ClientesModel dados)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodCliente", dados.CodCliente!),
                    new ParametroSql("input", "200", "RazaoSocial", dados.RazaoSocial!),
                    new ParametroSql("input", "200", "NomeFantasia", dados.NomeFantasia!),
                    new ParametroSql("input", "18", "Cnpj", dados.Cnpj),
                    new ParametroSql("input", "50", "InscricaoEstadual", dados.InscricaoEstadual!),
                    new ParametroSql("input", "9", "Cep", dados.Cep!),
                    new ParametroSql("input", "100", "Setor", dados.Setor!),
                    new ParametroSql("input", "10", "Numero", dados.Numero!),
                    new ParametroSql("input", "250", "Endereco", dados.Endereco!),
                    new ParametroSql("input", "100", "Cidade", dados.Cidade!),
                    new ParametroSql("input", "100", "Bairro", dados.Bairro!),
                    new ParametroSql("input", "150", "Complemento", dados.Complemento!),
                    new ParametroSql("input", "2", "Estado", dados.Estado!),
                    new ParametroSql("input", "14", "Telefone", dados.Telefone!),
                    new ParametroSql("input", "14", "Cpf", dados.Cpf),
                    new ParametroSql("input", "14", "NomePessoa", dados.NomePessoa),
                    new ParametroSql("input", "14", "Genero", dados.Genero),
                    new ParametroSql("input", "14", "DataNascimento", dados.DataNascimento),
                    new ParametroSql("input", "16", "Celular", dados.Celular!),
                    new ParametroSql("input", "1000", "Email", dados.Email!),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional017EditarCliente]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string RemoverDadosEmpresa(int codEmpresa)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodCliente", codEmpresa),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional019RemoverDadosEmpresa]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string RemoverCliente(int CodCliente)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "", "CodCliente", CodCliente),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpOperacional018RemoverClienteCadastrado]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }


        public byte[] PuxaLogoEmpresa(int CodEmpresa)
        {
            byte[] imageBytes = null;

            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ParametroSql> list = new()
                {
                    new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
                };

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpOperacional013RetornaDadosEmpresa]", list);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (row["Imagem"] != DBNull.Value)
                    {
                        imageBytes = (byte[])row["Imagem"];
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao obter imagem da empresa: {exp.Message}");
            }

            return imageBytes;
        }

        public DataRow PuxaDadosEmpresa(int CodEmpresa)
        {
            DataRow row = null;
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);
                List<ParametroSql> list = new()
            {
            new ParametroSql("input", "", "CodEmpresa", CodEmpresa),
        };

                DataTable dt = contexto.ExecutaComandoRetornoData("[SpOperacional013RetornaDadosEmpresa]", list);

                if (dt != null && dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                }
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro ao obter dados da empresa: {exp.Message}");
            }
            return row;
        }






















    }
}

