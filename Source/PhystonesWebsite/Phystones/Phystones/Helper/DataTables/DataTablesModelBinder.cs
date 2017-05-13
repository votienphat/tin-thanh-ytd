using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Phystones.Helper.DataTables
{
    public class DataTablesModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;
            int columns = GetValue<int>(valueProvider, "iColumns");
            if (columns == 0)
            {
                return BindV10Model(valueProvider);
            }
            return BindLegacyModel(valueProvider, columns);
        }


        private object BindV10Model(IValueProvider valueProvider)
        {
            var obj = new DataTablesParam();
            obj.Start = GetValue<int>(valueProvider, "start");
            obj.Length = GetValue<int>(valueProvider, "length");
            obj.Search = new DataTablesSearch
            {
                Regex = GetValue<bool>(valueProvider, "search[regex]"),
                Value = GetValue<string>(valueProvider, "search[value]")
            };
            obj.Draw = GetValue<int>(valueProvider, "draw");

            int colIdx = 0;
            obj.Columns = new List<DataTablesColumn>();
            while (true)
            {
                string colPrefix = String.Format("columns[{0}]", colIdx);
                string colName = GetValue<string>(valueProvider, colPrefix + "[data]");
                if (String.IsNullOrWhiteSpace(colName))
                {
                    break;
                }

                obj.Columns.Add(new DataTablesColumn
                {
                    Name = colName,
                    Orderable = GetValue<bool>(valueProvider, colPrefix + "[orderable]"),
                    Searchable = GetValue<bool>(valueProvider, colPrefix + "[searchable]"),
                    Data = GetValue<string>(valueProvider, colPrefix + "[data]"),
                    Search = new DataTablesSearch
                    {
                        Value = GetValue<string>(valueProvider, colPrefix + "[search][value]"),
                        Regex = GetValue<bool>(valueProvider, colPrefix + "[searchable][regex]")
                    }
                });
                colIdx++;
            }

            colIdx = 0;
            obj.Orders = new List<DataTablesOrder>();
            while (true)
            {
                string colPrefix = String.Format("order[{0}]", colIdx);
                int? orderColumn = GetValue<int?>(valueProvider, colPrefix + "[column]");
                if (orderColumn.HasValue)
                {
                    obj.Orders.Add(new DataTablesOrder
                    {
                        Column = orderColumn.Value,
                        Direction = GetValue<string>(valueProvider, colPrefix + "[dir]")
                    });
                    colIdx++;
                }
                else
                {
                    break;
                }
            }

            return obj;
        }

        private DataTablesParam BindLegacyModel(IValueProvider valueProvider, int columns)
        {
            var obj = new DataTablesParam();

            obj.Start = GetValue<int>(valueProvider, "iDisplayStart");
            obj.Length = GetValue<int>(valueProvider, "iDisplayLength");
            obj.Search = new DataTablesSearch
            {
                Regex = GetValue<bool>(valueProvider, "sSearch"),
                Value = GetValue<string>(valueProvider, "bEscapeRegex")
            };
            obj.Draw = GetValue<int>(valueProvider, "sEcho");

            obj.Columns = new List<DataTablesColumn>();
            obj.Orders = new List<DataTablesOrder>();
            for (int i = 0; i < columns; i++)
            {
                obj.Columns.Add(new DataTablesColumn
                {
                    Orderable = GetValue<bool>(valueProvider, "bSortable_" + i),
                    Searchable = GetValue<bool>(valueProvider, "bSearchable_" + i),
                    Search = new DataTablesSearch
                    {
                        Value = GetValue<string>(valueProvider, "sSearch_" + i),
                        Regex = GetValue<bool>(valueProvider, "bEscapeRegex_" + i)
                    }
                });

                int? orderColumn = GetValue<int?>(valueProvider, "iSortCol_" + i);
                if (orderColumn.HasValue)
                {
                    obj.Orders.Add(new DataTablesOrder
                    {
                        Column = orderColumn.Value,
                        Direction = GetValue<string>(valueProvider, "sSortDir_" + i)
                    });
                }
            }
            return obj;
        }

        private static T GetValue<T>(IValueProvider valueProvider, string key)
        {
            ValueProviderResult valueResult = valueProvider.GetValue(key);
            return (valueResult == null)
                ? default(T)
                : (T)valueResult.ConvertTo(typeof(T));
        }
    }
}