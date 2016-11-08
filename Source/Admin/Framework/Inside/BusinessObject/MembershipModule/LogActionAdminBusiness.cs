using System;
using System.Collections.Generic;
using BusinessObject.MembershipModule.Contract;
using BusinessObject.MembershipModule.Enums;
using BusinessObject.MembershipModule.Models.Request;
using DataAccess.Contract.Membership;
using EntitiesObject.Entities.MetroMembershipEntities;
using MyUtility.Extensions;
using Newtonsoft.Json;

namespace BusinessObject.MembershipModule
{
    public class LogActionAdminBusiness : ILogActionAdminBusiness
    {
        #region Viriables

        private readonly IPageFunctionRepository _pageFunctionRepo;
        private readonly ILogActionAdminRepository _logActionAdminRepo;
        private readonly IActionAdminRepository _actionAdminRepository;
        #endregion

        #region Constructor

        public LogActionAdminBusiness(IPageFunctionRepository pageFunctionRepo, ILogActionAdminRepository logActionAdminRepo,  IActionAdminRepository actionAdminRepository)
        {
            _pageFunctionRepo = pageFunctionRepo;
            _logActionAdminRepo = logActionAdminRepo;
            _actionAdminRepository = actionAdminRepository;
        }

        #endregion

        #region Method

        /// <summary>
        /// Ghi log thao tac cua admin
        /// </summary>
        /// <history>
        /// 2016-01-20 Create By TrungTT
        /// </history>
        public LogActionAdminStatus InsertLog(LogActionAdminRequestModel model)
        {
            if (model == null) return LogActionAdminStatus.Fail;

            var pageInfo = _pageFunctionRepo.GetPageFunctionById(model.ActionId.Value());
            if (pageInfo == null) return LogActionAdminStatus.Fail;

            var actionName = pageInfo.Name;
            var actionId = pageInfo.Id;

            var beforeConfig = (model.Config.GetType() != typeof(string))
                ? JsonConvert.SerializeObject(model.Config)
                : model.Config.ToString();

            try
            {
                var ret = _logActionAdminRepo
                    .InsertLog(model.AdminId, actionId, actionName, model.ObjectId, model.Description, beforeConfig,
                        model.IpRequest, model.UserAgent);

                /*
                    DaoMongoFactory.MongoLogActionAdmin.SaveLog(new LogActionAdminMongo()
                    {
                        ActionId = actionId,
                        ActionName = actionName,
                        AdminId = model.AdminId,
                        CreateDate = DateTime.Now,
                        Description = model.Description,
                        UserAgent = model.UserAgent,
                        IpAddress = model.IpRequest,
                        ObjectId = model.ObjectId,
                        OldValue = oldValue,
                        UserId = model.UserId
                    });
                    */

                return ret > 0 ? LogActionAdminStatus.Success : LogActionAdminStatus.Fail;
            }
            catch (Exception ex)
            {
                Logger.CommonLogger.DefaultLogger.Error("InsertLog", ex);
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// Lấy danh sách adtion của Admin
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 17-2-2016 Create By MinhT
        /// </history>
        public IEnumerable<Ins_ActionAdmin_GetAll_Result> GetListActionAdmin()
        {
            return _actionAdminRepository.GetAll();
        }

        /// <summary>
        /// Lấy danh sách ghi log action của Admin by AdminName
        /// </summary>
        /// <param name="adminName"></param>
        /// <param name="actionId"></param>
        /// <param name="rowStart"></param>
        /// <param name="rowEnd"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        /// <history>
        /// 17-2-2016 Create By MinhT
        /// </history>
        public IEnumerable<Ins_LogActionAdmin_GetByAdminName_Result> GetListActionAdminByName(string adminName, int actionId, int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _logActionAdminRepo.GetListActionAdminByName(adminName, actionId, rowStart, rowEnd, orderBy, isDescending, out totalRow);
        }

    }
}
