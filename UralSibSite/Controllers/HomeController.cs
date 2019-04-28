using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UralSibSite.Graphics;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
//remove later
using UralSibSite.Models;
using UralSibSite.Models.Offices;
using UralSibSite.Models.Assesments;
using UralSibSite.Models.Coupons;

namespace UralSibSite.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Main()
        {
            await OfficeContext.UpdateDb();
            await AssesmentContext.UpdateDb();
            await CouponContext.UpdateDb();
            List<Coupon> todayCoupons = CouponContext.Coupons.FindAll((x) => {
                return (x.CouponStatus == Status.Done && x.VisitDate < DateTime.Now && x.VisitDate > DateTime.Today);
                });
            int yurCount = 0 , phizCount = 0;
           todayCoupons.ForEach(
                (x) => {
                    if (x.ServiceType == "юр") yurCount++;
                    if (x.ServiceType == "физ") phizCount++;
                       }
                );
            ViewBag.phiz = phizCount;
            ViewBag.yur = yurCount;
            ViewBag.sum = phizCount + yurCount;
           
            return View();
        }

        public ActionResult Error()
        {
            //  Graphics.Diagrams.
            return View();
        }
        public async Task<ActionResult> Departments()
        {
            try
            {
                //todo get request 
                await OfficeContext.UpdateDb();
                ViewBag.ListOffices = OfficeContext.Offices;
                
            }
            catch (HttpException e)
            {
                //todo error logic
            }
            return View();
        }
    
        public async Task<ActionResult> DepartmentInfo(int Id)
        {

            try
            {
                await OfficeContext.UpdateDb();
                ViewBag.OfficeId = Id;
                ViewBag.office = OfficeContext.Offices.Find(x => x.CompanyId == Id);
            
            }
            catch (HttpException e)
            {
                //todo error logic
            }
            return View();
        } 
        public FileContentResult Diagram()
        {
            FileContentResult diagram = Diagrams.GetChart(SeriesChartType.Pie,null,300,700);
            return diagram;
        }

        [HttpGet]
        public async Task<ActionResult> Assesments()
        {
            try
            {
                await AssesmentContext.UpdateDb();
                ViewBag.Assesments = AssesmentContext.Assesments;
              
            }
            catch (HttpException e)
            {
                //todo error logic
            }
            return View();
        }
        
       [HttpPost]
        public async Task<ActionResult> AssesmentsInfo(int UserId)
        {
            try
            { 
                await AssesmentContext.UpdateDb();
                ViewBag.Assesments = AssesmentContext.Assesments;
                ViewBag.Assesments = AssesmentContext.Assesments.FindAll((x) => x.UserId == UserId);
                ViewBag.USID = UserId;
            }
            catch (HttpException e)
            {
                //todo error logic
            }
            return View();
        }
        public FileContentResult MakePieChart()
        {
            List<Tuple<double, string>> data = new List<Tuple<double, string>>() { Tuple.Create(12.3, "cesdca"), Tuple.Create(12.3, "cecea") };
            return Diagrams.GetPieChart(data, "TTTT", "CCCC", "AAAA", 500, 400);
       }
   

    }
}