# ConsoleTable

A lightweight .NET library for creating beautifully formatted console tables with customizable headers, rows, padding, and text alignment.

## Features

- 📊 Create formatted tables with headers and data rows
- 🎨 Unicode box-drawing characters for clean borders
- 🔄 Automatic column width calculation
- 📏 Configurable cell padding
- ↔️ Text alignment options (left/right) for headers and rows
- 📐 Support for varying column counts across rows

## Installation

### Package Manager
```
Install-Package ConsoleTable
```

### .NET CLI
```
dotnet add package ConsoleTable
```

### PackageReference
```xml
<PackageReference Include="ConsoleTable" Version="1.0.0" />
```

## Quick Start

```csharp
using ConsoleTable;

var table = new Table();

// Set headers
table.SetHeaders("Name", "Age", "City");

// Add rows
table.AddRow("Alice", "30", "New York");
table.AddRow("Bob", "25", "Los Angeles");
table.AddRow("Charlie", "35", "Chicago");

// Display the table
Console.WriteLine(table.ToString());
```

Output:
```
┌─────────┬─────┬─────────────┐
│ Name    │ Age │ City        │
├─────────┼─────┼─────────────┤
│ Alice   │ 30  │ New York    │
├─────────┼─────┼─────────────┤
│ Bob     │ 25  │ Los Angeles │
├─────────┼─────┼─────────────┤
│ Charlie │ 35  │ Chicago     │
└─────────┴─────┴─────────────┘
```

## API Reference

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Padding` | `int` | `1` | The number of spaces on each side of cell content |
| `HeaderTextAlignRight` | `bool` | `false` | When `true`, header text is right-aligned |
| `RowTextAlignRight` | `bool` | `false` | When `true`, row text is right-aligned |

### Methods

| Method | Description |
|--------|-------------|
| `SetHeaders(params string[] headers)` | Sets the table headers. Calling this again will overwrite previous headers. |
| `AddRow(params string[] row)` | Adds a data row to the table. |
| `ClearRows()` | Removes all data rows from the table (headers are preserved). |
| `ToString()` | Returns the formatted table as a string. |

## Examples

### Basic Table with Headers

```csharp
var table = new Table();
table.SetHeaders("Product", "Price", "Quantity");
table.AddRow("Apple", "$1.50", "100");
table.AddRow("Banana", "$0.75", "150");
table.AddRow("Orange", "$2.00", "80");

Console.WriteLine(table.ToString());
```

Output:
```
┌─────────┬───────┬──────────┐
│ Product │ Price │ Quantity │
├─────────┼───────┼──────────┤
│ Apple   │ $1.50 │ 100      │
├─────────┼───────┼──────────┤
│ Banana  │ $0.75 │ 150      │
├─────────┼───────┼──────────┤
│ Orange  │ $2.00 │ 80       │
└─────────┴───────┴──────────┘
```

### Table Without Headers

```csharp
var table = new Table();
table.AddRow("Row 1, Col 1", "Row 1, Col 2");
table.AddRow("Row 2, Col 1", "Row 2, Col 2");

Console.WriteLine(table.ToString());
```

Output:
```
┌──────────────┬──────────────┐
│ Row 1, Col 1 │ Row 1, Col 2 │
├──────────────┼──────────────┤
│ Row 2, Col 1 │ Row 2, Col 2 │
└──────────────┴──────────────┘
```

### Custom Padding

```csharp
var table = new Table { Padding = 3 };
table.SetHeaders("Name", "Value");
table.AddRow("Item", "100");

Console.WriteLine(table.ToString());
```

Output:
```
┌──────────┬───────────┐
│   Name   │   Value   │
├──────────┼───────────┤
│   Item   │   100     │
└──────────┴───────────┘
```

### Right-Aligned Text

```csharp
var table = new Table
{
    HeaderTextAlignRight = true,
    RowTextAlignRight = true
};

table.SetHeaders("Description", "Amount");
table.AddRow("Total Sales", "1,234,567");
table.AddRow("Expenses", "987,654");
table.AddRow("Profit", "246,913");

Console.WriteLine(table.ToString());
```

Output:
```
┌─────────────┬───────────┐
│ Description │    Amount │
├─────────────┼───────────┤
│ Total Sales │ 1,234,567 │
├─────────────┼───────────┤
│    Expenses │   987,654 │
├─────────────┼───────────┤
│      Profit │   246,913 │
└─────────────┴───────────┘
```

### Clearing and Reusing a Table

```csharp
var table = new Table();
table.SetHeaders("Status", "Count");
table.AddRow("Active", "10");
table.AddRow("Inactive", "5");

Console.WriteLine("Before clearing:");
Console.WriteLine(table.ToString());

table.ClearRows();
table.AddRow("Active", "15");
table.AddRow("Inactive", "3");

Console.WriteLine("After clearing and adding new rows:");
Console.WriteLine(table.ToString());
```

### Varying Column Counts

The table handles rows with different numbers of columns gracefully:

```csharp
var table = new Table();
table.SetHeaders("Name", "Date", "Number", "Id");
table.AddRow("Item 1", "2025-01-01", "100");    // 3 columns
table.AddRow("Item 2", "2025-01-02");            // 2 columns
table.AddRow("Item 3");                          // 1 column

Console.WriteLine(table.ToString());
```

Output:
```
┌────────┬────────────┬────────┬────┐
│ Name   │ Date       │ Number │ Id │
├────────┼────────────┼────────┼────┘
│ Item 1 │ 2025-01-01 │ 100    │
├────────┼────────────┼────────┘
│ Item 2 │ 2025-01-02 │
├────────┼────────────┘
│ Item 3 │
└────────┘
```

### Rows with More Columns Than Headers

```csharp
var table = new Table();
table.SetHeaders("Name");
table.AddRow("Item 1", "Extra 1");
table.AddRow("Item 2", "Extra 2", "Extra 3");

Console.WriteLine(table.ToString());
```

Output:
```
┌────────┐
│ Name   │
├────────┼─────────┐
│ Item 1 │ Extra 1 │
├────────┼─────────┼─────────┐
│ Item 2 │ Extra 2 │ Extra 3 │
└────────┴─────────┴─────────┘
```

### Combined Styling Options

```csharp
var table = new Table
{
    Padding = 2,
    HeaderTextAlignRight = false,
    RowTextAlignRight = true
};

table.SetHeaders("Item", "Quantity", "Price");
table.AddRow("Widget A", "50", "$10.00");
table.AddRow("Widget B", "1000", "$5.50");
table.AddRow("Widget C", "5", "$100.00");

Console.WriteLine(table.ToString());
```

Output:
```
┌────────────┬────────────┬───────────┐
│  Item      │  Quantity  │  Price    │
├────────────┼────────────┼───────────┤
│  Widget A  │        50  │   $10.00  │
├────────────┼────────────┼───────────┤
│  Widget B  │      1000  │    $5.50  │
├────────────┼────────────┼───────────┤
│  Widget C  │         5  │  $100.00  │
└────────────┴────────────┴───────────┘
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.