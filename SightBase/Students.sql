CREATE TABLE [dbo].[Students]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StudentNumber] CHAR(8) NOT NULL, 
    [Surname] VARCHAR(25) NULL, 
    [FullName] VARCHAR(40) NULL, 
    [Initials] VARCHAR(5) NULL, 
    [Card Number] INT NULL
)
