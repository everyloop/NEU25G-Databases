use("mydb");

db.myCollection.find({"dimensions.width": 100});

// db.myCollection.find(
//     {dimensions: {$exists: true}},
//     {
//         _id: false,
//         productName: true,
//         "dimensions.width": true
//     }
// );
// select productName, width where where dimensions

// db.myCollection.update(
//     {productName: "Phone"},
//     {$set: {"dimensions": {
//         length: 200,
//         width: 100,
//         height: 20
//     }}}
// )

// db.myCollection.update(
//     {productName: "Phone"},
//     {$set: {"alternativeColors": [
//         "White",
//         "Red",
//         "Blue"
//     ]}}
// )

// db.myCollection.update(
//     {productName: "Phone"},
//     {$push: {"alternativeColors": "Green"}}
// )

// db.myCollection.update(
//     {productName: "Phone"},
//     {$pop: {"alternativeColors": 1}}
// )

// db.myCollection.update(
//     {productName: "Phone"},
//     {$addToSet: {"alternativeColors": "Purple"}}
// )

// db.myCollection.update(
//     {productName: "Phone"},
//     {$pullAll: {"alternativeColors": ["Red", "Purple", "Green"]}}
// )
