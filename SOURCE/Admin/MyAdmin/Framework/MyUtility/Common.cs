/**********************************************************************
 * Author: ThongNT
 * DateCreate: 06-25-2014 
 * Description: Common define common static function 
 * ####################################################################
 * Author:......................
 * DateModify: .................
 * Description: ................
 * 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyUtility
{
    public class Common
    {
        #region ma hoa

        #region md5
        /// <summary>
        /// Author: ThongNT
        /// <para></para>
        /// Md5 Encrypt
        /// </summary>
        /// <param name="signOrginal"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string signOrginal)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(signOrginal));
                string hashString = ConvertByteToString(data);
                return hashString;
            }
        }

        public static string MD5_encode(string str_encode)
        {
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();
            using (var md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str_encode));
                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                foreach (byte t in data)
                {
                    sBuilder.Append(t.ToString("x2"));
                }
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        #endregion

        #region HMAC SHA 256

        /// <summary>
        /// Author: ThongNT
        /// <para></para>
        /// Convert byte array to hexa string
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private static string ConvertByteToString(IEnumerable<byte> buff)
        {
            return buff.Aggregate("", (current, t) => current + t.ToString("X2"));
        }

        /// <summary>
        /// Author: ThongNT
        /// <para></para>
        /// SHA256 Encrypt
        /// </summary>
        /// <param name="stringToHash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetHashHmac(string stringToHash, string password)
        {
            var pass = Encoding.UTF8.GetBytes(password);
            using (var hmacsha256 = new HMACSHA256(pass))
            {
                hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                return ConvertByteToString(hmacsha256.Hash);
            }
        }

        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 18/12/2014</para>
        /// <para>mã hóa sha256</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String sha256_hash(String value)
        {
            var sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        #endregion

        #region TripDES

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2015-07-16</para>
        /// <para>Description: Ma hoa TripDES</para>
        /// </summary>
        /// <returns></returns>
        public static string EncryptTripDes(string stringToEncrypt, string securityKey, bool isUseHashing = true)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            var key = securityKey;

            if (isUseHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            var tdes = new TripleDESCryptoServiceProvider
                {
                    //set the secret key for the tripleDES algorithm
                    Key = keyArray,
                    //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                    Mode = CipherMode.ECB,
                    //padding mode(if any extra byte added)
                    Padding = PaddingMode.PKCS7,
                };

            var cTransform = tdes.CreateEncryptor();

            //transform the specified region of bytes array to resultArray
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //Release resources held by TripleDes Encryptor
            tdes.Clear();

            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2015-07-16</para>
        /// <para>Description: Giai ma TripDES</para>
        /// </summary>
        /// <returns></returns>
        public static string DescryptTripDes(string stringToDecrypt, string securityKey, bool isUseHashing = true)
        {
            byte[] keyArray;
            var toEncryptArray = Convert.FromBase64String(stringToDecrypt);
            var key = securityKey;

            if (isUseHashing)
            {
                //if hashing was used get the hash code with regards to your key
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

                //release any resource held by the MD5CryptoServiceProvider
                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            var tdes = new TripleDESCryptoServiceProvider
                {
                    //set the secret key for the tripleDES algorithm
                    Key = keyArray,

                    //mode of operation. there are other 4 modes.
                    //We choose ECB(Electronic code Book)
                    Mode = CipherMode.ECB,

                    //padding mode(if any extra byte added)
                    Padding = PaddingMode.PKCS7,
                };

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            
            //Release resources held by TripleDes Encryptor
            tdes.Clear();

            //return the Clear decrypted TEXT
            return Encoding.UTF8.GetString(resultArray);
        }

        #endregion

        /// <summary>
        /// Mã hóa mật khẩu (mã hóa 1 chiều)
        /// </summary>
        /// <param name="cleanString">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi sau khi giải mã</returns>
        public static string Encrypt(string cleanString)
        {
            Byte[] clearBytes = new System.Text.UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
            hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(hashedBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static TimeSpan ConvetDateTimeToTimeSpan(DateTime dateTime)
        {
            TimeSpan dateBetween = DateTime.Now - dateTime;
            return dateBetween;
        }

        #endregion

        #region RANDOM_STRING
        /// <summary>
        /// Trả về chuỗi random
        /// </summary>
        /// <param name="size">độ dài của chuỗi</param>
        /// <param name="lowerCase">viết hoa hay thường.True:Viết hoa,Flase:Viết thường</param>
        /// <returns>Chuỗi sau khi random</returns>
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            Random rndint = new Random();
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                int so = rnd.Next(0, 2);
                if (so == 1)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                }
                else
                {
                    ch = Convert.ToChar(rndint.Next(0, 9).ToString());
                }

                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        #endregion

        #region Triple Des
        public static string EncryptTripleDes(string toEncrypt,  string key,bool useHashing = true)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //// Get the key from config file

            //string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                //of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptTripleDes(string cipherString, string key, bool useHashing=true)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            ////Get your key from config file to open the lock!
            //string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            
            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            //set the secret key for the tripleDES algorithm
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)

            //padding mode(if any extra byte added)

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion

        #region ma hoa base64

        public static string EncryptBase64(string toEncrypt)
        {
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            return Convert.ToBase64String(toEncryptArray, 0, toEncryptArray.Length);
        }

        public static string DecryptBase64(string cipherString)
        {
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            return UTF8Encoding.UTF8.GetString(toEncryptArray);
        }
        #endregion

        #region Encrypt

        private static readonly byte[] Sbox = new byte[255];
        private static readonly byte[] MyKey = new byte[255];

        private static string pr_Encrypt(string src, string key)
        {
            var mtxt = Encoding.ASCII.GetBytes(src);
            var mkey = Encoding.ASCII.GetBytes(key);
            var result = Calculate(mtxt, mkey);
            return CharsToHex(result);
        }

        private static string pr_Decrypt(string src, string key)
        {
            var mtxt = HexToChars(src);
            var mkey = Encoding.ASCII.GetBytes(key);
            var result = Calculate(mtxt, mkey);
            return Encoding.ASCII.GetString(result);
        }

        private static void Initialize(IList<byte> pwd)
        {
            byte b = 0;
            var intLength = pwd.Count;

            for (byte a = 0; a < 255; a++)
            {
                MyKey[a] = pwd[(a % intLength)];
                Sbox[a] = a;
            }

            for (byte a = 0; a < 255; a++)
            {
                b = (byte)((b + Sbox[a] + MyKey[a]) % 255);
                var tempSwap = Sbox[a];
                Sbox[a] = Sbox[b];
                Sbox[b] = tempSwap;
            }
        }

        private static byte[] Calculate(IList<byte> plaintxt, IList<byte> psw)
        {
            Initialize(psw);
            byte i = 0;
            byte j = 0;
            var len = plaintxt.Count;
            var cipher = new byte[len];
            for (var a = 0; a < len; a++)
            {
                i = (byte)((i + 1) % 255);
                j = (byte)((j + Sbox[i]) % 255);

                var temp = Sbox[i];
                Sbox[i] = Sbox[j];
                Sbox[j] = temp;

                var idx = (byte)((Sbox[i] + Sbox[j]) % 255);
                int k = Sbox[idx];

                var cipherby = (byte)(plaintxt[a] ^ k);
                cipher[a] = cipherby;
            }
            return cipher;
        }

        private static byte[] HexToChars(string hex)
        {
            var len = hex.Length;
            var codes = new byte[len / 2];
            for (var i = (hex.Substring(0, 2) == "0x") ? 2 : 0; i < len; i += 2)
            {
                codes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return codes;
        }

        private static string CharsToHex(IList<byte> chars)
        {
            var result = string.Empty;
            var len = chars.Count;
            for (var i = 0; i < len; i++)
            {
                result += String.Format("{0:x2}", chars[i]);
            }
            return result;
        }

        public static string Encrypt(string cipher, string key, bool isencrypt)
        {
            return !isencrypt ? cipher : pr_Encrypt(cipher, key);
        }
        
        public static string Decrypt(string cipher, string key, bool isencrypt)
        {
            return !isencrypt ? cipher : pr_Decrypt(cipher, key);
        }

        #endregion
    }
}