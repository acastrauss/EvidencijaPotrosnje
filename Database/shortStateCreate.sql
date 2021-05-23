USE [States]
GO

/****** Object:  Table [dbo].[shortStateName]    Script Date: 5/23/2021 5:58:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[shortStateName](
	[fullName] [nchar](50) NOT NULL,
	[shortName] [nchar](2) NOT NULL
) ON [PRIMARY]
GO

