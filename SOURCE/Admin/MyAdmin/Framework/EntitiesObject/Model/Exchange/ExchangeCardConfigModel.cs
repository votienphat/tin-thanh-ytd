using System;

namespace EntitiesObject.Model.Exchange
{
    public class ExchangeCardConfigModel
    {
        public bool IsAutoExchange { get; set; }

        public decimal MaxExchange { get; set; }

        public decimal MinExchange { get; set; }

        /// <summary>
        /// Đơn vị tính bằng phút
        /// </summary>
        public int RuleTimePlayGame { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ExchangeCardConfigModel()
        {
            IsAutoExchange = false;
            MaxExchange = 0;
            MinExchange = 0;
            RuleTimePlayGame = 0;
            UpdatedDate = DateTime.Now;
        }
    }
}
