CREATE TABLE [dbo].[User]
(
	[Id] nvarchar(128) not null PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NCHAR(10) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate()

)

