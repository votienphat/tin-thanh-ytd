<<<<<<< HEAD
﻿using DataAccess.Repositories.Infrastructure;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.Web;
>>>>>>> 7c95c30dc45d72cd825a1900048f07bb52b4624c
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using EntitiesObject.Entities.WebEntities;
<<<<<<< HEAD

namespace DataAccess.Repositories.Membership
=======
using System.Data.Entity.Core.Objects;

namespace DataAccess.Repositories.Web
>>>>>>> 7c95c30dc45d72cd825a1900048f07bb52b4624c
{
    public class SloganRepository : DaoRepository<WebEntities, Slogan>, ISloganRepository
    {
        public List<Out_Slogan_Get_Result> SloganGet()
        {
            return Uow.Context.Out_Slogan_Get().ToList();
        }
<<<<<<< HEAD
=======

        public Out_Slogan_GetById_Result Get(int id)
        {
            return Uow.Context.Out_Slogan_GetById(id).FirstOrDefault();
        }

        public int SaveDataSlogan(int Id, string Title, string Author, string ContentBody, string Language, bool IsActive)
        {
            return Uow.Context.Out_Slogan_Save(Id, Title, Author, ContentBody, Language, IsActive);
        }

        public List<Out_Slogan_GetListData_Result> ListDataSlogan(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Slogan_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
>>>>>>> 7c95c30dc45d72cd825a1900048f07bb52b4624c
    }
}