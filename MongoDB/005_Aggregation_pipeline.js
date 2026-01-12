use("sample_mflix");

db.movies.aggregate([
    { $match: 
        {"year": {$gte: 2000}}
    },
    { $project: {
      "_id": true,
      "year": true,
      "title": true,
      "test": "$plot",
      "nextYear": { $add: ["$year", 1] },
      "titleWithYear": { $concat: ["$title", " (", {$toString: "$year"}, ")"] },
      "titleWords": {$split: ["$title", " "]},
      "imdb.rating": true,
      "imdbRating": "$imdb.rating"
    }},
    // { $lookup: {
    //   from: "comments",
    //   localField: "_id",
    //   foreignField: "movie_id",
    //   as: "comments",
    //   pipeline: [{$project: {
    //     _id: false,
    //     name: true,
    //     email: true,
    //     text: true
    //   }}]
    // }}
    { $group: {
      _id: "$year",
      "NumberOfMovies": { $count: {}},
      "AverageRating": { $avg: "$imdbRating"}
    }},
    { $match: {
      "AverageRating": { $gte: 6.5}
    }}
]);

// select year, title, plot as 'test' from movies where year <= 1910