use("mydb");

//db.myCollection.countDocuments({weight: 0});


// db.myCollection.updateMany(
//     {"productName": "Laptop"},
//     { $set: { color: "Pink" }}
// )
// update myCollection set color = 'Pink' where productName = 'Laptop';

// db.myCollection.updateOne(
//     {"productName": "Laptop"},
//     { $unset: { color: "" }}
// )

// db.myCollection.updateMany(
//     {"Weigth": {$exists: true}},
//     { $rename: { "Weigth": "weight" }}
// )

// db.myCollection.updateMany(
//     {"weight": {$exists: true}},
//     { $mul: { "weight": 2 }}
// )
// update myCollection set weight = weight * 2 

// db.myCollection.updateMany(
//     {weight: 0},
//     { $unset: { weight: "" }}
// )

db.myCollection.find();