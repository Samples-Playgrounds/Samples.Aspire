# Aspire with Client projects (MAUI, WPF, WinForms, etc.)

readme.md

*   https://github.com/dotnet/aspire/issues/876

*   https://github.com/dotnet/maui/issues/18802

```
dotnet build ./SampleAspireStarter.AppHost/SampleAspireStarter.AppHost.csproj
```

```
./SampleAspireStarter.AppHost/SampleAspireStarter.AppHost.csproj : error NU1201: Project SampleMAUI is not compatible with net8.0 (.NETCoreApp,Version=v8.0). Project SampleMAUI supports:
./SampleAspireStarter.AppHost/SampleAspireStarter.AppHost.csproj : error NU1201:   - net8.0-android34.0 (.NETCoreApp,Version=v8.0)
./SampleAspireStarter.AppHost/SampleAspireStarter.AppHost.csproj : error NU1201:   - net8.0-ios17.0 (.NETCoreApp,Version=v8.0)
./SampleAspireStarter.AppHost/SampleAspireStarter.AppHost.csproj : error NU1201:   - net8.0-maccatalyst17.0 (.NETCoreApp,Version=v8.0)
```

Possible workaround:

*   https://github.com/dotnet/maui/issues/18802#issuecomment-1815460892

`SetTargetFramework`

`SkipGetTargetFrameworkProperties`


https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#projectreference

ProjectReference

  SetTargetFramework

  SkipGetTargetFrameworkProperties

  GlobalPropertiesToRemove



```
dotnet build ./SampleMAUI/SampleMAUI.csproj -t:run -f:net8.0-ios
```

Android needs running emulator or device (yes small inconsistency in MAUI dev UX):

```
dotnet build ./SampleMAUI/SampleMAUI.csproj -t:run -f:net8.0-android -p:AdbTarget=pixel_5_-_api_31
```

```
dotnet build ./SampleMAUI/SampleMAUI.csproj -t:run -f:net8.0-maccatalyst
```


```
dotnet build  -t:run -f:net8.0-ios

xcrun simctl list

```
    dotnet \
      watch \
        build \
          $d \
          -t:Run \
            -f:net7.0-ios \
            -p:_DeviceName=:v2:udid=$IOS_DEVICE_ID
```

https://github.com/dotnet/xamarin/issues/26

https://learn.microsoft.com/en-us/xamarin/android/deploy-test/building-apps/build-properties

    https://developer.android.com/tools/adb#issuingcommands

Added `net8.0` TFM to MAUI app project (similar trick is done for Maui Unit/UI Tests projects):

Original:

```
		<TargetFrameworks>net8.0-android;net8.0-ios;net8.0-maccatalyst</TargetFrameworks>
```

Changed to:

```
		<TargetFrameworks>net8.0;net8.0-android;net8.0-ios;net8.0-maccatalyst</TargetFrameworks>
```


```
CSC : error CS5001: Program does not contain a static 'Main' method suitable for an entry point [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0]
/usr/local/share/dotnet/sdk/8.0.100/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(491,5): error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier 'android-arm64'. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-android]
/usr/local/share/dotnet/sdk/8.0.100/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(491,5): error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier 'android-arm'. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-android]
Properties/launchSettings.json : error : The path '../../../../../../../../../Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/Properties/launchSettings.json' would result in a file outside of the app bundle and cannot be used. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-maccatalyst]
Properties/launchSettings.json : error :          [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-maccatalyst]
/usr/local/share/dotnet/sdk/8.0.100/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(491,5): error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier 'android-x64'. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-android]
/usr/local/share/dotnet/sdk/8.0.100/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(491,5): error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier 'android-x86'. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-android]
Properties/launchSettings.json : error : The path '../../../../../../../../../Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/Properties/launchSettings.json' would result in a file outside of the app bundle and cannot be used. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-ios]
Properties/launchSettings.json : error :          [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-ios]

The build failed. Fix the build errors and run again.
```

Changed


```
    <OutputType>Exe</OutputType>
```

```
		<OutputType Condition=" '$(TargetFramework)' != 'net8.0' ">Exe</OutputType>
```


Results in error:

```
/usr/local/share/dotnet/sdk/8.0.100/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(491,5): error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier 'android-arm64'. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-android]
/usr/local/share/dotnet/sdk/8.0.100/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(491,5): error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier 'android-arm'. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-android]
/usr/local/share/dotnet/sdk/8.0.100/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(491,5): error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier 'android-x86'. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-android]
/usr/local/share/dotnet/sdk/8.0.100/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Sdk.FrameworkReferenceResolution.targets(491,5): error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier 'android-x64'. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-android]
Properties/launchSettings.json : error : The path '../../../../../../../../../Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/Properties/launchSettings.json' would result in a file outside of the app bundle and cannot be used. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-maccatalyst]
Properties/launchSettings.json : error :          [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-maccatalyst]
Properties/launchSettings.json : error : The path '../../../../../../../../../Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/Properties/launchSettings.json' would result in a file outside of the app bundle and cannot be used. [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-ios]
Properties/launchSettings.json : error :          [/Users/Shared/Projects/d/Samples-Playgrounds/Aspire/m/samples/SampleAspireStarterWithMAUI/SampleMAUI/SampleMAUI.csproj::TargetFramework=net8.0-ios]

The build failed. Fix the build errors and run again.
m/samples/SampleAspireStarterWithMAUI % 
```


So, iOS has issues with `Properties/launchSettings.json` and  


```
error NETSDK1082: There was no runtime pack for Microsoft.AspNetCore.App available for the specified RuntimeIdentifier
```


is reported only for Android.
