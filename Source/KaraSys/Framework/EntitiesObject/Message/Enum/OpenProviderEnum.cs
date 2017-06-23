using System.ComponentModel;

namespace EntitiesObject.Message.Enum
{
    public enum OpenProviderEnum
    {
        /// <summary>
        /// Định nghĩa OpenProvider Id
        /// <para>Author: PhatVT</para>
        /// <para>Create Date: 23/12/2014</para>
        /// </summary>
        [Description("Web")]
        Web = 0,

        [Description("Yahoo")]
        Yahoo = 1,

        [Description("Google")]
        Google = 2,

        [Description("Facebook")]
        Facebook = 3,
    }
}