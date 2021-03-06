USE [States]
GO
SET IDENTITY_INSERT [dbo].[State] ON 

INSERT [dbo].[State] ([stateName], [stateID]) VALUES (N'Serbia    ', 157)
INSERT [dbo].[State] ([stateName], [stateID]) VALUES (N'Germany   ', 158)
INSERT [dbo].[State] ([stateName], [stateID]) VALUES (N'Russia    ', 159)
SET IDENTITY_INSERT [dbo].[State] OFF
GO
SET IDENTITY_INSERT [dbo].[StateConsumption] ON 

INSERT [dbo].[StateConsumption] ([covRation], [dateFrom], [dateShort], [dateTo], [dateUTC], [stateCode], [value], [valueScale], [stateConsumptionID], [stateID]) VALUES (22, CAST(N'2021-02-02T00:00:00.000' AS DateTime), CAST(N'2021-02-02T00:00:00.000' AS DateTime), CAST(N'2021-02-02T00:00:00.000' AS DateTime), CAST(N'2021-02-02T00:00:00.000' AS DateTime), N'RS                            ', 22, 22, 137546, 157)
SET IDENTITY_INSERT [dbo].[StateConsumption] OFF
GO
SET IDENTITY_INSERT [dbo].[StateWeather] ON 

INSERT [dbo].[StateWeather] ([airTemperature], [cloudCover], [devpointTemperature], [gustValue], [horizontalVisibility], [humidity], [presentWeather], [recentWeather], [reducedPressure], [stationPressure], [windDirection], [windSpeed], [stateWeatherID], [localTime], [stateID]) VALUES (22, N'22                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  ', 22, 22, 22, 22, N'22                                                ', N'22                                                ', 22, 22, N'22                                                ', 22, 935987, CAST(N'2021-02-02T00:00:00.000' AS DateTime), 157)
SET IDENTITY_INSERT [dbo].[StateWeather] OFF
GO
