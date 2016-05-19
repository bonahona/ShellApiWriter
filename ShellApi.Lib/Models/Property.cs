using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShellApi.Lib.Models
{
    public class Property : IDatabaseObject
    {
        public int Id { get; set; }
        public String PropertyName { get; set; }
        public String ShortDescription { get; set; }
        public String Description { get; set; }
        public bool IsStatic { get; set; }
        public AccessModifierType AccessModifierType { get; set; }
        public CustomModifiers CustomModifiersId { get; set; }
        public ProjectClass ProjectClass { get; set; }
        public String TypeName { get; set; }
        public ProjectClass Type { get; set; }

        public void WriteTo(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = String.Format("INSERT INTO property(PropertyName, ShortDescription, Description, IsStatic, AccessModifierId, CustomModifiersId, ProjectClassId, TypeId) values('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7})", PropertyName, ShortDescription, Description, IsStatic, (int)AccessModifierType, (int)CustomModifiersId, ProjectClass.Id, Type.Id);
            command.ExecuteNonQuery();
            Id = (int)command.LastInsertedId;
        }

        public void ReadFrom(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
