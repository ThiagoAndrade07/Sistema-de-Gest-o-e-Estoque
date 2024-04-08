using SGEServer.Class;
using SGEServer.Data;
using SGEServer.Models;
using System.Data;
using SGEServer.Auth;

namespace SGEServer.Data
{
	public class Acesso
	{
		private readonly IConfiguration _config;

		public Acesso(IConfiguration config)
		{
			_config = config;
		}

		public async Task<LoginModel> Login(DadosAcessoLogin Usr)
		{
			LoginModel Acesso = new();

			try
			{
				IEnumerable<Dictionary<string, object>> dados;

				var contexto = new DataBase(InfoConnection.DataConect);

				List<ParametroSql> list = new()
				{
					new ParametroSql("input", "", "Login", Usr.Usuario!.ToString()),
					new ParametroSql("input", "", "Senha", Usr.Password!.ToString())
				};

				dados = await Task.FromResult(contexto.ExecutaComandoRetornoDictionary("[SpSistema001EfetuaLogin]", list));

				var first = dados.FirstOrDefault();

				if (first == null) throw new Exception("Não foi possivel se comunicar com o banco");

				Acesso.CodUsuario = (int)first["Codigo"];
				Acesso.Usuario = (string)first["Usuario"];
				Acesso.Nome = (string)first["Nome"];
				Acesso.CodEmpresa = (int)first["CodEmpresa"];
				Acesso.Empresa = (string)first["Empresa"];
				Acesso.UserResposta = (string)first["Mensagem"];
			}
			catch (Exception ex)
			{
				Acesso.UserResposta = "ERRO: " + ex.Message;
			}

			return Acesso;
		}

		public LoginModel ObterLogin(int codLogin)
		{
			LoginModel Acesso = new();

			try
			{
				DataTable Dt;

				var contexto = new DataBase(InfoConnection.DataConect);

				List<ParametroSql> list = new()
				{
					new ParametroSql("input", "", "idUsuario", codLogin)
				};

				Dt = contexto.ExecutaComandoRetornoData("[SpSistema002ObterLogin]", list);

				if (Dt != null)
				{
					foreach (DataRow It in Dt.Rows)
					{
						Acesso.CodUsuario = Convert.ToInt32(It["Codigo"].ToString());
						Acesso.Usuario = It["Usuario"].ToString()!;
						Acesso.Nome = It["Nome"].ToString()!;
						Acesso.CodEmpresa = Convert.ToInt32(It["CodEmpresa"].ToString());
						Acesso.Empresa = It["Empresa"].ToString();
						Acesso.UserResposta = It["Mensagem"].ToString()!;
					}
					if (Acesso.CodUsuario > 0)
					{
						TokenService tk = new(_config);
						Acesso.Token = tk.GenerateToken(Acesso);

						// VERIFICA ACESSOS DO USUÁRIO
						//if (Acesso.AcessosLista != null)
						//{
						//	TokenService tk = new(_config);
						//	Acesso.Token = tk.GenerateToken(Acesso);
						//}
						//else
						//{
						//	Acesso.UserResposta = "ERRO: Usuário Sem acesso";
						//}
					}
				}
				else
				{
					Acesso.UserResposta = "ERRO: DADOS NÃO LOCALIZADOS";
					return Acesso;
				}
			}
			catch { throw; }
			return Acesso;
		}

		// RETORNA ACESSOS DO USUÁRIO
		//public List<ListaAcesso> ObterAcessos(int IdLogin, int centro)
		//{
		//	List<ListaAcesso> Acessos = new();

		//	try
		//	{
		//		DataTable Dt;

		//		var contexto = new DataBase(InfoConnection.DataConect);
		//		List<ParametroSql> list = new()
		//		{
		//			new ParametroSql("input", "", "Usuario", IdLogin),
		//			new ParametroSql("input", "", "Centro", centro)
		//		};

		//		Dt = contexto.ExecutaComandoRetornoData("PwebSistema002carregaMenu", list);

		//		if (Dt != null)
		//		{
		//			foreach (DataRow It in Dt.Rows)
		//			{
		//				ListaAcesso linha = new()
		//				{
		//					Id = Convert.ToInt32(It["Codigo"].ToString()),
		//					Descricao = It["Pagina"].ToString()!,
		//					Endereco = It["Endereco"].ToString()!,
		//					PaginaPai = Convert.ToInt32(It["PagPai"].ToString()),
		//					Ordem = Convert.ToInt32(It["Ordem"].ToString()),
		//					Cor = It["Cor"].ToString()!,
		//					Icone = It["Icone"].ToString()!,
		//					Rota = It["Rota"].ToString()!,
		//					ModuloSistemico = It["Modulo"].ToString()!,
		//				};
		//				Acessos.Add(linha);
		//			}
		//			return Acessos;
		//		}
		//		else
		//		{
		//			return Acessos;
		//		}
		//	}
		//	catch { throw; }
		//}
	}
}
