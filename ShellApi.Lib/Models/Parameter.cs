using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShellApi.Lib.Models
{
    public class Parameter : IDatabaseObject
    {
        public int Id { get; set; }
        public String ParameterName { get; set; }
        public String DefaultValue { get; set; }
        public Method Method { get; set; }
        public String TypeName { get; set; }
        public ProjectClass Type { get; set; }

        public void WriteTo(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = String.Format("INSERT INTO parameter(ParameterName, DefaultValue, MethodId, TypeId) values('{0}', '{1}', {2}, {3})", ParameterName, DefaultValue, Method.Id, Type.Id);
            command.ExecuteNonQuery();
            Id = (int)command.LastInsertedId;
        }

        public void ReadFrom(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
