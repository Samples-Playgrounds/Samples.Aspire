# Samples

samples.md

*   https://github.com/dotnet/aspire-samples

*   https://github.com/timheuer/AspireTodo

error NETSDK1004: Assets file '.\obj\project.assets.json' not found. Run a NuGet package restore to generate this file.



```
PS D:\Samples\Aspire\m\samples\SampleAspireStarterWithClients> dotnet run --project .\Clients\Client.AppWPF\Client.AppWPF.csproj
```


```
D:\Samples\Aspire\m\samples\SampleAspireStarterWithClients\Clients\Client.AppWPF\Client.AppWPF.csproj : error NU1301: Unable to load the service index for sour
ce https://nugetized.blob.core.windows.net/live-reload/index.json.
C:\Program Files\dotnet\sdk\8.0.100\NuGet.targets(156,5): error : Unable to load the service index for source https://nugetized.blob.core.windows.net/live-relo
ad/index.json. [D:\Samples\Aspire\m\samples\SampleAspireStarterWithClients\Clients\Client.AppWPF\Client.AppWPF.csproj]
C:\Program Files\dotnet\sdk\8.0.100\NuGet.targets(156,5): error :   Response status code does not indicate success: 409 (Public access is not permitted on this
 storage account.). [D:\Samples\Aspire\m\samples\SampleAspireStarterWithClients\Clients\Client.AppWPF\Client.AppWPF.csproj]

The build failed. Fix the build errors and run again.
```
