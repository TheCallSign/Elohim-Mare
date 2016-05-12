CREATE TABLE [dbo].[ResnetInfo]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StudentNumber] CHAR(8) NULL,
	[Hostname] NCHAR(20) NULL, 
    [Residence] VARCHAR(15) NULL, 
    [IPAddr] BINARY(4) NULL, 
    FOREIGN KEY (StudentNumber) REFERENCES Students([StudentNumber])
)
