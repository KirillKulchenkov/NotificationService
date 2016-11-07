CREATE TABLE [dbo].[MobileAppInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [MobileAppName] VARCHAR(150) NOT NULL, 
    [IOSCertificateFile] VARBINARY(MAX) NOT NULL, 
    [CertificatePassword] NVARCHAR(MAX) NOT NULL, 
	[AndroidSenderId] NVARCHAR(MAX) NOT NULL, 
    [AndriodAuthtoken] NVARCHAR(MAX) NOT NULL, 
    [SourceId] UNIQUEIDENTIFIER NOT NULL, 
    [Modified] DATETIME2 NOT NULL
)
