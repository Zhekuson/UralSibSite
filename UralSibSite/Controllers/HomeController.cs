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
using Excel = Microsoft.Office.Interop.Excel;
using UralSibSite.Models;

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

            List<Office> offices = await APIConnection.ApiConnections.GetAllOfficesAsync();
            ViewBag.ListOffices = offices;
            return View();
        }
        public ActionResult DepartmentInfo(int Id)
        {
            
            
            return View();
        } 
        public FileContentResult Diagram()
        {
            FileContentResult diagram = Diagrams.GetChart(SeriesChartType.Pie,null,300,700);
            return diagram;
        }
     
    }
}