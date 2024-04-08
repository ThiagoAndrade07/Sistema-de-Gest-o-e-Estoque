using System.ComponentModel.DataAnnotations;

namespace SGEServer.Models
{
	public class LoginModel
	{
		public int CodUsuario { get; set; }
        public string? Usuario { get; set; }
        public string? Nome { get; set; }
		public int CodEmpresa { get; set; }
        public string? Empresa { get; set; }
        public string? UserResposta { get; set; }
        public string? Token { get; set; }
	}

	public class DadosAcessoLogin
	{
		public string? Usuario { get; set; }
		public string? Password { get; set; }
	}
}
