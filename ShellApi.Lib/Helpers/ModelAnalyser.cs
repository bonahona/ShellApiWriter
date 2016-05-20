using ShellApi.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShellApi.Lib.Helpers
{
    public static class ModelAnalyser
    {
        public static Project DescribeProject(Assembly assembly)
        {
            var result = new Project();

            foreach(var type in assembly.DefinedTypes) {
                result.ProjectClasses.Add(DescribeProjectClass(type, result));
            }

            return result;
        }

        public static ProjectClass DescribeProjectClass(Type type, Project project)
        {
            var result = new ProjectClass();
            result.Project = project;

            result.ClassName = type.Name;
            
            if(type.BaseType != null) {
                result.BaseClassName = type.BaseType.Name;
            }

            if (type.IsInterface) {
                result.CustomModifiers = CustomModifiers.Interface;
            } else if (type.IsAbstract) {
                result.CustomModifiers = CustomModifiers.Abstract;
            } else {
                result.CustomModifiers = CustomModifiers.None;
            }

            result.Namespace = type.Namespace;

            foreach (var interfaceItem in type.GetInterfaces()) {
                result.Interfaces.Add(DescribeInterface(interfaceItem, result));
            }

            foreach(var property in type.GetProperties()) {

                var describedProperty = DescribeProperty(property, type, result);

                if (describedProperty != null) {
                    result.Properties.Add(DescribeProperty(property, type, result));
                }
            }

            foreach(var field in type.GetFields()) {

                var descibedField = DescribeField(field, type, result);

                if (descibedField != null) {
                    result.Properties.Add(descibedField);
                }
            }

            foreach (var method in type.GetMethods()) {
                var describedMethod = DescribeMethod(method, type, result);

                if (describedMethod != null) {
                    result.Methods.Add(describedMethod);
                }
            }

            return result;
        }

        public static Interface DescribeInterface(Type type, ProjectClass projectClass)
        {
            var result = new Interface();
            result.ProjectClass = projectClass;

            result.ImplementedTypeName = type.Name;

            return result;
        }

        public static Property DescribeProperty(PropertyInfo property, Type type, ProjectClass projectClass)
        {
            if(property.DeclaringType != type) {
                return null;
            }

            var result = new Property();
            result.ProjectClass = projectClass;
            result.PropertyName = property.Name;
            result.TypeName = property.PropertyType.Name;

            var isStatic = false;
            var highestModifier = AccessModifierType.Private;
            foreach(var setter in property.GetAccessors()) {
                if (setter.IsStatic) {
                    isStatic = true;
                }

                if (setter.IsPublic) {
                    highestModifier = AccessModifierType.Public;
                } else if (setter.IsFamily) {
                    highestModifier = AccessModifierType.Protected;
                } else if (setter.IsPrivate) {
                    highestModifier = AccessModifierType.Private;
                }
            }

            result.IsStatic = isStatic;
            result.AccessModifierType = highestModifier;

            return result;
        }

        public static Property DescribeField(FieldInfo field, Type type, ProjectClass projectClass)
        {
            var result = new Property();
            result.ProjectClass = projectClass;
            result.PropertyName = field.Name;
            result.TypeName = field.FieldType.Name;
            result.IsStatic = field.IsStatic;

            if (field.IsPrivate) {
                result.AccessModifierType = AccessModifierType.Private;
            }else if (field.IsFamily) {
                result.AccessModifierType = AccessModifierType.Protected;
            }else if (field.IsPublic) {
                result.AccessModifierType = AccessModifierType.Public;
            }
           
            return result;
        }

        public static Method DescribeMethod(MethodInfo method, Type type, ProjectClass projectClass)
        {
            if(method.DeclaringType != type) {
                return null;
            }

            var result = new Method();
            result.ProjectClass = projectClass;
            result.MethodName = method.Name;
            result.IsStatic = method.IsStatic;
            result.ReturnTypeName = method.ReturnType.Name;

            if (method.IsPrivate) {
                result.AccessModifierType = AccessModifierType.Private;
            } else if (method.IsFamily) {
                result.AccessModifierType = AccessModifierType.Protected;
            } else if (method.IsPublic) {
                result.AccessModifierType = AccessModifierType.Public;
            }

            if (method.IsAbstract) {
                result.CustomModifiersType = CustomModifiers.Abstract;
            }

            foreach (var parameter in method.GetParameters()) {
                result.Parameters.Add(DescribeParameter(parameter, result));
            }

            return result;
        }

        public static Parameter DescribeParameter(ParameterInfo parameterInfo, Method method)
        {
            var result = new Parameter();
            result.Method = method;
            result.ParameterName = parameterInfo.Name;
            result.DefaultValue = parameterInfo.DefaultValue.ToString();
            result.TypeName = parameterInfo.ParameterType.Name;

            return result;
        }
    }
}
