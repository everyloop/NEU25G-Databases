
use('fredrik');

//db.myCollection.find();
// select * from myCollection;

//db.myCollection.find({"lastName": "Johansson"});
// select * from myCollection where lastName = 'Johansson';

// db.myCollection.find({"firstName": "Fredrik", "lastName": "Johansson"});
// select * from myCollection where firstName = 'Fredrik' and lastName = 'Johansson';

//db.myCollection.find({"age": {"$gte": 40}});
// select * from myCollection where age >= 40;

// db.myCollection.find(
//     { 
//         $or: [ 
//             {"age": {"$gte": 40}}, 
//             {"lastName": "Johansson"} 
//         ]
//     }, 
//     {
//         "_id": 0,
//         "firstName": 1,
//         "age": 1
//     }
// );
// select firstName, age from myCollection where age >= 40 or lastName = 'Johansson';


// select * from myCollection where name = 'Fredrik' or name = 'Kalle' or name = 'Adam'
// select * from myCollection where name not in ('Fredrik', 'Kalle', 'Adam');

// dot notation
//db.myCollection.find({"contacts.email": "max@gmail.com"});

// db.myCollection.find(
//     { 
//         $or: [ 
//             {"age": {"$gte": 40}}, 
//             {"lastName": "Johansson"} 
//         ]
//     }
// )

// db.myCollection.updateMany(
//     { 
//         $or: [ 
//             {"age": {"$gte": 40}}, 
//             {"lastName": "Johansson"} 
//         ]
//     },
//     { $set: { "contacts": { "email": "example@gmail.com", "phone": "07023487656" } }}
// )

// db.myCollection.updateMany(
//     {},
//     { $set: { "siblings": "newValue" }}
// )

db.myCollection.deleteMany({"contacts.email": "example@gmail.com"});