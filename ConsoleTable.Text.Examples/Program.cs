namespace ConsoleTable.Text.Examples;

class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();

        var table = new Table
        {
            Rows = new List<string[]>
            {
                new[] { "" },
                Array.Empty<string>()
            }
        };

        Console.WriteLine("Table with empty row and empty column:");
        Console.WriteLine(table.ToTable());

        WriteDefaultTable();

        WriteDefaultTableWithProperties();

        WriteTableWithStyling(true, true, true, 10);

        WriteTableWithStyling(false, true, true, 10);

        WriteTableWithStyling(true, false, true, 10);

        WriteTableWithStyling(true, true, false, 10);

        WriteTableWithStyling(false, false, false, 10);

        WriteTableOnlyHeaders();

        WriteTableOnlyRows();

        WriteTableOnlyFooters();

        WriteTableMoreHeaders();

        WriteTableLessHeaders();

        WriteTableEachRowRandom();

        WriteTableFluent();

        WriteMultiLineTable();

        WriteTableWithoutBorders();

        //WriteBigTable();

        Console.WriteLine();
        Console.WriteLine();
        Console.Read();
    }

    private static void WriteDefaultTable()
    {
        Console.WriteLine();
        Console.WriteLine("Default table:");

        // Setup the table
        var table = new Table();

        // Set headers
        table.SetHeaders("Name", "Age", "City");

        // Add rows
        table.AddRow("Alice Cooper", "30", "New York");
        table.AddRows(new string[][]
        {
            new string[] { "Bob", "25", "Los Angeles" },
            new string[] { "Charlie Brown", "47", "Chicago" }
        });

        // Set footers
        table.SetFooters("Total: 3", "Total Age: 102");

        // Display the table
        Console.WriteLine(table.ToTable());
        Console.WriteLine();
    }

    private static void WriteMultiLineTable()
    {
        Console.WriteLine();
        Console.WriteLine("Multi line table:");

        // Setup the table
        var table = new Table
        {
            RowTextAlignmentRight = false,
            HeaderTextAlignmentRight = false,
            FooterTextAlignmentRight = false,
            Padding = 2,
            Headers = new string[] { "Name", $"Age{Environment.NewLine}&{Environment.NewLine}Birthyear", "City" },
            Rows = new List<string[]>
            {
                new string[] { "Alice Cooper", $"30{Environment.NewLine}1995", "New York" },
                new string[] { "Bob", $"25{Environment.NewLine}2000", "Los Angeles" },
                new string[] { "Charlie Brown", $"67{Environment.NewLine}1958", "Chicago" },
                new string[] { "Gloria", $"40{Environment.NewLine}1985", $"Chicago{Environment.NewLine}Originally from Bogota, Colombia" }
            },
            Footers = new string[] { $"Total: 4{Environment.NewLine}3 Male - 1 Female", "Total Age: 162" }
        };

        // Display the table
        Console.WriteLine(table.ToTable());
        Console.WriteLine();
    }

    private static void WriteDefaultTableWithProperties()
    {
        Console.WriteLine();
        Console.WriteLine("Default table with properties instead of methods:");

        var table = new Table
        {
            CachingEnabled = true,
            Headers = new string[] { "Name", "Age", "City" },
            Rows = new List<string[]>
            {
                new string[] { "Alice Cooper", "30", "New York" },
                new string[] { "Bob", "25", "Los Angeles" },
                new string[] { "Charlie Brown", "47", "Chicago" }
            },
            Footers = new string[] { "Total: 3", "Total Age: 102" }
        };

        Console.WriteLine(table.ToTable());
        Console.WriteLine();
    }

    private static void WriteTableOnlyRows()
    {
        Console.WriteLine();
        Console.WriteLine("Table only rows:");

        var table = new Table();

        for (int i = 1; i <= 5; i++)
        {
            table.AddRow($"name {i}", (i * 15).ToString());
        }

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableOnlyHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table only headers:");

        var table = new Table();
        table.SetHeaders("Name", "Age", "City");

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableOnlyFooters()
    {
        Console.WriteLine();
        Console.WriteLine("Table only footers:");

        var table = new Table();
        table.SetFooters("Total: 3", "Total Age: 102");

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableMoreHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table with more headers:");

        var table = new Table();

        table.SetHeaders("Name", "Age", "City", "Country");

        table.AddRows(
            new string[] { "Alice Cooper", "30" },
            new string[] { "Bob", "25" },
            new string[] { "Charlie Brown", "47" }
        );

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableLessHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table with less headers:");

        var table = new Table();

        table.SetHeaders("Name");

        table.AddRows(
            new string[] { "Alice Cooper", "30", "New York" },
            new string[] { "Bob", "25", "Los Angeles" },
            new string[] { "Charlie Brown", "47", "Chicago" }
        );

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
        table.AddRow("Jenny", $"43{Environment.NewLine}1982");
        table.AddRow("John");
        table.AddRow("Johny");
        table.AddRow();
        table.AddRow(null!);
        table.AddRow("Thomas", "33", "Brussels", $"Belgium{Environment.NewLine}BE", "Europe", "Earth", "Solar System");
        table.AddRow("Nathalie", "29", "Paris", "France", "Europe", "Earth", "Solar System");
        table.AddRow("Mathias", "37", "Oslo", "Norway", "Europe", "Earth", "Solar System");
        table.AddRow("Kenny", "55", "Tokyo");

        table.SetFooters("Footer 1", "Footer 2");

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableWithStyling(bool headerTextAlignRight, bool rowTextAlignRight, bool footerTextAlignRight, int padding)
    {
        Console.WriteLine();
        Console.WriteLine($"Table with following styling:");
        Console.WriteLine($"Header text alignment: {(headerTextAlignRight ? "right" : "left")}");
        Console.WriteLine($"Row text alignment: {(rowTextAlignRight ? "right" : "left")}");
        Console.WriteLine($"Footer text alignment: {(footerTextAlignRight ? "right" : "left")}");
        Console.WriteLine($"Padding: {padding}");

        var table = new Table
        {
            CachingEnabled = true,
            Padding = padding,
            HeaderTextAlignmentRight = headerTextAlignRight,
            RowTextAlignmentRight = rowTextAlignRight,
            FooterTextAlignmentRight = footerTextAlignRight
        };

        table.SetHeaders("Name", "Age", "City");

        for (int i = 1; i <= 10; i++)
        {
            if (i % 2 == 0)
                table.AddRow($"Name {i}", (i * 8).ToString(), $"City {i}");
            else
                table.AddRow($"Very Long Name {i}", (i * 8).ToString(), $"City {i}");
        }

        table.SetFooters("Footer 1", "Footer 2", "Footer 3");

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
            .AddRows(
                new string[] { "Bob", "25", "Los Angeles" },
                new string[] { "Charlie Brown", "47", "Chicago" }
            )
            .SetFooters("Total: 3", "Total Age: 102")
            .ToTable();

        Console.WriteLine(tableString);
        Console.WriteLine();
    }

    private static void WriteTableWithoutBorders()
    {
        Console.WriteLine();
        Console.WriteLine("Table without borders:");

        // Setup the table
        var table = new Table
        {
            ShowBorders = false
        };

        // Set headers
        table.SetHeaders("Name", "Age", "City");

        // Add rows
        table.AddRow("Alice Cooper", "30", "New York");
        table.AddRows(new string[][]
        {
            new string[] { "Bob", "25", "Los Angeles" },
            new string[] { "Charlie Brown", "47", "Chicago" }
        });

        // Set footers
        table.SetFooters("Total: 3", "Total Age: 102");

        // Display the table
        Console.WriteLine(table.ToTable());
        Console.WriteLine();
    }

    private static void WriteBigTable()
    {
        Console.WriteLine();
        Console.WriteLine("Big table (may take some seconds to generate):");

        var table = new Table
        {
            CachingEnabled = true,
            HeaderTextAlignmentRight = false,
            RowTextAlignmentRight = false,
            Padding = 2
        };

        var columnCount = 5;
        var headers = new List<string>();
        for (var columnPos = 1; columnPos <= columnCount; columnPos++)
        {
            if (columnPos % 2 == 0)
                headers.Add($"Header {columnPos}");
            else
                headers.Add($"MultiLine{Environment.NewLine}Header {columnPos}");
        }
        table.Headers = headers.ToArray();

        var rows = new List<string[]>();
        for (var rowPos = 1; rowPos <= 100000; rowPos++)
        {
            var row = new string[columnCount];
            for (var columnPos = 1; columnPos <= columnCount; columnPos++)
            {
                if (columnPos % 2 == 0)
                    row[columnPos - 1] = $"Row {rowPos} -> Column {columnPos}";
                else
                    row[columnPos - 1] = $"Row {rowPos}{Environment.NewLine}Column {columnPos}";
            }
            rows.Add(row);
        }
        table.Rows = rows;

        var footers = new List<string>();
        for (var columnPos = 1; columnPos <= columnCount; columnPos++)
        {
            if (columnPos % 2 == 0)
                footers.Add($"Footer {columnPos}");
            else
                footers.Add($"Footer{Environment.NewLine}{columnPos}");
        }
        table.Footers = footers.ToArray();

        var tableString = table.ToTable();

        Console.WriteLine(tableString);
        Console.WriteLine();
    }
}
