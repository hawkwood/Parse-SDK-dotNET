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
        public static string Build
        {
            get =>
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            "build 0";
        //Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
#else
        Application.version; // unity override
        //Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
#endif
#else
            Assembly.GetEntryAssembly().GetName().Version.Build.ToString();
#endif
        }
        /// <summary>
        /// The version number of the application.
        /// </summary>
        public static string Version
        {
            get =>
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            "1.0";
        //Assembly.GetExecutingAssembly().GetName().Version.ToString();
#else
        Application.version; // unity override
        //Assembly.GetExecutingAssembly().GetName().Version.ToString();
#endif
#else
            Assembly.GetEntryAssembly().GetName().Version.ToString();
#endif
        }
        // TODO: Verify if this means Parse appId or just a unique identifier.

        /// <summary>
        /// The identifier of the application
        /// </summary>
        public static string Identifier => AppDomain.CurrentDomain.FriendlyName;

        /// <summary>
        /// The name of the current application.
        /// </summary>
        public static string Name
        {
            get =>
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            "UnityEditor";
#else
            Application.productName; // unity override
#endif
#else
            Assembly.GetEntryAssembly().GetName().Name;
#endif
        }
        /// <summary>
        /// The name of the current application.
        /// </summary>
        public static string CompanyName
        {
            get =>
#if UNITY || UNITY_EDITOR
#if UNITY_EDITOR
            "Unity Technologies";
             //System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).CompanyName;
#else
            Application.companyName; // unity override
             //System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).CompanyName;
#endif
#else
            System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName;
#endif
        }
        internal static Version ParseVersion => new AssemblyName(typeof(ParseClient).GetTypeInfo().Assembly.FullName).Version;

        /// <summary>
        /// The base path to parse caching folder location
        /// </summary>
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
            return Path.Combine("Parse", isFallback ? "_fallback" : "_global", $"{(isFallback ? new System.Random { }.Next().ToString() : identifier)}.cachefile");
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
