using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShellApi.Lib.Extensions;

namespace ShellApi.Lib.Models
{
    public class ProjectClass : IDatabaseObject
    {
        public int Id { get; set; }
        public String ClassName { get; set; }
        public String ShortDescription { get; set; }
        public String Description { get; set; }
        public String Namespace { get; set; }
        public String ExeternalSource { get; set; }
        public bool IsPrimitive { get; set; }
        public CustomModifiers CustomModifiers { get; set; }
        public ProjectClass BaseClass { get; set; }
        public String BaseClassName { get; set; }
        public Project Project { get; set; }
 

        public List<Interface> Interfaces { get; set; }

        public List<Property> Properties { get; set; }
        public List<Method> Methods { get; set; }

        public ProjectClass()
        {
            Interfaces = new List<Interface>();
            Properties = new List<Property>();
            Methods = new List<Method>();
        }

        public String GetBaseClassId()
        {
            if(BaseClass != null) {
                return BaseClass.Id.ToString();
            }else {
                return "null";
            }
        }

        public void ReadFrom(MySqlConnection conenction)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = String.Format("INSERT INTO projectclass(ClassName, ShortDescription, Description, Namespace, IsPrimitive, BaseClassId, ProjectId, CustomModifiersId) values('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, {7})", ClassName, ShortDescription, Description, Namespace, IsPrimitive.ToInt(), GetBaseClassId(), Project.Id, (int)CustomModifiers);
            command.ExecuteNonQuery();
            Id = (int)command.LastInsertedId;
        }
    }
}
