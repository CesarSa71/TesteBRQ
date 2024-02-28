/*
    USE master
    GO
    DROP DATABASE BRQ_Question2
*/

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'BRQ_Question2')
    BEGIN
    CREATE DATABASE BRQ_Question2
    PRINT 'Database created'
    END
GO

USE BRQ_Question2
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Trades')
    BEGIN
    CREATE TABLE dbo.Trades (
        TradeID BIGINT IDENTITY(1,1) PRIMARY KEY,
        TradeValue FLOAT NOT NULL,
        ClientSector NVARCHAR(10)
    )
    PRINT 'Table Trades created'
    END
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TradesCategories')
    BEGIN
    CREATE TABLE dbo.TradesCategories (
        TradeID BIGINT NOT NULL,
        Category NVARCHAR(20) NULL
    )
    PRINT 'Table TradesCategories created'
    END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Trades)
    BEGIN
    INSERT INTO dbo.Trades (TradeValue, ClientSector) VALUES (2000000, 'Private')
    INSERT INTO dbo.Trades (TradeValue, ClientSector) VALUES (400000, 'Public')
    INSERT INTO dbo.Trades (TradeValue, ClientSector) VALUES (500000, 'Public')
    INSERT INTO dbo.Trades (TradeValue, ClientSector) VALUES (3000000, 'Public')
    -- INSERT INTO dbo.Trades (TradeValue, ClientSector) VALUES (300000, 'Private')
    PRINT 'Portifolio inserted'
    END
GO

CREATE OR ALTER FUNCTION dbo.CategorizeTrade
(
    @Value FLOAT,
    @ClientSector NVARCHAR(10)
)
RETURNS NVARCHAR(20)
AS
BEGIN
    DECLARE @Result NVARCHAR(20);
    IF LOWER(@ClientSector) = 'public' AND @Value < 1000000
        SET @Result = 'LOWRISK'
    ELSE IF LOWER(@ClientSector) = 'public' AND @Value > 1000000
        SET @Result = 'MEDIUMRISK'
    ELSE IF LOWER(@ClientSector) = 'private' AND @Value > 1000000
        SET @Result = 'HIGHRISK'
    ELSE
        SET @Result = NULL
    RETURN @Result;
END;
GO

CREATE OR ALTER PROCEDURE dbo.CategorizeTrades
AS
BEGIN
    MERGE INTO dbo.TradesCategories AS dest
    USING dbo.Trades AS src
    ON dest.TradeID = src.TradeID
    WHEN MATCHED THEN
        UPDATE SET 
            dest.Category = dbo.CategorizeTrade(src.TradeValue, src.ClientSector)
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (TradeID, Category)
        VALUES (src.TradeID, dbo.CategorizeTrade(src.TradeValue, src.ClientSector))
    WHEN NOT MATCHED BY SOURCE THEN
        DELETE;
END
GO

EXEC dbo.CategorizeTrades
GO

SELECT  Category
FROM    dbo.TradesCategories