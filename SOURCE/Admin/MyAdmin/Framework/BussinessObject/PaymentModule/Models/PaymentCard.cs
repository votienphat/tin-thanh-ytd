using System.Collections.Generic;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.PaymentModule.Models
{
    public class PaymentCard : MyConfig
    {
        public List<CardType> CardTypes { get; set; }
        public IEnumerable<CardRate> CardRate { get; set; }
    }



    
}
