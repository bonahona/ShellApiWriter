using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShellApi.Lib.Helpers;

namespace ShellApi.Lib.Models
{
    public class Project : IDatabaseObject
    {
        public int Id { get; set; }
        public int ProjectCategoryId { get; set; }
        public int ProjectLanguageId { get; set; }
        public String ProjectName { get; set; }
        public String ShortDescription { get; set; }
        public String Description { get; set; }

        public List<ProjectClass> ProjectClasses { get; set; }

        public Project()
        {
            ProjectName = "UnnamedProject";
            ProjectClasses = new List<ProjectClass>();
        }

        public void ResolveInternalClassNames()
        {
            var classes = new Dictionary<String, ProjectClass>();

            foreach(var projectClass in ProjectClasses) {
                classes.Add(projectClass.ClassName, projectClass);
            }

            foreach(var projectClass in ProjectClasses.ToList()) {
                if (!String.IsNullOrEmpty(projectClass.BaseClassName)) {
                    if (!classes.ContainsKey(projectClass.BaseClassName)) {
                        classes.Add(projectClass.BaseClassName, GeneratePrimitiveClass(projectClass.BaseClassName));
                    }

                    projectClass.BaseClass = classes[projectClass.BaseClassName];
                }

                foreach (var interfaceItem in projectClass.Interfaces) {
                    interfaceItem.ImplementedType = GetClassFromClassName(interfaceItem.ImplementedTypeName, classes);
                }

                foreach(var method in projectClass.Methods) {
                    method.ReturnType = GetClassFromClassName(method.ReturnTypeName, classes);

                    foreach(var parameter in method.Parameters) {
                        parameter.Type = GetClassFromClassName(parameter.TypeName, classes);
                    }
                }

                foreach (var property in projectClass.Properties) {
                    property.Type = GetClassFromClassName(property.TypeName, classes);
                }
            }
        }

        public ProjectClass GetClassFromClassName(String className, Dictionary<String, ProjectClass> classes)
        {
            if (!classes.ContainsKey(className)) {
                classes.Add(className, GeneratePrimitiveClass(className));
            }

            return classes[className];
        }

        public ProjectClass GeneratePrimitiveClass(String className)
        {
            var result = new ProjectClass();
            result.ClassName = className;
            result.IsPrimitive = true;
            result.Project = this;

            ProjectClasses.Add(result);

            return result;
        }

        public void ReadFrom(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(MySqlConnection connection)
        {
            TurnOffKeyConstraints(connection);

            var command = connection.CreateCommand();
            command.CommandText = String.Format("INSERT INTO project(ProjectName, ShortDescription, Description) values('{0}', '{1}', '{2}')", ProjectName, ShortDescription, Description);
            command.ExecuteNonQuery();
            Id = (int)command.LastInsertedId;

            var flatProject = GetFlatProject();
            flatProject.WriteTo(connection);

            TurnOnKeyConstraints(connection);
        }

        public ProjectHelper GetFlatProject()
        {
            var result = new ProjectHelper();

            foreach(var projectClass in ProjectClasses) {
                result.ProjectClasses.Add(projectClass);

                if(projectClass.BaseClass != null) {
                    result.ProjectClasses.Add(projectClass.BaseClass);
                }

                foreach (var interfaceItem in projectClass.Interfaces) {
                    result.ProjectClasses.Add(interfaceItem.ImplementedType);
                }

                foreach(var method in projectClass.Methods) {
                    result.Methods.Add(method);
                    result.ProjectClasses.Add(method.ReturnType);

                    foreach (var parameter in method.Parameters) {
                        result.Parameters.Add(parameter);
                        result.ProjectClasses.Add(parameter.Type);
                    }
                }

                foreach (var property in projectClass.Properties) {
                    result.Properties.Add(property);
                    result.ProjectClasses.Add( property.Type);
                }
            }

            return result;
        }

        public void TurnOffKeyConstraints(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SET FOREIGN_KEY_CHECKS = 0";
            command.ExecuteNonQuery();
        }

        public void TurnOnKeyConstraints(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SET FOREIGN_KEY_CHECKS = 1";
            command.ExecuteNonQuery();
        }
    }
}
