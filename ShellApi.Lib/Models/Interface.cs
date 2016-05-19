using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShellApi.Lib.Models
{
    public class Interface : IDatabaseObject
    {
        public int Id { get; set; }
        public String ImplementedTypeName { get; set; }
        public ProjectClass ImplementedType { get; set; }

        public ProjectClass ProjectClass { get; set; }

        public void WriteTo(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public void ReadFrom(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = String.Format("INSERT INTO inheritinterface(ProjectClassId, InheritInterfaceId) values({0}, {1})", ImplementedType.Id, ProjectClass.Id);
            command.ExecuteNonQuery();
            Id = (int)command.LastInsertedId;
        }
    }
}
