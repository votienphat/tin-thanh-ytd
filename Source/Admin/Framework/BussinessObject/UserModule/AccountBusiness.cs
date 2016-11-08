using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject.UserModule.Contract;
using BussinessObject.UserModule.Enums;
using BussinessObject.UserModule.Models;
using BussinessObject.UserModule.Models.Request;
using BussinessObject.UserModule.Models.Response;
using DataAccess.Contract.UserModule;
using DataAccessRedis.Module.Contract;
using EntitiesObject.Entities.UserEntities;
using Facebook;
using MyUtility;
using MyUtility.Extensions;
using Newtonsoft.Json.Linq;

namespace BussinessObject.UserModule
{
    public class AccountBusiness : IAccountBusiness
    {
        #region Variables
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRedis _accountRedis;
        #endregion

        public AccountBusiness(IAccountRepository accountRepository, IAccountRedis accountRedis)
        {
            _accountRepository = accountRepository;
            _accountRedis = accountRedis;
        }

        /// <summary>
        /// Lấy thông tin account theo user ID
        /// <para>Author: PhatVT</para>
        /// <para>Created Date: 18/12/2014</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo GetInfo(int userId)
        {
            var user = _accountRepository.GetAccountById(userId);
            if (user == null)
            {
                return null;
            }
            return new UserInfo
            {
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive,
                LastLogin = user.LastLogin.HasValue ? user.LastLogin.Value : new DateTime(1900, 1, 1),
                NickName = user.Username,
                UserId = user.UserID,
                Token = user.Token,
            };
        }

        #region Internal

        internal string RenameFileName( int userid, string filename)
        {
            var rename = "_" + userid;
            return filename.Insert(filename.LastIndexOf('.'), rename);
        }
        internal string DecodeBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return string.Empty;
            var myByte = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(myByte);
        }
        internal byte[] StringBase64ToByteArray(string base64String)
        {
            //decode from string base64
            var myByte = Convert.FromBase64String(DecodeBase64(base64String));
            return myByte;
        }

        #endregion

        public string GetLinkAvatar(int userId)
        {
            return _accountRepository.GetLinkAvatar(userId);
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <history>
        /// 12/01/2016 PhatVT: Tạo mới
        /// </history>
        public LoginResponse Login(LoginRequest request)
        {
            var response = new LoginResponse();

            switch (request.OpenProviderId)
            {
                case OpenProviderIdEnum.Google:
                    // Gắn kết nối Google vào đây
                    // Link được lấy từ request.GoogleRequest.ConfirmLink
                    // Không hardcode trong đây
                    //   var urlTemp = "https://www.googleapis.com/plus/v1/people/me?access_token=" + accessTokenModel.AccessToken;
                    var url = request.GoogleRequest.ConfirmLink + request.GoogleRequest.AccessToken;
                    string html;
                    var sendRequest = NetworkCommon.SendGetRequest(url, out html);
                    JObject jObject = JObject.Parse(html);
                    response.Account.Email  = jObject.SelectToken("displayName").ToString();
                    response.Account.FullName=jObject.SelectToken("emails[0].value").ToString();
                    response.Account.UserId = int.Parse(jObject.SelectToken("id").ToString());
                    break;
                case OpenProviderIdEnum.Facebook:
                    // Gắn kết nối Facebook vào đây
                    var client = new FacebookClient(request.FacebookRequest.AccessToken);
                    dynamic result = client.Get("me", new { fields = "name,email" });
                    response.Account.Email =result.email;
                    response.Account.FullName = result.name;
                    response.Account.UserId = result.id;
                    break;
                default:
                    break;
            }
            return response;
        }


        public List<Out_Account_GetUsersOnline_Result> GetUsersOnline(int position, int pageSize)
        {
            return _accountRepository.GetUsersOnline(position, pageSize).ToList();
        }

        /// <summary>
        /// Lấy danh sách user online + cờ xác định là bạn
        /// </summary>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 17/3/2016 Create By TaiNM
        /// </history>
        public List<Out_Account_GetUsersOnline_V2_Result> GetUsersOnline_V2(int position, int pageSize, int userId)
        {
            return _accountRepository.GetUsersOnline_V2(0, pageSize, userId).ToList();

            // Phần dưới này dùng để gọi Redis
            //var lstData = _accountRedis.GetUserOnline(pageSize, userId, DateTime.Now).ToList();
            //if (!lstData.Any()) // || position == 1) // Khong can kiem tra trang dau tien - Duynd - 18/05/2016
            //{
            //    lstData = _accountRepository.GetUsersOnline_V2(0, pageSize, userId).ToList();
            //}

            //return lstData;
        }

        /// <summary>
        /// Lấy danh sách user online + cờ xác định là bạn
        /// </summary>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 19/05/2016 Create By Duynd
        /// </history>
        public List<Out_Account_GetUsersOnline_V2_Result> GetUsersOnline_V2(int position, int pageSize, int userId, DateTime dtmClearCacheRedis)
        {
            List<Out_Account_GetUsersOnline_V2_Result> listUserOnline = _accountRedis.GetUserOnline(pageSize, userId, dtmClearCacheRedis);
            if (!listUserOnline.Any())
            {
                listUserOnline = GetUsersOnline_V2(position, pageSize, userId);
                _accountRedis.SetUserOnline(pageSize,userId,dtmClearCacheRedis,listUserOnline);
            }
            return listUserOnline;
        }

        /// <summary>
        /// Lấy tên tài khoản theo userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 22/3/2016 Create By TaiNM
        /// </history>
        public string GetNameByUserId(int userId)
        {
            return _accountRepository.GetNameByUserId(userId);
        }

        /// <summary>
        /// Gọi Store API để kích hoặc Update gold user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="wsPacket"></param>
        /// <returns></returns>
        /// <history>
        /// 1/4/2016 Create By MinhT
        /// </history>
        public bool CallApiKickOrUpdateGold(int userId, int wsPacket)
        {
            try
            {
                var value = _accountRepository.CallApiKickOrUpdateGold(userId, wsPacket);
                //Logger.CommonLogger.DefaultLogger.Debug(string.Format("Gọi Store Kick/Update gold Bị lỗi, Giá trị trả về: {0}", value));
                return value == CallStoreResponse.ThanhCong.Value();
            }
            catch (Exception ex)
            {
                Logger.CommonLogger.DefaultLogger.Debug(string.Format("Gọi Store Kick/Update gold Bị lỗi, Giá trị trả về: {0}", ex));
                return false;
            }
         }

        /// <summary>
        /// Check user update phone
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated : 11/04/2016</para>
        public bool IsUpdatePhoneUser(int userId)
        {
            return _accountRepository.IsUpdatePhoneUser(userId);
        }

        /// <summary>
        /// lay thoi gian choi game trong ngay cua user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated : 13/04/2016</para>
        public int ATPPlayerRecord_GetTimePlayToday(int userId)
        {
            return _accountRepository.ATPPlayerRecord_GetTimePlayToday(userId);
        }

        /// <summary>
        /// Cap nhat gold cho user tren DBGame
        /// Created user: Duynd - 05/05/2016        
        /// </summary>
        //public int UpdateGoldUser(int intUserID, int intType, decimal decGold)
        //{
        //    return _accountRepository.UpdateGoldUser(intUserID, intType, decGold);
        //}

        public async Task<int> UpdateGoldUser(int intUserID, int intType, decimal decGold)
        {
            return _accountRepository.UpdateGoldUser(intUserID, intType, decGold);
        }
    }
}
