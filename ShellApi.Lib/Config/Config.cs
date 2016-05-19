using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellApi.Lib.Config
{
    public class Config
    {
        public String Server { get; set; }
        public String User { get; set; }
        public String Password { get; set; }
        public String Database { get; set; }

        public static Config CreateConfig(String server, String user, String password, String database)
        {
            var result = new Config();
            result.Server = server;
            result.User = user;
            result.Password = password;
            result.Database = database;

            return result;
        }

        public String GetConnectionString()
        {
            var result = String.Format("server={0};uid={1};pwd={2};database={3};", Server, User, Password, Database);
            return result;
        }
    }
}
