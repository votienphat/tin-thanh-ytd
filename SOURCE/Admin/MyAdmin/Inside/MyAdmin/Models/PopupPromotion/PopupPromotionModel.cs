using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyAdmin.Models.PopupPromotion
{
    public class PopupPromotionModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Chưa nhập tiêu đề.")]
        [MaxLength(1000,ErrorMessage = "Tối đa 200 ký tự.")]
        public string Link { get; set; }
        public string Target { get; set; }
        [Required(ErrorMessage = "Chưa nhập link event.")]
        [MaxLength(200, ErrorMessage = "Tối đa 200 ký tự.")]
        public string Title { get; set; }

        public string PublishDate { get; set; }
        public string UserList { get; set; }
        public string AssociationList { get; set; }

        /// <summary>
        /// Sap xep thong bao facebook
        /// </summary>
        public int OrderIndex { get; set; }

    }

    public enum LuckyEveryDayEnum
    {
        [Description("Đã nhận")]
        Nhan = 1,

        [Description("Chưa nhận")]
        ChuaNhan = 0
    }
}