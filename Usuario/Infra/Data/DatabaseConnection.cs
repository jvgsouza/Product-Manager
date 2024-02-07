

using MySql.Data.MySqlClient;

namespace Usuario.Infra.Data
{
    public class DatabaseConnection : IDbConnection
    {
        public MySqlConnection getConnection()
        {
            using (MySqlConnection connection = new MySqlConnection(getConnectionString()))
            {
                return connection;
            }
        }

        public string getConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            string con = configuration[$"ConnectionStrings:Default"]!;
            return con;
        }
    }
}