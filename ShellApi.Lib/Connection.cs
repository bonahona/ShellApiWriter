using ShellApi.Lib.Helpers;
using ShellApi.Lib.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShellApi.Lib
{
    public static class Connection
    {
        public static void DescribeAssembly(Assembly assembly, Config.Config config)
        {
            var connection = new MySql.Data.MySqlClient.MySqlConnection();
            connection.ConnectionString = config.GetConnectionString();
            connection.Open();

            var project = ModelAnalyser.DescribeProject(assembly);
            project.ResolveInternalClassNames();
            project.WriteTo(connection);
        }
    }
}
