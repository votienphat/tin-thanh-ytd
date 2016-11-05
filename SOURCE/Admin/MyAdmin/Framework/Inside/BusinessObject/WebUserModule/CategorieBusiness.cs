using System;
using System.Collections.Generic;
using BusinessObject.MembershipModule.Enums;
using BusinessObject.MembershipModule.Models.Request;
using BusinessObject.WebUserModule.Contract;
using DataAccess.Contract.Membership;
using DataAccess.Contract.WebUser;
using EntitiesObject.Entities.MetroUserEntities;
using MyUtility.Extensions;
using Newtonsoft.Json;

namespace BusinessObject.WebUserModule
{
    public class CategorieBusiness : ICategorieBusiness
    {
        #region Viriables

        private readonly ICategorieRepository _categorieRepo;
        #endregion

        #region Constructor

        public CategorieBusiness( ICategorieRepository categorieRepo)
        {
            _categorieRepo = categorieRepo;
        }

        #endregion
        public IEnumerable<Out_Categories_Get_Result> GetListCategorie()
        {
            return _categorieRepo.GetListCategorie();
        }
    }
}
