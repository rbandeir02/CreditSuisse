
--Create table for the Trades categories
CREATE TABLE Trades (
    TradeID INT IDENTITY(1,1) PRIMARY KEY,
    Value DECIMAL(18,2),
    ClientSector VARCHAR(50)
);


CREATE OR REPLACE PROCEDURE CategorizeTrades
AS
BEGIN
    -- Create a temporary table to store the categorized trades
    CREATE TABLE #CategorizedTrades (
        TradeID INT,
        Category VARCHAR(50)
    );

    -- Insert categorized trades based on rules
    INSERT INTO #CategorizedTrades (TradeID, Category)
    SELECT
        TradeID,
        CASE
            WHEN Value < 1000000 AND ClientSector = 'Public' THEN 'LOWRISK'
            WHEN Value >= 1000000 AND ClientSector = 'Public' THEN 'MEDIUMRISK'
            WHEN Value >= 1000000 AND ClientSector = 'Private' THEN 'HIGHRISK'
            ELSE NULL -- Trade does not fit into any category
        END AS Category
    FROM
        Trades;

    -- Output categorized trades
    SELECT * FROM #CategorizedTrades;

    -- Drop the temporary table
    DROP TABLE #CategorizedTrades;
END;
