

using L004_EFcore_Intro.Model;
using Microsoft.EntityFrameworkCore;

EnsureDatabaseIsCreated();

static void EnsureDatabaseIsCreated()
{
    using var db = new BloggingContext();
    db.Database.EnsureCreated();
}
