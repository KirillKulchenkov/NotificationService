﻿CREATE TABLE [dbo].[QueueLog]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT newid(), 
    [Created] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [TriesDone] INT NOT NULL DEFAULT 0, 
    [Sent] DATETIME2 NULL, 
    [Error] NVARCHAR(MAX) NULL, 
    [SourceId] UNIQUEIDENTIFIER, 
    [Payload] VARCHAR(MAX) NOT NULL, 
    [Token] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_QueueLog] PRIMARY KEY ([Id])
)
