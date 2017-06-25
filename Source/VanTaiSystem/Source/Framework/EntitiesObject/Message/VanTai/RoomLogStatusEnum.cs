using System.ComponentModel;

namespace EntitiesObject.Message.KaraSys
{
    public enum RoomLogStatusEnum
    {
        [Description("Running")]
        Running = 1,

        [Description("Finish")]
        Finish = 2,

        [Description("Cancel")]
        Cancel = 3,
    }
}