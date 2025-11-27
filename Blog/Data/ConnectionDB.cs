using Microsoft.Data.SqlClient;

namespace Blog.API.Data
{
    public class ConnectionDB
    {
        private readonly string _connectionString; // usa o readonly pois esta propriedade não vai poder ser alterado
                                                   // após a se dar um valor


        public  ConnectionDB(IConfiguration configuration) // IConfiguration=Aonde fica conexões externas incluindo a conexão com o DB
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");  // endereçamento do BD, adicionado no arquivo appsettings
        }


        // Função que vai ser receber minha conexão com o banco de dados
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
