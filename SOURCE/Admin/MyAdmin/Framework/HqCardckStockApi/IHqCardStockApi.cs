using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using HqCardckStockApi.Model;
using MyUtility;

namespace HqCardckStockApi
{
    public interface IHqCardStockApi
    {
        HqCardResponse CheckoutCard(HqCardRequest request);
    }

    internal class HqCardStockApi : IHqCardStockApi
    {

        public HqCardResponse CheckoutCard(HqCardRequest request)
        {
            switch (request.Telco)
            {
                case "Mobi":
                    request.Telco = "VMS";
                    break;
                case "Vina":
                    request.Telco = "VNP";
                    break;
                case "Viettel":
                    request.Telco = "VTT";
                    break;
                case "Bit":
                    request.Telco = "BIT";
                    break;
                    
            }
            var response = new HqCardResponse();
            request.UrlCheckoutCard = "http://api.doithe.net:6688/get-card.html";
            request.Key = "sVz2br2YoN1";
            request.Secret = "v0Rf07UMBcpUNTIpUWYRD";
            request.Time = DateTime.Now.ToString("yyyyMMddHHmmss");
            request.Sign = string.Concat(request.OrderId, request.Telco, request.Price, request.Time);
            request.Sign = HmacSha256(request.Sign, request.Secret);

            using (var client = new WebClient())
            {
                var param = "Key=" + request.Key + "&OrderID=" + request.OrderId +
                            "&Telco=" + request.Telco + "&Price=" + request.Price + "&Time=" + request.Time + "&Sign=" + request.Sign; 
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = client.UploadString(request.UrlCheckoutCard, param);
                //var result = "{\"e\":0,\"r\":{\"OrderID\":\"10006\",\"Telco\":\"VMS\",\"Price\":\"10000\",\"Serial\":\"037081000003157\",\"PinCode\":\"379718868628\",\"DateExpired\":\"2017-12-30\"}}";
                response.ResponseText = result;
                /*
                 * Json Success
                 * {"e":0,"r":{"OrderID":"10006","Telco":"VMS","Price":"10000","Serial":"037081000003157","PinCode":"379718868628","DateExpired":"2017-12-30"}}
                 */
                //result = result.Replace("\"{", "{").Replace("\\\"", "\"").Replace("}\"", "}");

                //Logger.CommonLogger.PaymentLogger.Debug("\r\n jsonCardparam: " + param);
                Logger.CommonLogger.PaymentLogger.Debug("\r\n jsonCard: " + result);
                Logger.CommonLogger.PaymentLogger.Debug("\r\n========================================================\r\n");
                /* Handle response*/
                try
                {
                    var serializer = new JavaScriptSerializer();
                    response = serializer.Deserialize<HqCardResponse>(result);
                    response.ResponseText = result;
                    if (response.e == 0)
                    {
                        /* Success */
                        try
                        {
                            var rs = serializer.Deserialize<HqCardResponseOk>(result);
                            response.Card = rs.r;
                        }
                        catch 
                        {
                            response.r = "Loi Parse Json CardItem";
                        }
                    }
                }
                catch
                {
                    response.r = "Loi Parse Json result : " + result;
                }
            }
            return response;
        }

        private string HmacSha256(string source, string key)
        {
            var srcBytes = Encoding.UTF8.GetBytes(source);
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var hash = new HMACSHA256(keyBytes);
            var hashSource = hash.ComputeHash(srcBytes);
            var result = hashSource.Aggregate("", (current, t) => current + t.ToString("X2")).ToLower();
            return result;
        }
    }

    public class HqCardStockFactory
    {
        public static IHqCardStockApi CardStock
        {
            get { return new HqCardStockApi(); }
        }
    }
}
