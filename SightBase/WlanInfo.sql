CREATE TABLE [dbo].[WlanInfo]
(
	[EntryID] INT NOT NULL, 
    [DeviceID] INT NULL FOREIGN KEY (DeviceID) REFERENCES WlanDevices(DeviceID), 
    [Time] TIMESTAMP NULL, 
    [Location] VARCHAR(30) NULL, 
    CONSTRAINT [PK_WlanInfo] PRIMARY KEY ([EntryID]),
	
)
