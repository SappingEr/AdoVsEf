CREATE PROCEDURE [dbo].[GetProductById] @ProductId INT
AS
SELECT [p].[ProductID]
     , [p].[CategoryID]
     , [p].[Discontinued]
     , [p].[ProductName]
     , [p].[QuantityPerUnit]
     , [p].[SupplierID]
     , [p].[UnitPrice]
     , [p].[UnitsInStock]
     , [p].[UnitsOnOrder]
FROM [dbo].[Products] AS [p]
WHERE [p].[ProductID] = @ProductId;
GO
