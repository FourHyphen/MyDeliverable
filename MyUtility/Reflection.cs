using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility
{
    public static class Reflection
    {
        public static object DoStaticMethod(string className, string methodName, object[] args = null)
        {
            MethodInfo method = GetMethod(Type.GetType(className), methodName, args);
            return method.Invoke(null, args);
        }

        public static object DoMethod<T>(T instance, string methodName, object[] args = null) where T:class
        {
            MethodInfo method = GetMethod(instance.GetType(), methodName, args);
            return method.Invoke(instance, args);
        }

        private static MethodInfo GetMethod(Type type, string methodName, object[] args = null)
        {
            Type[] overload = GetOverloadArgs(args);
            return type.GetMethod(methodName, overload);
        }

        private static Type[] GetOverloadArgs(object[] args)
        {
            if (args == null)
            {
                return new Type[0];
            }

            Type[] overload = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var tmp = args[i];
                overload[i] = tmp.GetType();
            }

            return overload;
        }
    }
}
