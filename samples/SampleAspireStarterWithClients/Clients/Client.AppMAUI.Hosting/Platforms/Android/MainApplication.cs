using Android.App;
using Android.Runtime;
using Client.AppMAUI.Hosting;

namespace Client.AppMAUI.Hosting;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => 
													// MauiProgram.CreateMauiApp()
													Program.CreateMauiApp()
													;
}
