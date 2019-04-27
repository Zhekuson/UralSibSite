using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UralSibSite.Models.Coupons
{
    public enum Status
    {
        Active,
        Done
    } 
    public class Coupon
    {
        [JsonProperty("ID")]
        public int Id { get; set; }
        [JsonProperty("UserID")]
        public int UserId { get; set; }
        [JsonProperty("OfficeID")]
        public int OfficeId { get; set; }
        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; set; }
        [JsonProperty("VisitTime")]
        public DateTime VisitDate { get; set; }
        [JsonProperty("OfficeAddress")]
        public string OfficeAddress { get; set; }
        [JsonProperty("ServiceType")]
        public string ServiceType { get; set; }
        [JsonProperty("CouponStatus")]
        public Status CouponStatus { get; set; }

    }
}