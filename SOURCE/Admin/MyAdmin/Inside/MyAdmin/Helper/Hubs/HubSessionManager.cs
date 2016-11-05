using Microsoft.AspNet.SignalR.Hubs;

namespace MyAdmin.Helper.Hubs
{
    public class HubSessionManager
    {
        #region Private Variables


        #endregion

        #region Public Variables

        /// <summary>
        /// Thông tin user
        /// </summary>
        public static int GetAdminId(HubCallerContext context)
        {
            int result = 0;

            if (context != null && context.User != null)
            {
                var identities = context.User.Identity.Name.Split('|');
                int.TryParse(identities[0], out result);
            }

            return result;
        }

        #endregion
    }
}