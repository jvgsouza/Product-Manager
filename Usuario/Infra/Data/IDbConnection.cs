using MySql.Data.MySqlClient;

namespace Usuario.Infra.Data
{
    public interface IDbConnection
    {
        public MySqlConnection getConnection();
        public string getConnectionString();
    }
}
