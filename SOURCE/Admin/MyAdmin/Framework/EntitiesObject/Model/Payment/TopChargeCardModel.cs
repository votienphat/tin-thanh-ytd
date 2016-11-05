
namespace EntitiesObject.Model.Payment
{
    public class TopChargeCardModel
    {
        public string Username { get; set; }

        public int TotalAmount { get; set; }

        public int RowNumber { get; set; }

        public TopChargeCardModel()
        {
            Username = string.Empty;
            TotalAmount = 0;
            RowNumber = 0;
        }
    }
}
