# Parse SDK for .NET

## Target Platforms
The latest version of the Parse SDK for .NET is written with compatibility to .NET Standard in mind. Although .NET Standard is widely supported in various third party development frameworks like Xamarin or Unity3D the  very base of applications and the actual support of API calls differs from a pure .NET Standard implementation (e.g. available with Visual Studio 2019).  

The Parse SDK for .NET does includes the possibility to configure its behaviour on a certain target platform as is required by your actual project. This might be the case if you want to use Parse within an Unity3D or Xamarin application on Android or iOS devices. This document explains the idea behind the interface and class used to achieve this, gives an insight to the default implementation for .NET Standard platforms and an example which can be used in Unity3D applications or the Unity3D editor.  

## Why is this necessary?
As mentioned above, various third party frameworks or engines do claim compatibility with .NET Standard but behave slightly different or provide unusable return values when specific methods are called which work without problems in a app build for Windows PC.  

This does include file access on mobile devices where the default .NET special folders are partially mapped to inaccessible folders on Android or the missing access to the applications entry assembly on platforms where a .NET to native code conversion or cross-compilation is performed. Both cases occur when building mobile applications with Unity3D and would require more in-depth changes to the Parse SDK.  

Which is why the target platform became a configurable option of the Parse SDK and can be adjusted to your project's needs in either a general and generic way or in a very specific individual.
This document focuses on the general ways, which should work best for most users and comply with the ideas behind the current state of the Parse SDK the most.

## The ParseTargetPlatform class and IParseTargetPlatform interface
The idea behind the parse target platform is a centralized available instance which provides the required and yet platform-specific information to the SDK. The information provided are defined by the IParseTargetPlatform interface and can be implemented by everyone in a custom class to be used with the Parse SDK for .NET.

The Parse SDK for .NET contains a default implementation which provides these information for .NET Standard applications (it uses the previously used method calls and wraps them into readonly properties, events and methods according to the interface definition). `The default implementation is used when no custom implementation is provided.`  

It is necessary to provide your custom target platform implementation befor your first call to any Parse SDK method (which will most likely be the initialization). An example on how this is done in Unity3D is provided further down this document.

```cs
namespace Parse
{
    /// <summary>
    /// Target platform configuration used to setup the Parse SDK on different .NET compatible target platforms
    /// </summary>
    public class ParseTargetPlatform
    {
        public static IParseTargetPlatform CurrentPlatform
        {
            get {..} set {..}
        }
    }

    /// <summary>
    /// Interface definition to provide platform specific information to the Parse SDK running on various platforms (e.g. .NET standard, Unity3D, Xamarin, etc.)
    /// </summary>
    public interface IParseTargetPlatform
    {
        string BuildVersion { get; }
        string DisplayVersion { get; }
        string Identifier { get; }
        string ProductName { get; }
        string CompanyName { get; }
        string BasePath { get; }
        string GetRelativeStorageFallbackPath(bool isFallback, string identifier);
        event Action ProcessExit;
    }

}

```


## Custom ParseTargetPlatform for Unity3D
A very common use case for a custom  target platform is a game or application made with Unity which uses the Parse SDK for .NET.

Due to build process of Unity when targeting Android and iOS calls to `Assembly.GetEntryAssembly()` caused exceptions to be thrown at runtime because there is no actual reference to an .NET assembly and the code is called from native code unknown to the .NET part (or the cross compiled counterpart of it).

Another issue you might face is the support for file access and the mapping of special folders or file locations to the mobile devices platform. Unity3D provides a special folder location to the programmer, which is available via `Application.persistentAppData` and does not point to the same location the default implementation in the Parse SDK is referring to (which is the return value of `Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)`).  

Using a custom implementation of the `IParseTargetPlatform` interface can easily resolve this problems and allow the use of the Parse SDK for .NET assembly within Unity3D without any specific compilation flags or directives.

The class listed below, includes a working implementation of the interface and maps the Unity internal information to be accessible to the Parse SDK when required. The implementation includes handling of Unity runtime and Unity editor differences, which is not always required by adjusted to the project's needs.

```cs
public class ParseUnityPlatform : Parse.IParseTargetPlatform
{

    /// <summary>
    /// The version number of the application.
    /// </summary>
    public string BuildVersion
    {
        get =>
#if UNITY_EDITOR
            "build 0";
#else
        Application.version; // unity override
#endif
    }
    /// <summary>
    /// The version number of the application.
    /// </summary>
    public string DisplayVersion
    {
        get =>
#if UNITY_EDITOR
            "1.0";
#else
        Application.version; // unity override
#endif
    }

    /// <summary>
    /// The identifier of the application
    /// </summary>
    public string Identifier => AppDomain.CurrentDomain.FriendlyName;

    /// <summary>
    /// The name of the current application.
    /// </summary>
    public string ProductName
    {
        get =>
#if UNITY_EDITOR
            "UnityEditor";
#else
            Application.productName; // unity override
#endif
    }
    /// <summary>
    /// The name of the current application.
    /// </summary>
    public string CompanyName
    {
        get =>
#if UNITY_EDITOR
            "Unity Technologies";
#else
            Application.companyName;
#endif
    }

    /// <summary>
    /// The base path to parse caching folder location
    /// </summary>
    public string BasePath
    {
        get =>
#if UNITY_EDITOR
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#else
            Application.persistentDataPath;
#endif
    }


    public string GetRelativeStorageFallbackPath(bool isFallback, string identifier)
    {
        return Path.Combine("Parse", isFallback ? "_fallback" : "_global", $"{(isFallback ? new System.Random { }.Next().ToString() : identifier)}.cachefile");
    }


    public event Action ProcessExit
    {
        add { Application.quitting += value; }
        remove { Application.quitting -= value; }
    }

}
```


The only thing required to assign this custom implementation to the Parse SDK is an assignment to `ParseTargetPlatform.CurrentPlatform` which might look like this:
```cs
ParseTargetPlatform.CurrentPlatform = new ParseUnityPlatform();
```
Remember that this call should be done befor any other call to the Parse SDK is made to avoid erroneous initialization of the SDK.
