﻿#region Mr. Advice
// Mr. Advice
// A simple post build weaving package
// http://mradvice.arxone.com/
// Released under MIT license http://opensource.org/licenses/mit-license.php
#endregion

namespace ArxOne.MrAdvice.Utility
{
    using System;
    using System.Linq;
    using System.Reflection;
    using IO;
    using Mono.Cecil;

    internal static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="typeReference">The type reference.</param>
        /// <returns></returns>
        public static Type GetType(this Assembly assembly, TypeReference typeReference)
        {
            try
            {
                var fullName = typeReference.FullName.Replace('/', '+');
                var type = assembly.GetTypes().First(t => t.FullName == fullName);
                return type;
            }
            catch (ReflectionTypeLoadException e)
            {
                Logger.WriteError("Error while load types from {0}: {1}\n{2}", assembly.FullName, e.ToString(),
                   string.Join(Environment.NewLine + "------------" + Environment.NewLine, e.LoaderExceptions.Select(le => le.ToString())));
                throw;
            }
        }
    }
}