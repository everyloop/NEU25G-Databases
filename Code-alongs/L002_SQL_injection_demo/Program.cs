
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
    // SQL-INJECTION!!!

    // Följande kod konkatenerar användardata in i en interpolation string som sedan skickas till SQL Server.
    // Servern kan därmed inte gör skillnad på kod som utvecklaren skrivit och eventuell injecerad kod som användaren har skrivit.

    // Ett exempel är om användaren t.ex skriver in följande kod som input:
    // ' and 1 = 0 union select top 10 username, email, id from users; --

    //var query = $"""
    //            select top 10
    //                [IATA],
    //                [Airport name],
    //                [Location served]
    //            from 
    //                Airports
    //            where
    //                [Location served] like '%{searchString}%';
    //            """;


    // PARAMETERIZED QUERY

    // För att skydda sig mot SQL-injection får man ALDRIG konkatenera användardata och SQL-kod innan denna skickas till SQL-servern.
    // Använd istället parameterized queries, som skickar användardata skilt från query, så server vet vilket som är vilket.

    // Query bör ALLTID vara en statisk sträng

    var query = """
                select top 10
                    [IATA],
                    [Airport name],
                    [Location served]
                from 
                    Airports
                where
                    [Location served] like concat('%', @searchText, '%');
                """;

    using (var command = new SqlCommand(query, connection))
    {
        // Den statiska strängen ovan använder sig av en parameter @searchText, som vi kan definera som:
        
        command.Parameters.AddWithValue("@searchText", searchString);

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