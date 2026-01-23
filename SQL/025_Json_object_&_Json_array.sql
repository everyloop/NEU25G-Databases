
declare @json nvarchar(max) = JSON_OBJECT(
	'FirstName':'Fredrik', 
	'LastName': 'Johansson',
	'Contacts': JSON_OBJECT(
		'Email':'fredrik@gmail.com',
		'Phone': 07024435764
	),
	'Colors': JSON_ARRAY('Blue', 'Red', 'White')
);

declare @jsonarray nvarchar(max) = JSON_ARRAY(4, 3, 5, 'Test')

-- Returnerar 1 om en sträng är korrekt json, annars 0
--select ISJSON(@json);

select 
	JSON_VALUE(@json, '$.Contacts.Email') as 'Email', 
	JSON_VALUE(@json, '$.Colors[1]') as 'Color', 
	JSON_VALUE(@jsonarray, '$[3]') as 'ArrayValue';
