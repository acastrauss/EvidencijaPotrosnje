USE [States]
GO

UPDATE [dbo].[StateWeather]
   SET [airTemperature] = <airTemperature, float,>
      ,[cloudCover] = <cloudCover, nchar(50),>
      ,[devpointTemperature] = <devpointTemperature, int,>
      ,[gustValue] = <gustValue, int,>
      ,[horizontalVisibility] = <horizontalVisibility, int,>
      ,[humidity] = <humidity, int,>
      ,[presentWeather] = <presentWeather, nchar(50),>
      ,[recentWeather] = <recentWeather, nchar(50),>
      ,[reducedPressure] = <reducedPressure, float,>
      ,[stationPressure] = <stationPressure, float,>
      ,[windDirection] = <windDirection, nchar(50),>
      ,[windSpeed] = <windSpeed, int,>
      ,[stateWeatherID] = <stateWeatherID, int,>
      ,[localTime] = <localTime, datetime,>
 WHERE <Search Conditions,,>
GO

