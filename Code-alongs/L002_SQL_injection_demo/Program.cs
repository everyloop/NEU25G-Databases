
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

var connectionString = "Data Source=localhost;Database=everyloop;Integrated Security=True;TrustServerCertificate=True;";

using (var connection = new SqlConnection(connectionString))
{
    connection.Open();

    var searchString = string.Empty;

    while (true)
    {
        Console.Write("Enter new search string: ");

        searchString = Console.ReadLine();

        if (searchString == string.Empty) break;

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("Search string: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(searchString);
        Console.ResetColor();

        SearchAirports(connection, searchString);
    }
}

static void SearchAirports(SqlConnection connection, string searchString)
{
    var query = $"""
                select top 10
                    [IATA],
                    [Airport name],
                    [Location served]
                from 
                    Airports
                where
                    [Location served] like '%{searchString}%';
                """;

    using (var command = new SqlCommand(query, connection))
    {
        try
        {
            using (var reader = command.ExecuteReader())
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.Write($"{reader.GetName(0).ToUpper(),-10}");
                Console.Write($"{reader.GetName(1).ToUpper(),-50}");
                Console.Write($"{reader.GetName(2).ToUpper()}");
                Console.WriteLine("\n");

                Console.ForegroundColor = ConsoleColor.Gray;

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue(0),-10}{reader.GetValue(1),-50}{reader.GetValue(2)}");
                }

                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine(query);
    Console.ResetColor();
    Console.WriteLine();
}