CREATE TABLE [dbo].[Students]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Student Number] NCHAR(8) NOT NULL, 
    [Surname] NCHAR(25) NULL, 
    [Firstnames] NCHAR(40) NULL, 
    [Initials] NCHAR(5) NULL, 
    [Card Number] INT NULL
)
