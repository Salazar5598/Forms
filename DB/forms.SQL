/*
Created: 1/11/2024
Modified: 1/11/2024
Model: Forms
Database: MS SQL Server 2017
*/


-- Create tables section -------------------------------------------------

-- Table Form

CREATE TABLE [Form]
(
 [Form_id] Int IDENTITY NOT NULL,
 [Name] Varchar(100) NOT NULL,
 [Description] Varchar(100) NULL,
 [Link] Varchar(100) NULL,
 [User_id] Int NULL
)
go

-- Create indexes for table Form

CREATE INDEX [IX_Relationship3] ON [Form] ([User_id])
go

-- Add keys for table Form

ALTER TABLE [Form] ADD CONSTRAINT [PK_Form] PRIMARY KEY ([Form_id])
go

-- Table Fields

CREATE TABLE [Fields]
(
 [Field_id] Int IDENTITY NOT NULL,
 [Name] Varchar(100) NOT NULL,
 [Title] Varchar(100) NULL,
 [Required] Bit NULL,
 [Form_id] Int NULL,
 [Type_id] Int NULL
)
go

-- Create indexes for table Fields

CREATE INDEX [IX_Relationship2] ON [Fields] ([Form_id])
go

CREATE INDEX [IX_Relationship4] ON [Fields] ([Type_id])
go

-- Add keys for table Fields

ALTER TABLE [Fields] ADD CONSTRAINT [PK_Fields] PRIMARY KEY ([Field_id])
go

-- Table User

CREATE TABLE [User]
(
 [User_id] Int IDENTITY NOT NULL,
 [Full_name] Varchar(100) NOT NULL,
 [User] Varchar(100) NOT NULL,
 [Password] Varchar(100) NULL
)
go

-- Add keys for table User

ALTER TABLE [User] ADD CONSTRAINT [PK_User] PRIMARY KEY ([User_id])
go

-- Table Fields_Type

CREATE TABLE [Fields_Type]
(
 [Type_id] Int IDENTITY NOT NULL,
 [Name] Varchar(100) NOT NULL
)
go

-- Add keys for table Fields_Type

ALTER TABLE [Fields_Type] ADD CONSTRAINT [PK_Fields_Type] PRIMARY KEY ([Type_id])
go

-- Create foreign keys (relationships) section ------------------------------------------------- 


ALTER TABLE [Fields] ADD CONSTRAINT [Relationship2] FOREIGN KEY ([Form_id]) REFERENCES [Form] ([Form_id]) ON UPDATE NO ACTION ON DELETE NO ACTION
go


ALTER TABLE [Form] ADD CONSTRAINT [Relationship3] FOREIGN KEY ([User_id]) REFERENCES [User] ([User_id]) ON UPDATE NO ACTION ON DELETE NO ACTION
go


ALTER TABLE [Fields] ADD CONSTRAINT [Relationship4] FOREIGN KEY ([Type_id]) REFERENCES [Fields_Type] ([Type_id]) ON UPDATE NO ACTION ON DELETE NO ACTION
go




