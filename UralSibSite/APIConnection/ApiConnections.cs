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
using UralSibSite.Models.Coupons;
namespace UralSibSite.APIConnection
{
    public static class ApiConnections
    {
        public static string baseurlOffices = "https://aerothedeveloper.ru/api/office";
        public static string baseurlAssesments = "https://aerothedeveloper.ru/api/assesments";
        public static string baseurlCoupons = "https://aerothedeveloper.ru/api/coupons";

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
        public static async Task<List<Coupon>> GetCouponByUserIdAsync(int UserId)
        {

            HttpConnector connector = new HttpConnector(baseurlCoupons + $"?userId={UserId}");
            var request = connector.CreateRequest(baseurlCoupons + $"?userId={UserId}");
            using (Stream stream = await connector.GetResponse(request))
            using (StreamReader reader = new StreamReader(stream))
            {

                string info = await reader.ReadToEndAsync();
                List<Coupon> entities = new List<Coupon>();
                entities = (List<Coupon>)Newtonsoft.Json.JsonConvert.DeserializeObject(info, typeof(List<Coupon>));
                return entities;
            }
        }

    }
}