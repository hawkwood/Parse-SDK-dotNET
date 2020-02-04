# Parse SDK for .NET
[![Build status](https://ci.appveyor.com/api/projects/status/uoit0ona7m3x9bw6?svg=true)](https://ci.appveyor.com/project/ParseCommunity/parse-sdk-dotnet)
[![codecov](https://codecov.io/gh/parse-community/Parse-SDK-dotNET/branch/master/graph/badge.svg)](https://codecov.io/gh/parse-community/Parse-SDK-dotNET)
[![Nuget][nuget-svg]][nuget-link]
[![License][license-svg]][license-link]
[![Join The Conversation](https://img.shields.io/discourse/https/community.parseplatform.org/topics.svg)](https://community.parseplatform.org/c/parse-server)
[![Backers on Open Collective](https://opencollective.com/parse-server/backers/badge.svg)](#backers)
[![Sponsors on Open Collective](https://opencollective.com/parse-server/sponsors/badge.svg)](#sponsors)
![Twitter Follow](https://img.shields.io/twitter/follow/ParsePlatform.svg?label=Follow%20us%20on%20Twitter&style=social)

## Getting Started
The latest stable release of the SDK is available as a [NuGet package][nuget-link]. Note that the latest package currently available on the official distribution channel is quite old.
To use the most up-to-date code, build this project and reference the generated NuGet package.

## Using the Code
Make sure you are using the project's root namespace:

```cs
using Parse;
```

Then, in your program's entry point, paste the following code, with the text reflecting your application and Parse Server setup emplaced between the quotation marks.

```cs
ParseClient.Initialize(new ParseClient.Configuration
{
    ApplicationID = "",
    ServerURI = ""
});
```

`ApplicationID` is your app's `ApplicationId` field from your Parse Server.  
`ServerURI` is the full URL to your web-hosted Parse Server.  

Depending on the platforms your application targets you might need to set up your SDK properly and provide it with some additional information like application version, name, company name, etc. By default the SDK is using a target platform configuration for .NET Standard applications which should work with all .NET Standard conform applications.
Please refer to the [Target Platforms](Platforms.md) document for further details, known issues and examples.


## Building The Library
You can build the library from Visual Studio Code (with the proper extensions), Visual Studio 2017 Community and higher, or Visual Studio for Mac 7 and higher. You can also build the library using the command line:

### On Windows or any .NET Core compatible Unix-based system with the .NET Core SDK installed:
```batch
dotnet build Parse.sln
```

Results can be found in either `Parse/bin/Release/netstandard2.0/` or `Parse/bin/Debug/netstandard2.0/` relative to the root project directory, where `/` is the path separator character for your system.

## How Do I Contribute?
We want to make contributing to this project as easy and transparent as possible. Please refer to the [Contribution Guidelines][contributing].

## License

```
Copyright (c) 2015-present, Parse, LLC.
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. An additional grant 
of patent rights can be found in the PATENTS file in the same directory.
```

 [contributing]: https://github.com/parse-community/Parse-SDK-dotNET/blob/master/CONTRIBUTING.md
 [license-svg]: https://img.shields.io/badge/license-BSD-lightgrey.svg
 [license-link]: https://github.com/parse-community/Parse-SDK-dotNET/blob/master/LICENSE
 [nuget-link]: http://nuget.org/packages/parse
 [nuget-svg]: https://img.shields.io/nuget/v/parse.svg
 [parse-docs-link]: http://docs.parseplatform.org/
