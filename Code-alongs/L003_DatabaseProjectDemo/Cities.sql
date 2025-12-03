CREATE TABLE [dbo].[Cities]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [CountryID] INT NULL, 
    CONSTRAINT [FK_Cities_Countries] FOREIGN KEY (CountryID) REFERENCES Countries (Id)
)
