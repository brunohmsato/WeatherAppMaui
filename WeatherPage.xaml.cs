using MauiApp1.Services;

namespace MauiApp1;

public partial class WeatherPage : ContentPage
{
    private double latitude, longitude;
    public List<Models.List> WeatherList;

    public WeatherPage()
    {
        InitializeComponent();
        WeatherList = new List<Models.List>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();

        if (location != null)
        {
            latitude = location.Latitude;
            longitude = location.Longitude;
        }
    }

    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double lat, double lon)
    {
        var resultado = await ApiService.GetWeatherByCoordAsync(lat, lon);

        if (resultado != null)
        {
            UpdateUI(resultado);
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Procurar por cidade", accept: "Procurar", cancel: "Cancelar");

        if (response != null)
        {
            await GetWeatherDataByCity(response);
        }
    }

    public async Task GetWeatherDataByCity(string cidade)
    {
        var resultado = await ApiService.GetWeatherByCityNameAsync(cidade);

        if (resultado != null)
        {
            UpdateUI(resultado);
        }
    }

    public void UpdateUI(dynamic result)
    {
        //Row 1
        LblCity.Text = result.city.name;
        LblWeatherDescript.Text = result.list[0].weather[0].description;

        //Row 2
        //ImgWeatherIcon.Source = result.list[0].weather[0].fullIconUrl;
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;

        //Row 3
        LblUmidade.Text = result.list[0].main.humidity + "%";
        LblTemperatura.Text = result.list[0].main.temperature + "°C";
        LblVentos.Text = result.list[0].wind.speed + "km/h";

        //Row 4
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }

        CvWeather.ItemsSource = WeatherList;
    }
}