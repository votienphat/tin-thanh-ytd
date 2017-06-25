using System.Web;
using System.Web.Mvc;
using VanTaiSystem.ActionFilter;
using VanTaiSystem.Helper.DataTables;

[assembly: PreApplicationStartMethod(typeof(RegisterDatatablesModelBinder), "Start")]

namespace VanTaiSystem.ActionFilter
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