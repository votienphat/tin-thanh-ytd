namespace BusinessObject.MembershipModule.Enums
{
    public enum MemberShipEnum
    {
    }
    public enum ResultPageFunctionEnum
    {
        Success = 1
        ,
        Fail = 0
            ,
        NameExists = -1
            ,
        SystemErorr = -1001
        , SqlErorr = -2
    }
    public enum ResultMemberPermissionEnum
    {
        Success = 1
        ,
        Fail = 0
            ,
        NickNameExists = -1
            ,
        SystemErorr = -1001
        ,
        SqlErorr = -2
            , EmailExists = -3
    }
}