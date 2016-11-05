using System;

namespace MyAdmin.Models.Account
{
    public class InfoUserClient
    {
        public int UserId { get; set; }
        public string TokenId { get; set; }
        public DateTime TokenExp { get; set; }
    }
}