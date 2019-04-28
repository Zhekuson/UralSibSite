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
using System.Web.UI.WebControls;

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
                await CouponContext.UpdateDb();
                await AssesmentContext.UpdateDb();
                ViewBag.OfficeId = Id;
                ViewBag.office = OfficeContext.Offices.Find(x => x.CompanyId == Id);
                List<Coupon> localCoupons = CouponContext.Coupons.FindAll((x) => x.OfficeId == Id);
                ViewBag.LocalCoupons = localCoupons;

                
                ViewBag.Diagram2 = 0;//todo
                ViewBag.Diagram3 = 0;//todo
                //todo rating beautiful
            }
            catch (HttpException e)
            {
                //todo error logic
            }
            return View();
        } 
        [ActionName("CreateGraph")]
        public FileResult CreateGraph()
        {
            return Diagrams.GetChart();
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

        [ActionName("PieChart")]
        public FileContentResult PieChart()
        {
            // Форматировать диаграмму
            Chart Chart1 = new Chart();
            var dates = new List<Tuple<double, string>>(
                new[]
                {
                    new Tuple<double, string>(86,"86%"),
                    new Tuple<double, string>(14,"14%")
                }
            );
            Chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart1.BorderlineColor = Color.Gray;
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // Форматировать область диаграммы
            Chart1.ChartAreas[0].BackColor = Color.Wheat;

            Chart1.Series.Add(new Series("Default")
            {
                ChartType = SeriesChartType.Pie
            });
            
            var ms = new MemoryStream();
            Chart1.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }

        public class MyObjectDataSource
        {

            public class DataItem
            {
                public string Name { get; set; }
                public double Popularity { get; set; }
            }

            public DataItem[] GetData()
            {
                return new DataItem[] {
            new DataItem() {Name = "Негативные отзывы", Popularity = 14 },
            new DataItem() {Name = "Положительные отзывы", Popularity = 86},
        };
            }
        }
        [ActionName("BarChart")]
        public ChartResult BarChart(int id)
        {
            Chart chart = new Chart();
            chart.Width = 480;
            chart.Height = 300;
            chart.RenderType = RenderType.ImageTag;
            chart.Palette = ChartColorPalette.Grayscale;
           



            chart.BorderlineWidth = 1;
            chart.BorderlineColor = Color.Gray;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;

            chart.ChartAreas.Add("Default");

            chart.Legends.Add("Legend1");

            chart.Series.Add("Clients");


            List<int> data = new List<int>();
            data.Add(100);
            data.Add(200);
            data.Add(150);
            data.Add(600);
            data.Add(497);

            //Series 1 
            foreach (int value in data)
            {
                chart.Series["Clients"].Points.AddY(value);
            }



            return new ChartResult(chart, ChartImageFormat.Png);
        }

    }
}
public class ChartResult : FileResult
{
    private const int _bufferSize = 0x1000;

    public ChartResult(Chart chart, ChartImageFormat imageFormat)
      : base(MapImageFormatToMimeType(imageFormat))
    {
        if (null == chart) throw new ArgumentNullException("chart");

        this.Chart = chart;
        this.ImageFormat = imageFormat;
    }

    public ChartResult(Chart chart)
      : this(chart, ChartImageFormat.Png)
    {
    }

    public Chart Chart
    {
        get;
        private set;
    }

    public ChartImageFormat ImageFormat
    {
        get;
        private set;
    }

    private static string MapImageFormatToMimeType(ChartImageFormat imageFormat)
    {
        switch (imageFormat)
        {
            case ChartImageFormat.Png:
                return "image/png";

            case ChartImageFormat.Jpeg:
                return "image/jpeg";

            case ChartImageFormat.Gif:
                return "image/gif";

            case ChartImageFormat.Bmp:
                return "image/bmp";

            case ChartImageFormat.Tiff:
                return "image/tiff";

            // TODO: MIME types for EMF? 
            // case ChartImageFormat.Emf: 
            // case ChartImageFormat.EmfPlus: 
            // case ChartImageFormat.EmfDual: 

            default:
                throw new ArgumentException("Unsupported format");
        }
    }

    protected override void WriteFile(HttpResponseBase response)
    {
        // NB: Can't save directly to the output stream, 
        // as most image formats require a seekable stream. 

        using (var imageStream = new MemoryStream())
        {
            this.Chart.SaveImage(imageStream, this.ImageFormat);
            imageStream.WriteTo(response.OutputStream);
        }
    }
}