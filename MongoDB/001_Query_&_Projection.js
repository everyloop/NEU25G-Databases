use("mydb");

//db.myCollection.find({"productName": "Laptop"});
// select * from myCollection where productName = 'Laptop';

// db.myCollection.find(
//     {}, 
//     {
//         "_id": false,
//         "productName": true, 
//         "color": true
//     }
// );
// select productName, color from myCollection;

// db.myCollection.find({
//     "productName": "Laptop", 
//     "color": "White"
// });
// select * from myCollection where productName = 'Laptop' and color = 'White'

//db.myCollection.find({"NumberOfKeys": {$gte: 100}})
//select * from myCollection where NumberOfKeys >= 100;

//db.myCollection.find({"color": {$in: ["White", "Black"]}})
//select * from myCollection where color in ("White", "Black")

db.myCollection.find({
    $or: [
        {"productName": "Phone"},
        {"NumberOfKeys": {$gte: 100}}
    ]
})
// select * from myCollection
// where productName = 'Phone' or NumberOfKeys >= 100;