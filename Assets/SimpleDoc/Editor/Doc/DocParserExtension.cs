using System;
using System.Linq;
using System.Reflection;

namespace Doc
{
    public static class DocParserExtension
    {
        public static string GetTypeConstructors(this Type t)
        {
            var constructors = t.GetConstructors();
            var fullString = "";

            foreach (var constructorInfo in constructors)
            {
                fullString += $"{constructorInfo.Name}(";

                foreach (var parameter in constructorInfo.GetParameters())
                {
                    fullString += $"{parameter.ParameterType.Name} {parameter.Name}, ";
                }

                fullString += ") | ";
            }

            return fullString;
        }
        
        public static string MethodSignature(this MethodInfo mi)
        {
            string[] param = mi.GetParameters()
                .Select(p => $"{p.ParameterType.Name} {p.Name}")
                .ToArray();

            string signature = $"{mi.ReturnType.Name} {mi.Name}({string.Join(",", param)})";

            return signature;
        }
    }
}