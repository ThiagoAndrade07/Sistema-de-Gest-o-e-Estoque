using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SGEServer.Data
{
    public class DataBase
    {
        public enum LocalConect
        {
            QA,
            Fercamp
        }

        public SqlConnection Conexao;

        private byte[] Key { get; set; }

        private byte[] IniVetor { get; set; }

        private Aes Algorithm { get; set; }

        private DataBase()
        {
        }

        public DataBase(LocalConect local)
        {
            string entryText = "";
            if (local == LocalConect.QA)
            {
                entryText = "Persist Security Info=True;Initial Catalog=Gestao_De_Estoque;Data Source=sistemagestaoestoque.database.windows.net;User ID=AzureSA;Password=hope@core2023;Connect Timeout=180;";
            }

            if (local == LocalConect.Fercamp)
            {
                entryText = "Persist Security Info=True;Initial Catalog=SGE_Fercamp;Data Source=sistemagestaoestoque.database.windows.net;User ID=AzureSA;Password=hope@core2023;Connect Timeout=180;";
            }

            byte[] key = new byte[16]
            {
                12, 2, 56, 117, 12, 67, 33, 23, 12, 2,
                56, 117, 12, 67, 33, 24
            };
            DataBase dataBase = new DataBase(key, "");
            //string connectionString = dataBase.Decrypt(entryText);
            string connectionString = entryText;
            Conexao = new SqlConnection(connectionString);
        }

        private DataBase(byte[] key, string Value)
        {
            Key = key;
            IniVetor = new byte[16]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16
            };
            Algorithm = Aes.Create();
        }

        private string Decrypt(string entryText)
        {
            byte[] array = Convert.FromBase64String(entryText);
            byte[] bytes;
            using (ICryptoTransform transform = Algorithm.CreateDecryptor(Key, IniVetor))
            {
                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(array, 0, array.Length);
                cryptoStream.FlushFinalBlock();
                bytes = memoryStream.ToArray();
            }

            Algorithm.Dispose();
            return Encoding.Default.GetString(bytes);
        }

        private static void AdicionarParamatros(SqlCommand cmdComando, Dictionary<string, object> parametros)
        {
            if (parametros == null)
            {
                return;
            }

            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                SqlParameter sqlParameter = cmdComando.CreateParameter();
                sqlParameter.ParameterName = parametro.Key;
                sqlParameter.Value = parametro.Value ?? DBNull.Value;
                cmdComando.Parameters.Add(sqlParameter);
            }
        }

        private void AbrirConexao()
        {
            if (Conexao.State != ConnectionState.Open)
            {
                Conexao.Open();
            }
        }

        private void FecharConexao()
        {
            if (Conexao.State == ConnectionState.Open)
            {
                Conexao.Close();
                Conexao.Dispose();
            }
        }

        private SqlCommand CriarComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            SqlCommand sqlCommand = Conexao.CreateCommand();
            sqlCommand.CommandText = comandoSQL;
            AdicionarParamatros(sqlCommand, parametros);
            return sqlCommand;
        }

        public int ExecutaComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            int result = 0;
            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comando SQL não pode ser nulo ou vazio");
            }

            try
            {
                AbrirConexao();
                SqlCommand sqlCommand = CriarComando(comandoSQL, parametros);
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                FecharConexao();
            }

            return result;
        }

        public string ExecutaComandoRetornoString(string comandoSQL, List<ParametroSql> parametros)
        {
            List<string> list = new List<string>();
            string text = string.Empty;
            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comando SQL não pode ser nulo ou vazio");
            }

            try
            {
                AbrirConexao();
                using SqlCommand sqlCommand = new SqlCommand(comandoSQL.ToString(), Conexao)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (ParametroSql parametro in parametros)
                {
                    if (parametro.directionParametro.ToString() == "output")
                    {
                        sqlCommand.Parameters.Add("@" + parametro.nomeParametro.ToString(), SqlDbType.NVarChar, int.Parse(parametro.tamanhoParametro)).Direction = ParameterDirection.Output;
                        list.Add(parametro.nomeParametro.ToString());
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@" + parametro.nomeParametro.ToString(), parametro.valorParametro);
                    }
                }

                sqlCommand.ExecuteNonQuery();
                foreach (string item in list)
                {
                    text = sqlCommand.Parameters["@" + item.ToString()].Value.ToString() + "|" + text;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                FecharConexao();
            }

            return text;
        }

        public DataTable ExecutaComandoRetornoData(string comandoSQL, List<ParametroSql> parametros)
        {
            DataTable dataTable = new DataTable();
            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comando SQL não pode ser nulo ou vazio");
            }

            try
            {
                AbrirConexao();
                using SqlCommand sqlCommand = new SqlCommand(comandoSQL.ToString(), Conexao)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (parametros != null)
                {
                    foreach (ParametroSql parametro in parametros)
                    {
                        sqlCommand.Parameters.AddWithValue("@" + parametro.nomeParametro.ToString(), parametro.valorParametro);
                        if (parametro.directionParametro.ToString() == "input")
                        {
                            sqlCommand.Parameters["@" + parametro.nomeParametro.ToString()].Direction = ParameterDirection.Input;
                        }
                    }
                }

                using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                FecharConexao();
            }

            return dataTable;
        }

        public byte[]? ExecutaComandoRetornaImagem(string comandoSQL, List<ParametroSql> parametros)
        {
            byte[] result = null;
            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comando SQL não pode ser nulo ou vazio");
            }

            try
            {
                AbrirConexao();
                using SqlCommand sqlCommand = new SqlCommand(comandoSQL.ToString(), Conexao)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (ParametroSql parametro in parametros)
                {
                    if (parametro.directionParametro.ToString() == "input")
                    {
                        sqlCommand.Parameters.AddWithValue("@" + parametro.nomeParametro.ToString(), parametro.valorParametro);
                    }
                }

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    result = (byte[])sqlDataReader["IMAGEM"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                FecharConexao();
            }

            return result;
        }

        public string ExecutaComandoRetornaXML(string comandoSQL, List<ParametroSql> parametros)
        {
            string result = null;
            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comando SQL não pode ser nulo ou vazio");
            }

            try
            {
                AbrirConexao();
                using SqlCommand sqlCommand = new SqlCommand(comandoSQL.ToString(), Conexao)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (ParametroSql parametro in parametros)
                {
                    if (parametro.directionParametro.ToString() == "input")
                    {
                        sqlCommand.Parameters.AddWithValue("@" + parametro.nomeParametro.ToString(), parametro.valorParametro);
                    }
                }

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    result = sqlDataReader["IMAGEM"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                FecharConexao();
            }

            return result;
        }

        public List<Dictionary<string, object>> ExecutaComandoRetornoDictionary(string comandoSQL, List<ParametroSql> parametros)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comando SQL não pode ser nulo ou vazio");
            }

            List<Dictionary<string, object>> list2;
            try
            {
                AbrirConexao();
                using SqlCommand sqlCommand = new SqlCommand(comandoSQL.ToString(), Conexao)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (parametros != null)
                {
                    foreach (ParametroSql parametro in parametros)
                    {
                        sqlCommand.Parameters.AddWithValue("@" + parametro.nomeParametro.ToString(), parametro.valorParametro);
                        if (parametro.directionParametro.ToString() == "output")
                        {
                            sqlCommand.Parameters["@" + parametro.nomeParametro.ToString()].Direction = ParameterDirection.Output;
                            list.Add(parametro.nomeParametro.ToString());
                        }
                        else
                        {
                            sqlCommand.Parameters["@" + parametro.nomeParametro.ToString()].Direction = ParameterDirection.Input;
                        }
                    }
                }

                using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                list2 = new List<Dictionary<string, object>>();
                while (sqlDataReader.Read())
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        string name = sqlDataReader.GetName(i);
                        object obj = sqlDataReader.GetValue(i);
                        if (obj == DBNull.Value)
                        {
                            obj = string.Empty;
                        }

                        dictionary.Add(name, obj);
                    }

                    list2.Add(dictionary);
                    dictionary = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                FecharConexao();
            }

            return list2;
        }
    }
}
