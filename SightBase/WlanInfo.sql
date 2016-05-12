CREATE TABLE [dbo].[WlanInfo]
(
	[EntryID] INT NOT NULL, 
    [DeviceID] INT NULL FOREIGN KEY (DeviceID) REFERENCES WlanDevices(DeviceID), 
    CONSTRAINT [PK_WlanInfo] PRIMARY KEY ([EntryID]),
	
)
