CREATE TABLE [dbo].[ExpiredToken]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT newid(), 
    [Expired] DATETIME2 NOT NULL DEFAULT GetDate(), 
	[Token] Varchar(Max) NOT NULL ,
	[NewToken] Varchar(Max)
)
