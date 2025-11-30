using Xunit;

namespace ConsoleTable.Tests;

public class TableTests
{
    [Fact]
    public void ToString_EmptyTable_ReturnsEmptyString()
    {
        var table = new Table();

        var result = table.ToString();

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToString_WithHeadersOnly_ReturnsFormattedTable()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");

        var result = table.ToString();

        Assert.Contains("Name", result);
        Assert.Contains("Age", result);
    }

    [Fact]
    public void ToString_WithRowsOnly_ReturnsFormattedTable()
    {
        var table = new Table();
        table.AddRow("John", "30");
        table.AddRow("Jane", "25");

        var result = table.ToString();

        Assert.Contains("John", result);
        Assert.Contains("Jane", result);
        Assert.Contains("30", result);
        Assert.Contains("25", result);
    }

    [Fact]
    public void ToString_WithHeadersAndRows_ReturnsFormattedTable()
    {
        var table = new Table();
        table.SetHeaders("Name", "Age");
        table.AddRow("John", "30");
        table.AddRow("Jane", "25");

        var result = table.ToString();

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

        var result = table.ToString();

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

        var result = table.ToString();

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

        var result = table.ToString();

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

        var result1 = table1.ToString();
        var result2 = table2.ToString();

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

        var result = table.ToString();

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
    public void ToString_WithSingleColumn_ReturnsProperCorners()
    {
        var table = new Table();
        table.SetHeaders("Single");

        var result = table.ToString();

        // Should have proper corners for single column
        Assert.Contains("┌", result);
        Assert.Contains("┐", result);
        Assert.Contains("└", result);
        Assert.Contains("┘", result);
    }

    [Fact]
    public void ToString_MoreHeadersThanRowColumns_HandlesCorrectly()
    {
        var table = new Table();
        table.SetHeaders("Name", "Date", "Number", "Id");
        table.AddRow("name 1", "date 1", "1");
        table.AddRow("name 2", "date 2");
        table.AddRow("name 3");

        var result = table.ToString();

        Assert.Contains("Name", result);
        Assert.Contains("Date", result);
        Assert.Contains("Number", result);
        Assert.Contains("Id", result);
        Assert.Contains("name 1", result);
        Assert.Contains("name 2", result);
        Assert.Contains("name 3", result);
    }

    [Fact]
    public void ToString_LessHeadersThanRowColumns_HandlesCorrectly()
    {
        var table = new Table();
        table.SetHeaders("Name");
        table.AddRow("name 1", "date 1");
        table.AddRow("name 2", "date 2", "1");

        var result = table.ToString();

        Assert.Contains("Name", result);
        Assert.Contains("name 1", result);
        Assert.Contains("date 1", result);
        Assert.Contains("name 2", result);
        Assert.Contains("date 2", result);
        Assert.Contains("1", result);
    }

    [Fact]
    public void ToString_VaryingRowWidths_HandlesCorrectly()
    {
        var table = new Table();
        table.AddRow("short");
        table.AddRow("much longer text here");
        table.AddRow("mid length");

        var result = table.ToString();

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

        var result = table.ToString();

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

        var result = table.ToString();

        Assert.Contains("Row1", result);
        Assert.Contains("Row2", result);
        Assert.Contains("Row3", result);
    }

    [Fact]
    public void ToString_ContainsTableBorderCharacters()
    {
        var table = new Table();
        table.SetHeaders("Header");
        table.AddRow("Value");

        var result = table.ToString();

        // Check for various border characters
        Assert.Contains("│", result);  // Vertical line
        Assert.Contains("─", result);  // Horizontal line
        Assert.Contains("├", result);  // Left joint
        Assert.Contains("┤", result);  // Right joint
    }

    [Fact]
    public void ToString_MultipleColumns_ContainsMiddleJoint()
    {
        var table = new Table();
        table.SetHeaders("Col1", "Col2");
        table.AddRow("Val1", "Val2");

        var result = table.ToString();

        Assert.Contains("┼", result);  // Middle joint
    }

    [Fact]
    public void ToString_SingleRowSingleColumn_ReturnsValidTable()
    {
        var table = new Table();
        table.AddRow("X");

        var result = table.ToString();

        Assert.Contains("X", result);
        Assert.Contains("┌", result);
        Assert.Contains("┐", result);
        Assert.Contains("└", result);
        Assert.Contains("┘", result);
    }

    [Fact]
    public void ToString_EmptyStringValues_HandlesCorrectly()
    {
        var table = new Table();
        table.SetHeaders("Name", "Value");
        table.AddRow("", "");

        var result = table.ToString();

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

        var resultLeft = tableLeft.ToString();
        var resultRight = tableRight.ToString();

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

        var resultLeft = tableLeft.ToString();
        var resultRight = tableRight.ToString();

        // Both should contain the value, but in different positions
        Assert.Contains("V", resultLeft);
        Assert.Contains("V", resultRight);
        Assert.NotEqual(resultLeft, resultRight);
    }
}
