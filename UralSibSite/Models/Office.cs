using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UralSibSite.Models
{
    public class Office
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("companyId")]
        public int CompanyId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Adress { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("coords")]
        public OfficeCoords Coordinates { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
        [JsonProperty("workHours")]
        public string WorkHours { get; set; }
        [JsonProperty("phone")]
        public List<OfficePhone> Phone{get; set;}
    }
}