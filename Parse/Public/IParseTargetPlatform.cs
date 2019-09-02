using System;

namespace Parse
{
    /// <summary>
    /// Interface definition to provide platform specific information to the Parse SDK running on various platforms (e.g. .NET standard, Unity3D, Xamarin, etc.)
    /// </summary>
    public interface IParseTargetPlatform
    {
        /// <summary>
        /// The build version number of the application.
        /// </summary>
        string BuildVersion { get; }
        /// <summary>
        /// The display version number of the application.
        /// </summary>
        string DisplayVersion { get; }

        /// <summary>
        /// The identifier of the application
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// The name of the current application.
        /// </summary>
        string ProductName { get; }

        /// <summary>
        /// The name of the current application.
        /// </summary>
        string CompanyName { get; }

        /// <summary>
        /// The base path to parse caching folder base location
        /// </summary>
        string BasePath { get; }

        /// <summary>
        /// gets the relative identifier-based storage fallback path 
        /// </summary>
        /// <param name="isFallback">should the resulting path be used as a fallback path (e.g. temporary)</param>
        /// <param name="identifier">the identifier used in case the path is not a fallback</param>
        /// <returns>the path used for storage relative to <see cref="IParseTargetPlatform.BasePath"/> of the <see cref="ParseTargetPlatform.CurrentPlatform"/></returns>
        string GetRelativeStorageFallbackPath(bool isFallback, string identifier);

        /// <summary>
        /// The current process exit or application termination event used to hook internal parse resource cleanup and event handlers to
        /// </summary>
        /// <remarks>This should be an event invoked when the running application is exited.</remarks>
        event Action ProcessExit;

    }

}
