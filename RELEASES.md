# Releases

## v1.0.0 (Initial Release)

### Features

- 📊 Create formatted tables with headers and data rows
- 📏 Configurable cell padding
- ↔️ Text alignment options (left/right) for headers and rows
- 🔄 Automatic column width calculation
- 🎨 Unicode box-drawing characters for clean borders
- 📐 Support for varying column counts across rows

### API

- `SetHeaders(params string[] headers)` - Sets the table headers
- `AddRow(params string[] row)` - Adds a data row to the table
- `ClearRows()` - Removes all data rows from the table
- `ToString()` - Returns the formatted table as a string

### Properties

- `Padding` - Number of spaces on each side of cell content (default: 1)
- `HeaderTextAlignRight` - Right-align header text (default: false)
- `RowTextAlignRight` - Right-align row text (default: false)
