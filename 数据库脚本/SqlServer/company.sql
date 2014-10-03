USE [test]
GO

/****** Object:  Table [dbo].[company]    Script Date: 2014/10/3 23:13:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[company](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[company_name] [varchar](100) NULL,
	[industry] [varchar](100) NULL,
	[address] [varchar](255) NULL,
 CONSTRAINT [PK_company] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

