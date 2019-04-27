using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UralSibSite.Models
{
    
    public class OfficePhone
    {
      //  [JsonProperty("Number")]
        public string Number { get; set; }
       // [JsonProperty("Type")]
        public string Type { get; set; }
       // [JsonProperty("Info")]
        public string Info { get; set; }
    }
}