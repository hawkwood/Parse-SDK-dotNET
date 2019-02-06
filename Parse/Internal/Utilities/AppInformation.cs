using System;
using System.IO;
using System.Reflection;

#if UNITY
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endif

namespace Parse.Internal
{
    public class AppInformation
    {
        /// <summary>
        /// The version number of the application.
        /// </summary>
        public static string Build { get; } =
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            "build 0";
#else
            Application.version; // unity override
#endif
#else
            Assembly.GetEntryAssembly().GetName().Version.Build.ToString();
#endif
        /// <summary>
        /// The version number of the application.
        /// </summary>
        public static string Version { get; } =
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            "1.0";
#else
            Application.version; // unity override
#endif
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
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            "UnityEditor";
#else
            Application.productName; // unity override
#endif
#else
            Assembly.GetEntryAssembly().GetName().Version.Build.ToString();
#endif
        /// <summary>
        /// The name of the current application.
        /// </summary>
        public static string CompanyName { get; } =
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            "Unity Technologies";
#else
            Application.companyName; // unity override
#endif
#else
            System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName;
#endif

        internal static Version ParseVersion => new AssemblyName(typeof(ParseClient).GetTypeInfo().Assembly.FullName).Version;


        public static string BasePath =>
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#else
            Application.persistentDataPath;
#endif
#else
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#endif



        public static string GetRelativeStorageFallbackPath(bool isFallback, string identifier)
        {
#if UNITY
            return Path.Combine(isFallback ? "_fallback" : "_global", $"{(isFallback ? new System.Random { }.Next().ToString() : identifier)}.cachefile");
#else
            return Path.Combine("Parse", isFallback ? "_fallback" : "_global", $"{(isFallback ? new System.Random { }.Next().ToString() : identifier)}.cachefile");
#endif
        }


        public static event Action ProcessExit
        {
#if UNITY
            add { Application.quitting += value; }
            remove { Application.quitting -= value; }
#else
            add { AppDomain.CurrentDomain.ProcessExit += (_, __) => value.Invoke(); }
            remove { AppDomain.CurrentDomain.ProcessExit -= (_, __) => value.Invoke(); }
#endif
        }


    }

}
