-- Table valued function returnerar tabell data som man kan selecta från.
-- T.ex openrowset (hämta från fil), openjson, generate_series, string_split

--select * from generate_series(1, 10)

--select * from string_split('Detta är ett exempel på en text', ' ')

declare @json nvarchar(max) = '    {
        "Id": 10248,
        "Customer": "VINET",
        "EmployeeId": 5,
        "Timestamps": {
            "OrderDate": "2012-07-04",
            "RequiredDate": "2012-08-01",
            "ShippedDate": "2012-07-16"
        },
        "ShipVia": 3,
        "Freight": 1.675000000000000e+001,
        "ShipInfo": {
            "Name": "Vins et alcools Chevalier",
            "Address": "59 rue de l''Abbaye",
            "City": "Reims",
            "Region": "Western Europe",
            "PostalCode": "51100",
            "Country": "France"
        }
    }'

-- openrowset: table valued function som läser in data från en extern källa (t.ex en .csv eller .json fil.) 

-- openjson läser jsondata och returnerar som tabell data
select * from openjson(@json);

select * from openjson(@json) WITH
(
    Id int,
    CustomerId nvarchar(20) '$.Customer',
    OrderDate DATETIME2 '$.Timestamps.OrderDate',
    --FirstProduct nvarchar(100) '$.Items[0].ProductName'
    --Timestamps nvarchar(max) as json
)
