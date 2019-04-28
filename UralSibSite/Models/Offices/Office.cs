using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UralSibSite.Models.Offices
{
    public class Office
    {
       [JsonProperty("ID")]
        public int Id { get; set; }
       [JsonProperty("CompanyID")]
        public int CompanyId { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
       [JsonProperty("Address")]
        public string Address { get; set; }
       [JsonProperty("Country")]
        public string Country { get; set; }
       [JsonProperty("Coordinates")]
        public OfficeCoords Coordinates { get; set; }
       [JsonProperty("Website")]
        public string Website { get; set; }
       [JsonProperty("WorkHours")]
        public string WorkHours { get; set; }
       [JsonProperty("Phone")]
        public List<OfficePhone> Phone{get; set;}
    }
}