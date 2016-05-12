CREATE TABLE [dbo].[StaticHostnames]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [IPAddr] BINARY(4) NULL, 
    [Hostname] VARCHAR(35) NULL, 
    [DescriptiveName] VARCHAR(50) NULL
)
