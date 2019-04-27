using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UralSibSite.APIConnection;
namespace UralSibSite.Models.Assesments
{
    public class AssesmentContext
    {
        public static List<Assesment> Assesments;
        public static async Task UpdateDb()
        {
            Assesments = await ApiConnections.GetAllEntitiesAsync<Assesment>(ApiConnections.baseurlAssesments);
        }
    }
}