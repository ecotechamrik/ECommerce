GO

USE [webasqvy_ecotechdoors]

GO

CREATE NONCLUSTERED INDEX IX_ProductSizeAndPrices_ProductCode ON ProductSizeAndPrices(ProductCode);

GO
/****** Object:  StoredProcedure [ecotechdoors].[sp_getProductAttributeDetails]    Script Date: 27-09-2020 23:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp_getProductAttributeDetails 850
-- Get Product Attribute Details By The Product Attribute ID  
CREATE PROC [ecotechdoors].[sp_getProductAttributeDetails]  
 @ProductAttributeID INT  
AS  
BEGIN  
 DECLARE @cols AS NVARCHAR(MAX),  
  @query  AS NVARCHAR(MAX)  
  
 SELECT @cols = STUFF((SELECT ',' + QUOTENAME(ProductHeightName)   
      from [ecotechdoors].[ProductHeights] PH  
  
      group by PH.ProductHeightName, PH.ProductHeightID  
      order by PH.ProductHeightID  
  
    FOR XML PATH(''), TYPE  
    ).value('.', 'NVARCHAR(MAX)')   
   ,1,1,'')  
  
 PRINT @cols  
  
 SET @query = N'SELECT Description,ProductThicknessName,' + @cols + N' from   
     (  
     SELECT COALESCE(PSP.Description, '''') AS Description,   
       PT.ProductThicknessName,  
       PH.ProductHeightName,   
       CAST(PSP.[ProductSizeAndPriceID] AS NVARCHAR(50)) + ''_'' +   
       PSP.ProductCode + ''_'' +          
       (CAST(CAST((
			CASE   
				  WHEN COALESCE(PSP.PriceVoid, 0) > 0 THEN PSP.PriceVoid   
				  WHEN COALESCE(PSP.SellingPrice, 0) > 0 THEN PSP.SellingPrice
				  ELSE 0
			   END
	   ) AS FLOAT) AS NVARCHAR(100))) AS PriceVoid
     FROM [ecotechdoors].[ProductHeights] PH  
  
     INNER JOIN [ecotechdoors].[ProductSizeAndPrices] PSP  
     ON PH.ProductHeightID = PSP.ProductHeightID  
  
     INNER JOIN [ecotechdoors].[ProductAttributeThickness] PAT  
     ON PAT.ProductAttributeThicknessID = PSP.ProductAttributeThicknessID  
  
     INNER JOIN [ecotechdoors].[ProductAttributes] PA  
     ON PA.ProductAttributeID = PAT.ProductAttributeID AND PA.ProductAttributeID = ' + CAST(@ProductAttributeID AS NVARCHAR(50)) + '  
  
     INNER JOIN [ecotechdoors].[ProductThicknesses] PT  
     ON PT.ProductThicknessID = PAT.ProductThicknessID  
  
     --ORDER BY PSP.Description, PH.ProductHeightName  
    ) x  
    pivot   
    (  
     MAX(PriceVoid)  
     for ProductHeightName in (' + @cols + N')  
    ) p '  
  
 EXEC sp_executesql @query;  
END

GO

/****** Object:  StoredProcedure [ecotechdoors].[sp_getProductList]    Script Date: 27-09-2020 23:14:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC sp_getProductList 6

CREATE PROC [ecotechdoors].[sp_getProductList]
	@ProductID int
AS
BEGIN
	DECLARE @columns NVARCHAR(MAX) = '', 
			@SQL NVARCHAR(MAX) = '';

	SELECT @columns += QUOTENAME(ProductSizeName) + ','
	FROM ProductSizes	

	SET @columns = LEFT(@columns, LEN(@columns) - 1);

	SET @SQL = '

	SELECT * FROM 
	(
		SELECT S.[ProductSizeName], 
		CASt(PA.[PriceVoid] AS NVARCHAR(50)) + ''-'' + 
		CAST(PA.[ProductAttributeID] AS NVARCHAR(50)) + ''-'' + 
		CAST(PA.[ProductCode] AS NVARCHAR(100)) 
		AS SizeDetails, PA.Width
		
		
		
		FROM [ecotechdoors].[ProductAttributes] PA

		INNER JOIN [ecotechdoors].[ProductSizes] S
		ON S.[ProductSizeID] = PA.ProductSizeID

		INNER JOIN [ecotechdoors].[Products] P 
		ON PA.ProductID = P.ProductID AND P.ProductID = ' + CAST(@ProductID AS VARCHAR(10)) + '

	) Records

	PIVOT(
		MAX(SizeDetails)
		FOR ProductSizeName IN (' + @columns + ')
	) AS pivot_table;';

	EXEC sp_executesql @sql;
END

GO

/****** Object:  StoredProcedure [ecotechdoors].[sp_getProductWidthDetails]    Script Date: 27-09-2020 23:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC sp_getProductWidthDetails 1, 2  
  
CREATE PROC [ecotechdoors].[sp_getProductWidthDetails]  
 @ProductAttributeThicknessID INT,  
 @ProductHeightID INT  
AS  
BEGIN  
 SELECT PS.ProductWidthID, PS.ProductWidthName,  
 PSP.ProductSizeAndPriceID, PSP.ProductCode, PSP.ProductAttributeThicknessID, PSP.ProductHeightID, PSP.ProductWidthID,   
 COALESCE(PSP.PriceDate, GETDATE()) AS PriceDate, COALESCE(PSP.InvDate, GETDATE()) AS InvDate, PSP.RetailPriceDisc, PSP.PriceVoid, PSP.Markup, PSP.SellingPrice,   
 PSP.CreatedDateTime, PSP.UpdatedDateTime
 FROM [ecotechdoors].[ProductWidths] PS  
   
 LEFT JOIN [ecotechdoors].[ProductSizeAndPrices] PSP  
 ON PS.ProductWidthID = PSP.ProductWidthID   
 AND PSP.ProductAttributeThicknessID = @ProductAttributeThicknessID AND ProductHeightID = @ProductHeightID  
   
END

GO

/****** Object:  StoredProcedure [ecotechdoors].[sp_getWithAttributeThicknessID]    Script Date: 27-09-2020 23:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC sp_getWithAttributeThicknessID 5    
        
CREATE PROC [ecotechdoors].[sp_getWithAttributeThicknessID]        
 @ProductAttributeID int        
AS        
BEGIN        
 SELECT PT.*, COALESCE(PAT.[ProductAttributeThicknessID], 0) ProductAttributeThicknessID, PAT.*, PA.*    
 FROM [ecotechdoors].[ProductThicknesses] PT        
        
 LEFT OUTER JOIN [ecotechdoors].[ProductAttributeThickness] PAT        
 ON PT.[ProductThicknessID] = PAT.[ProductThicknessID] AND PAT.ProductAttributeID = @ProductAttributeID 
        
 LEFT OUTER JOIN [ecotechdoors].[ProductAttributes] PA        
 ON PA.[ProductAttributeID] = PAT.[ProductAttributeID]        
END

GO

/****** Object:  StoredProcedure [ecotechdoors].[sp_ImportProdAttrDetailsFromOldDB]    Script Date: 27-09-2020 23:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
-- EXEC sp_ImportProdAttrDetailsFromOldDB '13','mahoganycraftsman','','36','Engineered Mahogany Craftsman','("80","""")("84","""")("90","""")("96","""")'    
    
CREATE PROC [ecotechdoors].[sp_ImportProdAttrDetailsFromOldDB]    
 -- Product Entries    
 @ProductID INT,    
 @DoorTypeName NVARCHAR(500),    
 @PicCode NVARCHAR(MAX),    
 @Width INT,    
 @ProdSizePriceDesc NVARCHAR(500),    
 @ProdSizePrices NVARCHAR(MAX)    
AS    
BEGIN    
 DECLARE @DoorTypeID INT    
 DECLARE @ProductAttributeID INT    
 DECLARE @CurrencyID INT    
 DECLARE @Height INT    
 DECLARE @ProductCode NVARCHAR(MAX)    
 DECLARE @ProductCodeInitials NVARCHAR(MAX)    
 DECLARE @ProductThicknessID INT    
 DECLARE @ProductAttributeThicknessID INT    
 DECLARE @ProductWidthID INT    
 DECLARE @ProductHeightID INT    
 DECLARE @ProdSizePriceDes_Current NVARCHAR(MAX)    
 DECLARE @ProdSizePriceQuery TABLE (Query NVARCHAR(MAX))    
 DECLARE @ProdSizePriceRowCount INT    
 DECLARE @ProdSizePriceValue NVARCHAR(MAX)    
 DECLARE @Params NVARCHAR(MAX)     
 DECLARE @PSAndPInsertQuery TABLE (Query NVARCHAR(MAX))    
    
 IF(COALESCE(@DoorTypeName,'') != '')    
  BEGIN    
   IF EXISTS (SELECT [DoorTypeID] FROM [ecotechdoors].[DoorType] WHERE [DoorTypeCode] = @DoorTypeName)    
    SELECT @DoorTypeID = [DoorTypeID] FROM [ecotechdoors].[DoorType] WHERE [DoorTypeCode] = @DoorTypeName    
   ELSE    
    BEGIN    
     INSERT INTO [ecotechdoors].[DoorType] (DoorTypeName, DoorTypeCode) VALUES (@DoorTypeName, @DoorTypeName);    
     SELECT @DoorTypeID = SCOPE_IDENTITY();    
    END    
    
   -- Check If ProductAttribute Already Exists    
   IF EXISTS (SELECT TOP 1 [ProductAttributeID] FROM [ecotechdoors].[ProductAttributes]     
        WHERE ProductID = @ProductID AND (COALESCE([DoorTypeID],0) = @DoorTypeID OR [ProductAttributeName] = @DoorTypeName))    
    SET @ProductAttributeID = (SELECT TOP 1 (ProductAttributeID) FROM [ecotechdoors].[ProductAttributes]     
        WHERE ProductID = @ProductID AND (COALESCE([DoorTypeID],0) = @DoorTypeID OR [ProductAttributeName] = @DoorTypeName))    
   ELSE    
    BEGIN    
     -- SELECT     
     SET @CurrencyID = (SELECT TOP 1 (CurrencyID) from [ecotechdoors].[Currencies] WHERE COALESCE([IsDefault], 0) = 1)    
         
     -- INSERT PRODUCT ATTRIBUTE DETAILS INTO ProductAttributes Table    
     INSERT INTO [ecotechdoors].[ProductAttributes] (ProductID, CurrencyID, ProductAttributeName, Description, DoorTypeID)     
      VALUES (@ProductID, @CurrencyID, @DoorTypeName, @ProdSizePriceDesc, @DoorTypeID);    
     SELECT @ProductAttributeID = SCOPE_IDENTITY();    
    END    
    
    IF @ProdSizePrices != ''    
     BEGIN    
      -- DECLARE Product Size and Prices Parameters    
      DECLARE @PriorityNumber NVARCHAR(50)    
      DECLARE @InventoryNumber NVARCHAR(MAX)    
      DECLARE @Notes NVARCHAR(MAX)    
      DECLARE @PriceVoidDate NVARCHAR(50)    
      DECLARE @PriceDate NVARCHAR(50)    
      DECLARE @InvDate NVARCHAR(50)    
      DECLARE @GroupNumber NVARCHAR(50)    
      DECLARE @RetailPriceDisc NVARCHAR(50)    
      DECLARE @RetailPrice NVARCHAR(50)    
      DECLARE @BuildingCode NVARCHAR(MAX)    
      DECLARE @LocationCode NVARCHAR(MAX)    
      DECLARE @InventoryLevel NVARCHAR(50)    
      DECLARE @LeadTime NVARCHAR(MAX)    
    
      DECLARE @1_SupplierID NVARCHAR(50)    
      DECLARE @SupplierModeCode NVARCHAR(50)    
      DECLARE @SupplierCost NVARCHAR(50)    
      DECLARE @LandedCost NVARCHAR(50)    
      DECLARE @FreightCost NVARCHAR(50)    
    
      DECLARE @BestQuantityNo NVARCHAR(50)    
      DECLARE @OrderNowNo NVARCHAR(50)    
      DECLARE @RetailBin NVARCHAR(MAX)    
      DECLARE @WholeSaleBin NVARCHAR(MAX)    
      DECLARE @IndexNumber NVARCHAR(50)    
    
      DECLARE @PracticalMarkup NVARCHAR(50)    
      DECLARE @PracticalCost NVARCHAR(50)    
    
      DECLARE @RetailMarkupDisc NVARCHAR(50)    
      DECLARE @RetailMarkup NVARCHAR(50)    
      
      DECLARE @LivePriceDisc NVARCHAR(50)    
      DECLARE @LivePrice NVARCHAR(50)    
    
      DECLARE @ProductSizeAndPriceID INT    
      DECLARE @MinSupplierID INT    
    
      INSERT INTO @ProdSizePriceQuery SELECT * FROM STRING_SPLIT(@ProdSizePrices,')')    
          
      SET @ProdSizePriceRowCount = 1    
    
      -- While Loop for Product Size And Prices Multiple Variations based on Height and Width    
      WHILE @ProdSizePriceRowCount <= (SELECT COUNT(Query) FROM @ProdSizePriceQuery WHERE Query != '')    
       BEGIN     
        -- SELECT nth RECORD FROM THE TABLE    
        SELECT @ProdSizePriceValue = Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @ProdSizePriceQuery WHERE Query != '') T1    
        WHERE T1.RowIndex = @ProdSizePriceRowCount    
    
        IF @ProdSizePriceValue != ''    
         BEGIN    
          SET @Params = REPLACE(SUBSTRING(@ProdSizePriceValue, CHARINDEX('(', @ProdSizePriceValue) + 1, LEN(@ProdSizePriceValue) - 1),'"','''');    
    
          /* INSERT DATA INTO Product Price and Size Table & Supplier Table */    
          INSERT INTO @PSAndPInsertQuery SELECT * FROM STRING_SPLIT(@Params, ',')    
    
          SET @Height = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 1)    
          SET @ProductCode = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 2)    
    
          IF(@ProductCode != '')    
           BEGIN    
            SET @ProductThicknessID = (SELECT [ProductThicknessID] FROM [ecotechdoors].[ProductThicknesses] WHERE [ProductThicknessName] = SUBSTRING(@ProductCode,CHARINDEX(CAST(@Height AS NVARCHAR(10)), @ProductCode) + 2, LEN(@ProductCode)))    
            SET @ProductCodeInitials = SUBSTRING(@ProductCode, 0, CHARINDEX(CAST(@Width AS NVARCHAR(10)), @ProductCode))    
            SET @ProductHeightID = (SELECT [ProductHeightID] FROM [ecotechdoors].[ProductHeights] WHERE [ProductHeightName] = @Height)    
            SET @ProductWidthID = (SELECT [ProductWidthID] FROM [ecotechdoors].[ProductWidths] WHERE [ProductWidthName] = @Width)    
    
            IF (@ProdSizePriceDesc != '')    
             SET @ProdSizePriceDes_Current = @ProdSizePriceDesc + ' ' + CAST(@Width AS NVARCHAR(10)) + '"'    
               
            -- SELECT ProductAttributeThicknessID    
            IF EXISTS (SELECT ProductAttributeThicknessID FROM [ecotechdoors].[ProductAttributeThickness] WHERE ProductAttributeID = @ProductAttributeID AND ProductThicknessID = @ProductThicknessID AND ProductCodeInitials = @ProductCodeInitials)    
             BEGIN    
              SELECT @ProductAttributeThicknessID = ProductAttributeThicknessID FROM [ecotechdoors].[ProductAttributeThickness] WHERE ProductAttributeID = @ProductAttributeID AND ProductThicknessID = @ProductThicknessID AND ProductCodeInitials = @ProductCodeInitials
             END    
            ELSE    
             BEGIN    
              INSERT INTO [ecotechdoors].[ProductAttributeThickness] (ProductAttributeID, ProductThicknessID, ProductCodeInitials, Active)    
              VALUES (@ProductAttributeID, @ProductThicknessID, @ProductCodeInitials, 1)    
    
              SELECT @ProductAttributeThicknessID = SCOPE_IDENTITY();    
             END    
    
            -- If Product Size And Prices not Supplied    
            IF((SELECT COUNT(Query) FROM @PSAndPInsertQuery) = 2)    
             BEGIN    
              INSERT INTO [ecotechdoors].[ProductSizeAndPrices](ProductCode, ProductAttributeThicknessID, ProductHeightID, ProductWidthID, CreatedDateTime, UpdatedDateTime, Description)    
              VALUES (@ProductCode, @ProductAttributeThicknessID, @ProductHeightID, @ProductWidthID, CAST(GETDATE() AS DATE), CAST(GETDATE() AS DATE), @ProdSizePriceDes_Current)    
             END    
            -- If Product Size And Prices Supplied    
            ELSE IF ((SELECT COUNT(Query) FROM @PSAndPInsertQuery) = 32)    
             BEGIN    
              SET @PriorityNumber = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 3)    
              SET @InventoryNumber = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 4)    
              SET @Notes = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 5)    
    
              SET @PriceVoidDate = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 6)    
              SET @PriceDate = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 7)    
              SET @InvDate = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 8)    
              SET @GroupNumber = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 9)    
              SET @RetailPriceDisc = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 10)    
                  
              SET @RetailPrice = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 11)    
              SET @BuildingCode = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 12)    
              SET @LocationCode = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 13)    
              SET @InventoryLevel = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 14)    
              SET @LeadTime = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 15)    
    
              SET @1_SupplierID = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 16)    
              SET @SupplierModeCode = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 17)    
              SET @SupplierCost = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 18)    
              SET @LandedCost = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 19)    
              SET @FreightCost = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 20)    
    
              SET @BestQuantityNo = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 21)    
              SET @OrderNowNo = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 22)    
              SET @RetailBin = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 23)    
              SET @WholeSaleBin = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 24)    
              SET @IndexNumber = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 25)    
			  SET @PracticalMarkup = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 26)    
              SET @PracticalCost = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 27)    
              SET @RetailMarkupDisc = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 28)    
              SET @RetailMarkup = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 29)    
      
              SET @LivePriceDisc = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 30)    
              SET @LivePrice = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @PSAndPInsertQuery ) T2 WHERE T2.RowIndex = 31)    
    
              -- SET DEFAULT VALUES OF ALL PRODUCT SIZE AND PRICES PARAMETERS IF BLANK    
              IF RTRIM(LTRIM(REPLACE(@PriorityNumber,'''',''))) = '' SET @PriorityNumber = 0    
              IF RTRIM(LTRIM(REPLACE(@InventoryNumber,'''',''))) = '' SET @InventoryNumber = 0    
              IF (RTRIM(LTRIM(REPLACE(@PriceVoidDate,'''',''))) = '' OR RTRIM(LTRIM(REPLACE(@PriceVoidDate,'''',''))) = '0000-00-00') SET @PriceVoidDate = NULL    
              IF (RTRIM(LTRIM(REPLACE(@PriceDate,'''',''))) = '' OR RTRIM(LTRIM(REPLACE(@PriceDate,'''',''))) = '0000-00-00') SET @PriceDate = NULL    
              IF (RTRIM(LTRIM(REPLACE(@InvDate,'''',''))) = '' OR RTRIM(LTRIM(REPLACE(@InvDate,'''',''))) = '0000-00-00') SET @InvDate = NULL    
              IF RTRIM(LTRIM(REPLACE(@GroupNumber,'''',''))) = '' SET @GroupNumber = 0    
              IF RTRIM(LTRIM(REPLACE(@RetailPriceDisc,'''',''))) = '' SET @RetailPriceDisc = 0    
              IF RTRIM(LTRIM(REPLACE(@RetailPrice,'''',''))) = '' SET @RetailPrice = 0    
              IF RTRIM(LTRIM(REPLACE(@InventoryLevel,'''',''))) = '' SET @InventoryLevel = 0    
              IF RTRIM(LTRIM(REPLACE(@SupplierCost,'''',''))) = '' SET @SupplierCost = 0    
              IF RTRIM(LTRIM(REPLACE(@LandedCost,'''',''))) = '' SET @LandedCost = 0    
              IF RTRIM(LTRIM(REPLACE(@FreightCost,'''',''))) = '' SET @FreightCost = 0    
              IF RTRIM(LTRIM(REPLACE(@BestQuantityNo,'''',''))) = '' SET @BestQuantityNo = 0    
              IF RTRIM(LTRIM(REPLACE(@OrderNowNo,'''',''))) = '' SET @OrderNowNo = 0    
              IF RTRIM(LTRIM(REPLACE(@IndexNumber,'''',''))) = '' SET @IndexNumber = 0    
              IF RTRIM(LTRIM(REPLACE(@PracticalMarkup,'''',''))) = '' SET @PracticalMarkup = 0    
              IF RTRIM(LTRIM(REPLACE(@PracticalCost,'''',''))) = '' SET @PracticalCost = 0    
              IF RTRIM(LTRIM(REPLACE(@RetailMarkupDisc,'''',''))) = '' SET @RetailMarkupDisc = 0    
              IF RTRIM(LTRIM(REPLACE(@RetailMarkup,'''',''))) = '' SET @RetailMarkup = 0    
              IF RTRIM(LTRIM(REPLACE(@LivePriceDisc,'''',''))) = '' SET @LivePriceDisc =0    
              IF RTRIM(LTRIM(REPLACE(@LivePrice,'''',''))) = '' SET @LivePrice = 0    
    
              -- CHECK IF RECORD ALREADY EXISTS FOR THE GIVEN DETAILS    
              IF NOT EXISTS(SELECT [ProductSizeAndPriceID] FROM [ecotechdoors].[ProductSizeAndPrices] WHERE ProductAttributeThicknessID = @ProductAttributeThicknessID AND ProductCode = @ProductCode AND ProductHeightID = @ProductHeightID AND ProductWidthID = @ProductWidthID)    
               BEGIN    
                -- INSERT DATA INTO ProductSizeAndPrices Table    
                INSERT INTO [ecotechdoors].[ProductSizeAndPrices](    
                 ProductCode,     
                 ProductAttributeThicknessID,     
                 ProductHeightID,     
                 ProductWidthID,     
                 CreatedDateTime,     
				 UpdatedDateTime,     
                 Description,    
                     
                 PriorityNumber,    
                 InventoryNumber,    
                 Notes,    
                 PriceVoidDate,    
                 PriceDate,    
    
                 InvDate,    
                 GroupNumber,    
                 RetailPriceDisc,    
                 RetailPrice,    
                 BuildingCode,    
    
                 LocationCode,    
                 InventoryLevel,    
                 LeadTime,    
                 BestQuantityNo,    
                 OrderNowNo,    
    
                 RetailBin,    
                 WholeSaleBin,    
                 IndexNumber,    
                 PracticalMarkup,    
                 PracticalCost,    
    
                 RetailMarkupDisc,    
                 Markup,      
                 LivePriceDisc,    
                 SellingPrice    
                )    
                VALUES (    
                 @ProductCode,     
                 @ProductAttributeThicknessID,     
                 @ProductHeightID,     
                 @ProductWidthID,     
                 CAST(GETDATE() AS DATE),     
                 CAST(GETDATE() AS DATE),     
                 @ProdSizePriceDes_Current,    
    
                 CAST(@PriorityNumber AS FLOAT),    
                 CAST(@InventoryNumber AS NVARCHAR(MAX)),    
                 CAST(@Notes AS NVARCHAR(MAX)),    
                 CAST(@PriceVoidDate AS DATE),    
                 CAST(@PriceDate AS DATE),    
    
                 CAST(@InvDate AS DATE),    
                 CAST(@GroupNumber AS FLOAT),    
                 CAST(@RetailPriceDisc AS FLOAT),    
                 CAST(@RetailPrice AS FLOAT),    
                 CAST(@BuildingCode AS NVARCHAR(MAX)),    
    
                 CAST(@LocationCode AS NVARCHAR(MAX)),    
                 CAST(@InventoryLevel AS INT),    
                 CAST(@LeadTime AS NVARCHAR(MAX)),    
                 CAST(@BestQuantityNo AS INT),    
                 CAST(@OrderNowNo AS INT),    
    
                 CAST(@RetailBin AS NVARCHAR(MAX)),    
                 CAST(@WholeSaleBin AS NVARCHAR(MAX)),    
                 CAST(@IndexNumber AS INT),    
                 CAST(@PracticalMarkup AS FLOAT),    
                 CAST(@PracticalCost AS FLOAT),    
    
                 CAST(@RetailMarkupDisc AS FLOAT),    
                 CAST(@RetailMarkup AS FLOAT),    
                 CAST(@LivePriceDisc AS FLOAT),    
                 CAST(@LivePrice AS FLOAT)    
                )    
                    
                SELECT @ProductSizeAndPriceID = SCOPE_IDENTITY();    
                     
               END    
              ELSE    
               BEGIN    
                SELECT @ProductSizeAndPriceID = ProductSizeAndPriceID     
                FROM [ecotechdoors].[ProductSizeAndPrices]     
                WHERE ProductAttributeThicknessID = @ProductAttributeThicknessID AND ProductCode = @ProductCode AND ProductHeightID = @ProductHeightID AND ProductWidthID = @ProductWidthID    
               END    
    
              DECLARE @SupplierID INT    
    
              IF EXISTS (SELECT SupplierID FROM [ecotechdoors].[Suppliers] WHERE [SupplierCode] = @1_SupplierID)    
               BEGIN    
                SELECT @SupplierID = SupplierID FROM [ecotechdoors].[Suppliers] WHERE [SupplierCode] = @1_SupplierID    
                UPDATE [ecotechdoors].[Suppliers] SET [IsActive] = 1 WHERE SupplierID = @SupplierID    
               END    
              ELSE    
               BEGIN    
                INSERT INTO [ecotechdoors].[Suppliers] (SupplierName, SupplierCode, CreatedDateTime, UpdatedDateTime, IsActive)    
                VALUES (@1_SupplierID, @1_SupplierID, CAST(GETDATE() AS DATE), CAST(GETDATE() AS DATE), 1)    
    
                SELECT @SupplierID = SCOPE_IDENTITY();    
               END    

			  IF NOT EXISTS (SELECT ProductSupplierID FROM [ecotechdoors].[ProductSuppliers] WHERE ProductSizeAndPriceID = @ProductSizeAndPriceID AND SupplierID = @SupplierID)
				BEGIN
					INSERT INTO [ecotechdoors].[ProductSuppliers] (ProductSizeAndPriceID, SupplierID, InboundCost, TransportationCost, LandedCost, IsOption, IsLive)    
                          VALUES (@ProductSizeAndPriceID, @SupplierID, @SupplierCost, @FreightCost, (CAST(@SupplierCost AS FLOAT) + CAST(@FreightCost AS FLOAT)), 1, 1)    
				END
			  ELSE
				BEGIN
					UPDATE [ecotechdoors].[ProductSuppliers] 
					SET InboundCost = @SupplierCost, TransportationCost = @FreightCost, LandedCost = (CAST(@SupplierCost AS FLOAT) + CAST(@FreightCost AS FLOAT)), IsOption = 1, IsLive = 1
					WHERE ProductSizeAndPriceID = @ProductSizeAndPriceID AND SupplierID = @SupplierID
				END
    
              SET @MinSupplierID = (SELECT TOP 1 [ProductSupplierID] FROM [ecotechdoors].[ProductSuppliers] WHERE [ProductSizeAndPriceID] = @ProductSizeAndPriceID ORDER BY [LandedCost] ASC)    
                  
              -- SET Minimum Landed Cost Supplier as Live Supplier. All Else to Not Live.     
              UPDATE [ecotechdoors].[ProductSuppliers] SET IsLive = CASE [ProductSupplierID] WHEN @MinSupplierID THEN 1 ELSE 0 END    
              WHERE [ProductSizeAndPriceID] = @ProductSizeAndPriceID     
    
             END    
           END    
         END    
    
        DELETE FROM @PSAndPInsertQuery    
        SET @ProdSizePriceRowCount = @ProdSizePriceRowCount + 1;    
       END    
       DELETE FROM @ProdSizePriceQuery           
     END    
  END    
END 

GO

/****** Object:  StoredProcedure [ecotechdoors].[sp_ImportProductDetailsFromOldDB]    Script Date: 27-09-2020 23:15:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- EXEC sp_InsertUpdateProductAttributes  
  
CREATE PROC [ecotechdoors].[sp_ImportProductDetailsFromOldDB]  
 -- Product Entries  
 @ProductName NVARCHAR(500),  
 @ProductCode NVARCHAR(MAX),  
 @DefaultDoorTypeName NVARCHAR(MAX),  
 @ProductDesc NVARCHAR(MAX),  
 @Category NVARCHAR(200),  
 @SubCategory NVARCHAR(200),  
 @ProductGrade NVARCHAR(200),  
 @ProductDesign NVARCHAR(200),  
  
 -- Product Attributes Entries  
 @Value NVARCHAR(MAX) = ''
AS  
BEGIN  
 DECLARE @ProductID INT  
 DECLARE @DefaultDoorTypeID INT  
 DECLARE @CategoryID INT  
 DECLARE @SubCategoryID INT  
 DECLARE @ProductDesignID INT  
 DECLARE @ProductGradeID INT  
  
 IF(COALESCE(@DefaultDoorTypeName,'') != '')  
  SELECT @DefaultDoorTypeID = [DoorTypeID] FROM [ecotechdoors].[DoorType] WHERE [DoorTypeName] = @DefaultDoorTypeName  
  
 IF(COALESCE(@Category,'') != '')  
  BEGIN  
   IF EXISTS (SELECT [CategoryID] FROM [ecotechdoors].[Categories] WHERE [CategoryName] = @Category)  
    SELECT @CategoryID = [CategoryID] FROM [ecotechdoors].[Categories] WHERE [CategoryName] = @Category  
   ELSE  
    BEGIN  
     INSERT INTO [ecotechdoors].[Categories] (CategoryName, CategoryOrder, IsActive) VALUES (@Category, 1, 1);  
     SELECT @CategoryID = SCOPE_IDENTITY();  
    END  
  END  
  
 IF(COALESCE(@SubCategory,'') != '')  
  SELECT @SubCategoryID = [SubCategoryID] FROM [ecotechdoors].[SubCategories] WHERE [SubCategoryCode] = @SubCategory  
  
 IF(COALESCE(@ProductGrade,'') != '')  
  BEGIN  
   IF EXISTS (SELECT [ProductGradeID] FROM [ecotechdoors].[ProductGrades] WHERE [ProductGradeName] = @ProductGrade)  
    SELECT @ProductGradeID = [ProductGradeID] FROM [ecotechdoors].[ProductGrades] WHERE [ProductGradeName] = @ProductGrade  
   ELSE  
    BEGIN  
     INSERT INTO [ecotechdoors].[ProductGrades] ([ProductGradeName]) VALUES (@ProductGrade);  
     SELECT @ProductGradeID = SCOPE_IDENTITY();  
    END  
  END   
  
 IF(COALESCE(@ProductDesign,'') != '')  
  BEGIN  
   IF EXISTS (SELECT [ProductDesignID] FROM [ecotechdoors].[ProductDesigns] WHERE [ProductDesignName] = @ProductDesign)  
    SELECT @ProductDesignID = [ProductDesignID] FROM [ecotechdoors].[ProductDesigns] WHERE [ProductDesignName] = @ProductDesign  
   ELSE  
    BEGIN  
     INSERT INTO [ecotechdoors].[ProductDesigns] ([ProductDesignName]) VALUES (@ProductDesign);  
     SELECT @ProductDesignID = SCOPE_IDENTITY();  
    END  
  END  
  
 -- STEP 1: INSERT ENTRIES INTO PRODUCT TABLE  
 IF NOT EXISTS(SELECT ProductID FROM [ecotechdoors].[Products] WHERE [ProductName] = @ProductName AND CategoryID = @CategoryID AND SubCategoryID = @SubCategoryID)  
  BEGIN  
   SET @ProductDesc = REPLACE(REPLACE(REPLACE(@ProductDesc, '`',''''), '[','('), ']',')');
   INSERT INTO [ecotechdoors].[Products] (ProductName, ProductDesc, CategoryID, SubCategoryID, ProductDesignID, ProductGradeID, IPAddress, IsActive, [CreatedDate], [CreatedBy])  
             VALUES (@ProductName, @ProductDesc, @CategoryID, @SubCategoryID, @ProductDesignID, @ProductGradeID, '70.51.132.251', 1, CAST(GETDATE() AS DATE), 1);  
  
   SET @ProductID = SCOPE_IDENTITY();  
  END  
 ELSE  
  SET @ProductID = (SELECT TOP 1(ProductID) FROM [ecotechdoors].[Products] WHERE [ProductName] = @ProductName AND CategoryID = @CategoryID AND SubCategoryID = @SubCategoryID)  
  
  
 -- STEP 2: INSERT PRODUCT ATTRIBUTE ENTRIES INTO PRODUCT ATTRIBUTES TABLE  
  
 IF @Value != ''  
  BEGIN  
   DECLARE @InsertQuery TABLE (Query NVARCHAR(MAX))  
   INSERT INTO @InsertQuery SELECT * FROM STRING_SPLIT(@Value,'}')  
  
   DECLARE @RowCount INT = 1  
  
   WHILE @RowCount <= (SELECT COUNT(Query) FROM @InsertQuery)  
    BEGIN   
     DECLARE @ProdAttrValue NVARCHAR(MAX)  
       
     -- SELECT nth RECORD FROM THE TABLE  
     SELECT @ProdAttrValue = Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @InsertQuery ) T1  
     WHERE T1.RowIndex = @RowCount  
  
     IF @ProdAttrValue != ''  
      BEGIN  
       DECLARE @Params NVARCHAR(MAX)   
       SET @Params = ''  
  
       SET @Params = REPLACE(SUBSTRING(@ProdAttrValue, CHARINDEX('{', @ProdAttrValue) + 1, CHARINDEX('(', @ProdAttrValue) - 2),'"','''');  
  
       DECLARE @SPInsertQuery TABLE (Query NVARCHAR(MAX))  
       INSERT INTO @SPInsertQuery SELECT * FROM STRING_SPLIT(@Params, ',')  
  
       DECLARE @DoorTypeName NVARCHAR(500)  
       DECLARE @PicCode NVARCHAR(MAX)  
       DECLARE @Width INT  
       DECLARE @ProdSizePriceDesc NVARCHAR(500)  
       DECLARE @ProdSizePrices NVARCHAR(MAX)  
  
       SET @DoorTypeName = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @SPInsertQuery ) T2 WHERE T2.RowIndex = 1)  
       SET @PicCode = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @SPInsertQuery ) T2 WHERE T2.RowIndex = 2)  
       SET @Width = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @SPInsertQuery ) T2 WHERE T2.RowIndex = 3)  
       SET @ProdSizePriceDesc = (SELECT Query FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT '')) AS  RowIndex FROM @SPInsertQuery ) T2 WHERE T2.RowIndex = 4)  
	   SET @ProdSizePriceDesc = REPLACE(REPLACE(REPLACE(@ProdSizePriceDesc, '`',''''), '[','('), ']',')');
       SET @ProdSizePrices = SUBSTRING(@ProdAttrValue, CHARINDEX('(', @ProdAttrValue), LEN(@ProdAttrValue));
  
       IF ((SELECT COUNT(Query) FROM @SPInsertQuery) > 0)  
        BEGIN  
         EXEC sp_ImportProdAttrDetailsFromOldDB @ProductID, @DoorTypeName, @PicCode, @Width, @ProdSizePriceDesc, @ProdSizePrices  
        END  
       DELETE FROM @SPInsertQuery  
      END  
  
     SET @RowCount = @RowCount + 1;  
    END  
  
   DELETE FROM @InsertQuery  
  END   
END  

GO

/****** Object:  StoredProcedure [ecotechdoors].[sp_InsertUpdateProductAttributes]    Script Date: 27-09-2020 23:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC sp_InsertUpdateProductAttributes

CREATE PROC [ecotechdoors].[sp_InsertUpdateProductAttributes]
	--@ProductID INT,
	--@DoorTypeID INT,
	--@DoorTypeName NVARCHAR(MAX),
	--@SQLQuery NVARCHAR(MAX)
AS
BEGIN
	DECLARE @RowCount INT = 1

	DECLARE @InsertQuery TABLE (Query NVARCHAR(MAX))
	INSERT INTO @InsertQuery SELECT * FROM dbo.SplitString('ABC,DEF,GJI',',')

	WHILE @RowCount <= (SELECT COUNT(Query) FROM @InsertQuery)
		BEGIN 
			DECLARE @queryData NVARCHAR(MAX) = ''
			SET @queryData = @queryData + 'INSERT INTO [ecotechdoors].[DoorType] VALUES ';

				-- SELECT nth RECORD FROM THE TABLE
				WITH myTableWithRows AS (
					SELECT (ROW_NUMBER() OVER (ORDER BY Query)) as row,*
					FROM @InsertQuery)

				SELECT @queryData = @queryData + myTableWithRows.Query FROM myTableWithRows WHERE row = @RowCount

			SELECT @queryData
			SET @RowCount = @RowCount + 1;
		END

	DELETE FROM @InsertQuery
END

GO

/****** Object:  StoredProcedure [ecotechdoors].[sp_setAllAttributesInActive]    Script Date: 27-09-2020 23:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC sp_setAllAttributesInActive 1
      
CREATE PROC [ecotechdoors].[sp_setAllAttributesInActive]      
 @ProductAttributeID int      
AS      
BEGIN      
 UPDATE [ecotechdoors].[ProductAttributeThickness] 
 SET ACTIVE = 0 WHERE ProductAttributeID = @ProductAttributeID
END

GO

USE [webasqvy_ecotechdoors]
GO

/****** Object:  StoredProcedure [ecotechdoors].[sp_getProducts]    Script Date: 28-09-2020 19:47:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- EXEC sp_getProducts 'EEFIVG'

ALTER PROC [ecotechdoors].[sp_getProducts]
	@ProductCode NVARCHAR(400)
AS
BEGIN
	SELECT DISTINCT(P.ProductID), P.ProductName, C.CategoryID, C.CategoryName, S.SubCategoryID, S.SubCategoryName
	FROM [ecotechdoors].[ProductSizeAndPrices] PSP
	INNER JOIN [ecotechdoors].[ProductAttributeThickness] PAT ON PAT.ProductAttributeThicknessID = PSP.ProductAttributeThicknessID
	INNER JOIN [ecotechdoors].[ProductAttributes] PA ON PA.[ProductAttributeID] = PAT.[ProductAttributeID]
	INNER JOIN [ecotechdoors].[Products] P ON P.[ProductID] = PA.ProductID
	LEFT OUTER JOIN [ecotechdoors].[Categories] C ON P.CategoryID = C.CategoryID
	LEFT OUTER JOIN [ecotechdoors].[SubCategories] S ON P.SubCategoryID = S.SubCategoryID
	WHERE PSP.ProductCode LIKE '%' + @ProductCode + '%'
END
GO


