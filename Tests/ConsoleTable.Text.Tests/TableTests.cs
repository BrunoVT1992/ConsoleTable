using Xunit;

namespace ConsoleTable.Text.Tests;

public class TableTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void PerformanceCheck(bool cacheEnabled)
    {
        var table = new Table
        {
            CachingEnabled = cacheEnabled,
            HeaderTextAlignmentRight = true,
            RowTextAlignmentRight = false,
            Padding = 5
        };

        var columnCount = 100;
        var headers = new List<string>();
        for (var columnPos = 1; columnPos <= columnCount; columnPos++)
        {
            headers.Add($"Header {columnPos}");
        }
        table.Headers = headers.ToArray();

        var rows = new List<string[]>();
        for (var rowPos = 1; rowPos <= 100000; rowPos++)
        {
            var row = new string[columnCount];
            for (var columnPos = 1; columnPos <= columnCount; columnPos++)
            {
                row[columnPos - 1] = $"Row {rowPos} -> Column {columnPos}";
            }
            rows.Add(row);
        }
        table.Rows = rows;

        var tableResult1 = table.ToTable();
        Assert.NotEmpty(tableResult1);

        var tableResult2 = table.ToTable();
        Assert.NotEmpty(tableResult2);
    }

    [Fact]
    public void ToTable_EmptyTable_ReturnsEmptyString()
    {
        var table = new Table();

        var result = table.ToTable();

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToTable_WithHeadersOnly_ReturnsFormattedTable()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
    }

    [Fact]
    public void ToTable_WithRowsOnly_ReturnsFormattedTable()
    {
        var table = new Table();
        table.AddRow("John", "30");
        table.AddRow("Jane", "25");

        var result = table.ToTable();

        Assert.Contains("John", result);
        Assert.Contains("Jane", result);
        Assert.Contains("30", result);
        Assert.Contains("25", result);
    }

    [Fact]
    public void ToTable_WithHeadersAndRows_ReturnsFormattedTable()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");
        table.AddRow("Jane", "25");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
        Assert.Contains("John", result);
        Assert.Contains("Jane", result);
    }

    [Fact]
    public void ClearRows_RemovesAllRows()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");
        table.AddRow("Jane", "25");

        table.ClearRows();

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
        Assert.DoesNotContain("John", result);
        Assert.DoesNotContain("Jane", result);
    }

    [Fact]
    public void Clear_IsEmpty()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");
        table.AddRow("Jane", "25");

        table.Clear();

        var result = table.ToTable();

        Assert.Empty(result);
    }

    [Fact]
    public void ClearRows_ThenAddNewRows_Works()
    {
        var table = new Table();
        table.SetHeaders("Name");
        table.AddRow("John");
        table.ClearRows();
        table.AddRow("Jane");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.DoesNotContain("John", result);
        Assert.Contains("Jane", result);
    }

    [Theory]
    [InlineData(true, true, 10)]
    [InlineData(false, false, 0)]
    [InlineData(true, true, 0)]
    [InlineData(true, false, 10)]
    [InlineData(false, true, 10)]
    public void ToTable_WithStyling(bool headerTextAlignRight, bool rowTextAlignRight, int padding)
    {
        var table = new Table
        {
            HeaderTextAlignmentRight = headerTextAlignRight,
            RowTextAlignmentRight = rowTextAlignRight,
            Padding = padding,
        };

        table.SetHeaders("Name");
        table.AddRow("John");
        table.AddRow("Jane");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("John", result);
        Assert.Contains("Jane", result);
    }

    [Fact]
    public void Padding_AffectsTableWidth()
    {
        var table1 = new Table { Padding = 1 };
        table1.SetHeaders("Name");
        table1.AddRow("Test");

        var table2 = new Table { Padding = 5 };
        table2.SetHeaders("Name");
        table2.AddRow("Test");

        var result1 = table1.ToTable();
        var result2 = table2.ToTable();

        // Table with more padding should have longer lines
        var lines1 = result1.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var lines2 = result2.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        Assert.True(lines2[0].Length > lines1[0].Length);
    }

    [Fact]
    public void Padding_ZeroPadding_Works()
    {
        var table = new Table { Padding = 0 };
        table.SetHeaders("Name");
        table.AddRow("Test");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Test", result);
    }

    [Fact]
    public void HeaderTextAlignRight_DefaultValue_IsFalse()
    {
        var table = new Table();

        Assert.False(table.HeaderTextAlignmentRight);
    }

    [Fact]
    public void HeaderTextAlignRight_CanBeSet()
    {
        var table = new Table { HeaderTextAlignmentRight = true };

        Assert.True(table.HeaderTextAlignmentRight);
    }

    [Fact]
    public void RowTextAlignRight_DefaultValue_IsFalse()
    {
        var table = new Table();

        Assert.False(table.RowTextAlignmentRight);
    }

    [Fact]
    public void RowTextAlignRight_CanBeSet()
    {
        var table = new Table { RowTextAlignmentRight = true };

        Assert.True(table.RowTextAlignmentRight);
    }

    [Fact]
    public void ToTable_WithSingleColumn_ReturnsProperCorners()
    {
        var table = new Table();
        table.SetHeaders("Single");

        var result = table.ToTable();

        // Should have proper corners for single column
        Assert.Contains("┌", result);
        Assert.Contains("┐", result);
        Assert.Contains("└", result);
        Assert.Contains("┘", result);
    }

    [Fact]
    public void ToTable_MoreHeadersThanRowColumns_HandlesCorrectly()
    {
        var table = new Table();
        table.SetHeaders("Name", "Date", "Number", "Id");
        table.AddRow("name 1", "date 1", "1");
        table.AddRow("name 2", "date 2");
        table.AddRow("name 3");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Date", result);
        Assert.Contains("Number", result);
        Assert.Contains("Id", result);
        Assert.Contains("name 1", result);
        Assert.Contains("name 2", result);
        Assert.Contains("name 3", result);
    }

    [Fact]
    public void ToTable_LessHeadersThanRowColumns_HandlesCorrectly()
    {
        var table = new Table();
        table.SetHeaders("Name");
        table.AddRow("name 1", "date 1");
        table.AddRow("name 2", "date 2", "1");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("name 1", result);
        Assert.Contains("date 1", result);
        Assert.Contains("name 2", result);
        Assert.Contains("date 2", result);
        Assert.Contains("1", result);
    }

    [Fact]
    public void ToTable_VaryingRowWidths_HandlesCorrectly()
    {
        var table = new Table();
        table.AddRow("short");
        table.AddRow("much longer text here");
        table.AddRow("mid length");

        var result = table.ToTable();

        Assert.Contains("short", result);
        Assert.Contains("much longer text here", result);
        Assert.Contains("mid length", result);
    }

    [Fact]
    public void SetHeaders_OverwritesPreviousHeaders()
    {
        var table = new Table();
        table.SetHeaders("Old1", "Old2");
        table.SetHeaders("New1", "New2");

        var result = table.ToTable();

        Assert.DoesNotContain("Old1", result);
        Assert.DoesNotContain("Old2", result);
        Assert.Contains("New1", result);
        Assert.Contains("New2", result);
    }

    [Fact]
    public void AddRow_MultipleTimes_AddsAllRows()
    {
        var table = new Table();
        table.AddRow("Row1");
        table.AddRow("Row2");
        table.AddRow("Row3");

        var result = table.ToTable();

        Assert.Contains("Row1", result);
        Assert.Contains("Row2", result);
        Assert.Contains("Row3", result);
    }

    [Fact]
    public void ToTable_ContainsTableBorderCharacters()
    {
        var table = new Table();
        table.SetHeaders("Header");
        table.AddRow("Value");

        var result = table.ToTable();

        // Check for various border characters
        Assert.Contains("│", result);  // Vertical line
        Assert.Contains("─", result);  // Horizontal line
        Assert.Contains("├", result);  // Left joint
        Assert.Contains("┤", result);  // Right joint
    }

    [Fact]
    public void ToTable_MultipleColumns_ContainsMiddleJoint()
    {
        var table = new Table();
        table.SetHeaders("Col1", "Col2");
        table.AddRow("Val1", "Val2");

        var result = table.ToTable();

        Assert.Contains("┼", result);  // Middle joint
    }

    [Fact]
    public void ToTable_SingleRowSingleColumn_ReturnsValidTable()
    {
        var table = new Table();
        table.AddRow("X");

        var result = table.ToTable();

        Assert.Contains("X", result);
        Assert.Contains("┌", result);
        Assert.Contains("┐", result);
        Assert.Contains("└", result);
        Assert.Contains("┘", result);
    }

    [Fact]
    public void ToTable_EmptyStringValues_HandlesCorrectly()
    {
        var table = new Table();
        table.SetHeaders("Name", "Value");
        table.AddRow("", "");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Value", result);
    }

    [Fact]
    public void HeaderTextAlignRight_True_AlignsToDifferentPosition()
    {
        var tableLeft = new Table { HeaderTextAlignmentRight = false };
        tableLeft.SetHeaders("H");
        tableLeft.AddRow("VeryLongValue");

        var tableRight = new Table { HeaderTextAlignmentRight = true };
        tableRight.SetHeaders("H");
        tableRight.AddRow("VeryLongValue");

        var resultLeft = tableLeft.ToTable();
        var resultRight = tableRight.ToTable();

        // Both should contain the header, but in different positions
        Assert.Contains("H", resultLeft);
        Assert.Contains("H", resultRight);
        Assert.NotEqual(resultLeft, resultRight);
    }

    [Fact]
    public void RowTextAlignRight_True_AlignsToDifferentPosition()
    {
        var tableLeft = new Table { RowTextAlignmentRight = false };
        tableLeft.SetHeaders("HeaderHeader");
        tableLeft.AddRow("V");

        var tableRight = new Table { RowTextAlignmentRight = true };
        tableRight.SetHeaders("HeaderHeader");
        tableRight.AddRow("V");

        var resultLeft = tableLeft.ToTable();
        var resultRight = tableRight.ToTable();

        // Both should contain the value, but in different positions
        Assert.Contains("V", resultLeft);
        Assert.Contains("V", resultRight);
        Assert.NotEqual(resultLeft, resultRight);
    }

    [Fact]
    public void Headers_SetHeadersProperty()
    {
        var table = new Table();
        table.Headers = new[] { "Name", "Age" };

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
    }

    [Fact]
    public void Headers_SetProperty_OverwritesPreviousHeaders()
    {
        var table = new Table();
        table.Headers = new[] { "Old1", "Old2" };
        table.Headers = new[] { "New1", "New2" };

        var result = table.ToTable();

        Assert.DoesNotContain("Old1", result);
        Assert.DoesNotContain("Old2", result);
        Assert.Contains("New1", result);
        Assert.Contains("New2", result);
    }

    [Fact]
    public void Headers_SetHeadersProperty_ClearsCache()
    {
        var table = new Table();
        table.Headers = new[] { "Header1" };
        var firstResult = table.ToTable();

        table.Headers = new[] { "Header2" };
        var secondResult = table.ToTable();

        Assert.Contains("Header1", firstResult);
        Assert.DoesNotContain("Header1", secondResult);
        Assert.Contains("Header2", secondResult);
    }

    [Fact]
    public void Rows_SetRowsProperty()
    {
        var table = new Table();
        table.Rows = new List<string[]>
        {
            new[] { "John", "30" },
            new[] { "Jane", "25" }
        };

        var result = table.ToTable();

        Assert.Contains("John", result);
        Assert.Contains("Jane", result);
        Assert.Contains("30", result);
        Assert.Contains("25", result);
    }

    [Fact]
    public void Rows_SetRowsProperty_OverwritesPreviousRows()
    {
        var table = new Table();
        table.Rows = new List<string[]> { new[] { "OldRow" } };
        table.Rows = new List<string[]> { new[] { "NewRow" } };

        var result = table.ToTable();

        Assert.DoesNotContain("OldRow", result);
        Assert.Contains("NewRow", result);
    }

    [Fact]
    public void Rows_SetRowsProperty_ClearsCache()
    {
        var table = new Table();
        table.Rows = new List<string[]> { new[] { "Row1" } };
        var firstResult = table.ToTable();

        table.Rows = new List<string[]> { new[] { "Row2" } };
        var secondResult = table.ToTable();

        Assert.Contains("Row1", firstResult);
        Assert.DoesNotContain("Row1", secondResult);
        Assert.Contains("Row2", secondResult);
    }

    [Fact]
    public void Rows_SetNull_CreatesEmptyList()
    {
        var table = new Table();
        table.AddRow("InitialRow");
        table.Rows = null;

        var result = table.ToTable();

        Assert.Empty(result);
        Assert.NotNull(table.Rows);
        Assert.Empty(table.Rows);
    }

    [Fact]
    public void AddRows_AddsMultipleRows()
    {
        var table = new Table();
        table.AddRows(
            new[] { "Row1Col1", "Row1Col2" },
            new[] { "Row2Col1", "Row2Col2" },
            new[] { "Row3Col1", "Row3Col2" }
        );

        var result = table.ToTable();

        Assert.Contains("Row1Col1", result);
        Assert.Contains("Row2Col1", result);
        Assert.Contains("Row3Col1", result);
    }

    [Fact]
    public void AddRows_AppendsToExistingRows()
    {
        var table = new Table();
        table.AddRow("ExistingRow");
        table.AddRows(
            new[] { "NewRow1" },
            new[] { "NewRow2" }
        );

        var result = table.ToTable();

        Assert.Contains("ExistingRow", result);
        Assert.Contains("NewRow1", result);
        Assert.Contains("NewRow2", result);
    }

    [Fact]
    public void AddRows_WithNull_DoesNotThrow()
    {
        var table = new Table();
        table.AddRow("ExistingRow");

        var exception = Record.Exception(() => table.AddRows(null));

        Assert.Null(exception);
        var result = table.ToTable();
        Assert.Contains("ExistingRow", result);
    }

    [Fact]
    public void AddRows_ClearsCache()
    {
        var table = new Table();
        table.AddRow("InitialRow");
        var firstResult = table.ToTable();

        table.AddRows(new[] { "NewRow" });
        var secondResult = table.ToTable();

        Assert.DoesNotContain("NewRow", firstResult);
        Assert.Contains("NewRow", secondResult);
    }

    [Fact]
    public void ClearCache()
    {
        var table = new Table
        {
            CachingEnabled = true
        };

        table.AddRow("1");
        var firstResult = table.ToTable();

        table.ClearCache();
        var secondResult = table.ToTable();

        Assert.Contains("1", firstResult);
        Assert.Contains("1", secondResult);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CachingEnabled(bool cachingEnabled)
    {
        var table = new Table
        {
            CachingEnabled = cachingEnabled
        };

        table.AddRow("1");

        var firstResult = table.ToTable();
        var secondResult = table.ToTable();

        Assert.Contains("1", firstResult);
        Assert.Contains("1", secondResult);
    }

    [Fact]
    public void ToString_ReturnsFormattedTable()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");

        var result = table.ToString();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
        Assert.Contains("John", result);
        Assert.Contains("30", result);
    }
}
