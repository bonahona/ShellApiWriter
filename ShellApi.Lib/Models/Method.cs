using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShellApi.Lib.Models
{
    public class Method : IDatabaseObject
    {
        public int Id { get; set; }
        public String MethodName { get; set; }
        public String ShortDescription { get; set; }
        public String Description { get; set; }
        public bool IsStatic { get; set; }
        public AccessModifierType AccessModifierType { get; set; }
        public CustomModifiers CustomModifiersType { get; set; }
        public ProjectClass ProjectClass { get; set; }

        public String ReturnTypeName { get; set; }
        public ProjectClass ReturnType { get; set; }

        public List<Parameter> Parameters { get; set; }

        public Method()
        {
            Parameters = new List<Parameter>();
        }

        public void WriteTo(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = String.Format("INSERT INTO method(MethodName, ShortDescription, Description, IsStatic, AccessModifierId, CustomModifiersId, ProjectClassId, ReturnTypeId) values('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7})", MethodName, ShortDescription, Description, IsStatic, (int)AccessModifierType, (int)CustomModifiersType, ProjectClass.Id, ReturnType.Id);
            command.ExecuteNonQuery();
            Id = (int)command.LastInsertedId;
        }

        public void ReadFrom(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
