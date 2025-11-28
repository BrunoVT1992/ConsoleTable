namespace ConsoleTable.Examples;

class Program
{
    static void Main(string[] args)
    {
        WriteTableWithStyling(true, true, 10, false);

        WriteTableWithStyling(false, true, 10, false);

        WriteTableWithStyling(true, false, 10, false);

        WriteTableWithStyling(false, false, 2, true);

        WriteTableWithoutHeaders();

        WriteTableMoreHeaders();

        WriteTableLessHeaders();

        WriteTableEachRowRandom();

        WriteTableFluent();

        Console.Read();
    }

    private static void WriteTableWithoutHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table without headers:");

        var table = new Table();

        for (int i = 0; i <= 5; i++)
            table.AddRow($"name {i}", DateTime.Now.AddDays(-i).ToLongDateString(), i.ToString());

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableMoreHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table with more headers:");

        var table = new Table();

        table.SetHeaders("Name", "Date", "Number", "Id");

        table.AddRow("name 1", DateTime.Now.AddDays(-1).ToLongDateString(), "1");
        table.AddRow("name 2", DateTime.Now.AddDays(-2).ToLongDateString());
        table.AddRow("name 3");

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableLessHeaders()
    {
        Console.WriteLine();
        Console.WriteLine("Table with less headers:");

        var table = new Table();

        table.SetHeaders("Name");

        table.AddRow("name 1", DateTime.Now.AddDays(-1).ToLongDateString());
        table.AddRow("name 2", DateTime.Now.AddDays(-2).ToLongDateString(), "1");

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableEachRowRandom()
    {
        Console.WriteLine();
        Console.WriteLine("Table with random row column count:");

        var table = new Table();

        table.SetHeaders("Name", "Date");

        table.AddRow("name 1", DateTime.Now.AddDays(-1).ToLongDateString());
        table.AddRow("name 2", DateTime.Now.AddDays(-2).ToLongDateString(), "1");
        table.AddRow("name 3", DateTime.Now.AddDays(-3).ToLongDateString(), "1", "2", "3");
        table.AddRow("name 4", DateTime.Now.AddDays(-4).ToLongDateString());
        table.AddRow("name 5");
        table.AddRow("name 55");
        table.AddRow();
        table.AddRow(null!);
        table.AddRow("name 7", DateTime.Now.AddDays(-9).ToLongDateString(), "1", "2", "3", "4", "5");
        table.AddRow("name 8", DateTime.Now.AddDays(-10).ToLongDateString(), "1", "2", "3", "4", "5");
        table.AddRow("name 9", DateTime.Now.AddDays(-11).ToLongDateString(), "1", "2", "3");
        table.AddRow("name 10", DateTime.Now.AddDays(-12).ToLongDateString());

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableWithStyling(bool headerTextAlignRight, bool rowTextAlignRight, int padding, bool headerTextToUpperCase)
    {
        Console.WriteLine();
        Console.WriteLine($"Table with folowing styling:");
        Console.WriteLine($"Header text alignment: {(headerTextAlignRight ? "right" : "left")}");
        Console.WriteLine($"Row text alignment: {(rowTextAlignRight ? "right" : "left")}");
        Console.WriteLine($"Padding: {padding}");
        Console.WriteLine($"Header text to upper case: {headerTextToUpperCase}");

        var table = new Table
        {
            Padding = padding,
            HeaderTextAlignmentRight = headerTextAlignRight,
            RowTextAlignmentRight = rowTextAlignRight,
            HeaderTextToUpperCase = headerTextToUpperCase
        };

        table.SetHeaders("Name", "Date", "Number");

        for (int i = 0; i <= 10; i++)
        {
            if (i % 2 == 0)
                table.AddRow($"name {i}", DateTime.Now.AddDays(-i).ToLongDateString(), i.ToString());
            else
                table.AddRow($"very long name {i}", DateTime.Now.AddDays(-i).ToLongDateString(), (i * 5000).ToString());
        }

        Console.WriteLine(table.ToString());
        Console.WriteLine();
    }

    private static void WriteTableFluent()
    {
        Console.WriteLine();
        Console.WriteLine("Table fluent:");

        var tableString = new Table()
            .SetHeaders("Name", "Date")
             .AddRow("name 1", DateTime.Now.ToLongDateString())
             .AddRow("name 2", DateTime.Now.AddDays(-1).ToLongDateString())
             .AddRow("name 3", DateTime.Now.AddDays(-2).ToLongDateString())
             .AddRow("name 4", DateTime.Now.AddDays(-3).ToLongDateString())
             .AddRow("name 5", DateTime.Now.AddDays(-4).ToLongDateString())
             .ToTable();

        Console.WriteLine(tableString);
        Console.WriteLine();
    }
}
