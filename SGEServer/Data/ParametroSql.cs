namespace SGEServer.Data
{
    public class ParametroSql
    {
        public string directionParametro;

        public string tamanhoParametro;

        public string nomeParametro;

        public object valorParametro;

        public ParametroSql(string directionParametro, string tamanhoParametro, string nomeParametro, object valorParametro)
        {
            this.directionParametro = directionParametro;
            this.tamanhoParametro = tamanhoParametro;
            this.nomeParametro = nomeParametro;
            this.valorParametro = valorParametro;
        }
    }
}
