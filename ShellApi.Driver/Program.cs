using ShellApi.Lib.Config;
using ShellApi.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellApi.Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            var  connection = new MySql.Data.MySqlClient.MySqlConnection();
            connection.ConnectionString = Config.CreateConfig("127.0.0.1", "root", "", "documentation").GetConnectionString();
            connection.Open();

            var test = ModelAnalyser.DescribeProject(typeof(Program).Assembly);
            test.ResolveInternalClassNames();
            test.WriteTo(connection);

            Console.Read();
        }

        public void MyMethod()
        {

        }

        public int TEsting(String type = "Bona")
        {
            return 2;
        }
    }
}
