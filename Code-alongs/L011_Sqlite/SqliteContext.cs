using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class SqliteContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new SqliteConnectionStringBuilder() {
            DataSource = "database.db",
            Mode = SqliteOpenMode.ReadWriteCreate,
        }.ToString();

        optionsBuilder
            .UseSqlite(connectionString)
            .LogTo(
                message => MyLogger(message),
                //new[] { DbLoggerCategory.Database.Transaction.Name, DbLoggerCategory.Database.Command.Name },
                new[] { DbLoggerCategory.Database.Name },
                LogLevel.Information,
                DbContextLoggerOptions.Level | DbContextLoggerOptions.LocalTime
            )
            .EnableSensitiveDataLogging();
    }

    private void MyLogger(string message)
    {
        var lines = message.Split('\n');

        Console.WriteLine();

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0) Console.ForegroundColor = ConsoleColor.Blue;
            if (i == 1) Console.ForegroundColor = ConsoleColor.DarkGray;
            if (i == 2) Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(lines[i]);
        }

        Console.ResetColor();
        Console.WriteLine();
    }
}

class Country
{
    public int CountryId { get; set; }
    public string Name { get; set; }
    public List<City> Cities { get; set; }
}

class City
{
    public int CityId { get; set; }
    public string Name { get; set; }
    public Country Country { get; set; }
    public int CountryId { get; set; }
}
