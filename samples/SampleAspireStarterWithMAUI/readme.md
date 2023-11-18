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



