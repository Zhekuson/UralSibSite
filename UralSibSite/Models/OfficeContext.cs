﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UralSibSite.APIConnection;

namespace UralSibSite.Models
{
    public static class OfficeContext
    {
        public static List<Office> Offices;

        public async static Task UpdateDb()
        {
           Offices = await APIConnection.ApiConnections.GetAllOfficesAsync();
        }
        
    }
}