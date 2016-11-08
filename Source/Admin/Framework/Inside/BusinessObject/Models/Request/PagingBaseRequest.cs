namespace BusinessObject.Models.Request
{
    public class PagingBaseRequest
    {
        /// <summary>
        /// Default is 0
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// Default is 10
        /// </summary>
        public int PageLength { get; set; }

        public PagingBaseRequest()
        {
            StartIndex = 0;
            PageLength = 10;
        }
    }
}