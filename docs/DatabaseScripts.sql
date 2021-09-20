USE [master]
GO

IF DB_ID('KlickHealthData') IS NULL
BEGIN
    CREATE DATABASE [KlickHealthData]
END
GO

USE [KlickHealthData]
GO


IF (NOT EXISTS(SELECT *  
				FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_SCHEMA = 'dbo' 
                AND TABLE_NAME = 'ProvinceInfo'))
BEGIN
	CREATE TABLE [dbo].[ProvinceInfo]
	(
		[ProvinceCode] VARCHAR(5) NOT NULL PRIMARY KEY
		,[ProvinceName] VARCHAR(50) NOT NULL	
		,[CreatedBy] VARCHAR(50)
		,[CreatedOn] DATETIME
		,[ModifiedBy] VARCHAR(50)
		,[ModifiedOn] DATETIME
	)
END
GO

IF (NOT EXISTS(SELECT *  
				FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_SCHEMA = 'dbo' 
                AND TABLE_NAME = 'MedicineInfo'))
BEGIN
	CREATE TABLE [dbo].[MedicineInfo]
	(
		[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY
		,[MedicineName] VARCHAR(50) NOT NULL	
		,[CreatedBy] VARCHAR(50)
		,[CreatedOn] DATETIME
		,[ModifiedBy] VARCHAR(50)
		,[ModifiedOn] DATETIME
	)
END
GO



IF (NOT EXISTS(SELECT *  
				FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_SCHEMA = 'dbo' 
                AND TABLE_NAME = 'UserInformation'))
BEGIN
	CREATE TABLE [dbo].[UserInformation]
	(
		[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY
		,[Name] VARCHAR(50) NOT NULL
		,[EmailAddress] VARCHAR(60)
		,[ProvinceCode] VARCHAR(5)
		,[IsActive] BIT DEFAULT(1)
		,[CreatedBy] VARCHAR(50)
		,[CreatedOn] DATETIME
		,[ModifiedBy] VARCHAR(50)
		,[ModifiedOn] DATETIME
		,CONSTRAINT FK_UserInformation_ProvinceCode FOREIGN KEY (ProvinceCode)
		 REFERENCES [dbo].[ProvinceInfo](ProvinceCode)
	)
END
GO


IF (NOT EXISTS(SELECT *  
				FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_SCHEMA = 'dbo' 
                AND TABLE_NAME = 'MedicationDetails'))
BEGIN
	CREATE TABLE [dbo].[MedicationDetails]
	(
		[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY
		,[UserId] INT NOT NULL
		,[MedicineId] INT NOT NULL	
		,CONSTRAINT FK_MedicationDerails_UserId FOREIGN KEY (UserId)
		 REFERENCES [dbo].[UserInformation](Id)
		,CONSTRAINT FK_MedicationDerails_MedicineId FOREIGN KEY (MedicineId)
		 REFERENCES [dbo].[MedicineInfo](Id)
	)
END
GO


-----------------SQL Scritps for Province-----------------------

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='AB')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('AB','Alberta','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='BC')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('BC','British Columbia','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='MB')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('MB','Manitoba','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='NB')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('NB','New Brunswick','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='NL')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('NL','Newfoundland and Labrador','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='NT')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('NT','Northwest Territories','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='NS')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('NS','Nova Scotia','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='NU')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('NU','Nunavut','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='ON')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('ON','Ontario','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='PE')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('PE','Prince Edward Island','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='QC')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('QC','Quebec','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='SK')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('SK','Saskatchewan','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [ProvinceCode] FROM [dbo].[ProvinceInfo] WHERE [ProvinceCode]='YT')
BEGIN
	INSERT INTO [dbo].[ProvinceInfo]([ProvinceCode],[ProvinceName],[CreatedBy],[CreatedOn])
	VALUES('YT','Yukon','SYSTEM',GETDATE())
END
GO


-----------------SQL Scritps for Medicine-----------------------

IF NOT EXISTS(SELECT [MedicineName] FROM [dbo].[MedicineInfo] WHERE [MedicineName]='Diabetes T1')
BEGIN
	INSERT INTO [dbo].[MedicineInfo]([MedicineName],[CreatedBy],[CreatedOn])
	VALUES('Diabetes T1','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [MedicineName] FROM [dbo].[MedicineInfo] WHERE [MedicineName]='Diabetes T2')
BEGIN
	INSERT INTO [dbo].[MedicineInfo]([MedicineName],[CreatedBy],[CreatedOn])
	VALUES('Diabetes T2','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [MedicineName] FROM [dbo].[MedicineInfo] WHERE [MedicineName]='Migraine')
BEGIN
	INSERT INTO [dbo].[MedicineInfo]([MedicineName],[CreatedBy],[CreatedOn])
	VALUES('Migraine','SYSTEM',GETDATE())
END
GO

IF NOT EXISTS(SELECT [MedicineName] FROM [dbo].[MedicineInfo] WHERE [MedicineName]='Obesity')
BEGIN
	INSERT INTO [dbo].[MedicineInfo]([MedicineName],[CreatedBy],[CreatedOn])
	VALUES('Obesity','SYSTEM',GETDATE())
END
GO
IF NOT EXISTS(SELECT [MedicineName] FROM [dbo].[MedicineInfo] WHERE [MedicineName]='Depression')
BEGIN
	INSERT INTO [dbo].[MedicineInfo]([MedicineName],[CreatedBy],[CreatedOn])
	VALUES('Depression','SYSTEM',GETDATE())
END
GO






