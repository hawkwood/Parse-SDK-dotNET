using Parse.Platforms;

namespace Parse
{
    /// <summary>
    /// Target platform configuration used to setup the Parse SDK on different .NET Standard compatible target platforms (which might not be fully .NET Standard compatible or behave slightly differntly
    /// </summary>
    public class ParseTargetPlatform
    {
        private static IParseTargetPlatform _currentPlatform = null;
        /// <summary>
        /// The reference to the current <see cref="IParseTargetPlatform"/> implementation used by the application.
        /// </summary>
        /// <remarks>This should be set prior to any other call towards the Parse SDK. The reference set should be an implementation of <see cref="IParseTargetPlatform"/> which provides all information for the targeting platform.</remarks>
        public static IParseTargetPlatform CurrentPlatform
        {
            get
            {
                // if the current reference is not set use the default one, which resembles the default behaviour of the main branch based on .NET Standard
                //  ->  See the github repository for additional information on custom implementations for other target platforms.
                if (_currentPlatform == null)
                    _currentPlatform = new ParseNETStandard();
                return _currentPlatform;
            }
            set
            { _currentPlatform = value; }
        }

    }

}
