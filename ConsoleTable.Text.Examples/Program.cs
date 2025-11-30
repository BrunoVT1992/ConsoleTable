namespace ConsoleTable.Text.Examples;

class Program
{
    static void Main(string[] args)
    {
        WriteDefaultTable();

        WriteTableWithStyling(true, true, 10);

        WriteTableWithStyling(false, true, 10);

        WriteTableWithStyling(true, false, 10);

        WriteTableWithStyling(false, false, 10);

        WriteTableWithoutHeaders();

        WriteTableMoreHeaders();

        WriteTableLessHeaders();

        WriteTableEachRowRandom();

        WriteTableFluent();

        Console.Read();
    }

    private static void WriteDefaultTable()
    {
        Console.WriteLine();
        Console.WriteLine("Default table:");

        var table = new Table();
        table.SetHeaders("Name", "Age", "City");
        table.AddRow("Alice Cooper", "30", "New York");
        table.AddRow("Bob", "25", "Los Angeles");
        table.AddRow("Charlie Brown", "47", "Chicago");

        Console.WriteLine(table.ToTable());
        Console.WriteLine();
    }

    private static void WriteTableWithoutHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table without headers:");

        var table = new Table();

        for (int i = 1; i <= 5; i++)
            table.AddRow($"name {i}", (i * 15).ToString());

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableMoreHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table with more headers:");

        var table = new Table();

        table.SetHeaders("Name", "Age", "City", "Country");

        table.AddRow("Alice Cooper", "30");
        table.AddRow("Bob", "25");
        table.AddRow("Charlie Brown", "47");

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableLessHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table with less headers:");

        var table = new Table();

        table.SetHeaders("Name");

        table.AddRow("Alice Cooper", "30", "New York");
        table.AddRow("Bob", "25", "Los Angeles");
        table.AddRow("Charlie Brown", "47", "Chicago");

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableEachRowRandom()
    {
        Console.WriteLine();
        Console.WriteLine("Table with random row column count:");

        var table = new Table();

        table.SetHeaders("Name", "Age", "City");

        table.AddRow("Alice");
        table.AddRow("Bob", "25", "Antwerp", "Belgium");
        table.AddRow("Charlie", "47", "Chicago");
        table.AddRow("Karina", "33", "Lima", "Peru", "South-America");
        table.AddRow("Jenny", "43");
        table.AddRow("John");
        table.AddRow("Johny");
        table.AddRow();
        table.AddRow(null!);
        table.AddRow("Thomas", "33", "Brussels", "Belgium", "Europe", "Earth", "Solar System");
        table.AddRow("Nathalie", "29", "Paris", "France", "Europe", "Earth", "Solar System");
        table.AddRow("Mathias", "37", "Oslo", "Norway", "Europe", "Earth", "Solar System");
        table.AddRow("Kenny", "55", "Tokyo");

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableWithStyling(bool headerTextAlignRight, bool rowTextAlignRight, int padding)
    {
        Console.WriteLine();
        Console.WriteLine($"Table with folowing styling:");
        Console.WriteLine($"Header text alignment: {(headerTextAlignRight ? "right" : "left")}");
        Console.WriteLine($"Row text alignment: {(rowTextAlignRight ? "right" : "left")}");
        Console.WriteLine($"Padding: {padding}");

        var table = new Table
        {
            Padding = padding,
            HeaderTextAlignmentRight = headerTextAlignRight,
            RowTextAlignmentRight = rowTextAlignRight
        };

        table.SetHeaders("Name", "Age", "City");

        for (int i = 1; i <= 10; i++)
        {
            if (i % 2 == 0)
                table.AddRow($"Name {i}", (i * 8).ToString(), $"City {i}");
            else
                table.AddRow($"Very Long Name {i}", (i * 8).ToString(), $"City {i}");
        }

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableFluent()
    {
        Console.WriteLine();
        Console.WriteLine("Table fluent:");

        var tableString = new Table()
            .SetHeaders("Name", "Age", "City")
            .AddRow("Alice Cooper", "30", "New York")
            .AddRow("Bob", "25", "Los Angeles")
            .AddRow("Charlie Brown", "47", "Chicago")
            .ToTable();

        Console.WriteLine(tableString);
        Console.WriteLine();
    }
}
