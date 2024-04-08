using SGEServer.Class;
using SGEServer.Data;
using SGEServer.Models;

namespace SGEServer.Controllers
{
    public class GeralController
    {
        public string CadastrarCategoria(string categoria)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "200", "Categoria", categoria),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpParametro001CadastroCategoriaProduto]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string RemoverCategoria(int categoria)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "200", "Categoria", categoria),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpParametro002RemoverCategoriaProduto]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string CadastrarUnidadeMedida(string abreviatura, string nomeCompleto)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "10", "Abreviatura", abreviatura),
                    new ParametroSql("input", "50", "NomeCompleto", nomeCompleto),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpParametro003CadastroUnidadeMedida]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }

        public string RemoverUnidadeMedida(int unidadeMedida)
        {
            try
            {
                var contexto = new DataBase(InfoConnection.DataConect);

                List<ParametroSql> list = new()
                {

                    new ParametroSql("input", "", "CodUnidadeMedida", unidadeMedida),
                    new ParametroSql("input", "", "CodEmpresa", UserInfo.CodEmpresa),
                    new ParametroSql("output", "500", "Resp", 0),
                };

                return contexto.ExecutaComandoRetornoString("[SpParametro004RemoverUnidadeMedida]", list);
            }
            catch (Exception exp)
            {
                throw new Exception($"Erro:! {exp.Message}");
            }
        }
    }
}
