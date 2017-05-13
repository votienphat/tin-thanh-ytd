using System;

namespace Phystones.Models
{
    public class BaseResponse
    {
        public int Code { get; set; }

        public object Data { get; set; }
    }
}