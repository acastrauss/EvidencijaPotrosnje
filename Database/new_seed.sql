USE [States]
GO
/****** Object:  Table [dbo].[shortStateName]    Script Date: 5/30/2021 7:06:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shortStateName](
	[fullName] [nchar](50) NOT NULL,
	[shortName] [nchar](2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 5/30/2021 7:06:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[stateName] [nchar](10) NOT NULL,
	[stateID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[stateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateConsumption]    Script Date: 5/30/2021 7:06:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateConsumption](
	[covRation] [int] NULL,
	[dateFrom] [datetime] NULL,
	[dateShort] [datetime] NULL,
	[dateTo] [datetime] NULL,
	[dateUTC] [datetime] NULL,
	[stateCode] [nchar](30) NULL,
	[value] [float] NULL,
	[valueScale] [float] NULL,
	[stateConsumptionID] [int] IDENTITY(1,1) NOT NULL,
	[stateID] [int] NULL,
 CONSTRAINT [PK_StateConsumption] PRIMARY KEY CLUSTERED 
(
	[stateConsumptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateWeather]    Script Date: 5/30/2021 7:06:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateWeather](
	[airTemperature] [float] NULL,
	[cloudCover] [nchar](50) NULL,
	[devpointTemperature] [int] NULL,
	[gustValue] [int] NULL,
	[horizontalVisibility] [int] NULL,
	[humidity] [int] NULL,
	[presentWeather] [nchar](50) NULL,
	[recentWeather] [nchar](50) NULL,
	[reducedPressure] [float] NULL,
	[stationPressure] [float] NULL,
	[windDirection] [nchar](50) NULL,
	[windSpeed] [int] NULL,
	[stateWeatherID] [int] IDENTITY(1,1) NOT NULL,
	[localTime] [datetime] NULL,
	[stateID] [int] NULL,
 CONSTRAINT [PK_StateWeather] PRIMARY KEY CLUSTERED 
(
	[stateWeatherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StateConsumption]  WITH CHECK ADD  CONSTRAINT [FK_StateConsumption_State] FOREIGN KEY([stateID])
REFERENCES [dbo].[State] ([stateID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StateConsumption] CHECK CONSTRAINT [FK_StateConsumption_State]
GO
ALTER TABLE [dbo].[StateWeather]  WITH CHECK ADD  CONSTRAINT [FK_StateWeather_State] FOREIGN KEY([stateID])
REFERENCES [dbo].[State] ([stateID])
GO
ALTER TABLE [dbo].[StateWeather] CHECK CONSTRAINT [FK_StateWeather_State]
GO
