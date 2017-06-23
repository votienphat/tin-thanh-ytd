using System.ComponentModel;

namespace EntitiesObject.Message.KaraSys
{
    public enum RoomStatusEnum
    {
        [Description("Incoming")]
        Incoming = 0,

        [Description("Available")]
        Available = 1,

        [Description("Running")]
        Running = 2,

        [Description("Maintenance")]
        Maintenance = 3,
    }
}