using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTable.Text
{
    public class Table
    {
        private string[] _headers;
        private List<string[]> _rows = new List<string[]>();
        private string _tableCache = null;

        private int _padding = 1;
        /// <summary>
        /// Gets or sets the amount of padding in spaces left and right of the rows cell content. Default is 1
        /// </summary>
        public int Padding
        {
            get => _padding;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Padding), "Padding must be greater than or equal to 0.");

                _padding = value;
                ClearCache();
            }
        }

        private bool headerTextAlignmentRight;
        /// <summary>
        /// Gets or sets a value indicating whether the header text is aligned to the right or left
        /// </summary>
        public bool HeaderTextAlignmentRight
        {
            get =>
                headerTextAlignmentRight;
            set
            {
                headerTextAlignmentRight = value;
                ClearCache();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row text is aligned to the right or left
        /// </summary>
        private bool rowTextAlignmentRight;
        public bool RowTextAlignmentRight
        {
            get =>
                rowTextAlignmentRight;
            set
            {
                rowTextAlignmentRight = value;
                ClearCache();
            }
        }

        /// <summary>
        /// Sets the headers of the table. Overwrites them each time.
        /// </summary>
        public Table SetHeaders(params string[] headers)
        {
            _headers = headers;
            ClearCache();
            return this;
        }

        /// <summary>
        /// Adds a new row to the table
        /// </summary>
        public Table AddRow(params string[] row)
        {
            _rows.Add(row);
            ClearCache();
            return this;
        }

        /// <summary>
        /// Clears all the rows from the table
        /// </summary>
        public Table ClearRows()
        {
            _rows.Clear();
            ClearCache();
            return this;
        }

        private int[] GetMaxCellWidths(List<string[]> table)
        {
            var maximumColumns = 0;
            foreach (var row in table)
            {
                if (row.Length > maximumColumns)
                    maximumColumns = row.Length;
            }

            var maximumCellWidths = new int[maximumColumns];
            for (int i = 0; i < maximumCellWidths.Count(); i++)
                maximumCellWidths[i] = 0;

            var paddingCount = 0;
            if (Padding > 0)
            {
                //Padding is left and right
                paddingCount = Padding * 2;
            }

            foreach (var row in table)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    var maxWidth = row[i].Length + paddingCount;

                    if (maxWidth > maximumCellWidths[i])
                        maximumCellWidths[i] = maxWidth;
                }
            }

            return maximumCellWidths;
        }

        private StringBuilder CreateTopLine(int[] maximumCellWidths, int rowColumnCount, StringBuilder formattedTable)
        {
            for (int i = 0; i < rowColumnCount; i++)
            {
                if (i == 0 && i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.TopLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.TopRightJoint));
                else if (i == 0)
                    formattedTable.Append(string.Format("{0}{1}", TableDrawing.TopLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine)));
                else if (i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.TopJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.TopRightJoint));
                else
                    formattedTable.Append(string.Format("{0}{1}", TableDrawing.TopJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine)));
            }

            return formattedTable;
        }

        private StringBuilder CreateBottomLine(int[] maximumCellWidths, int rowColumnCount, StringBuilder formattedTable)
        {
            for (int i = 0; i < rowColumnCount; i++)
            {
                if (i == 0 && i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.BottomLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.BottomRightJoint));
                else if (i == 0)
                    formattedTable.Append(string.Format("{0}{1}", TableDrawing.BottomLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine)));
                else if (i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.BottomRightJoint));
                else
                    formattedTable.Append(string.Format("{0}{1}", TableDrawing.BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine)));
            }

            return formattedTable;
        }

        private StringBuilder CreateValueLine(int[] maximumCellWidths, string[] row, bool alignRight, StringBuilder formattedTable)
        {
            int cellIndex = 0;
            int lastCellIndex = row.Length - 1;

            var paddingString = string.Empty;
            if (Padding > 0)
                paddingString = string.Concat(Enumerable.Repeat(' ', Padding));

            foreach (var column in row)
            {
                var restWidth = maximumCellWidths[cellIndex];
                if (Padding > 0)
                    restWidth -= Padding * 2;

                var cellValue = alignRight ? column.PadLeft(restWidth, ' ') : column.PadRight(restWidth, ' ');

                if (cellIndex == 0 && cellIndex == lastCellIndex)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}{3}{4}", TableDrawing.VerticalLine, paddingString, cellValue, paddingString, TableDrawing.VerticalLine));
                else if (cellIndex == 0)
                    formattedTable.Append(string.Format("{0}{1}{2}{3}", TableDrawing.VerticalLine, paddingString, cellValue, paddingString));
                else if (cellIndex == lastCellIndex)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}{3}{4}", TableDrawing.VerticalLine, paddingString, cellValue, paddingString, TableDrawing.VerticalLine));
                else
                    formattedTable.Append(string.Format("{0}{1}{2}{3}", TableDrawing.VerticalLine, paddingString, cellValue, paddingString));

                cellIndex++;
            }

            return formattedTable;
        }

        private StringBuilder CreateSeperatorLine(int[] maximumCellWidths, int previousRowColumnCount, int rowColumnCount, StringBuilder formattedTable)
        {
            var maximumCells = Math.Max(previousRowColumnCount, rowColumnCount);

            for (int i = 0; i < maximumCells; i++)
            {
                if (i == 0 && i == maximumCells - 1)
                {
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.LeftJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.RightJoint));
                }
                else if (i == 0)
                {
                    formattedTable.Append(string.Format("{0}{1}", TableDrawing.LeftJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine)));
                }
                else if (i == maximumCells - 1)
                {
                    if (i > previousRowColumnCount)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.TopJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.TopRightJoint));
                    else if (i > rowColumnCount)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.BottomRightJoint));
                    else if (i > previousRowColumnCount - 1)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.TopRightJoint));
                    else if (i > rowColumnCount - 1)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.BottomRightJoint));
                    else
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine), TableDrawing.RightJoint));
                }
                else
                {
                    if (i > previousRowColumnCount)
                        formattedTable.Append(string.Format("{0}{1}", TableDrawing.TopJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine)));
                    else if (i > rowColumnCount)
                        formattedTable.Append(string.Format("{0}{1}", TableDrawing.BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine)));
                    else
                        formattedTable.Append(string.Format("{0}{1}", TableDrawing.MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], TableDrawing.HorizontalLine)));
                }
            }

            return formattedTable;
        }

        private void ClearCache()
        {
            _tableCache = null;
        }

        /// <summary>
        /// Clear all the headers and rows from the table
        /// </summary>
        public Table Clear()
        {
            SetHeaders(null);
            ClearRows();
            ClearCache();
            return this;
        }

        /// <summary>
        /// Generates the formatted table to a string
        /// </summary>
        public string ToTable()
        {
            if (!string.IsNullOrEmpty(_tableCache))
                return _tableCache;

            var table = new List<string[]>();

            var firstRowIsHeader = false;
            if (_headers?.Any() == true)
            {
                table.Add(_headers);
                firstRowIsHeader = true;
            }

            if (_rows?.Any() == true)
            {
                foreach (var row in _rows)
                {
                    //Weird behaviour with empty rows
                    if ((row == null || row.Length <= 0) && _headers?.Length > 0)
                        table.AddRange(new string[][] { new string[] { " " } });
                    else
                        table.Add(row);
                }
            }

            if (!table.Any())
                return string.Empty;

            var formattedTable = new StringBuilder();

            var previousRow = table.First();
            var nextRow = table.First();

            int[] maximumCellWidths = GetMaxCellWidths(table);

            formattedTable = CreateTopLine(maximumCellWidths, nextRow.Count(), formattedTable);

            int rowIndex = 0;
            int lastRowIndex = table.Count - 1;

            for (int i = 0; i < table.Count; i++)
            {
                var row = table[i];

                var align = RowTextAlignmentRight;
                if (i == 0 && firstRowIsHeader)
                    align = HeaderTextAlignmentRight;

                formattedTable = CreateValueLine(maximumCellWidths, row, align, formattedTable);

                previousRow = row;

                if (rowIndex != lastRowIndex)
                {
                    nextRow = table[rowIndex + 1];
                    formattedTable = CreateSeperatorLine(maximumCellWidths, previousRow.Count(), nextRow.Count(), formattedTable);
                }

                rowIndex++;
            }

            formattedTable = CreateBottomLine(maximumCellWidths, previousRow.Count(), formattedTable);

            _tableCache = formattedTable.ToString();

            return _tableCache;
        }

        public override string ToString()
        {
            return ToTable();
        }
    }
}
