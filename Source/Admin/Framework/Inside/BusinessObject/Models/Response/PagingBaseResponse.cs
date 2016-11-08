namespace BusinessObject.Models.Response
{
    public class PagingBaseResponse
    {
        public bool HasNextPage { get; set; }

        public int TotalRows { get; set; }
    }

    /// <summary>
    /// <para>Author:Huyht</para>
    /// <para>DateCreated: 14/10/2015</para>
    /// <para>Định nghĩa nội dung trả về trạng thái update </para>
    /// </summary>
    public class StatusModel
    {
        public UpdateStatus Status { get; set; }
    }

    public enum UpdateStatus
    {
        ThanhCong = 1,
        ThatBai = 0
    }
}