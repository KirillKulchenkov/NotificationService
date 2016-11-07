CREATE TABLE [dbo].[ExpiredToken]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT newid(), 
    [Expired] DATETIME2 NOT NULL DEFAULT GetDate(), 
	[Token] Varchar(255) NOT NULL ,
	[NewToken] Varchar(255), 
    CONSTRAINT [AK_ExpiredToken_Column] UNIQUE ([Token])
)

GO

 --ALTER TABLE [dbo].[ExpiredToken] ADD CONSTRAINT [AK_ExpiredToken_Column] UNIQUE ([Token])
 --GO