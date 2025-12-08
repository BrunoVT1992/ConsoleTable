using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleTable.Text
{
    public class Table
    {
        private string _tableCache = null;

        private bool _cachingEnabled = true;
        /// <summary>
        /// Enables the caching of the generated table string when the ToTable method is called. Default is true.
        /// Cache will be cleared on any property change or method call.
        /// </summary>
        public bool CachingEnabled
        {
            get => _cachingEnabled;
            set
            {
                _cachingEnabled = value;

                if (!_cachingEnabled)
                    ClearCache();
            }
        }

        private string[] _headers = Array.Empty<string>();
        /// <summary>
        /// Gets or sets the headers of the table. This is a single optional top row.
        /// </summary>
        public string[] Headers
        {
            get => _headers;
            set
            {
                _headers = value ?? Array.Empty<string>();
                ClearCache();
            }
        }

        private List<string[]> _rows = new List<string[]>();
        /// <summary>
        /// Gets or sets the rows of the table
        /// </summary>
        public List<string[]> Rows
        {
            get => _rows;
            set
            {
                _rows = value ?? new List<string[]>();
                ClearCache();
            }
        }

        private string[] _footers = Array.Empty<string>();
        /// <summary>
        /// Gets or sets the footers of the table. This is a single optional bottom row without a border.
        /// </summary>
        public string[] Footers
        {
            get => _footers;
            set
            {
                _footers = value ?? Array.Empty<string>();
                ClearCache();
            }
        }

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

        private bool _headerTextAlignmentRight;
        /// <summary>
        /// Gets or sets a value indicating whether the header text is aligned to the right or left
        /// </summary>
        public bool HeaderTextAlignmentRight
        {
            get =>
                _headerTextAlignmentRight;
            set
            {
                _headerTextAlignmentRight = value;
                ClearCache();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row text is aligned to the right or left
        /// </summary>
        private bool _rowTextAlignmentRight;
        public bool RowTextAlignmentRight
        {
            get => _rowTextAlignmentRight;
            set
            {
                _rowTextAlignmentRight = value;
                ClearCache();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the footer text is aligned to the right or left
        /// </summary>
        private bool _footerTextAlignmentRight;
        public bool FooterTextAlignmentRight
        {
            get => _footerTextAlignmentRight;
            set
            {
                _footerTextAlignmentRight = value;
                ClearCache();
            }
        }

        /// <summary>
        /// Sets the headers of the table. Overwrites them each time.
        /// </summary>
        public Table SetHeaders(params string[] headers)
        {
            Headers = headers;
            return this;
        }

        /// <summary>
        /// Sets the footers of the table. Overwrites them each time.
        /// </summary>
        public Table SetFooters(params string[] footers)
        {
            Footers = footers;
            return this;
        }

        /// <summary>
        /// Adds a new row to the table
        /// </summary>
        public Table AddRow(params string[] row)
        {
            if (Rows == null)
                Rows = new List<string[]>();

            Rows.Add(row);
            ClearCache();
            return this;
        }

        /// <summary>
        /// Adds multiple rows to the table
        /// </summary>
        public Table AddRows(params string[][] rows)
        {
            if (rows != null)
            {
                if (Rows == null)
                    Rows = new List<string[]>();

                Rows.AddRange(rows);
                ClearCache();
            }

            return this;
        }

        /// <summary>
        /// Clears all the rows from the table
        /// </summary>
        public Table ClearRows()
        {
            if (Rows == null)
            {
                Rows = new List<string[]>();
            }
            else
            {
                Rows.Clear();
                ClearCache();
            }

            return this;
        }

        private int[] GetMaxCellWidths()
        {
            var table = new List<string[]>();

            if (Headers?.Length > 0)
            {
                table.Add(Headers);
            }

            if (Rows?.Any() == true)
            {
                table.AddRange(Rows);
            }

            if (Footers?.Length > 0)
            {
                table.Add(Footers);
            }

            var maximumColumns = 0;
            foreach (var row in table)
            {
                if (row.Length > maximumColumns)
                    maximumColumns = row.Length;
            }

            var maximumCellWidths = new int[maximumColumns];
            for (int i = 0; i < maximumCellWidths.Length; i++)
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

        private StringBuilder CreateBottomLine(int[] maximumCellWidths, int rowColumnCount, char horizontalLine, StringBuilder formattedTable)
        {
            for (int i = 0; i < rowColumnCount; i++)
            {
                if (i == 0 && i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.BottomLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine), TableDrawing.BottomRightJoint));
                else if (i == 0)
                    formattedTable.Append(string.Format("{0}{1}", TableDrawing.BottomLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine)));
                else if (i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine), TableDrawing.BottomRightJoint));
                else
                    formattedTable.Append(string.Format("{0}{1}", TableDrawing.BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine)));
            }

            return formattedTable;
        }

        private StringBuilder CreateValueLine(int[] maximumCellWidths, string[] row, bool alignRight, string verticalLine, StringBuilder formattedTable)
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
                    formattedTable.AppendLine(string.Format("{0}{1}{2}{3}{4}", verticalLine, paddingString, cellValue, paddingString, verticalLine));
                else if (cellIndex == 0)
                    formattedTable.Append(string.Format("{0}{1}{2}{3}", verticalLine, paddingString, cellValue, paddingString));
                else if (cellIndex == lastCellIndex)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}{3}{4}", verticalLine, paddingString, cellValue, paddingString, verticalLine));
                else
                    formattedTable.Append(string.Format("{0}{1}{2}{3}", verticalLine, paddingString, cellValue, paddingString));

                cellIndex++;
            }

            return formattedTable;
        }

        private StringBuilder CreateSeperatorLine(int[] maximumCellWidths, int previousRowColumnCount, int rowColumnCount, char horizontalLine, StringBuilder formattedTable)
        {
            var maximumCells = Math.Max(previousRowColumnCount, rowColumnCount);

            for (int i = 0; i < maximumCells; i++)
            {
                if (i == 0 && i == maximumCells - 1)
                {
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.LeftJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine), TableDrawing.RightJoint));
                }
                else if (i == 0)
                {
                    formattedTable.Append(string.Format("{0}{1}", TableDrawing.LeftJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine)));
                }
                else if (i == maximumCells - 1)
                {
                    if (i > previousRowColumnCount)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.TopJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine), TableDrawing.TopRightJoint));
                    else if (i > rowColumnCount)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine), TableDrawing.BottomRightJoint));
                    else if (i > previousRowColumnCount - 1)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine), TableDrawing.TopRightJoint));
                    else if (i > rowColumnCount - 1)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine), TableDrawing.BottomRightJoint));
                    else
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TableDrawing.MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine), TableDrawing.RightJoint));
                }
                else
                {
                    if (i > previousRowColumnCount)
                        formattedTable.Append(string.Format("{0}{1}", TableDrawing.TopJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine)));
                    else if (i > rowColumnCount)
                        formattedTable.Append(string.Format("{0}{1}", TableDrawing.BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine)));
                    else
                        formattedTable.Append(string.Format("{0}{1}", TableDrawing.MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], horizontalLine)));
                }
            }

            return formattedTable;
        }

        /// <summary>
        /// Clears the cached generated table string
        /// </summary>
        public Table ClearCache()
        {
            _tableCache = null;
            return this;
        }

        /// <summary>
        /// Clear all the headers and rows from the table
        /// </summary>
        public Table Clear()
        {
            SetHeaders(null);
            ClearRows();
            SetFooters(null);
            ClearCache();
            return this;
        }

        /// <summary>
        /// Generates the formatted table to a string
        /// </summary>
        public string ToTable()
        {
            if (CachingEnabled && !string.IsNullOrEmpty(_tableCache))
                return _tableCache;

            if (Rows?.Any() == true)
            {
                for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
                {
                    var row = Rows[rowIndex];
                    //Weird behaviour with empty rows. So we create 1 column with a space inside.
                    if ((row == null || row.Length <= 0))
                        Rows[rowIndex] = new string[] { " " };
                }
            }

            if (Headers?.Any() != true && Rows?.Any() != true && Footers?.Any() != true)
                return string.Empty;

            var formattedTable = new StringBuilder();

            int[] maximumCellWidths = GetMaxCellWidths();

            var topLineCreated = false;
            var previousRow = Array.Empty<string>();
            var nextRow = Array.Empty<string>();

            if (Headers?.Any() == true)
            {
                formattedTable = CreateTopLine(maximumCellWidths, Headers.Count(), formattedTable);
                topLineCreated = true;

                formattedTable = CreateValueLine(maximumCellWidths, Headers, HeaderTextAlignmentRight, TableDrawing.VerticalLine, formattedTable);

                previousRow = Headers;

                //When there are no rows immediatly draw the bottom line after the header
                if (Rows?.Any() == true)
                {
                    nextRow = Rows.First();
                    formattedTable = CreateSeperatorLine(maximumCellWidths, previousRow.Count(), nextRow.Count(), TableDrawing.HorizontalHeaderLine, formattedTable);
                }
                else
                {
                    formattedTable = CreateBottomLine(maximumCellWidths, Headers.Count(), TableDrawing.HorizontalHeaderLine, formattedTable);
                }
            }

            if (Rows?.Any() == true)
            {
                if (!topLineCreated)
                {
                    formattedTable = CreateTopLine(maximumCellWidths, Rows.First().Count(), formattedTable);
                    topLineCreated = true;
                }

                int rowIndex = 0;
                int lastRowIndex = Rows.Count - 1;

                for (int i = 0; i < Rows.Count; i++)
                {
                    var row = Rows[i];

                    formattedTable = CreateValueLine(maximumCellWidths, row, RowTextAlignmentRight, TableDrawing.VerticalLine, formattedTable);

                    previousRow = row;

                    if (rowIndex != lastRowIndex)
                    {
                        nextRow = Rows[rowIndex + 1];
                        formattedTable = CreateSeperatorLine(maximumCellWidths, previousRow.Count(), nextRow.Count(), TableDrawing.HorizontalLine, formattedTable);
                    }

                    rowIndex++;
                }

                formattedTable = CreateBottomLine(maximumCellWidths, previousRow.Count(), TableDrawing.HorizontalLine, formattedTable);
            }

            if (Footers?.Any() == true)
            {
                formattedTable = CreateValueLine(maximumCellWidths, Footers, FooterTextAlignmentRight, TableDrawing.EmptySpace, formattedTable);
            }

            var generatedTable = formattedTable.ToString();

            if (CachingEnabled)
                _tableCache = generatedTable;

            return generatedTable;
        }

        public override string ToString()
        {
            return ToTable();
        }
    }
}
