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
using static UralSibSite.Models.Offices.OfficeContext;
using UralSibSite.Models.Offices;
using UralSibSite.Models.Assesments;

namespace UralSibSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Error()
        {
            //  Graphics.Diagrams.
            return View();
        }
        public async Task<ActionResult> Departments()
        {
            //todo get request 
            await UpdateDb();
            ViewBag.ListOffices = OfficeContext.Offices;
            return View();
        }
        public async Task<ActionResult> DepartmentInfo(int Id)
        {
            await OfficeContext.UpdateDb();
            ViewBag.Office = Offices.Find(x => x.Id == Id);
            return View();
        } 
        public FileContentResult Diagram()
        {
            FileContentResult diagram = Diagrams.GetChart(SeriesChartType.Pie,null,300,700);
            return diagram;
        }
     
        public async Task<ActionResult> Assesments()
        {
            await AssesmentContext.UpdateDb();
            ViewBag.Assesments = AssesmentContext.Assesments;
            return View();
        }
    }
}