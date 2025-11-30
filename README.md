# ConsoleTable

A lightweight .NET library for creating beautifully formatted console tables with customizable headers, rows, padding, and text alignment.

## Features

- Create formatted tables as a string with headers and data rows
- Unicode box-drawing characters for clean borders
- Automatic column width calculation
- Configurable cell padding
- Text alignment options (left/right) for headers and rows
- Easy clearing and reusing of tables
- Simple and intuitive API
- Optimized for performance
- Support for varying column counts across rows (each row can have its own number of cells).

## Installation

### Package Manager
```
Install-Package ConsoleTable.Text
```

### .NET CLI
```
dotnet add package ConsoleTable.Text
```

### PackageReference
```xml
<PackageReference Include="ConsoleTable.Text" Version="1.0.0" />
```

## Quick Start

```csharp
using ConsoleTable.Text;

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
│ Charlie │ 47  │ Chicago     │
└─────────┴─────┴─────────────┘
```

## API Reference

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Padding` | `int` | `1` | The number of spaces on each side of cell content |
| `HeaderTextAlignmentRight` | `bool` | `false` | When `true`, header text is right-aligned otherwise left aligned |
| `RowTextAlignmentRight` | `bool` | `false` | When `true`, row text is right-aligned otherwise left aligned |

### Methods

| Method | Description |
|--------|-------------|
| `SetHeaders(params string[] headers)` | Sets the table headers. Calling this again will overwrite previous headers. Headers are not required. |
| `AddRow(params string[] row)` | Adds a data row to the table. Rows are not required. |
| `ClearRows()` | Removes all data rows from the table (headers are preserved). |
| `Clear()` | Clear all the headers and rows from the table. |
| `ToTable() / ToString()` | Returns the formatted table as a string. |

## Examples

### Custom Padding

```csharp
using ConsoleTable.Text;

var table = new Table { Padding = 10 };

table.SetHeaders("Name", "Age", "City");

table.AddRow("Alice", "30", "New York");
table.AddRow("Bob", "25", "Los Angeles");
table.AddRow("Charlie", "47", "Chicago");

Console.WriteLine(table.ToTable());
```

Output:
```
┌───────────────────────────┬───────────────────────┬───────────────────────────────┐
│          Name             │          Age          │          City                 │
├───────────────────────────┼───────────────────────┼───────────────────────────────┤
│          Alice            │          30           │          New York             │
├───────────────────────────┼───────────────────────┼───────────────────────────────┤
│          Bob              │          25           │          Los Angeles          │
├───────────────────────────┼───────────────────────┼───────────────────────────────┤
│          Charlie          │          47           │          Chicago              │
└───────────────────────────┴───────────────────────┴───────────────────────────────┘
```

### Text Alignment Right

```csharp
using ConsoleTable.Text;

var table = new Table { HeaderTextAlignmentRight = true, RowTextAlignmentRight = true };

table.SetHeaders("Name", "Age", "City");

table.AddRow("Alice Cooper", "30", "New York");
table.AddRow("Bob", "25", "Los Angeles");
table.AddRow("Charlie Brown", "47", "Chicago");

Console.WriteLine(table.ToTable());
```

Output:
```
┌───────────────┬─────┬─────────────┐
│          Name │ Age │        City │
├───────────────┼─────┼─────────────┤
│  Alice Cooper │  30 │    New York │
├───────────────┼─────┼─────────────┤
│           Bob │  25 │ Los Angeles │
├───────────────┼─────┼─────────────┤
│ Charlie Brown │  47 │     Chicago │
└───────────────┴─────┴─────────────┘
```

### Table with inconsistent columns across rows

```csharp
using ConsoleTable.Text;

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
```

Output:
```
┌──────────┬─────┬──────────┐
│ Name     │ Age │ City     │
├──────────┼─────┴──────────┘
│ Alice    │
├──────────┼─────┬──────────┬─────────┐
│ Bob      │ 25  │ Antwerp  │ Belgium │
├──────────┼─────┼──────────┼─────────┘
│ Charlie  │ 47  │ Chicago  │
├──────────┼─────┼──────────┼─────────┬───────────────┐
│ Karina   │ 33  │ Lima     │ Peru    │ South-America │
├──────────┼─────┼──────────┴─────────┴───────────────┘
│ Jenny    │ 43  │
├──────────┼─────┘
│ John     │
├──────────┤
│ Johny    │
├──────────┤
│          │
├──────────┤
│          │
├──────────┼─────┬──────────┬─────────┬───────────────┬───────┬──────────────┐
│ Thomas   │ 33  │ Brussels │ Belgium │ Europe        │ Earth │ Solar System │
├──────────┼─────┼──────────┼─────────┼───────────────┼───────┼──────────────┤
│ Nathalie │ 29  │ Paris    │ France  │ Europe        │ Earth │ Solar System │
├──────────┼─────┼──────────┼─────────┼───────────────┼───────┼──────────────┤
│ Mathias  │ 37  │ Oslo     │ Norway  │ Europe        │ Earth │ Solar System │
├──────────┼─────┼──────────┼─────────┴───────────────┴───────┴──────────────┘
│ Kenny    │ 55  │ Tokyo    │
└──────────┴─────┴──────────┘
```

### Write a Table Fluent

```csharp
using ConsoleTable.Text;

 var tableString = new Table()
    .SetHeaders("Name", "Age", "City")
    .AddRow("Alice Cooper", "30", "New York")
    .AddRow("Bob", "25", "Los Angeles")
    .AddRow("Charlie Brown", "47", "Chicago")
    .ToTable();

Console.WriteLine(tableString);
```

Output:
```
┌───────────────┬─────┬─────────────┐
│ Name          │ Age │ City        │
├───────────────┼─────┼─────────────┤
│ Alice Cooper  │ 30  │ New York    │
├───────────────┼─────┼─────────────┤
│ Bob           │ 25  │ Los Angeles │
├───────────────┼─────┼─────────────┤
│ Charlie Brown │ 47  │ Chicago     │
└───────────────┴─────┴─────────────┘
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.