using System.Reflection;

namespace Parse.Platforms
{
    /// <summary>
    /// Xamarin specific implementation for the <see cref="IParseTargetPlatform"/> interface to provide platform specific data for Xamarin target platforms.
    /// </summary>
    public class ParseXamarinPlatform : ParseNETStandardPlatform
    {

        /// <summary>
        /// The entry assembly used to determine names and version numbers
        /// </summary>
        protected override Assembly ApplicationAssembly
        { get; } = Assembly.GetExecutingAssembly();

    }

}
