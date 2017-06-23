using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace MyUtility
{
    public static class FileExtension
    {

        public static DataTable CsvCardToDataTable(Stream stream)
        {
            var dt = new DataTable();
            var csvreader = new StreamReader(stream);

            string[] headers = csvreader.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(string.IsNullOrEmpty(header) ? string.Empty : header.Trim());
            }
            while (!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var rows = (line??"").Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = string.IsNullOrEmpty(rows[i]) ? string.Empty : rows[i].Trim();
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Kiểm tra xem file có phải là file ảnh không
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static bool IsValidImage(byte[] bytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Image.FromStream(ms);
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra Extension ten file
        /// </summary>
        /// <returns></returns>
        public static bool IsValidExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            var splitFileName = fileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (splitFileName.Length > 0)
            {
                var length = splitFileName.Length;

                switch (splitFileName[length - 1].ToLower())
                {
                    case "jpg":
                    case "jpeg":
                    case "gif":
                    case "png":
                        return true;

                }
            }

            return false;
        }

    }
}
