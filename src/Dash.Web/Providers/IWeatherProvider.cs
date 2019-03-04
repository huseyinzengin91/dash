using System.Collections.Generic;
using Dash.Web.Models;

namespace Dash.Web.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
