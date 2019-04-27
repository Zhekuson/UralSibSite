using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using UralSibSite.Models.Offices;
using UralSibSite.Models.Assesments;
using UralSibSite.Models;

namespace UralSibSite.APIConnection
{
    public static class ApiConnections
    {
        public static string baseurlOffices = "http://aerothedeveloper.ru/api/office";
        public static string baseurlAssesments = "http://aerothedeveloper.ru/api/assesments";


        public static async Task<List<Entity>> GetAllEntitiesAsync<Entity>(string url)
        {
            HttpConnector connector = new HttpConnector(url);
            var request = connector.CreateRequest(url);
            using (Stream stream = await connector.GetResponse(request))
            using (StreamReader reader = new StreamReader(stream))
            {

                string info = await reader.ReadToEndAsync();
                List<Entity> entities = new List<Entity>();
                entities = (List<Entity>)Newtonsoft.Json.JsonConvert.DeserializeObject(info, typeof(List<Entity>));
                return entities;
            }
        }
    }
}