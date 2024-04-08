using SGEServer.Data;
using SGEServer.Models;
using System.Data;

namespace SGEServer.Controllers
{
	public class LoginController_old
	{
		public string EfetuarLogin(string user, string password)
		{
			try
			{
				var contexto = new DataBase(InfoConnection.DataConect);

				List<ParametroSql> list = new()
				{

					new ParametroSql("input", "50", "Usuario", user),
					new ParametroSql("input", "2000", "Senha", password),
					new ParametroSql("output", "500", "Resp", 0),
				};

				return contexto.ExecutaComandoRetornoString("[SpSistema001EfetuarLogin]", list);
			}
			catch (Exception exp)
			{
				throw new Exception($"Erro:! {exp.Message}");
			}
		}

		public LoginModel ObterDadosUsuario(string user)
		{
			try
			{
				var contexto = new DataBase(InfoConnection.DataConect);
				LoginModel dados = new LoginModel();

				List<ParametroSql> list = new()
				{
					new ParametroSql("input", "50", "Usuario", user),
				};

				DataTable Dt = contexto.ExecutaComandoRetornoData("[SpSistema002ObterDadosUsuario]", list);

				if (Dt != null)
				{
					foreach (DataRow It in Dt.Rows)
					{
						dados.CodUsuario = int.Parse(It["CodUsuario"].ToString()!);
						dados.Nome = It["Nome"].ToString()!;
						dados.CodEmpresa = int.Parse(It["CodEmpresa"].ToString()!);
					}
				}

				return dados;
			}
			catch (Exception exp)
			{
				throw new Exception($"Erro:! {exp.Message}");
			}
		}
	}
}
