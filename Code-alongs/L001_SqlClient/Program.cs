
using Microsoft.Data.SqlClient;

var connectionString = "Data Source=localhost;Database=everyloop;Integrated Security=True;TrustServerCertificate=True;";

var query = "select * from GameOfThrones where season = 1;";

using (var connection = new SqlConnection(connectionString))
using (var command = new SqlCommand(query, connection))
{
    connection.Open();

    using (var reader = command.ExecuteReader())
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            Console.Write($"{reader.GetName(i),-15}");
        }

        Console.WriteLine();

        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{GetFirstNCharacters(reader.GetValue(i), 13),-15}");
            }

            Console.WriteLine();
        }
    }
}

static string GetFirstNCharacters(object o, int n)
{
    string s = o.ToString();
    return s.ToString().Substring(0, s.Length > n ? n : s.Length);
}