using System.ComponentModel;

namespace BusinessObject.Enums
{
    // Tao them nhieu enum cua ItemGameUser trong class nay    
    public enum ItemGameUserStatusEnum
    {
        [Description("Chưa sử dụng")]
        Unused = 0,

        [Description("Đã sử dụng")]
        Used = 1,        
    }

    //public enum ItemGameUserTypeEnum
    //{
    //}
}