
namespace EntitiesObject.Models.ReportModel
{
    public class TopLevelUserModel
    {
        public int UserId { get; set; }

        public string DisplayName { get; set; }

        public int LevelUser { get; set; }

        public TopLevelUserModel()
        {
            UserId = 0;
            DisplayName = string.Empty;
            LevelUser = 0;
        }
    }
}
