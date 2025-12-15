using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace L009_Logging_and_Tracking.Model;

internal class DemoContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = new SqlConnectionStringBuilder()
        {
            ServerSPN = "localhost",
            InitialCatalog = "CityDB",
            IntegratedSecurity = true,
            TrustServerCertificate = true
        }.ToString();

        optionsBuilder
            .UseSqlServer(connectionString)
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
    public void PrintChangeTrackerDebugInfo(string? caption = null, bool autoDetectChanges = true)
    {
        if (caption is not null)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(caption);
            Console.ResetColor();
        }

        if (autoDetectChanges)
        {
            this.ChangeTracker.DetectChanges();
        }

        var debugInfo = this.ChangeTracker.DebugView.LongView;

        var lines = debugInfo.Split("\n");

        foreach (var line in lines)
        {
            var status = line.Trim('\r');
            if (status.EndsWith("Added"))
            {
                Console.Write(status[..^5]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Added");
                Console.ResetColor();
            }
            else if (status.EndsWith("Deleted"))
            {
                Console.Write(status[..^7]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Deleted");
                Console.ResetColor();
            }
            else if (status.EndsWith("Modified"))
            {
                Console.Write(status[..^8]);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Modified");
                Console.ResetColor();
            }
            else if (status.EndsWith("Unchanged"))
            {
                Console.Write(status[..^9]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Unchanged");
                Console.ResetColor();
            }
            else if (status.Contains("Modified Originally"))
            {
                var stringStart = status.Split("Modified Originally").First();
                var newValue = stringStart.Trim().Split(" ").Last();
                var oldValue = status.Split("Modified Originally").Last();
                var indexOfNewValue = stringStart.IndexOf(newValue);

                Console.Write(stringStart[..indexOfNewValue]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(newValue);
                Console.ResetColor();
                Console.Write(" Modified Originally");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(oldValue);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(status);
            }
        }
    }
}
