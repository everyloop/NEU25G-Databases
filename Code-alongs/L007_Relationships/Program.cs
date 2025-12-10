using L007_Relationships.Model;
using Microsoft.EntityFrameworkCore;

using var db = new RelationshipDemoContext();

db.Database.EnsureDeleted();
db.Database.EnsureCreated();

var query = db.Students.Include(s => s.Course);

Console.WriteLine(query.ToQueryString());

var students = query.ToList();

Console.WriteLine();