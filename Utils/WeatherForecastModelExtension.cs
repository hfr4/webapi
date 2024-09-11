public static class WeatherForecastModelExtension {
    public static WeatherForecastModelDto ToDto(this WeatherForecastModel a) {
        return new WeatherForecastModelDto {
            Id = a.Id,
            Date = a.Date,
            TemperatureC = a.TemperatureC,
            Summary = a.Summary,
        };
    }

    public static WeatherForecastModel ToModel(this WeatherForecastModelDto a) {
        return new WeatherForecastModel {
            Id = a.Id,
            Date = a.Date,
            TemperatureC = a.TemperatureC,
            Summary = a.Summary,
         };
    }
}