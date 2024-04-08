using static SGEServer.Data.DataBase;

namespace SGEServer.Data
{
    public class InfoConnection
    {
        public static string RemoteIpAddress { get; set; } = "-none-";
        public static string TokenAcesso { get; set; } = "-none-";
        public static LocalConect DataConect { get; set; }

    }
}
