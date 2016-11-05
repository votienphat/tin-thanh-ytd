using System.Collections.Generic;
using EntitiesObject.Entities.UserEntities;
using EntitiesObject.Model.PaymentConfig;

namespace BussinessObject.PaymentModule.Models
{
    // ReSharper disable once InconsistentNaming
    public class GoogleIAPModel
    {

        public bool IsMaintenance { get; set; }
        public string Message { get; set; }

        public IEnumerable<Out_PaymentConfig_GetRateGoogleIAP_Result> ListData { get; set; }

        public IEnumerable<PaymentConfigModel> ListDataByChannel { get; set; }
    }
}