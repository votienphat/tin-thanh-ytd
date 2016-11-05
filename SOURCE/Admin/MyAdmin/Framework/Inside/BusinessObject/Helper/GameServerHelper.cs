using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Net;
using System.Text;
using BusinessObject.Models.Response;
using Logger;
using MyUtility;
using MyUtility.Extensions;
using Newtonsoft.Json;

namespace BusinessObject.Helper
{
    public class GameServerHelper
    {
        /// <summary>
        /// Gọi web socket service có model trả về
        /// </summary>
        /// <typeparam name="T">Loại dữ liệu cần trả về</typeparam>
        /// <param name="socket"></param>
        /// <param name="packetId"></param>
        /// <param name="model">Đối tượng gửi đi</param>
        /// <param name="status">Trạng thái của websocket</param>
        /// <param name="getComplexModel">Có lấy đối tượng từ WebSocket hay không</param>
        /// <returns></returns>
        /// <history>
        /// 29/09/2015 Create By PhatVT
        /// </history>
        public static T Call<T>(SocketModel socket, WsPacket packetId, object model, out int status, bool getComplexModel = false)
        {
            T result = default(T);
            status = 0;
            try
            {

                // Tạo đối tượng mới với dữ liệu động
                dynamic item = new ExpandoObject();
                SetValue(item, model);

                // Tạo unixtime để mã hóa
                double unixTimeStamp = ConvertTimestamp(DateTime.Now);
                string unixTimeStampEncrypted = Encryption.Encrypt(unixTimeStamp.ToString(), socket.Key);
                var data = string.Format(@"{0}{1}{2}{3}", packetId.Value(), socket.Admin.Password, unixTimeStampEncrypted, socket.Key);
                var sign = Common.MD5_encode(data);

                // Mã hóa data để gọi socket
                ((IDictionary<string, object>)item)["UnixTime"] = unixTimeStampEncrypted;
                ((IDictionary<string, object>)item)["Sign"] = sign;
                data = JsonConvert.SerializeObject(item);
                var src = Encryption.Encrypt(data, socket.Key);

                string html;
                string next;
                NetworkCommon.SendGetRequest(socket.Url + "/" + src, new CookieContainer(), string.Empty, out html,
                    out next, false);

                var receiveData = html;

                result = JsonConvert.DeserializeObject<T>(receiveData);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("CallWebSocket", ex);
            }

            return result;
        }

        /// <summary>
        /// Chuyển từ gọi web socket sang HTTP
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="packetId"></param>
        /// <param name="model">Đối tượng gửi đi</param>
        /// <returns></returns>
        /// <history>
        /// 06/11/2015 Create By PhatVT
        /// </history>
        public static int Call(SocketModel socket, WsPacket packetId, object model)
        {
            int status = 0;

            try
            {
                // Tạo đối tượng mới với dữ liệu động
                dynamic item = new ExpandoObject();
                SetValue(item, socket.Admin);

                // Tạo unixtime để mã hóa
                double unixTimeStamp = ConvertTimestamp(DateTime.Now);
                string unixTimeStampEncrypted = Encryption.Encrypt(unixTimeStamp.ToString(), socket.Key);
                var data = string.Format(@"{0}{1}", unixTimeStampEncrypted, socket.Key);
                var sign = Common.MD5_encode(data);

                // Mã hóa data để gọi socket
                ((IDictionary<string, object>)item)["UnixTime"] = unixTimeStampEncrypted;
                ((IDictionary<string, object>)item)["Sign"] = sign;
                ((IDictionary<string, object>)item)["ID"] = (int)packetId;
                SetValue(item, model);
                data = JsonConvert.SerializeObject(item);
                var src = Encryption.Encrypt(data, socket.Key);

                var html = new ArrayList();
                string next;


                string htmlTemp;
                CommonLogger.DefaultLogger.Debug("CallWebSocket | " + socket.Url + "/" + src);
                NetworkCommon.SendGetRequest(socket.Url + "/" + src, new CookieContainer(), string.Empty, out htmlTemp,
               out next, false);
                html.Add(htmlTemp);



                for (int i = 0; i < html.Count; i++)
                {
                    var receiveData = html[i].ToString();
                    var response = JsonConvert.DeserializeObject<ResponseSocketModel>(receiveData);
                    if (response == null) continue;
                    status = response.Status;
                    if (status == UpdateStatus.ThanhCong.Value())
                    {
                        return status;
                    }
                }

            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("CallWebSocket", ex);
            }

            return status;
        }

        private static void SetValue(ExpandoObject item, object model)
        {
            if (model != null)
            {
                var listProperties = model.GetType().GetProperties();
                if (listProperties.Length > 0)
                {
                    foreach (var prop in listProperties)
                    {
                        string displayName = prop.Name;
                        var attributes = prop.GetCustomAttributes(true);
                        foreach (var attr in attributes)
                        {
                            var displayNameAttr = attr as DisplayNameAttribute;
                            if (displayNameAttr != null)
                            {
                                displayName = displayNameAttr.DisplayName;
                                break;
                            }
                        }

                        var value = model.GetType().GetProperty(prop.Name).GetValue(model, null);
                        ((IDictionary<string, object>)item)[displayName] = value;
                    }
                }
            }
        }

        private static double ConvertTimestamp(DateTime date)
        {
            var span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return Math.Round(span.TotalSeconds, 0);
        }
    }

    public class SocketModel
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public AdminSocketModel Admin { get; set; }
    }

    public class ResponseSocketModel
    {
        public int ID { get; set; }
        public int Status { get; set; }
        public string Msg { get; set; }
    }

    public class AdminSocketModel
    {
        public int ID { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// RC4 (MDV) cryptography	
    /// </summary>
    internal class Encryption
    {
        private static readonly byte[] Sbox = new byte[255];
        private static readonly byte[] Mykey = new byte[255];
        private static readonly int[] Key = { 125, 577, 638, 345, 96, 724, 302, 95, 74, 522 };

        /**
        * Encrypts a string with the specified key.
        */
        public static string Encrypt(string src, string key)
        {
            byte[] mtxt = Encoding.ASCII.GetBytes(src);
            byte[] mkey = Encoding.ASCII.GetBytes(key);
            byte[] result = Calculate(mtxt, mkey);
            return CharsToHex(result);
        }

        public static string Decrypt(string src, string key)
        {
            byte[] mtxt = HexToChars(src);
            byte[] mkey = Encoding.ASCII.GetBytes(key);
            byte[] result = Calculate(mtxt, mkey);
            return Encoding.ASCII.GetString(result);
        }

        #region Calculate

        private static byte[] Calculate(byte[] plaintxt, byte[] psw)
        {
            Initialize(psw);
            byte i = 0;
            byte j = 0;
            int len = plaintxt.Length;
            var cipher = new byte[len];
            for (int a = 0; a < len; a++)
            {
                i = (byte)((i + 1) % 255);
                j = (byte)((j + Sbox[i]) % 255);
                byte temp = Sbox[i];
                Sbox[i] = Sbox[j];
                Sbox[j] = temp;
                var idx = (byte)((Sbox[i] + Sbox[j]) % 255);
                int k = Sbox[idx];
                var cipherby = (byte)(plaintxt[a] ^ k);
                cipher[a] = cipherby;
            }
            return cipher;
        }
        #endregion

        #region Initialize
        private static void Initialize(byte[] pwd)
        {
            byte b = 0;
            int intLength = pwd.Length;

            for (byte a = 0; a < 255; a++)
            {
                Mykey[a] = pwd[(a % intLength)];
                Sbox[a] = a;
            }

            for (byte a = 0; a < 255; a++)
            {
                b = (byte)((b + Sbox[a] + Mykey[a]) % 255);
                byte tempSwap = Sbox[a];
                Sbox[a] = Sbox[b];
                Sbox[b] = tempSwap;
            }
        }
        #endregion

        #region CharsToHex
        private static string CharsToHex(byte[] chars)
        {
            string result = string.Empty;
            int len = chars.Length;
            for (int i = 0; i < len; i++)
            {
                result += String.Format("{0:x2}", chars[i]);
            }
            return result;
        }
        #endregion

        #region HexToChars
        private static byte[] HexToChars(string hex)
        {
            int len = hex.Length;
            var codes = new byte[len / 2];
            for (int i = (hex.Substring(0, 2) == "0x") ? 2 : 0; i < len; i += 2)
            {
                codes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return codes;
        }
        #endregion

        #region Encrypt2

        public static string Encrypt2(string srcStr)
        {
            string destStr = "";
            int len = srcStr.Length;
            for (int i = 0; i < len; i++)
            {
                char s = srcStr[i];
                int iascii = s;
                int k = i % 10;
                string ss = (iascii + Key[k] + (len % 1000)).ToString();
                ss = ss.PadLeft(4, '0');
                destStr += ss;
            }
            return destStr;
        }
        #endregion

        #region Decrypt2

        public static string Decrypt2(string srcStr)
        {
            try
            {
                string destStr = "";
                int len = srcStr.Length;
                int l = len / 4;
                for (int i = 0; i < len; i += 4)
                {
                    int num = int.Parse(srcStr.Substring(i, 4));//Number(srcStr.substr(i,3));
                    int k = (i / 4) % 10;
                    char kytu = Convert.ToChar(num - Key[k] - (l % 1000));//Char.(num-53);
                    destStr += kytu;
                }
                return destStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

    /// <summary>
    /// Định nghĩa mã packet để sử dụng
    /// </summary>
    public enum WsPacket
    {
        KickUser = 2,
        GetUserGameStatus = 1,
        UpdateGoldUser = 3,
        MaintainSever = 100,
        GetServerInfo = 101
    }
}