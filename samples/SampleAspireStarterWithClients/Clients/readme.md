


dotnet new \
    maui \
        -o Client.AppMAUI 
dotnet new \
    unoapp \
        -o Client.AppUNO
dotnet new \
    avalonia.app \
        -o Client.AppAvalonia 
dotnet new \
    avalonia.mvvm \
        -o Client.AppAvalonia.MVVM 
dotnet new \
    avalonia.xplat \
        -o Client.AppAvalonia.XPlat




dotnet new \
    uninstall \
        Uno.Templates
dotnet new \
    install \
        Uno.Templates
dotnet new \
    uninstall \
        Uno.ProjectTemplates.Dotnet
dotnet new \
    install \
        Uno.ProjectTemplates.Dotnet
dotnet new \
    uninstall \
        Uno.Extensions.Templates
dotnet new \
    install \
        Uno.Extensions.Templates
dotnet new \
    install \
        Uno.Templates
dotnet new install \
    Prism.Template
    

dotnet new \
    uninstall \
        Avalonia.Templates
dotnet new \
    install \
        Avalonia.Templates
    


rm -fr Client.AppUNO/
dotnet new unoapp \
    --output Client.AppUNO

rm -fr Client.AppAvaloniaUI/
dotnet new avalonia.app \
    --output Client.AppAvaloniaUI

rm -fr Client.AppAvaloniaUI.MVVM/
dotnet new avalonia.mvvm \
    --output Client.AppAvaloniaUI.MVVM

rm -fr Client.AppAvaloniaUI.XPlat/
dotnet new avalonia.xplat \
    --output Client.AppAvaloniaUI.XPlat
