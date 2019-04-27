using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using UralSibSite.Models;
namespace UralSibSite.APIConnection
{
    public static class ApiConnections
    {
        public static string baseurl = "http://aerothedeveloper.ru/api/office";
        public static async Task<List<Office>> GetAllOfficesAsync()
        {
            HttpConnector connector = new HttpConnector(baseurl);
            var request = connector.CreateRequest(baseurl);
            using (Stream stream = await connector.GetResponse(request))
            using (StreamReader reader = new StreamReader(stream))
            {

                string info = await reader.ReadToEndAsync();
                List<Office> offices = new List<Office>();
                
                
               
                offices = (List<Office>)Newtonsoft.Json.JsonConvert.DeserializeObject(info,typeof(List<Office>));
                return offices;
            }
             
        }
    }
}