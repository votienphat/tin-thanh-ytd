using System;
using System.Collections.Generic;
using System.Linq;

namespace EntitiesObject.Models.ReportModel
{
    public class DataRevenue
    {
        public DateTime date { get; set; }
        public decimal MaxRevenue { get; set; }

        public decimal MinRevenue { get; set; }

        public decimal AvgRevenue { get; set; }
        public List<DataRevenue> FullfillRevenueFullMonth(DateTime beginDate, DateTime endDate, List<DataRevenue> data)
        {
           
            var listTemp = new LinkedList<DataRevenue>();
            var tembeginDate = new DateTime(beginDate.Year, beginDate.Month, 1);//tao gia tri ngay bat dau
            if (!data.Any())
            {
                //khoi tao 2 thang cho chart
                listTemp.AddLast(new DataRevenue { date = tembeginDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                listTemp.AddLast(new DataRevenue { date = endDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                return listTemp.ToList();
            }
            #region khi chua co du lieu
            var temAtFirstOfdata = new DateTime(data[0].date.Year, data[0].date.Month, 1);
            if (DateTime.Compare(tembeginDate, temAtFirstOfdata.AddMonths(-1)) < 0)
            {
                //khoi tao 2 thang cho chart
                listTemp.AddLast(new DataRevenue { date = tembeginDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                listTemp.AddLast(new DataRevenue { date = data[0].date.AddMonths(-1), MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                tembeginDate = temAtFirstOfdata;
            }
            #endregion

            #region khi co du lieu
            var temAtEndofData = new DateTime(data[data.Count() - 1].date.Year, data[data.Count() - 1].date.Month, 1);
            int comparedTemBeginAndEnd = DateTime.Compare(tembeginDate, temAtEndofData);
            while (comparedTemBeginAndEnd < 0 || comparedTemBeginAndEnd == 0)
            {
                if (DateTime.Compare(tembeginDate, data[0].date) < 0)
                {
                    listTemp.AddLast(new DataRevenue
                    {
                        date = tembeginDate,
                        MaxRevenue = 0,
                        MinRevenue = 0,
                        AvgRevenue = 0
                    });
                }
                else
                {
                    listTemp.AddLast(data[0]);
                    if (data.Count() > 1) data.Remove(data[0]);
                }
                

                tembeginDate = tembeginDate.AddMonths(1);//them 1 ngay cho gia tri khoi dau
                temAtEndofData = new DateTime(data[data.Count() - 1].date.Year, data[data.Count() - 1].date.Month, 1);
                comparedTemBeginAndEnd = DateTime.Compare(tembeginDate, temAtEndofData);//so sanh gia tri khoi dau va ket thuc
            }
            #endregion

            #region khi het du lieu
            if (DateTime.Compare(tembeginDate.AddMonths(1), endDate) < 0)
            {
                //khoi tao 2 thang cho chart
                listTemp.AddLast(new DataRevenue { date = tembeginDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                listTemp.AddLast(new DataRevenue { date = endDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
            }
            else if (DateTime.Compare(tembeginDate, endDate) == 0)
                listTemp.AddLast(new DataRevenue { date = endDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
            #endregion

            return listTemp.ToList();
        }
        public List<DataRevenue> FullfillRevenueFullDay(DateTime beginDate, DateTime endDate, List<DataRevenue> data)
        {
            var listTemp = new LinkedList<DataRevenue>();
            var tembeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day);//tao gia tri ngay bat dau

            #region  neu khong co du lieu
            if (!data.Any())
            {
                //khoi tao 2 thang cho chart
                listTemp.AddLast(new DataRevenue { date = tembeginDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                listTemp.AddLast(new DataRevenue { date = endDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                return listTemp.ToList();
            }

            #endregion

            #region khi chua co du lieu
            var temAtFirstOfdata = new DateTime(data[0].date.Year, data[0].date.Month, data[0].date.Day);
            if (DateTime.Compare(tembeginDate, temAtFirstOfdata.AddDays(-1)) < 0)
            {
                //khoi tao 2 thang cho chart
                listTemp.AddLast(new DataRevenue { date = tembeginDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                listTemp.AddLast(new DataRevenue { date = data[0].date.AddDays(-1), MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                tembeginDate = temAtFirstOfdata;
            }
            #endregion

            #region khi co du lieu
            var temAtEndofData = new DateTime(data[data.Count() - 1].date.Year, data[data.Count() - 1].date.Month, data[data.Count() - 1].date.Day);
            int comparedTemBeginAndEnd = DateTime.Compare(tembeginDate, temAtEndofData);
            while (comparedTemBeginAndEnd < 0 || comparedTemBeginAndEnd == 0)
            {
                if (DateTime.Compare(tembeginDate, data[0].date) < 0)
                {
                    listTemp.AddLast(new DataRevenue
                    {
                        date = tembeginDate,
                        MaxRevenue = 0,
                        MinRevenue = 0,
                        AvgRevenue = 0
                    });
                }
                else
                {
                    listTemp.AddLast(data[0]);
                    if (data.Count() > 1) data.Remove(data[0]);
                }
                
                tembeginDate = tembeginDate.AddDays(1);//them 1 ngay cho gia tri khoi dau
                temAtEndofData = new DateTime(data[data.Count() - 1].date.Year, data[data.Count() - 1].date.Month, data[data.Count() - 1].date.Day);
                comparedTemBeginAndEnd = DateTime.Compare(tembeginDate, temAtEndofData);//so sanh gia tri khoi dau va ket thuc
            }
            #endregion

            #region khi het du lieu
            if (DateTime.Compare(tembeginDate.AddDays(1), endDate) < 0)
            {
                //khoi tao 2 thang cho chart
                listTemp.AddLast(new DataRevenue { date = tembeginDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
                listTemp.AddLast(new DataRevenue { date = endDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
            }
            else if (DateTime.Compare(tembeginDate, endDate) == 0)
                listTemp.AddLast(new DataRevenue { date = endDate, MaxRevenue = 0, MinRevenue = 0, AvgRevenue = 0 });
            #endregion


            return listTemp.ToList();
        }
    }

}
