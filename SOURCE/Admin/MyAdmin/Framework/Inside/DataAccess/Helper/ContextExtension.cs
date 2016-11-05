using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using DataAccess.Contract;

namespace DataAccess.Helper
{
    public static class ContextExtension
    {
        #region support method

        /// <summary>
        /// thong.nguyen
        /// </summary>
        /// <param name="context"></param>
        /// <param name="isolationLevel"></param>
        internal static void SetIsolationLevel(this DbContext context, IsolationLevel isolationLevel)
        {
            string sql;

            switch (isolationLevel)
            {
                case IsolationLevel.ReadUncommitted:
                    sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;";
                    break;
                case IsolationLevel.ReadCommitted:
                    sql = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED;";
                    break;
                default:
                    throw new Exception("ISOLATION LEVEL is not defined in this method.");
            }

            //(context as IObjectContextAdapter).ObjectContext.ExecuteStoreCommand(sql, null);
            if (context.Database.Connection.State != ConnectionState.Open)
            {
                // Explicitly open the connection, this connection will close when context is disposed
                context.Database.Connection.Open();
            }

            context.Database.ExecuteSqlCommand(sql);
        }


        public static IEnumerable<TResult> ExecuteStoredProcedure<TResult>(this Database database, IStoredProcedure<TResult> procedure, string storeName = null)
        {
            var parameters = CreateSqlParametersFromProperties(procedure);

            var format = CreateSpCommand<TResult>(parameters, storeName);

            return database.SqlQuery<TResult>(format, parameters.Cast<object>().ToArray());
        }

        private static List<SqlParameter> CreateSqlParametersFromProperties<TResult>(IStoredProcedure<TResult> procedure)
        {
            var procedureType = procedure.GetType();
            var propertiesOfProcedure = procedureType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var parameters =
                propertiesOfProcedure.Select(propertyInfo => new SqlParameter(string.Format("@{0}", propertyInfo.Name),
                                                                              propertyInfo.GetValue(procedure, new object[] { })))
                    .ToList();
            return parameters;
        }

        private static string CreateSpCommand<TResult>(List<SqlParameter> parameters, string storeName = null)
        {
            var queryString = storeName;
            if (string.IsNullOrEmpty(queryString))
            {
                storeName = typeof(TResult).Name;
                queryString = storeName.Substring(0, storeName.LastIndexOf('_'));
            }
            parameters.ForEach(x => queryString = string.Format("{0} {1},", queryString, x.ParameterName));

            return queryString.TrimEnd(',');
        }

        /// <summary>
        /// thực thi store có chứa dạng param user-definde
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <author>TrungLD</author>
        public static int ExecuteStoredProcedure(this Database dbContext, string storedProcedureName, params SqlParameter[] parameters)
        {
            var spSignature = new StringBuilder();
            object[] spParameters;
            bool hasTableVariables = parameters.Any(p => p.SqlDbType == SqlDbType.Structured);

            spSignature.AppendFormat("EXECUTE {0}", storedProcedureName);
            var length = parameters.Count() - 1;

            if (hasTableVariables)
            {
                var tableValueParameters = new List<SqlParameter>();

                for (int i = 0; i < parameters.Count(); i++)
                {
                    switch (parameters[i].SqlDbType)
                    {
                        case SqlDbType.Structured:
                            spSignature.AppendFormat(" @{0}", parameters[i].ParameterName);
                            tableValueParameters.Add(parameters[i]);
                            break;
                        case SqlDbType.VarChar:
                        case SqlDbType.Char:
                        case SqlDbType.Text:
                        case SqlDbType.NVarChar:
                        case SqlDbType.NChar:
                        case SqlDbType.NText:
                        case SqlDbType.Xml:
                        case SqlDbType.UniqueIdentifier:
                        case SqlDbType.Time:
                        case SqlDbType.Date:
                        case SqlDbType.DateTime:
                        case SqlDbType.DateTime2:
                        case SqlDbType.DateTimeOffset:
                        case SqlDbType.SmallDateTime:
                            // TODO: some magic here to avoid SQL injections
                            spSignature.AppendFormat(" '{0}'", parameters[i].Value.ToString());
                            break;
                        default:
                            spSignature.AppendFormat(" {0}", parameters[i].Value.ToString());
                            break;
                    }

                    if (i != length) spSignature.Append(",");
                }
                spParameters = tableValueParameters.Cast<object>().ToArray();
            }
            else
            {
                for (int i = 0; i < parameters.Count(); i++)
                {
                    spSignature.AppendFormat(" @{0}", parameters[i].ParameterName);
                    if (i != length) spSignature.Append(",");
                }
                spParameters = parameters.Cast<object>().ToArray();
            }

            return dbContext.ExecuteSqlCommand(spSignature.ToString(), spParameters);
        }

        #endregion
    }
}