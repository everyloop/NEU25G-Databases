
using L006_ModelConfiguration.Model;

using var db = new ConfigDemoContext();

db.Database.EnsureDeleted();
db.Database.EnsureCreated();

var studentA = new Student() {
    SocialSecurityNumber = "970412-3452",
    FirstName = "Anders",
    LastName = "Andersson",
    DateOfBirth = new DateOnly(1997, 4, 12)
};

var studentB = new Student()
{
    SocialSecurityNumber = "970602-5353",
    FirstName = "Bengt",
    LastName = "Bengtsson",
    DateOfBirth = new DateOnly(1997, 6, 2)
};

db.Students.Add(studentA);
db.Students.Add(studentB);

db.SaveChanges();

Console.WriteLine();

