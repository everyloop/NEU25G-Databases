-- För att få resultatet i json, avsluta query med for json auto, eller for json path
-- for json auto är enklast då den autoformaterar. Funkar även med join.
-- for json path kan man själv ange path med dot-notation. Använd isåfall subquery istället för join.

-- SELECT
--     Id,
--     CustomerId as 'Customer',
--     EmployeeId,
--     OrderDate as 'Timestamps.OrderDate',
--     RequiredDate as 'Timestamps.RequiredDate',
--     ShippedDate as 'Timestamps.ShippedDate',
--     ShipVia,
--     Freight,
--     ShipName as 'ShipInfo.Name',
--     ShipAddress as 'ShipInfo.Address',
--     ShipCity as 'ShipInfo.City',
--     ShipRegion as 'ShipInfo.Region',
--     ShipPostalCode as 'ShipInfo.PostalCode',
--     ShipCountry as 'ShipInfo.Country'
-- FROM 
--     company.orders
-- for json path

-- select 
--     o.Id,
--     OrderDate as 'Timestamps.OrderDate',
--     RequiredDate as 'Timestamps.RequiredDate',
--     ShippedDate as 'Timestamps.ShippedDate',
--     Items.ProductId,
--     Items.Quantity,
--     Items.UnitPrice
-- from 
--     company.orders o
--     join company.order_details Items on o.Id = Items.OrderId
-- for json auto

SELECT
    Id,
    CustomerId as 'Customer',
    EmployeeId,
    OrderDate as 'Timestamps.OrderDate',
    RequiredDate as 'Timestamps.RequiredDate',
    ShippedDate as 'Timestamps.ShippedDate',
    ShipVia,
    Freight,
    ShipName as 'ShipInfo.Name',
    ShipAddress as 'ShipInfo.Address',
    ShipCity as 'ShipInfo.City',
    ShipRegion as 'ShipInfo.Region',
    ShipPostalCode as 'ShipInfo.PostalCode',
    ShipCountry as 'ShipInfo.Country',
    (
        select 
            p.ProductName,
            od.UnitPrice,
            od.Quantity 
        from 
            company.order_details od
            join company.products p on p.Id = od.ProductId
        where 
            od.OrderId = o.Id for json path
    ) as 'Items'
FROM 
    company.orders o
for json path