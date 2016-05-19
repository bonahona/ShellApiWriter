using ShellApi.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShellApi.Lib.Helpers
{
    public class ProjectHelper : IDatabaseObject
    {
        public HashSet<ProjectClass> ProjectClasses { get; set; }
        public HashSet<Method> Methods { get; set; }
        public HashSet<Parameter> Parameters { get; set; }
        public HashSet<Property> Properties { get; set; }
        public HashSet<Interface> Interfaces { get; set; }

        public ProjectHelper()
        {
            ProjectClasses = new HashSet<ProjectClass>();
            Methods = new HashSet<Method>();
            Parameters = new HashSet<Parameter>();
            Properties = new HashSet<Property>();
            Interfaces = new HashSet<Interface>();
        }

        public void WriteTo(MySqlConnection connection)
        {
            foreach (var projectClass in ProjectClasses) {
                projectClass.WriteTo(connection);
            }

            foreach (var method in Methods) {
                method.WriteTo(connection);
            }

            foreach(var parameter in Parameters) {
                parameter.WriteTo(connection);
            }

            foreach (var property in Properties) {
                property.WriteTo(connection);
            }

            foreach(var interfaceItem in Interfaces) {
                interfaceItem.WriteTo(connection);
            }
        }

        public void ReadFrom(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
