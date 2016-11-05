namespace EntitiesObject.Models.ReportModel
{
    public class DataCCU
    {
        public int? date { get; set; }

        public int? MaxCCU { get; set; }

        public int? MinCCU { get; set; }

        public double? AvgCCU { get; set; }

        //public List<CCUModel> FullfillCcus(DateTime beginDate, DateTime endDate, List<DataCCU> data)
        //{

        //    var listTemp = new LinkedList<CCUModel>();
        //    var date = DateTime.Now.Date;
        //    if (!data.Any())
        //    {
        //        listTemp.AddLast(new CCUModel { date = date, MaxCCU = 0, MinCCU = 0, AvgCCU = 0 });
        //        listTemp.AddLast(new CCUModel { date = date.AddHours(23), MaxCCU = 0, MinCCU = 0, AvgCCU = 0 });
        //        return listTemp.ToList();
        //    }
        //    for (int i = 0; i < 24; i++)
        //    {
        //        if (i < data[0].date)
        //        {
        //            listTemp.AddLast(new CCUModel
        //            {
        //                date = date.AddHours(i),
        //                MaxCCU = 0,
        //                MinCCU = 0,
        //                AvgCCU = 0
        //            });
        //        }
        //        if (i == data[0].date)
        //        {
        //            var tmp = data.First();
        //            listTemp.AddLast(new CCUModel { date = date.AddHours(tmp.date.Value), MaxCCU = tmp.MaxCCU, MinCCU = tmp.MinCCU, AvgCCU = tmp.AvgCCU });
        //            data.Remove(data[0]);
        //        }
        //    }
        //    return listTemp.ToList();
        //}

    }
}
