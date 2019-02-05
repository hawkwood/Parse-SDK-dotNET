using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Parse.Internal
{
    public class AppInformation
    {
        /// <summary>
        /// The version number of the application.
        /// </summary>
        public static string Build { get; } = Assembly.GetEntryAssembly().GetName().Version.Build.ToString();
        /// <summary>
        /// The version number of the application.
        /// </summary>
        public static string Version { get; } = Assembly.GetEntryAssembly().GetName().Version.ToString();

        // TODO: Verify if this means Parse appId or just a unique identifier.

        /// <summary>
        /// The identifier of the application
        /// </summary>
        public string AppIdentifier => AppDomain.CurrentDomain.FriendlyName;

        /// <summary>
        /// The name of the current application.
        /// </summary>
        public static string Name { get; } = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

        internal static Version ParseVersion => new AssemblyName(typeof(ParseClient).GetTypeInfo().Assembly.FullName).Version;
    }

}
