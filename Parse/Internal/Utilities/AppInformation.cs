using System;
using System.IO;
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
        /// <summary>
        /// The name of the current application.
        /// </summary>
        public static string CompanyName { get; } =
#if UNITY
            UnityEngine.Application.companyName; // unity override
#else
            System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName;
#endif

        internal static Version ParseVersion => new AssemblyName(typeof(ParseClient).GetTypeInfo().Assembly.FullName).Version;


        public static string BasePath =>
#if UNITY
            UnityEngine.Application.persistentDataPath;
#else
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#endif



        public static string GetRelativeStorageFallbackPath(bool isFallback, string identifier)
        {
#if UNITY
            UnityEngine.Debug.LogWarning("GetRelativeStorageFallbackPath called");
            return Path.Combine(isFallback ? "_fallback" : "_global", $"{(isFallback ? new Random { }.Next().ToString() : identifier)}.cachefile");
#else
            return Path.Combine("Parse", isFallback ? "_fallback" : "_global", $"{(isFallback ? new Random { }.Next().ToString() : identifier)}.cachefile");
#endif
        }


        public static event Action ProcessExit
        {
#if UNITY
            add { UnityEngine.Application.quitting += value; }
            remove { UnityEngine.Application.quitting -= value; }
#else
            add { AppDomain.CurrentDomain.ProcessExit += (_, __) => value.Invoke(); }
            remove { AppDomain.CurrentDomain.ProcessExit -= (_, __) => value.Invoke(); }
#endif
        }


    }

    }
