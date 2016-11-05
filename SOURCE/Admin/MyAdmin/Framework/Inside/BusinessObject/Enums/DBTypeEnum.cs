using System.ComponentModel;

namespace BusinessObject.Enums
{
    public enum DBTypenEnum
    {
        [Description("Từ Redis")]
        Redis = 1,

        [Description("Từ Database")]
        Database = 0,
    }
}