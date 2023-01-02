using System;
using System.Reflection;

namespace BatzUtils;

// Author : Batzpup
public static class ReflectionHelper
{
        
        public static FieldInfo GetPrivateNonStaticField(object instance, string fieldName)
        {
            return instance.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static MethodInfo GetPrivateNonStaticMethod(object instance, string methodName)
        {
            return instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static MethodInfo GetPrivateStaticMethod(Type staticClass, string methodName)
        {
            return staticClass.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        }
        public static FieldInfo GetPrivateStaticField(Type instance, string fieldName)
        {
            return instance.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic);
        }
}
