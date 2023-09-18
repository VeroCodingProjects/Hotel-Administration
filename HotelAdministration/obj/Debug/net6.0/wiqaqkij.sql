IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Rooms] (
    [Id] int NOT NULL IDENTITY,
    [PricePerNight] nvarchar(12) NOT NULL,
    [IsAvailable] nvarchar(12) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [DateTime] datetime2 NOT NULL,
    CONSTRAINT [PK_Rooms] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230920135027_Rooms Status', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Rooms] ADD [NoRoomn] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230921190642_New migration', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'NoRoomn');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Rooms] DROP COLUMN [NoRoomn];
GO

ALTER TABLE [Rooms] ADD [NoRoom] nvarchar(100) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230921191237_NoRoom', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Rooms].[DateTime]', N'StartTime', N'COLUMN';
GO

ALTER TABLE [Rooms] ADD [EndTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231002081232_interval-booking', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231002083551_startEndDate', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231002084223_bookingsInterval', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231002090604_Vero', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231002092031_interval', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231002094917_IntervalBooking', N'6.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'StartTime');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Rooms] ALTER COLUMN [StartTime] nvarchar(100) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'EndTime');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Rooms] ALTER COLUMN [EndTime] nvarchar(100) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231002101514_IntervalRoomBooking', N'6.0.21');
GO

COMMIT;
GO

