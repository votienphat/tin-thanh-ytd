namespace BusinessObject.MembershipModule.Enums
{
    public enum MembershipCode
    {
        Failed = 0,
        Success = 1,
        SystemError = 9999,

        #region Login

        UnexistedAccount = 2,
        LockedAccount = 3,

        #endregion
    }
}