CREATE TABLE [dbo].[Cities] (
    [CityId]    INT           NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [CountryID] INT           NULL,
    PRIMARY KEY CLUSTERED ([CityId] ASC),
    CONSTRAINT [FK_Cities_Countries] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Countries] ([CountryId])
);

