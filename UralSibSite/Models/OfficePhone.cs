using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UralSibSite.Models
{
    public class OfficePhone
    {
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("info")]
        public string Info { get; set; }
    }
}