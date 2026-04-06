using Xunit;

namespace ConsoleTable.Text.Tests;

public class TableTests
{
    #region Headers
    [Fact]
    public void SetHeaders_Success()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
    }

    [Fact]
    public void Empty()
    {
        var table = new Table();

        var result = table.ToTable();

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void OnlyEmptyRows()
    {
        var table = new Table
        {
            Rows = new List<string[]>
            {
                new[] { "" },
                Array.Empty<string>()
            }
        };

        var result = table.ToTable();

        Assert.NotEqual(string.Empty, result);
    }

    [Fact]
    public void OnlyEmptyFooters()
    {
        var table = new Table
        {
            Footers = new string[] { "", "" }
        };

        var result = table.ToTable();

        Assert.NotEqual(string.Empty, result);
    }

    [Fact]
    public void OnlyEmptyHeaders()
    {
        var table = new Table
        {
            Headers = new string[] { "", "" }
        };

        var result = table.ToTable();

        Assert.NotEqual(string.Empty, result);
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
    public void SetHeaders_Null()
    {
        var table = new Table();
        table.SetHeaders(null);

        var result = table.ToTable();

        Assert.Empty(result);
    }

    [Fact]
    public void SetHeaders_Empty()
    {
        var table = new Table();
        table.SetHeaders([]);

        var result = table.ToTable();

        Assert.Empty(result);
    }

    [Fact]
    public void Headers_Success()
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
    #endregion

    #region Rows
    [Fact]
    public void AddRow_ReturnsFormattedTable()
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
    public void ClearRows_RemovesAllRows_KeepsHeadersAndFooters()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");
        table.AddRow("Jane", "25");
        table.SetFooters("Footer1", "Footer2");

        table.ClearRows();

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
        Assert.DoesNotContain("John", result);
        Assert.DoesNotContain("Jane", result);
        Assert.Contains("Footer1", result);
        Assert.Contains("Footer2", result);
    }

    [Fact]
    public void ClearRows_ThenAddNewRows()
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
    public void Rows_OverwritesPreviousRows()
    {
        var table = new Table();
        table.Rows = new List<string[]> { new[] { "OldRow" } };
        table.Rows = new List<string[]> { new[] { "NewRow" } };

        var result = table.ToTable();

        Assert.DoesNotContain("OldRow", result);
        Assert.Contains("NewRow", result);
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
    #endregion

    #region Footers
    [Fact]
    public void SetFooters_Success()
    {
        var table = new Table();
        table.SetFooters("Footer1", "Footer2");

        var result = table.ToTable();

        Assert.Contains("Footer1", result);
        Assert.Contains("Footer2", result);
    }

    [Fact]
    public void SetFooters_OverwritesPreviousFooters()
    {
        var table = new Table();
        table.SetFooters("OldFooter1", "OldFooter2");
        table.SetFooters("NewFooter1", "NewFooter2");

        var result = table.ToTable();

        Assert.DoesNotContain("OldFooter1", result);
        Assert.DoesNotContain("OldFooter2", result);
        Assert.Contains("NewFooter1", result);
        Assert.Contains("NewFooter2", result);
    }

    [Fact]
    public void SetFooters_Null()
    {
        var table = new Table();
        table.SetFooters(null);

        var result = table.ToTable();

        Assert.Empty(result);
    }

    [Fact]
    public void SetFooters_Empty()
    {
        var table = new Table();
        table.SetFooters([]);

        var result = table.ToTable();

        Assert.Empty(result);
    }

    [Fact]
    public void Footers_Success()
    {
        var table = new Table();
        table.Footers = new[] { "Footer1", "Footer2" };

        var result = table.ToTable();

        Assert.Contains("Footer1", result);
        Assert.Contains("Footer2", result);
    }

    [Fact]
    public void Footers_SetProperty_OverwritesPreviousFooters()
    {
        var table = new Table();
        table.Footers = new[] { "Old1", "Old2" };
        table.Footers = new[] { "New1", "New2" };

        var result = table.ToTable();

        Assert.DoesNotContain("Old1", result);
        Assert.DoesNotContain("Old2", result);
        Assert.Contains("New1", result);
        Assert.Contains("New2", result);
    }

    [Fact]
    public void Footers_SetFootersProperty_ClearsCache()
    {
        var table = new Table();
        table.Footers = new[] { "Footer1" };
        var firstResult = table.ToTable();

        table.Footers = new[] { "Footer2" };
        var secondResult = table.ToTable();

        Assert.Contains("Footer1", firstResult);
        Assert.DoesNotContain("Footer1", secondResult);
        Assert.Contains("Footer2", secondResult);
    }
    #endregion

    #region Styling
    [Theory]
    [InlineData(true, true, true, 10)]
    [InlineData(false, false, false, 0)]
    [InlineData(true, true, true, 0)]
    [InlineData(false, true, true, 10)]
    [InlineData(true, false, true, 10)]
    [InlineData(true, true, false, 10)]
    public void ToTable_WithStyling(bool headerTextAlignRight, bool rowTextAlignRight, bool footerTextAlignRight, int padding)
    {
        var table = new Table
        {
            HeaderTextAlignmentRight = headerTextAlignRight,
            RowTextAlignmentRight = rowTextAlignRight,
            FooterTextAlignmentRight = footerTextAlignRight,
            Padding = padding,
        };

        table.SetHeaders("Name");
        table.AddRow("John");
        table.AddRow("Jane");
        table.SetFooters("Footer");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("John", result);
        Assert.Contains("Jane", result);
        Assert.Contains("Footer", result);
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
    public void FooterTextAlignRight_True_AlignsToDifferentPosition()
    {
        var tableLeft = new Table { FooterTextAlignmentRight = false };
        tableLeft.SetFooters("F");
        tableLeft.AddRow("VeryLongValue");

        var tableRight = new Table { FooterTextAlignmentRight = true };
        tableRight.SetFooters("F");
        tableRight.AddRow("VeryLongValue");

        var resultLeft = tableLeft.ToTable();
        var resultRight = tableRight.ToTable();

        // Both should contain the footer, but in different positions
        Assert.Contains("F", resultLeft);
        Assert.Contains("F", resultRight);
        Assert.NotEqual(resultLeft, resultRight);
    }

    [Fact]
    public void ShowBorders_DefaultValue_IsTrue()
    {
        var table = new Table();

        Assert.True(table.ShowBorders);
    }

    [Fact]
    public void ShowBorders_False_RemovesBorderCharacters()
    {
        var table = new Table { ShowBorders = false };
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");
        table.AddRow("Jane", "25");
        table.SetFooters("Footer1", "Footer2");

        var result = table.ToTable();

        // Should NOT contain border characters
        Assert.DoesNotContain("│", result);  // Vertical line
        Assert.DoesNotContain("─", result);  // Horizontal line
        Assert.DoesNotContain("├", result);  // Left joint
        Assert.DoesNotContain("┤", result);  // Right joint
        Assert.DoesNotContain("┌", result);  // Top left corner
        Assert.DoesNotContain("┐", result);  // Top right corner
        Assert.DoesNotContain("└", result);  // Bottom left corner
        Assert.DoesNotContain("┘", result);  // Bottom right corner
        Assert.DoesNotContain("┼", result);  // Middle joint

        // Should still contain the actual content
        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
        Assert.Contains("John", result);
        Assert.Contains("30", result);
        Assert.Contains("Jane", result);
        Assert.Contains("25", result);
        Assert.Contains("Footer1", result);
        Assert.Contains("Footer2", result);
    }
    #endregion

    #region Cache
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
    #endregion

    #region Performance
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

        var footers = new List<string>();
        for (var columnPos = 1; columnPos <= columnCount; columnPos++)
        {
            footers.Add($"Footer {columnPos}");
        }
        table.Footers = footers.ToArray();

        var tableResult1 = table.ToTable();
        Assert.NotEmpty(tableResult1);

        var tableResult2 = table.ToTable();
        Assert.NotEmpty(tableResult2);
    }
    #endregion

    #region General
    [Fact]
    public void ToTable_EmptyTable_ReturnsEmptyString()
    {
        var table = new Table();

        var result = table.ToTable();

        Assert.Equal(string.Empty, result);
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
        table.SetFooters("Footer 1");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Date", result);
        Assert.Contains("Number", result);
        Assert.Contains("Id", result);
        Assert.Contains("name 1", result);
        Assert.Contains("name 2", result);
        Assert.Contains("name 3", result);
        Assert.Contains("Footer 1", result);
    }

    [Fact]
    public void ToTable_LessHeadersThanRowColumns_HandlesCorrectly()
    {
        var table = new Table();
        table.SetHeaders("Name");
        table.AddRow("name 1", "date 1");
        table.AddRow("name 2", "date 2", "city 1");
        table.SetFooters("Footer 1", "Footer 2", "Footer 3", "Footer 4");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("name 1", result);
        Assert.Contains("date 1", result);
        Assert.Contains("name 2", result);
        Assert.Contains("date 2", result);
        Assert.Contains("city 1", result);
        Assert.Contains("Footer 1", result);
        Assert.Contains("Footer 2", result);
        Assert.Contains("Footer 3", result);
        Assert.Contains("Footer 4", result);
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
        table.SetFooters("Footer");

        var result = table.ToTable();

        Assert.Contains("Name", result);
        Assert.Contains("Value", result);
        Assert.Contains("Footer", result);
    }

    [Fact]
    public void ToTable_ContainsTableBorderCharacters()
    {
        var table = new Table();
        table.SetHeaders("Header");
        table.AddRow("Value");
        table.SetFooters("Footer");

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
        table.SetFooters("Footer1", "Footer2");

        var result = table.ToTable();

        Assert.Contains("┼", result);  // Middle joint
    }

    [Fact]
    public void ToString_ReturnsFormattedTable()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");
        table.SetFooters("Footer1", "Footer2");

        var result = table.ToString();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
        Assert.Contains("John", result);
        Assert.Contains("30", result);
    }

    [Fact]
    public void Clear_IsEmpty()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");
        table.SetFooters("Footer1", "Footer2");

        table.Clear();

        var result = table.ToTable();

        Assert.Empty(result);
    }
    #endregion

    #region MultiLine
    [Fact]
    public void MultiLineRow_RendersAllLines()
    {
        var table = new Table();
        table.SetHeaders("Name", "City");
        table.AddRow("Alice", "Chicago\nUSA");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        // Both physical lines of the multi-line cell should appear between separators
        Assert.Contains(outputLines, l => l.Contains("Chicago") && l.Contains("│"));
        Assert.Contains(outputLines, l => l.Contains("USA") && l.Contains("│"));
    }

    [Fact]
    public void MultiLineHeader_RendersAllLines()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age\n(years)");
        table.AddRow("Alice", "30");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        Assert.Contains(outputLines, l => l.Contains("Age") && l.Contains("│"));
        Assert.Contains(outputLines, l => l.Contains("(years)") && l.Contains("│"));
    }

    [Fact]
    public void MultiLineFooter_RendersAllLines()
    {
        var table = new Table();
        table.AddRow("Alice", "30");
        table.SetFooters("Total: 1\nAll Female", "Sum: 30");

        var result = table.ToTable();

        Assert.Contains("Total: 1", result);
        Assert.Contains("All Female", result);
        Assert.Contains("Sum: 30", result);
    }

    [Fact]
    public void MultiLineRow_ColumnWidthBasedOnWidestLine()
    {
        var table = new Table { Padding = 0 };
        // "Short" is 5 chars, "VeryLongSecondLine" is 18 chars
        table.AddRow("Short\nVeryLongSecondLine");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        // The top border should be sized for the longest line, not the short one
        var topLine = outputLines.First(l => l.Contains("┌"));
        // Width = 18 chars of content (no padding) + 2 border chars
        Assert.Equal(20, topLine.Length);
    }

    [Fact]
    public void MultiLineRow_ShorterCellsPaddedWithBlanks_LeftAligned()
    {
        var table = new Table { Padding = 0 };
        table.AddRow("Single", "Line1\nLine2");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        // The second physical line should have an empty first column
        var secondValueLine = outputLines.First(l => l.Contains("Line2"));
        Assert.Contains("│", secondValueLine);
        Assert.DoesNotContain("Single", secondValueLine);
    }

    [Fact]
    public void MultiLineRow_LeftAligned_PadsCorrectly()
    {
        var table = new Table();
        table.AddRow("A\nLonger");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        // "A" line should be left-aligned and padded to the width of "Longer"
        var lineWithA = outputLines.First(l => l.Contains("A") && !l.Contains("Longer") && !l.Contains("─"));
        var lineWithLonger = outputLines.First(l => l.Contains("Longer"));

        // Both value lines must be the same total length (borders + padding + content)
        Assert.Equal(lineWithA.Length, lineWithLonger.Length);
    }

    [Fact]
    public void MultiLineRow_RightAligned_PadsCorrectly()
    {
        var table = new Table { RowTextAlignmentRight = true };
        table.AddRow("A\nLonger");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        var lineWithA = outputLines.First(l => l.Contains("A") && !l.Contains("Longer") && !l.Contains("─"));
        var lineWithLonger = outputLines.First(l => l.Contains("Longer"));

        // Both value lines must be the same total length
        Assert.Equal(lineWithA.Length, lineWithLonger.Length);

        // "A" should be right-aligned: preceded by spaces
        var contentA = lineWithA.Split('│')[1];
        Assert.True(contentA.TrimEnd().Length > contentA.TrimStart().Length,
            "Right-aligned 'A' should have leading spaces");
    }

    [Fact]
    public void MultiLineHeader_RightAligned()
    {
        var table = new Table { HeaderTextAlignmentRight = true };
        table.SetHeaders("H\nLongHeader");
        table.AddRow("Value");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        var lineWithH = outputLines.First(l => l.Contains("H") && !l.Contains("Long") && !l.Contains("─"));
        var contentH = lineWithH.Split('│')[1];

        // "H" should be right-aligned: more leading spaces than trailing
        Assert.True(contentH.TrimEnd().Length > contentH.TrimStart().Length,
            "Right-aligned 'H' should have leading spaces");
    }

    [Fact]
    public void MultiLineFooter_RightAligned()
    {
        var table = new Table { FooterTextAlignmentRight = true };
        table.AddRow("VeryLongValue");
        table.SetFooters("F\nLong");

        var result = table.ToTable();

        // Verify both footer lines are present in the rendered output.
        Assert.Contains("F", result);
        Assert.Contains("Long", result);

        var outputLines = GetOutputLines(result);
        // Find the footer line with just "F" (not "Long")
        var lineWithF = outputLines.First(l =>
            !l.Contains("│") && !l.Contains("─") &&
            l.Contains("F") && !l.Contains("Long") && !l.Contains("VeryLongValue"));
        // Right-aligned means leading spaces
        Assert.True(lineWithF.TrimEnd().Length > lineWithF.TrimStart().Length,
            "Right-aligned 'F' should have leading spaces");
    }

    [Fact]
    public void MultiLineRow_WithCustomPadding()
    {
        var table = new Table { Padding = 3 };
        table.AddRow("A\nB");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        var lineWithA = outputLines.First(l => l.Contains("A") && !l.Contains("─"));
        // With padding 3, there should be 3 spaces between the border and the content on each side
        Assert.Contains("│   A", lineWithA);
    }

    [Fact]
    public void MultiLineRow_MixedSingleAndMultiLine()
    {
        var table = new Table();
        table.SetHeaders("Name", "City");
        table.AddRow("Alice", "New York");
        table.AddRow("Bob", "Chicago\nUSA");
        table.AddRow("Charlie", "London");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        // All content should be present
        Assert.Contains("Alice", result);
        Assert.Contains("New York", result);
        Assert.Contains("Bob", result);
        Assert.Contains("Chicago", result);
        Assert.Contains("USA", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("London", result);

        // The "USA" line should have an empty name column
        var usaLine = outputLines.First(l => l.Contains("USA"));
        Assert.DoesNotContain("Bob", usaLine);
    }

    [Fact]
    public void MultiLineRow_WithoutBorders()
    {
        var table = new Table { ShowBorders = false };
        table.AddRow("A\nB", "C");

        var result = table.ToTable();

        Assert.Contains("A", result);
        Assert.Contains("B", result);
        Assert.Contains("C", result);
        Assert.DoesNotContain("│", result);
        Assert.DoesNotContain("─", result);
    }

    [Fact]
    public void MultiLineRow_CrLf_HandledCorrectly()
    {
        var table = new Table();
        table.AddRow("Line1\r\nLine2");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        Assert.Contains(outputLines, l => l.Contains("Line1"));
        Assert.Contains(outputLines, l => l.Contains("Line2"));
    }

    [Fact]
    public void MultiLineRow_Lf_HandledCorrectly()
    {
        var table = new Table();
        table.AddRow("Line1\nLine2");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        Assert.Contains(outputLines, l => l.Contains("Line1"));
        Assert.Contains(outputLines, l => l.Contains("Line2"));
    }

    [Fact]
    public void MultiLineRow_EmptyLinesPreserved()
    {
        var table = new Table { Padding = 0 };
        table.AddRow("A\n\nB");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        // Should produce 3 value lines between borders
        var valueLines = outputLines.Where(l => l.StartsWith("│") && !l.Contains("─")).ToList();
        Assert.Equal(3, valueLines.Count);
        Assert.Contains(valueLines, l => l.Contains("A"));
        Assert.Contains(valueLines, l => l.Contains("B"));
    }

    [Fact]
    public void MultiLineRow_AllLinesSameWidth()
    {
        var table = new Table();
        table.SetHeaders("Name", "Info");
        table.AddRow("Alice", "Line1\nLongerLine2\nL3");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        // All value lines for the multi-line row should have the same total width
        var rowValueLines = outputLines
            .Where(l => l.Contains("│") && (l.Contains("Line1") || l.Contains("LongerLine2") || l.Contains("L3")))
            .ToList();

        Assert.True(rowValueLines.Count >= 3);
        var expectedWidth = rowValueLines[0].Length;
        Assert.All(rowValueLines, l => Assert.Equal(expectedWidth, l.Length));
    }

    [Fact]
    public void MultiLineRow_HeadersRowsAndFooters_AllMultiLine()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age\n(years)");
        table.AddRow("Alice", "30");
        table.AddRow("Bob", "25\nyears old");
        table.SetFooters("Total\n2 people", "Sum\n55");

        var result = table.ToTable();

        Assert.Contains("Age", result);
        Assert.Contains("(years)", result);
        Assert.Contains("25", result);
        Assert.Contains("years old", result);
        Assert.Contains("Total", result);
        Assert.Contains("2 people", result);
        Assert.Contains("Sum", result);
        Assert.Contains("55", result);
    }

    [Fact]
    public void MultiLineRow_SingleLineContent_UnchangedBehavior()
    {
        // Ensure single-line content still works exactly as before
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("Alice", "30");

        var result = table.ToTable();
        var outputLines = GetOutputLines(result);

        // Should have exactly: top border, header, separator, row, bottom border = 5 lines
        Assert.Equal(5, outputLines.Length);
    }

    private static string[] GetOutputLines(string tableOutput)
    {
        return tableOutput
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToArray();
    }
    #endregion
}
