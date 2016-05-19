using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellApi.Lib.Models
{
    interface IDatabaseObject
    {
        void WriteTo(MySql.Data.MySqlClient.MySqlConnection connection);
        void ReadFrom(MySql.Data.MySqlClient.MySqlConnection connection);
    }
}
