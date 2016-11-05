using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HqCardckStockApi.Model
{
    public class HqCardRequest
    {
        /// <summary>
        /// http://api.doithe.net:6688/get-card.html
        /// <para>Method : POST</para>
        /// </summary>
        public string UrlCheckoutCard { get; set; }

        /// <summary>
        /// Key do DOITHE.NET cung cấp hoặc lấy từ thông tin
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Key do DOITHE.NET cung cấp hoặc lấy từ thông tin
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Mã giao dịch để đối soát, là chuổi duy nhất đối với mỗi lần giao dịch
        /// <para>(length:32-100)</para>
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Là mã loại thẻ:
        /// <para>- VMS: thẻ Mobifone</para>
        /// <para>- VNP: thẻ Vinaphone</para>
        /// <para>- VTT: thẻ Viettel</para>
        /// <para>- BIT: thẻ BIT</para>
        /// <para>Chỉ cần gửi mả Telco, ví dụ: VMS, BIT (chữ in hoa)</para>
        /// </summary>
        public string Telco { get; set; }

        /// <summary>
        /// Mệnh giá thẻ
        /// <para>Bao gồm các thẻ: 10000, 20000, 50000, 100000, 200000, 300000, 500000, 1000000, 2000000, 5000000</para>
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Thời gian gọi theo chuẩn YYYYmmddHHiiss(ví dụ 20140120085633) 
        /// <para>C# : yyyyMMddHHmmss</para>
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Chữ ký xác thực của mỗi lần giao dịch Chuổi mã hóa theo HMAC chuẩn SHA256. 
        /// <para>Sign = HMAC_SHA256( OrderID + Telco + Price + Time , Secret) </para>
        /// <para>Thông tin về HMAC_SHA256 : http://en.wikipedia.org/wiki/HMAC_SHA256%22 </para>
        /// <para>Tool kiểm tra: HMAC_SHA256 Online : http://www.freeformatter.com/hmac-generator.html </para>
        /// </summary>
        public string Sign { get; set; }
    }

    public class HqCardResponse
    {
        /// <summary>
        /// Mã lổi, thành công sẽ là 0
        /// </summary>
        public int e { get; set; }

        /// <summary>
        /// Ket qua tra ve
        /// <para>Thành công: {"e":0,"r":{"OrderID":"1432723037",...,"PinCode":"RRERER"}}</para>
        /// <para>Thất bại: {"e":34,"r":"Kho hàng đã hết thẻ, vui lòng liên hệ lại sau"}</para>
        /// </summary>
        public object r { get; set; }

        /// <summary>
        /// Chuoi ket qua tra ve tu doi tac
        /// </summary>
        public string ResponseText { get; set; }

        /// <summary>
        /// Thong tin the tra ve
        /// </summary>
        public HqCardItem Card { get; set; }
    }

    internal class HqCardResponseOk
    {
        /// <summary>
        /// Mã lổi, thành công sẽ là 0
        /// </summary>
        public int e { get; set; }

        /// <summary>
        /// Ket qua tra ve
        /// <para>Thành công: {"e":0,"r":{"OrderID":"1432723037",...,"PinCode":"RRERER"}}</para>
        /// <para>Thất bại: {"e":34,"r":"Kho hàng đã hết thẻ, vui lòng liên hệ lại sau"}</para>
        /// </summary>
        public HqCardItem r { get; set; }
    }

    public class HqCardItem
    {
        public  string OrderID { get; set; }
        public string PinCode { get; set; }
        public string Serial { get; set; }
        public string DateExpired { get; set; }
    }
}
