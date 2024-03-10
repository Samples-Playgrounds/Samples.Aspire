# Damian Edwards

Morn

Damian Edwards

I feel some feedback from us would be polite, so...

"Short" status report and some (out of tons) of questions. Sorry for delays, but I cannot allow my daily tasks to pile up.

I am transforming this from my notes which are in the form of mind-map/bullet-points/hints.


1.  standup

    I just saw my calendar and 
    
    On Thursday/Today there will be standup about MAUI and Aspire where one of the approaches will be presented.

    https://www.youtube.com/watch?v=b58K9vBjlIM&ab_channel=dotnet

2.  questions - context

    1.  dashboard error[s]
    
        1.  MAUI and UNO (`dotnet build -t:run -f:net8.0-*`)

            ```
            Unable to run your project
            Your project targets multiple frameworks. Specify which framework to run using '--framework'.
            ```

        2.  Console (`dotnet run`)

            ```
            [{"date":"2024-03-08","temperatureC":18,"summary":"Balmy","temperatureF":64},{"date":"2024-03-09","temperatureC":-2,"summary":"Mild","temperatureF":29},{"date":"2024-03-10","temperatureC":-11,"summary":"Chilly","temperatureF":13},{"date":"2024-03-11","temperatureC":30,"summary":"Mild","temperatureF":85},{"date":"2024-03-12","temperatureC":-11,"summary":"Sweltering","temperatureF":13}]
            ```

        3.  Avalonia  (`dotnet run`)

            ```
            One or more errors occurred. (nodename nor servname provided, or not known (apiservice:80))
            ```

    2.  `dotnet run` vs `dotnet build`

        based on 2.2 and 2.3 seems like frameworks that support `dotnet run` are "almost there"

        I am confused with console sample picking up `apiservice` end point, but not Avalonia. I need to investigate.

    3.  debugging for multiple clients

        Suppose you have 2+ Blazor WASM clients (true clients by your team). 
        
        If launched, what is debugging experience? 
        
        Does that work out of the box?

        Would that be a huge problem?

        Client app scenario/use-case: working on and if possible debugging chat app on multiple clients/devices. 

3.  performance as a feature

    We have discusions around:
    
    > "DI is bad because it "hurts" startup

    IMO both of our teams have the same goal - performance as a feature, but I am being corrected that ASP.net team cares more about throughput, while MAUI and other client teams care more about startup performance. And that is something I doubt if I think about resilience, scaling - apps need to be started/restarted as fast as possible.

    You were correct - R&Rs (rules and recommendations), best practices for client apps and DI would be great to create for our customers.

3.  approaches

    There were few more people working on this (discussions, feedback)

    Javier Suárez           https://github.com/jsuarezruiz

    Matthew Leibowitz       https://github.com/mattleibow

    Rui Marinho             https://github.com/rmarinho

    NOTE: for testing everything you'll need to setup Android/iOS/MacOSX (emulators, simulators)!!!

    Approaches:

    1.  Bret's approach (Bret Johnson)

        https://github.com/BretJohnson/aspire-mobile/

        https://github.com/BretJohnson/aspire-mobile/blob/main/src/AspireMobile.SettingsGenerator/GenerateSettings.cs
        
        1.  Concepts
        
            Basically approach has 

            1.  app-in-the-middle (console app) which is launched by Aspire.Host and 
            
            2.  console app searches for ENVVARs with substrings

            ```
            services__
            OTEL_
            LOGGING__CONSOLE
            services__
            ```

            3.  generates `AspireAppSettings.g.cs` in MAUI project

        2.  description/links/templates

            Testing (I use on mac)

            ```bash
            dotnet new uninstall \
                MauiAspire.ProjectTemplates
            dotnet new install \
                MauiAspire.ProjectTemplates
            ```

            https://github.com/BretJohnson/aspire-mobile/blob/main/src/AspireMobile.SettingsGenerator/GenerateSettings.cs#L13-L21

            ```bash
            rm -fr CloudSystem 
            dotnet new maui-aspire \
                -o CloudSystem

            dotnet watch \
                --project CloudSystem/CloudSystem.AppHost/CloudSystem.AppHost.csproj
            ```


            repo:

            https://github.com/BretJohnson/aspire-mobile

    2.  moljac's approach (Miljenko Mel Cvjetko, me)

        1.  Concepts

            1.  minimaly invasive for MAUI (Aspire.AppHost concept)

                only reference for project/nuget with extension methods for wireing up

            2.  AppHost centric

                no app-in-the-middle needed

            3.  launches client apps 1+ (one or more) (not necessarily)

                there is API not to launch apps, but prepared to only to generate settings

        2.  description/links/templates

            NOTE: Template has some issues. I need to finish new template!! Naming is hard.

            installation:

            ```bash
            dotnet new \
                uninstall \
                    HolisticWare.DotNetNew.Templates.Project.Architecture.AspireWithClientsMaui

            dotnet new \
                install \
                    HolisticWare.DotNetNew.Templates.Project.Architecture.AspireWithClientsMaui

            ```

            usage:

            ```bash
            rm -fr CloudSystem 
            dotnet new hw-aspire-clients-maui \
                -o CloudSystem

            dotnet watch \
                --project CloudSystem/ClientAppsIntegration.AppHost/ClientAppsIntegration.AppHost.csproj
            ```

            repos:

            https://github.com/Samples-Playgrounds/Samples.Aspire

            https://github.com/HolisticWare-Xamarin-Tools/HolisticWare.Tools.Clients.Maui.Aspire.Integration

            https://github.com/HolisticWare-Xamarin-Tools/HolisticWare.Tools.Devices/

            nugets:

            https://www.nuget.org/packages/HolisticWare.Tools.Clients.Maui.Aspire.Integration/

            https://www.nuget.org/packages/HolisticWare.Tools.Devices/

            template:

            https://github.com/HolisticWare-DotNet-New-Templates/HolisticWare.DotNetNew.Templates.Project.Architecture.AspireWithClientsMaui.CSharp

        3.  videos

            Optional - evolution of research

            1.  20240204

                https://www.youtube.com/watch?v=zkvAaSTPs0c&list=PLcjoDwf1xE_qWWBJLuCxHMVjX_PCC9XEQ

            2.  20240123

                eShopLite

                NOTE: multi repo (proof that Aspire is not only for monorepos and solution centric)

                https://www.youtube.com/watch?v=nnFf58Ff1jA&list=PLcjoDwf1xE_qWWBJLuCxHMVjX_PCC9XEQ

            3.  20240120

                Manually setup endpoints

                No Service Discovery

                https://www.youtube.com/watch?v=fx1XOUFdU8k&list=PLcjoDwf1xE_qWWBJLuCxHMVjX_PCC9XEQ

        4.  plan (kinda priority order)

            2 ideas

            1.  enumerating ENVVARs in Aspire.AppHost (like Bret does in Console App)

                Digging through

                `EnvironmentCallbackContext`

                `AllocatedEndpointAnnotation`

                https://github.com/dotnet/aspire/blob/240cde74a83cc5378a90c2b6eeb8d65f43658d80/src/Aspire.Hosting/Extensions/ResourceBuilderExtensions.cs#L164

                https://github.com/dotnet/aspire/blob/240cde74a83cc5378a90c2b6eeb8d65f43658d80/tests/Aspire.Hosting.Tests/WithReferenceTests.cs#L116


            2.  dumping/parsing Aspire manifest and parsing it

                Found some ideas.

                Tim's approach uses CLI (`dotnet build --publish`)

                https://github.com/timheuer/aspiremanifestgen

                https://github.com/timheuer/AspireManifestGen/blob/main/src/Commands/ManifestGen.cs#L47-L53

                Is there public API? 

                Object model for Manifest?

More questions coming soon.