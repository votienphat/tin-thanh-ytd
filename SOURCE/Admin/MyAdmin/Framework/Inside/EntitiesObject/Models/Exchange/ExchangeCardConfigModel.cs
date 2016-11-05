using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesObject.Models.Exchange
{
    /// <summary>
    /// 01/6/2016 Create by TaiNM
    /// </summary>
    public class ExchangeCardConfigModel
    {
        public bool IsAutoExchange { get; set; }

        [Range(0, 10000000, ErrorMessage = "Max Exchange phải >= 0 và nhỏ hơn 10.000.000")]
        public decimal MaxExchange { get; set; }

        [Range(0, 10000000, ErrorMessage = "Min Exchange phải >= 0 và nhỏ hơn 10.000.000")]
        public decimal MinExchange { get; set; }

        /// <summary>
        /// Đơn vị tính bằng phút
        /// </summary>
        [Range(0, 1440, ErrorMessage = "Thời gian Exchange phải >= 0 và nhỏ hơn 1440 phút")]
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
