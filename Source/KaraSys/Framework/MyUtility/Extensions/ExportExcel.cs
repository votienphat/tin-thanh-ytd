using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility.Extensions
{
    public class ExportExcel
    {
        public static void ExportExcelFile()
        {
            
        }

        		DataTable dataTable = new DataTable();
		for (num = 0; num < dt.Columns.Count; num++)
		{
			dataTable.Columns.Add(arrColumnName[num], arrType[num]);
		}
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			DataRow row = dataTable.NewRow();
			for (int j = 0; j < dt.Columns.Count; j++)
			{
				object str = dt.Rows[i][j];
				row[j] = str;
			}
			dataTable.Rows.Add(row);
		}
    }
}
