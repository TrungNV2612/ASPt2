using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T12V2.DTO
{
    internal class DbConnector
    {
        public string Database { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public DbConnector()
        {
            Server = "CNTT-TRUNGNV-LAP";
            Database = "MANAGER";
            User = "sa";
            Password = "@Automation1";
        }

        public DbConnector(string database, string server, string user, string password)
        {
            Database = database;
            Server = server;
            User = user;
            Password = password;
        }

        public SqlConnection GetConnection()
        {
            string connStr = BuildConnectionString();
            return new SqlConnection(connStr);
        }

        private string BuildConnectionString()
        {
            return string.Format("Data Source={0},1433;Initial Catalog={1};User Id={2};Password={3}",
                Server, Database, User, Password);
        }
    }
}
