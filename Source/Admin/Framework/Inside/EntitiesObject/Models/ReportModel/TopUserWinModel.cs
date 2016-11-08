
namespace EntitiesObject.Models.ReportModel
{
    public class TopUserWinModel
    {
        public int UserId { get; set; }

        public string DisplayName { get; set; }

        public decimal TotalGold { get; set; }

        public TopUserWinModel()
        {
            UserId = 0;
            DisplayName = string.Empty;
            TotalGold = 0;
        }
    }
}
