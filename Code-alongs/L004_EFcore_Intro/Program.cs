

using L004_EFcore_Intro.Model;
using Microsoft.EntityFrameworkCore;

//EnsureDatabaseIsDeleted();
//EnsureDatabaseIsCreated();

//Blog myBlog = GetBlog(".com");

//CreateBlog("myblog.com/coding");
//CreateBlog("myblog.com/SQL");


List<Blog> blogs = GetBlogs(searchString: ".com", printSQL: true);

foreach (var blog in blogs)
{
    Console.WriteLine($"{blog.Url}, Rating: {blog.RatingRenamed}");
}


static void CreateBlog(string url, int rating = 5)
{
    var blog = new Blog() { Url = url, RatingRenamed = rating };
    
    using var db = new BloggingContext();

    db.Blogs.Add(blog);

    db.SaveChanges();
}

static Blog? GetBlog(string searchString = "", bool printSQL = false)
{
    using var db = new BloggingContext();
    
    //var query = db.Blogs.Where(b => b.Url.Contains(searchString));

    //if (printSQL)
    //{
    //    Console.WriteLine(query.ToQueryString());
    //    Console.WriteLine();
    //}

    var defaultBlog = new Blog() { Id = 5, Url = "myblog.com/default", RatingRenamed = 5 };

    return db.Blogs.FirstOrDefault(b => b.Url.Contains(searchString)) ?? defaultBlog;
}

static List<Blog> GetBlogs(string searchString = "", bool printSQL = false)
{
    using var db = new BloggingContext();
    //var query = db.Blogs.Where(b => b.Rating >= 4 && b.Url.StartsWith("m")).OrderByDescending(b => b.Rating);
    var query = db.Blogs.Where(b => b.Url.Contains(searchString));

    if (printSQL)
    {
        Console.WriteLine(query.ToQueryString());
        Console.WriteLine();
    }

    return query.ToList();
}

static void EnsureDatabaseIsCreated()
{
    using var db = new BloggingContext();
    db.Database.EnsureCreated();
}

static void EnsureDatabaseIsDeleted()
{
    using var db = new BloggingContext();
    db.Database.EnsureDeleted();
}


