using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static UralSibSite.APIConnection.ApiConnections;


namespace UralSibSite.Models.Coupons
{
    public static class CouponContext
    {
        public static List<Coupon> Coupons;

        public async static Task UpdateDb()
        {
            Coupons = await GetAllEntitiesAsync<Coupon>(APIConnection.ApiConnections.baseurlCoupons);
        }
    }
}