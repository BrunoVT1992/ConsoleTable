﻿using ConsoleTable;
using System;

namespace ConsoleTableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteNormalTable();

            Console.WriteLine();

            WriteNormalTableDifferentStyle();

            Console.WriteLine();

            WriteTableWithoutHeaders();

            Console.WriteLine();

            WriteTableMoreHeaders();

            Console.WriteLine();

            WriteTableLessHeaders();

            Console.Read();
        }

        private static void WriteNormalTable()
        {
            Console.WriteLine("Normal table:");

            var table = new Table();

            table.SetHeaders("Name", "Date", "Number");

            for (int i = 0; i <= 10; i++)
            {
                if (i % 2 == 0)
                    table.AddRow($"name {i}", DateTime.Now.AddDays(-i).ToLongDateString(), i.ToString());
                else
                    table.AddRow($"long name {i}", DateTime.Now.AddDays(-i).ToLongDateString(), (i * 5000).ToString());
            }

            Console.WriteLine(table.ToString());
        }

        private static void WriteTableWithoutHeaders()
        {
            Console.WriteLine("Table without headers:");

            var table = new Table();

            for (int i = 0; i <= 5; i++)
                table.AddRow($"name {i}", DateTime.Now.AddDays(-i).ToLongDateString(), i.ToString());

            Console.WriteLine(table.ToString());
        }

        private static void WriteTableMoreHeaders()
        {
            Console.WriteLine("Table with more headers:");

            var table = new Table();

            table.SetHeaders("Name", "Date", "Number", "Id");

            table.AddRow("name 1", DateTime.Now.AddDays(-1).ToLongDateString(), "1");
            table.AddRow("name 2", DateTime.Now.AddDays(-2).ToLongDateString());
            table.AddRow("name 3");

            Console.WriteLine(table.ToString());
        }

        private static void WriteTableLessHeaders()
        {
            Console.WriteLine("Table with less headers:");

            var table = new Table();

            table.SetHeaders("Name");

            table.AddRow("name 1", DateTime.Now.AddDays(-1).ToLongDateString());
            table.AddRow("name 2", DateTime.Now.AddDays(-2).ToLongDateString(), "1");

            Console.WriteLine(table.ToString());
        }

        private static void WriteNormalTableDifferentStyle()
        {
            Console.WriteLine("Normal table with different style:");

            var table = new Table
            {
                Padding = 5,
                HeaderTextAlignRight = true,
                RowTextAlignRight = true
            };

            table.SetHeaders("Name", "Date", "Number");

            for (int i = 0; i <= 10; i++)
            {
                if (i % 2 == 0)
                    table.AddRow($"name {i}", DateTime.Now.AddDays(-i).ToLongDateString(), i.ToString());
                else
                    table.AddRow($"long name {i}", DateTime.Now.AddDays(-i).ToLongDateString(), (i * 5000).ToString());
            }

            Console.WriteLine(table.ToString());
        }
    }
}
