CREATE PROCEDURE [dbo].[GetTopProducts]
    @TopN INT
AS
BEGIN
    SELECT TOP (@TopN)
        p.ProductID,
        p.Name,
        p.ProductNumber,
        p.ListPrice,
        p.SellStartDate
    FROM
        Production.Product p
    ORDER BY
        p.ListPrice DESC
END