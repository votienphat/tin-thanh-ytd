using System.Web;
using System.Web.Mvc;
using KaraSys.ActionFilter;
using KaraSys.Helper.DataTables;

[assembly: PreApplicationStartMethod(typeof(RegisterDatatablesModelBinder), "Start")]

namespace KaraSys.ActionFilter
{
    public class RegisterDatatablesModelBinder
    {
        public static void Start()
        {
            if (!ModelBinders.Binders.ContainsKey(typeof(DataTablesParam)))
                ModelBinders.Binders.Add(typeof(DataTablesParam), new DataTablesModelBinder());
        }
    }

}