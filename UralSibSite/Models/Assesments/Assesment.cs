using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UralSibSite.Models.Assesments
{
    public class Assesment
    {
        [JsonProperty("ID")]
        public int Id { get; set; }
        [JsonProperty("UserID")]
        public int UserId { get; set; }
        [JsonProperty("CouponID")]
        public int CouponId { get; set; }
        [JsonProperty("OfficeID")]
        public int OfficeId { get; set; }
        [JsonProperty("Mark")]
        public int Mark { get; set; }
        [JsonProperty("Comment")]
        public string Comment { get; set; }

    }
}