using System.Web;
using System.Web.Mvc;
using MyAdmin.ActionFilter;
using MyAdmin.Helper.DataTables;

[assembly: PreApplicationStartMethod(typeof(RegisterDatatablesModelBinder), "Start")]

namespace MyAdmin.ActionFilter
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