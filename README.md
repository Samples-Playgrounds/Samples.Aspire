# Samples.Aspire

readme.md

Samples/playgrounds repo for .NET Aspire AND client (MAUI) integrations.

Some facts R&D was conducted with following in mind:

1.  trying to be minimally invasive 

    not to request changes in MAUI, Aspire or any other SDK - for now.
    
    Thus a lot of stuff is dirty hacks/workarounds.

2.  produce minimal PoC/MVP

    initial samples and eShopLite and Javier's sample were refactored into 4 separate repos/libraries/nugets

    1.  HolisticWare.Tools.Clients.Maui.Aspire.Integration

        MAUI dependency 

        Javier's Wrapper is inside

        https://github.com/HolisticWare-Xamarin-Tools/HolisticWare.Tools.Clients.Maui.Aspire.Integration/tree/main

        https://www.nuget.org/packages/HolisticWare.Tools.Clients.Maui.Aspire.Integration/

    2.  HolisticWare.Tools.Devices

        Aspire dependency - for device management and orchestration

        Planned:

        *   grabbing info about installed and running AVD and simulators, 
            
        *   ports, IP addresses, etc....

        https://github.com/HolisticWare-Xamarin-Tools/HolisticWare.Tools.Devices/tree/main

        https://www.nuget.org/packages/HolisticWare.Tools.Devices/ 

    3.  HolisticWare.Tools.Aspire.Hosting.Clients.Maui

        Aspire dependency

        wires up (currently launches only) MAUI project, devices and apps

        I need to dive in deeper into Aspire SDK to see what can be used

        Planned: source generator for some settings

        https://github.com/HolisticWare-Xamarin-Tools/HolisticWare.Tools.Aspire.Hosting.Clients.Maui/tree/main

        https://www.nuget.org/packages/HolisticWare.Tools.Aspire.Hosting.Clients.Maui/

Template[s] repo:

*   https://github.com/HolisticWare-DotNet-New-Templates/HolisticWare.DotNetNew.Templates.Project.Architecture.AspireWithClientsMaui.CSharp

Other repos:

*   https://github.com/BretJohnson/aspire-samples

    *   https://github.com/dotnet/aspire-samples


Some links

*   https://www.youtube.com/watch?v=fx1XOUFdU8k

*   

## Aspire

.NET Aspire

*   cloud ready stack 
    
    *   for building observable, production ready, distributed applications. 
        
*   delivered through a collection of NuGet packages

Cloud-native apps often consist of small, interconnected pieces or microservices rather than a single, monolithic code base. Cloud-native apps generally consume a large number of services

Aspire template `aspire-starter` contains sample client app implemented in ASP.net Blazor:

```
dotnet new aspire-starter -o AspireStarter
```

Extending Blazor/Web client to other types of client apps (Console, Desktop, MAUI)



Samples and playgrounds for Aspire and client integration (console apps, MAUI,...)


*  https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview

*  https://github.com/dotnet/aspire

  *  https://github.com/dotnet/aspire/issues/876

*   https://learn.microsoft.com/en-us/dotnet/aspire/reference/aspire-faq

*   https://learn.microsoft.com/en-us/dotnet/aspire/get-started/build-your-first-aspire-app?tabs=visual-studio

*   https://devblogs.microsoft.com/dotnet/introducing-dotnet-aspire-simplifying-cloud-native-development-with-dotnet-8/

*   https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment?tabs=visual-studio%2Cinstall-az-windows%2Cpowershell&pivots=azure-azd

*   https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#projectreference

## Videos

*   What Is .NET Aspire? The Insane Future of .NET!

    *   https://www.youtube.com/watch?v=DORZA_S7f9w&ab_channel=NickChapsas

*   How to Deploy .NET 8's New .NET Aspire Stack

    *   https://www.youtube.com/watch?v=HYe6y1kBuGI&ab_channel=NickChapsas

*   Building Cloud Native apps with .NET 8 | .NET Conf 2023

    *   https://www.youtube.com/watch?v=z1M-7Bms1Jg&ab_channel=dotnet

*   Cloud-native apps with .NET Aspire

    *   https://www.youtube.com/watch?v=J02mvcEKrsI&ab_channel=LaylaCodesIt



## Client integration (MAUI and other) related issues

*  https://github.com/dotnet/aspire/issues/876

*  https://github.com/dotnet/maui/issues/18802

## 


## Related and Contacts

*   David Fowler

    *   Distinguished Engineer .NET at Microsoft

    *   https://github.com/davidfowl

    *   https://github.com/dotnet/aspire/

*   Chet Husk

    *   Senior Product Manager

    *   https://github.com/baronfel

    *   https://github.com/dotnet/aspire/

*   Reuben Bond

    *   Principal Software Engineer .NET at Microsoft

    *   https://github.com/ReubenBond

    *   https://github.com/dotnet/orleans

*   products

    *   https://github.com/dotnet/aspire/

    *   https://github.com/dotnet/orleans

        *   https://learn.microsoft.com/en-us/samples/browse/?terms=orleans
        

