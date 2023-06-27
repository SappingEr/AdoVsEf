CREATE PROCEDURE [dbo].[GetOrderWithDetailsById] @OrderId INT
AS
SELECT [o].[OrderID]        AS [OrderID]
     , [o].[OrderDate]      AS [OrderDate]
     , [o].[RequiredDate]   AS [RequiredDate]
     , [o].[Freight]        AS [Freight]
     , [o].[ShipName]       AS [ShipName]
     , [o].[ShipAddress]    AS [ShipAddress]
     , [o].[ShipCity]       AS [ShipCity]
     , [o].[ShipRegion]     AS [ShipRegion]
     , [o].[ShipPostalCode] AS [ShipPostalCode]
     , [o].[ShipCountry]    AS [ShipCountry]
FROM [dbo].[Orders] AS [o]
WHERE [o].OrderID = @OrderId

SELECT [o].[OrderID]      AS [OrderID]
     , [p].[ProductName]  AS [ProductName]
     , [c].[CategoryName] AS [CategoryName]
     , [od].[UnitPrice]   AS [UnitPrice]
     , [od].[Quantity]    AS [Quantity]
     , [od].[Discount]    AS [Discount]
FROM [dbo].[Orders] AS [o]
         INNER JOIN [dbo].[Order Details] as [od] ON [o].OrderID = od.OrderID
    AND [o].OrderID = @OrderId
         INNER JOIN [dbo].[Products] AS [p] ON od.ProductID = p.ProductID
         INNER JOIN [dbo].[Categories] AS [c] ON c.CategoryID = p.CategoryID
GO