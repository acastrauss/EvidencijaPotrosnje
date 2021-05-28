USE [States]
GO
INSERT [dbo].[StateConsumption] ([covRation], [dateFrom], [dateShort], [dateTo], [dateUTC], [stateCode], [value], [valueScale], [stateConsumptionID]) VALUES (33, CAST(N'2021-05-20T11:15:58.000' AS DateTime), CAST(N'2021-05-20T11:15:58.000' AS DateTime), CAST(N'2021-05-20T11:15:58.000' AS DateTime), CAST(N'2021-05-20T11:15:58.000' AS DateTime), N'RS                            ', 33, 33, 0)
GO
INSERT [dbo].[StateWeather] ([airTemperature], [cloudCover], [devpointTemperature], [gustValue], [horizontalVisibility], [humidity], [presentWeather], [recentWeather], [reducedPressure], [stationPressure], [windDirection], [windSpeed], [stateWeatherID], [localTime]) VALUES (44, N'test                                              ', 44, 44, 44, 44, N'test                                              ', N'test                                              ', 44, 44, N'test                                              ', 44, 0, CAST(N'2021-05-20T11:15:58.673' AS DateTime))
GO
INSERT [dbo].[State] ([stateName], [stateID], [stateConsumptionID], [stateWeatherID]) VALUES (N'Serbia    ', 0, 0, 0)
GO
