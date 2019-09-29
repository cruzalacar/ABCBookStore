CREATE TABLE [dbo].[BookInfo]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NCHAR(50) NULL, 
    [Author] NCHAR(50) NULL, 
    [ISBN] NCHAR(50) NULL, 
    [Publish Date] DATE NULL, 
    [Publisher] NCHAR(10) NULL, 
    [Category] NCHAR(10) NULL, 
    [Pages] NCHAR(10) NULL, 
    [Price] NCHAR(10) NULL
)
