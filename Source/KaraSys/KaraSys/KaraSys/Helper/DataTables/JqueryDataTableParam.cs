using System.Collections.Generic;

namespace KaraSys.Helper.DataTables
{
    public class JqueryDataTableParam
    {
        public bool bEscapeRegex { get; set; }
        public List<bool> bEscapeRegexColumns { get; set; }
        public List<bool> bSearchable { get; set; }
        public List<bool> bSortable { get; set; }
        public int iColumns { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public List<int> iSortCol { get; set; }
        public int iSortingCols { get; set; }
        public int sEcho { get; set; }
        public string sSearch { get; set; }
        public List<string> sSearchColumns { get; set; }
        public List<string> sSortDir { get; set; }
        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }

        public int SortCollumn
        {
            get
            {
                if (iSortCol != null && iSortCol.Count > 0)
                    return iSortCol[0];
                return iSortCol_0;
            }
        }

        public string SortDir
        {
            get
            {
                if (sSortDir != null && sSortDir.Count > 0)
                    return sSortDir[0];
                if (string.IsNullOrEmpty(sSortDir_0))
                    return "asc";
                return sSortDir_0;
            }
        }
    }
}