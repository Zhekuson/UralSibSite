using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UralSibSite.Models
{
    public class OfficeCoords
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}