CREATE OR ALTER PROCEDURE dbo.tradesCategories AS
BEGIN
    DECLARE @clientSector VARCHAR (25),  
            @clientValue MONEY,
            @riskResult VARCHAR (25)

    DECLARE PORTFOLIO CURSOR FOR 
        SELECT CLIENT_SECTOR, 
               CLIENT_VALUE
          FROM INPUT_OUTPUT_LOG
         WHERE DATE_RESULT IS NULL;

    OPEN PORTFOLIO;

    FETCH NEXT FROM PORTFOLIO INTO @clientSector, @clientValue;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @riskResult = RISK
          FROM CATEGORIES
         WHERE SECTOR = @clientSector
           AND VALUE_FROM < @clientValue
           AND VALUE_UNTIL > @clientValue;

        IF NULLIF(@riskResult, '') IS NULL  
            SET @riskResult = 'UNDEFINED';

        PRINT @riskResult;

        UPDATE INPUT_OUTPUT_LOG
           SET RISK_RESULT = @riskResult,
               DATE_RESULT = CURRENT_TIMESTAMP
         WHERE CURRENT OF PORTFOLIO;

        SET @riskResult = '';

        FETCH NEXT FROM PORTFOLIO INTO @clientSector, @clientValue;
    END;

    CLOSE PORTFOLIO;

    DEALLOCATE PORTFOLIO;

END;

EXEC banco.dbo.tradesCategories;