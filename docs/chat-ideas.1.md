


## Discussion

*   David Folwer

    *   There are 2 things to look at:

        *   Launching the MAUI application

        *   Making service discovery work

Bret Johnson
    best practices for provisioning the endpoint for dev inner loop scenarios and service discovery for that matter

Chet Husk

    there's an open discussion that Peppers has open with the SDK team (my team) to make
    dotnet run
    delegate to
    Run
    targets provided by certain kinds of applications - but that clashes with the existing support for things like launchSettings.

    [11/30 5:43 PM] Chet Husk
    a big question I had for MAUI+Aspire is which actual target (simulator/device/etc) the Aspire orchestrator would launch. is that generally a static thing? would it require wiring up in the AppHost? would a user want to be prompted each time?

    because TFM isn't enough - you might have N different android simulators for different OS versions

David Fowler

    so if maui wires that up, it should  "just work"


Chet

IMO IConfiguration-based lookups of service discovery could be the final client-side surface area. everything else is just 'how do we get that data loaded into IConfiguration' (re: the 'nice end-to-end' point)

Peppers

    It seems like a plain dotnet run (with a device or emulator up and already running)
Seems like that should work today

```
[11/30 6:07 PM] Jonathan Peppers
Not sure we can do command-line arguments either
[11/30 6:07 PM] Jonathan Peppers
or working directory
[11/30 6:08 PM] Chet Husk
that's fine - you don't have to. those are part of the 'project' protocol. you can define whatever schema you want to support for a new protocol
[11/30 6:08 PM] Jonathan Peppers
Do we have a WinUI3 or UWP example? That might be what MAUI is closer to
[11/30 6:08 PM] David Fowler
Does Maui use launch profiles today at all?
[11/30 6:08 PM] Chet Husk
I don't know of any such example for those
[11/30 6:08 PM] Jonathan Peppers
I think MAUI can use them on Windows


[11/30 6:16 PM] Bret Johnson
my current thinking is:
We can have some support for the Aspire orchestrator launching MAUI apps, but I think it'll be a bit complicated and may not always work well. I'm also not sure that users will normally want that.
And thus I'm thinking it be good to also support a build time option, where it's easy-ish to build and run an Aspire orchestrator set of services locally & build/run a MAUI app locally that talks to them, but the launch steps are separate and the glue may happen at build time.

[11/30 6:17 PM] David Fowler
aspire doesn't build the project


[11/30 6:21 PM] Bret Johnson
if we had such a thing, are those the right conventions - spit out a json file with the config info, named say appsettings.json?
[11/30 6:22 PM] Bret Johnson
probably, i only ask in case there's some other dotnet/Aspire convention that's used already that may be a good choice to consider
[11/30 6:22 PM] Eilon Lipton
Though remember the trick is that putting the URL that Aspire thinks is the URL will not work for most MAUI scenarios because of emulators/localhost
[11/30 6:22 PM] Chet Husk
I worry about users getting confused with this auto-generated appsettings.json. Should this file be checked in? What if there's an appsettings already? If you tried to namespace it with an environment like the conventions use today (appsettings.Development.json, etc) how do you pass the environment name to the client app?


[12/5 5:16 PM] Eilon Lipton
Pasting this here too:
 
I'm not sure where you're at, but for .NET MAUI libraries the net8.0 TFM is sometimes present either for making some compilation scenarios simpler, or because the library can be used outside of .NET MAUI. There are no .NET MAUI applications that use the net8.0 TFM because that is not 'runnable' on any platform. A .NET MAUI application is always net8.0-PLATFORM (windows/android/ios/maccatalyst/tizen).
[12/5 5:17 PM] Eilon Lipton
It's really bizarre to me to have the Aspire AppHost project reference the MAUI project. In my mind that is logically incorrect because the AppHost should be for hosting the "services world" on "servers" and not any proper "remote" client apps (that is, an app that runs completely detached from the server world, except via HTTP).
 
Is there another way I should be thinking about this?
[12/5 5:18 PM] Chet Husk
I think a lot of folks are getting tripped up by 'ProjectReference' here.
[12/5 5:18 PM] Eilon Lipton
Somewhat relatedly, there's generally only one instance of the logical AppHost program running (even if it is in fact multiple instances of multiple services). But the Client app could have a logically infinite number of instances running completely independently (N clients talking to 1 server)
 
[12/5 5:19 PM] Chet Husk
It's not a traditional ProjectReference (in terms of assets flowing between projects to be part of the compilation) - it's 'just' a marker of 'I want this project to participate in orchestration in some way'
 



 [12/5 5:21 PM] Eilon Lipton
Chet Husk
It's not a traditional ProjectReference (in terms of assets flowing between projects to be part of the compilation) - it's 'just' a marker of 'I want this project to participate in orchestration in some way'

Is there anything to 'orchestrate' with regard to a proper Client app? I think that's a big piece of what I feel I'm missing here

[12/5 5:21 PM] Eilon Lipton
In my mind it feels like we're trying to get Aspire to do something that is unrelated to it, when really this is a dev tools problem where we want VS (or whatever) to launch multiple apps at the same time (something you can already easily do in VS), and send a URL from one to the other so they can talk to each other
[12/5 5:22 PM] Eilon Lipton
Like, would we really change Aspire's AppHost concept to support this? Or is it a dev tooling problem that in some cases happens to involve Aspire?
[12/5 5:22 PM] Chet Husk
build and env/url injection has been the topic so far.  You're right that VS has historically had the orchestration for this, but that leaves CLI/VSCode in the dust. And IMO that's not acceptable anymore.


[12/5 5:23 PM] Eilon Lipton
I'm imagining more like dotnet multi-run-watch-thingy --server MyAspireApp1 --client MyMauiApp1

[12/5 5:23 PM] Chet Husk
there's a long history of folks working on multi-project startup that I think Aspire has kind of superseded - Angelos historically did a bunch of work/research here
 like 1



 [12/5 5:38 PM] Eilon Lipton
David Fowler
This is pretty much what aspire does

I thought Aspire fills the role of different components of an app talking to each other? For me logically the 'remote client' is not part of that because of the environment in which it runs and that there can be N of them.

[12/5 5:38 PM] David Fowler
Does blazor wasm fall out of scope in your mind ?


[12/5 5:39 PM] Eilon Lipton
David Fowler
Does blazor wasm fall out of scope in your mind ?

Blazor WASM is an odd child to me, because it has to be hosted somewhere, and that somewhere often happens to be an ASP.NET Core app, which happens to be able to run within Aspire's universe

[12/5 5:39 PM] Eilon Lipton
So maybe logically yes, it's out of scope, but it happens to work anyway





[12/5 5:42 PM] Mel Cvjetko
Eilon Lipton
I thought Aspire fills the role of different components of an app talking to each other? For me logically the 'remote client' is not part of that because of the environment in which it runs and that there can be N of them.

Maui App with SignalR is component talking the same lang as Blazor WASM in my browser. Isn't it??

And it is valid Maui scenario/use-case
[12/5 5:43 PM] Mel Cvjetko
Eilon Lipton
Blazor WASM is an odd child to me, because it has to be hosted somewhere, and that somewhere often happens to be an ASP.NET Core app, which happens to be able to run within Aspire's universe

I see ASP.net  app hosting Blazor App like App Store/Google Play... to download WASM app

[12/5 6:09 PM] Rui Marinho
Hi everyone, I wanted to share my thoughts on some scenarios that I think we could support.
The first scenario involves MAUI applications communicating with a backend during the development process. AspireHost could know how to launch both Maui apps (for four platforms) and web (or other services) and pass the correct defaults to Maui to connect to. This would also mean passing OpenTelemetry configuration and HttpClient defaults, for example. Making resilient mobile apps is even harder as connectivity changes all the time (we can add this without Aspire too). Could we improve and configure everything so that users just use SignalR to connect to the backend?
The Dashboard is great! Inspired by what David Fowler said in his talk, all the telemetry helps developers better understand what the client/server are doing. The dashboard could show Maui apps communicating with the WebAPI or even with other metrics we could add, like layout time, page navigation, etc. However, I see this more in the development process and only some in production.
The creation of containers could be extended to deploy our app there for testing in a container that is prepped to just take an application and run it. Maui is also for desktop apps like WinUI and Catalyst, so just having it start the executable could also be integrated with the dashboard.
The second scenario is a solution for telemetry/analytics to replace AppCenter Analytics and Diagnostics. Could this be the replacement we need for our users who are now facing the AppCenter deprecation?"
I hope this helps! Let me know if you have any other questions.
 heart 1
[12/5 6:14 PM] Eilon Lipton
Those would all be great to have
[12/5 6:14 PM] Eilon Lipton
In my mind this is still a 'dev tools' problem and not a 'runtime orchestration' problem
[12/5 6:14 PM] Eilon Lipton
One of the key reasons for that is that this scenario makes no sense at all in production
[12/5 6:14 PM] Eilon Lipton
It's literally only a problem at dev time
[12/5 6:15 PM] David Fowler
Aspire orchestration is for dev time


[12/5 6:09 PM] Rui Marinho
Hi everyone, I wanted to share my thoughts on some scenarios that I think we could support.
The first scenario involves MAUI applications communicating with a backend during the development process. AspireHost could know how to launch both Maui apps (for four platforms) and web (or other services) and pass the correct defaults to Maui to connect to. This would also mean passing OpenTelemetry configuration and HttpClient defaults, for example. Making resilient mobile apps is even harder as connectivity changes all the time (we can add this without Aspire too). Could we improve and configure everything so that users just use SignalR to connect to the backend?
The Dashboard is great! Inspired by what David Fowler said in his talk, all the telemetry helps developers better understand what the client/server are doing. The dashboard could show Maui apps communicating with the WebAPI or even with other metrics we could add, like layout time, page navigation, etc. However, I see this more in the development process and only some in production.
The creation of containers could be extended to deploy our app there for testing in a container that is prepped to just take an application and run it. Maui is also for desktop apps like WinUI and Catalyst, so just having it start the executable could also be integrated with the dashboard.
The second scenario is a solution for telemetry/analytics to replace AppCenter Analytics and Diagnostics. Could this be the replacement we need for our users who are now facing the AppCenter deprecation?"
I hope this helps! Let me know if you have any other questions.
 heart 1
[12/5 6:14 PM] Eilon Lipton
Those would all be great to have
[12/5 6:14 PM] Eilon Lipton
In my mind this is still a 'dev tools' problem and not a 'runtime orchestration' problem
[12/5 6:14 PM] Eilon Lipton
One of the key reasons for that is that this scenario makes no sense at all in production
[12/5 6:14 PM] Eilon Lipton
It's literally only a problem at dev time
[12/5 6:15 PM] David Fowler
Aspire orchestration is for dev time



[12/5 6:21 PM] David Fowler
you're not wrong Eilon but you end up wanting orchestration and service discovery the moment you have 2 things trying to talk to each other
[12/5 6:22 PM] David Fowler
And as we mentioned earlier, this isn't an Aspire problem at all: it happens with a any 'regular' web service + client app. Once we start looking at Aspire, we immediately have to go solve the problem for everywhere else
There are 2 problems being solved here:
Service discovery - The service discovery APIs can be used in a normal client app
Orchestration - Launching multiple projects with the right set of configuration
1. we want for both dev and other environments
2. we want for dev only
 like 4


[12/5 6:25 PM] Rui Marinho
I think the question is MAUI apps don't fit in the "cloud" space. and if Aspire is only made to fit that  i think you are right Eilon Lipton but if we want to Aspire to be more than just to orchestrate on the "cloud" and be more as a good way to also work on the cli dev machine to help in the inner loop, again i m looking at the dashboard as a tool for Maui devs.
[12/5 6:26 PM] Eilon Lipton
I want us to think outside of Aspire, unless we think that really every app that needs anything non-trivial must use Aspire. Perhaps that's reasonable, but that could be a tough sell. I'm fine with Aspire having the best experience, but this problem seems so much broader (and yet in some ways simpler) than Aspire scenarios
[12/5 6:27 PM] Eilon Lipton
The Client App problem is "I want at dev time to run my server, send the URL to the client app, and then run the client"
[12/5 6:27 PM] Eilon Lipton
Whether it needs to discover anything more complicated than that could be an Aspire scenario/problem/solution
[12/5 6:27 PM] Eilon Lipton
But that basic server/URL/client covers like 100% of problems


[12/5 6:28 PM] Reuben Bond
Regarding Service Discovery, it seems like the right solution for both MAUI and Blazor WASM is to generate an appropriate appsettings.json file
 like 3
[12/5 6:30 PM] Bret Johnson
Generate an appsettings.json with potentially just a single initial URL, to bootstrap things, and then other services can be discovered via that via the Aspire IConfiguration based service discovery mechanism (doing a remote query)?
[12/5 6:31 PM] Reuben Bond
appsettings.json feeds into IConfiguration, so they are the same system
 like 1






[12/5 6:31 PM] Eilon Lipton
Reuben Bond
appsettings.json feeds into IConfiguration, so they are the same system

(In MAUI that's not set up by default, but we easily could, at least for dev time)

 surprised 1


[12/5 6:32 PM] Reuben Bond
Does MAUI add any IConfiguration sources?
[12/5 6:32 PM] Eilon Lipton
Not by default due to perf
[12/5 6:32 PM] Eilon Lipton
But Config theoretically is available, it's just not set up with anything
[12/5 6:33 PM] Reuben Bond
How do you configure a MAUI app?
[12/5 6:33 PM] Eilon Lipton
So in MAUI you get all the configuration you want, as long as you want only empty strings 
 laugh 1




[12/5 6:34 PM] Reuben Bond
Ok, so developers are used to hard-coding server URLs, then?
 like 1
[12/5 6:34 PM] Eilon Lipton
Well even a JSON file is hard-coded if it's checked in  But yes either literally hard-code in C#, or use IConfiguration yourself, or get from somewhere else
[12/5 6:45 PM] Jonathan Peppers
One thing iOS/Android has is runtimeconfig.json support
So there is a Mono MSBuild task that compiles these into a binary format for mobile
Then switches can be accessed by AppContext.TryGetSwitch() (these are also used for trimming flags)
[12/5 6:46 PM] Jonathan Peppers
These feel like useful key/value pairs we should be using for things like this



```