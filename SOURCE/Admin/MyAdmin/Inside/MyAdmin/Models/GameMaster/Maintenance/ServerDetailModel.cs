
namespace MyAdmin.Models.GameMaster.Maintenance
{
    public class ServerDetailModel
    {
        public string Status { get; set; }

        public string BWU { get; set; }

        public string BWD { get; set; }

        public string CPU { get; set; }

        public string URAM { get; set; }

        public string TRAM { get; set; }

        public string ARAM { get; set; }

        public string MAXBD { get; set; }

        public string MADBU { get; set; }

        public string MAXCPU { get; set; }

        public string MAXRAM { get; set; }

        public string TMAXBD { get; set; }

        public string TMADBU { get; set; }

        public string TMAXCPU { get; set; }

        public string TMAXRAM { get; set; }

        public string AVCPU { get; set; }

        public string DW { get; set; }

        public string DR { get; set; }

        public string DT { get; set; }

        public string MAXDR { get; set; }

        public string MAXDW { get; set; }

        public string MAXDT { get; set; }

        public string TMADBR { get; set; }

        public string TMAXBW { get; set; }

        public string TMAXBT { get; set; }

        public string APIFBSTATUS { get; set; }

        public string APIFBRMEASSAGE { get; set; }

        public ServerDetailModel()
        {
            Status = "0";
            BWU = "0";
            BWD = "0";
            CPU = "0";
            URAM = "0";
            TRAM = "0";
            ARAM = "0";
            MAXBD = "0";
            MADBU = "0";
            MAXCPU = "0";
            MAXRAM = "0";
            TMAXBD = string.Empty;
            TMADBU = string.Empty;
            TMAXCPU = string.Empty;
            TMAXRAM = string.Empty;
            AVCPU = "0";
            DW = "0";
            DR = "0";
            DT = "0";
            MAXDR = "0";
            MAXDW = "0";
            MAXDT = "0";
            TMADBR = string.Empty;
            TMAXBW = string.Empty;
            TMAXBT = string.Empty;
            APIFBSTATUS = string.Empty;
            APIFBRMEASSAGE = string.Empty;
        }
    }
}