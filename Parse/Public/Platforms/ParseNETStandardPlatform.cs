using System;
using System.IO;
using System.Reflection;

namespace Parse.Platforms
{
    /// <summary>
    /// Base implementation for the <see cref="IParseTargetPlatform"/> interface to provide platform specific data for .NET Standard target platforms.
    /// </summary>
    public class ParseNETStandard : IParseTargetPlatform
    {

        /// <summary>
        /// The version number of the application.
        /// </summary>
        public virtual string BuildVersion
        {
            get => Assembly.GetEntryAssembly().GetName().Version.Build.ToString();
        }
        /// <summary>
        /// The version number of the application.
        /// </summary>
        public virtual string DisplayVersion
        {
            get => Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// The identifier of the application
        /// </summary>
        public virtual string Identifier => AppDomain.CurrentDomain.FriendlyName;

        /// <summary>
        /// The name of the current application.
        /// </summary>
        public virtual string Name
        {
            get => Assembly.GetEntryAssembly().GetName().Name;
        }
        /// <summary>
        /// The name of the current application.
        /// </summary>
        public virtual string CompanyName
        {
            get => System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName;
        }

        /// <summary>
        /// The base path to parse caching folder location
        /// </summary>
        public virtual string BasePath
        {
            get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        /// <summary>
        /// gets the relative identifier-based storage fallback path 
        /// </summary>
        /// <param name="isFallback">should the resulting path be used as a fallback path (e.g. temporary)</param>
        /// <param name="identifier">the identifier used in case the path is not a fallback</param>
        /// <returns>the path used for storage relative to <see cref="IParseTargetPlatform.BasePath"/> of the <see cref="ParseTargetPlatform.CurrentPlatform"/></returns>
        public virtual string GetRelativeStorageFallbackPath(bool isFallback, string identifier)
        {
            return Path.Combine("Parse", isFallback ? "_fallback" : "_global", $"{(isFallback ? new System.Random { }.Next().ToString() : identifier)}.cachefile");
        }

        /// <summary>
        /// The current process exit or application termination event used to hook internal parse resource cleanup and event handlers to
        /// </summary>
        /// <remarks>This should be an event invoked when the running application is exited.</remarks>
        public virtual event Action ProcessExit
        {
            add { AppDomain.CurrentDomain.ProcessExit += (_, __) => value.Invoke(); }
            remove { AppDomain.CurrentDomain.ProcessExit -= (_, __) => value.Invoke(); }
        }

    }

}
