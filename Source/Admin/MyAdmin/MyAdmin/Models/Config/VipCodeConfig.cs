using System;
using System.Collections.Generic;

namespace MyAdmin.Models.Config
{
    public class VipCodeConfig
    {
        /// <summary>
        /// </summary>
        /// <history>
        /// 12/11/2014 PhatVT: Tạo mới
        /// </history>
        public bool Enable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <history>
        /// 12/11/2014 PhatVT: Tạo mới
        /// </history>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <history>
        /// 12/11/2014 PhatVT: Tạo mới
        /// </history>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Config xác định user được tặng code ở đâu
        /// 1 : Web
        /// 2 : Mobile
        /// </summary>
        /// <history>
        /// 12/11/2014 PhatVT: Tạo mới
        /// </history>
        public List<VipCodeFromWhereEnum> FromWhere { get; set; }

        /// <summary>
        /// </summary>
        /// <history>
        /// 12/11/2014 PhatVT: Tạo mới
        /// </history>
        public List<VipCodeModel> VipCodes { get; set; }

        //public VipCodeConfig()
        //{
        //    Enable = true;
        //    StartDate = new DateTime(2020, 8, 15, 10, 0, 0);
        //    EndDate = new DateTime(2020, 8, 30, 23, 59, 59);
        //    FromWhere = new List<VipCodeFromWhereEnum>() {VipCodeFromWhereEnum.Web};
        //    VipCodes = new List<VipCodeModel>()
        //    {
        //        new VipCodeModel(){CardAmount = 10000,NumberCode = 0},
        //        new VipCodeModel(){CardAmount = 20000,NumberCode = 0},
        //        new VipCodeModel(){CardAmount = 50000,NumberCode = 0},
        //        new VipCodeModel(){CardAmount = 100000,NumberCode = 0},
        //        new VipCodeModel(){CardAmount = 200000,NumberCode = 0},
        //        new VipCodeModel(){CardAmount = 500000,NumberCode = 0},
        //        new VipCodeModel(){CardAmount = 1000000,NumberCode = 0},
        //    };
        //}
    }

    public enum VipCodeFromWhereEnum
    {
        Web=1,
        Mobile=2
    }
    public class VipCodeModel
    {
        /// <summary>
        /// </summary>
        /// <history>
        /// 12/11/2014 PhatVT: Tạo mới
        /// </history>
        public int CardAmount { get; set; }

        /// <summary>
        /// </summary>
        /// <history>
        /// 12/11/2014 PhatVT: Tạo mới
        /// </history>
        public int NumberCode { get; set; }

        public VipCodeModel()
        {
            CardAmount = 50;
            NumberCode = 0;
        }
    }
}
