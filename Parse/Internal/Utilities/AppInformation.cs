using System;
using System.Reflection;

namespace Parse.Internal
{
    public class AppInformation
    {
        /// <summary>
        /// The version number of the application.
        /// </summary>
        public static string Build { get; } =
#if UNITY
          UnityEngine.Application.version; // unity override
#else
            Assembly.GetEntryAssembly().GetName().Version.Build.ToString();
#endif
        /// <summary>
        /// The version number of the application.
        /// </summary>
        public static string Version { get; } =
#if UNITY
            UnityEngine.Application.version; // unity override
#else
        Assembly.GetEntryAssembly().GetName().Version.Build.ToString();
#endif
        // TODO: Verify if this means Parse appId or just a unique identifier.

        /// <summary>
        /// The identifier of the application
        /// </summary>
        public static string Identifier => AppDomain.CurrentDomain.FriendlyName;

        /// <summary>
        /// The name of the current application.
        /// </summary>
        public static string Name { get; } =
#if UNITY
            UnityEngine.Application.productName; // unity override
#else
        Assembly.GetEntryAssembly().GetName().Version.Build.ToString();
#endif

        internal static Version ParseVersion => new AssemblyName(typeof(ParseClient).GetTypeInfo().Assembly.FullName).Version;

    }

}
