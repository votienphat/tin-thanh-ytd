using System;

namespace KaraSys.Models
{
    public class BaseResponse
    {
        public int Code { get; set; }

        public object Data { get; set; }
    }
}