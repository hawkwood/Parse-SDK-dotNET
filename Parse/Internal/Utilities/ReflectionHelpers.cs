// Copyright (c) 2015-present, Parse, LLC.  All rights reserved.  This source code is licensed under the BSD-style license found in the LICENSE file in the root directory of this source tree.  An additional grant of patent rights can be found in the PATENTS file in the same directory.

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Parse.Common.Internal
{
    public static class ReflectionHelpers
    {
        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetRuntimeProperties();
        }

        public static MethodInfo GetMethod(Type type, string name, Type[] parameters)
        {
            return type.GetRuntimeMethod(name, parameters);
        }

        public static bool IsPrimitive(Type type)
        {
            return type.GetTypeInfo().IsPrimitive;
        }

        public static IEnumerable<Type> GetInterfaces(Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }

        public static bool IsConstructedGenericType(Type type)
        {
            return type.IsConstructedGenericType;
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors
              .Where(c => (c.Attributes & MethodAttributes.Static) == 0);
        }

        public static Type[] GetGenericTypeArguments(Type type)
        {
            return type.GenericTypeArguments;
        }

        public static PropertyInfo GetProperty(Type type, string name)
        {
            return type.GetRuntimeProperty(name);
        }

        /// <summary>
        /// This method helps simplify the process of getting a constructor for a type.
        /// A method like this exists in .NET but is not allowed in a Portable Class Library,
        /// so we've built our own.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parameterTypes"></param>
        /// <returns></returns>
        public static ConstructorInfo FindConstructor(this Type self, params Type[] parameterTypes)
        {
            var constructors =
              from constructor in GetConstructors(self)
              let parameters = constructor.GetParameters()
              let types = from p in parameters select p.ParameterType
              where types.SequenceEqual(parameterTypes)
              select constructor;
            return constructors.SingleOrDefault();
        }

        public static bool IsNullable(Type t)
        {
            bool isGeneric = t.IsConstructedGenericType;
            return isGeneric && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

    }
}
