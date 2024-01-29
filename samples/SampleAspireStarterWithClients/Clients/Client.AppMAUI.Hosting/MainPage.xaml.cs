using Client.Services;

namespace Client.AppMAUI;

public partial class MainPage : ContentPage
{
	private readonly ILogger _logger;
	private readonly WeatherApiClient _weatherApiClient;
	private readonly CancellationTokenSource _closingCts = new();

	public MainPage(ILogger<MainPage> logger, WeatherApiClient weatherApiClient)
	{
		InitializeComponent();

		_logger = logger;
		_weatherApiClient = weatherApiClient;
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		SemanticScreenReader.Announce(CounterBtn.Text);
		
		CounterBtn.IsEnabled = false;
		//pbLoading.Visible = true;

		try
		{
			/*
			if (chkForceError.Checked)
			{
				throw new InvalidOperationException("Forced error!");
			}
			*/
			
			WeatherForecast[] weather = await _weatherApiClient.GetWeatherAsync(_closingCts.Token);
			//dgWeather.DataSource = weather;
		}
		catch (TaskCanceledException)
		{
			return;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error loading weather");

			// dgWeather.DataSource = null;
			DisplayAlert(ex.Message, "Error", "Accept", "Cancel");
		}

		//pbLoading.Visible = false;
		CounterBtn.IsEnabled = true;

		return;
	}
}

