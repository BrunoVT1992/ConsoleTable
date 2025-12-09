# ConsoleTable.Text

A lightweight .NET library for creating beautifully formatted console tables with customizable headers, rows, padding, and text alignment.

## Features

- Create formatted tables as a string with styled headers, footers and rows
- Unicode box-drawing characters for clean borders
- Automatic column width calculation
- Configurable cell padding
- Text alignment options (left/right) for headers, footers and rows
- Easy clearing and reusing of tables
- Simple and intuitive API
- Optimized for performance
- Support for varying column counts across rows (each row can have its own number of cells).

## Releases
Check releases for the changelog here [https://github.com/BrunoVT1992/ConsoleTable/releases/](https://github.com/BrunoVT1992/ConsoleTable/releases/)

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
<PackageReference Include="ConsoleTable.Text" Version="2.0.0" />
```

### nuget.org
Download this nuget package from [https://www.nuget.org/packages/ConsoleTable.Text](https://www.nuget.org/packages/ConsoleTable.Text)

## Quick Start

```csharp
using ConsoleTable.Text;

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

//Set footers
table.SetFooters("Total: 3", "Total Age: 102");

// Display the table
Console.WriteLine(table.ToTable());
```

Output:
```
┌───────────────┬────────────────┬─────────────┐
│ Name          │ Age            │ City        │
├═══════════════┼════════════════┼═════════════┤
│ Alice Cooper  │ 30             │ New York    │
├───────────────┼────────────────┼─────────────┤
│ Bob           │ 25             │ Los Angeles │
├───────────────┼────────────────┼─────────────┤
│ Charlie Brown │ 47             │ Chicago     │
└───────────────┴────────────────┴─────────────┘
  Total: 3        Total Age: 102
```

## API Reference

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Headers` | `string[]` | `Array.Empty<string>()` | The table headers. Headers are not required. |
| `Rows` | `List<string[]>` | `new List<string[]>()` | All the rows for the table. Rows are not required. |
| `Footers` | `string[]` | `Array.Empty<string>()` | The table footers. Footers are not required. |
| `Padding` | `int` | `1` | The number of spaces on each side of cell content |
| `HeaderTextAlignmentRight` | `bool` | `false` | When `true`, header text is right-aligned otherwise left aligned |
| `RowTextAlignmentRight` | `bool` | `false` | When `true`, row text is right-aligned otherwise left aligned |
| `FooterTextAlignmentRight` | `bool` | `false` | When `true`, footer text is right-aligned otherwise left aligned |
| `CachingEnabled` | `bool` | `true` | When `true`, the generated table string is cached when the ToTable method is called. Cache will be cleared on any property change or method call. |

### Methods

| Method | Description |
|--------|-------------|
| `SetHeaders(params string[] headers)` | Sets the table headers. Calling this again will overwrite previous headers. Headers are not required. |
| `AddRow(params string[] row)` | Adds a row to the table. Rows are not required. |
| `AddRows(params string[][] rows)` | Adds multiple rows to the table. Rows are not required. |
| `ClearRows()` | Removes all rows from the table (headers are preserved). |
| `SetFooters(params string[] footers)` | Sets the table footers. Calling this again will overwrite previous footers. Footers are not required. |
| `Clear()` | Clear all the headers, footers and rows from the table. |
| `ClearCache()` | Clears the generated table string cache. This can be done to save memory. |
| `ToTable() / ToString()` | Returns the formatted table as a string. |

## Examples

### Custom Padding

```csharp
using ConsoleTable.Text;

// Setup the table
var table = new Table { Padding = 10 };

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
```

Output:
```
┌─────────────────────────────────┬──────────────────────────────────┬───────────────────────────────┐
│          Name                   │          Age                     │          City                 │
├═════════════════════════════════┼══════════════════════════════════┼═══════════════════════════════┤
│          Alice Cooper           │          30                      │          New York             │
├─────────────────────────────────┼──────────────────────────────────┼───────────────────────────────┤
│          Bob                    │          25                      │          Los Angeles          │
├─────────────────────────────────┼──────────────────────────────────┼───────────────────────────────┤
│          Charlie Brown          │          47                      │          Chicago              │
└─────────────────────────────────┴──────────────────────────────────┴───────────────────────────────┘
           Total: 3                          Total Age: 102
```

### Text Alignment Right

```csharp
using ConsoleTable.Text;

// Setup the table
var table = new Table { HeaderTextAlignmentRight = true, RowTextAlignmentRight = true, FooterTextAlignmentRight = true };

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
```

Output:
```
┌───────────────┬────────────────┬─────────────┐
│          Name │            Age │        City │
├═══════════════┼════════════════┼═════════════┤
│  Alice Cooper │             30 │    New York │
├───────────────┼────────────────┼─────────────┤
│           Bob │             25 │ Los Angeles │
├───────────────┼────────────────┼─────────────┤
│ Charlie Brown │             47 │     Chicago │
└───────────────┴────────────────┴─────────────┘
       Total: 3   Total Age: 102
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

Console.WriteLine(table.ToTable());
```

Output:
```
┌──────────┬──────────┬──────────┐
│ Name     │ Age      │ City     │
├══════════┼══════════┴══════════┘
│ Alice    │
├──────────┼──────────┬──────────┬─────────┐
│ Bob      │ 25       │ Antwerp  │ Belgium │
├──────────┼──────────┼──────────┼─────────┘
│ Charlie  │ 47       │ Chicago  │
├──────────┼──────────┼──────────┼─────────┬───────────────┐
│ Karina   │ 33       │ Lima     │ Peru    │ South-America │
├──────────┼──────────┼──────────┴─────────┴───────────────┘
│ Jenny    │ 43       │
├──────────┼──────────┘
│ John     │
├──────────┤
│ Johny    │
├──────────┤
│          │
├──────────┤
│          │
├──────────┼──────────┬──────────┬─────────┬───────────────┬───────┬──────────────┐
│ Thomas   │ 33       │ Brussels │ Belgium │ Europe        │ Earth │ Solar System │
├──────────┼──────────┼──────────┼─────────┼───────────────┼───────┼──────────────┤
│ Nathalie │ 29       │ Paris    │ France  │ Europe        │ Earth │ Solar System │
├──────────┼──────────┼──────────┼─────────┼───────────────┼───────┼──────────────┤
│ Mathias  │ 37       │ Oslo     │ Norway  │ Europe        │ Earth │ Solar System │
├──────────┼──────────┼──────────┼─────────┴───────────────┴───────┴──────────────┘
│ Kenny    │ 55       │ Tokyo    │
└──────────┴──────────┴──────────┘
  Footer 1   Footer 2
```


### Write a Table Fluent

```csharp
using ConsoleTable.Text;

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
```

Output:
```
┌───────────────┬────────────────┬─────────────┐
│ Name          │ Age            │ City        │
├═══════════════┼════════════════┼═════════════┤
│ Alice Cooper  │ 30             │ New York    │
├───────────────┼────────────────┼─────────────┤
│ Bob           │ 25             │ Los Angeles │
├───────────────┼────────────────┼─────────────┤
│ Charlie Brown │ 47             │ Chicago     │
└───────────────┴────────────────┴─────────────┘
  Total: 3        Total Age: 102
```

### Header only

```csharp
using ConsoleTable.Text;

var table = new Table();
table.SetHeaders("Name", "Age", "City");

Console.WriteLine(table.ToString());
```

Output:
```
┌──────┬─────┬──────┐
│ Name │ Age │ City │
└══════┴═════┴══════┘
```

### Rows only

```csharp
using ConsoleTable.Text;

 var table = new Table();

for (int i = 1; i <= 5; i++) 
{
    table.AddRow($"name {i}", (i * 15).ToString());
}

Console.WriteLine(table.ToString());
```

Output:
```
┌────────┬────┐
│ name 1 │ 15 │
├────────┼────┤
│ name 2 │ 30 │
├────────┼────┤
│ name 3 │ 45 │
├────────┼────┤
│ name 4 │ 60 │
├────────┼────┤
│ name 5 │ 75 │
└────────┴────┘
```

### Footers only

```csharp
using ConsoleTable.Text;

var table = new Table();
table.SetFooters("Total: 3", "Total Age: 102");

Console.WriteLine(table.ToString());
```

Output:
```
  Total: 3   Total Age: 102
```

## Feature Requests & Bug Reports
If you want to log a bug or request a new feature, please do so by creating an issue on GitHub: [https://github.com/BrunoVT1992/ConsoleTable/issues](https://github.com/BrunoVT1992/ConsoleTable/issues)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Find me on
- GitHub: [https://github.com/BrunoVT1992](https://github.com/BrunoVT1992)
- [https://brunovt.be/](https://brunovt.be/)