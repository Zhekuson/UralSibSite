using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace UralSibSite.Graphics
{
    public class Diagrams
    {
        public static Color ShadowColor = Color.FromArgb(32, 0, 0, 0);
        public static Font DefaultFont = new Font("Helvetica", 14F, FontStyle.Bold);
        public static Color ForeColor = Color.FromArgb(26, 59, 105);
        public static FileContentResult GetChart()
        { 
            var dates = new List<Tuple<int, string>>(
             new[]
                    {
                           new Tuple<int, string> (65, "00:00"),
                           new Tuple<int, string> (69, "02:00"),
                           new Tuple<int, string> (90, "04:00"),
                           new Tuple<int, string> (81, "06:00"),
                           new Tuple<int, string> (81, "08:00"),
                           new Tuple<int, string> (55, "10:00"),
                           new Tuple<int, string> (40, "12:00"),
                           new Tuple<int, string> (40, "14:00"),
                           new Tuple<int, string> (40, "16:00"),
                           new Tuple<int, string> (19, "18:00"),
                           new Tuple<int, string> (40, "20:00"),
                           new Tuple<int, string> (40, "22:00"),
                           new Tuple<int, string> (40, "24:00"),
                    }
             );

            var chart = new Chart();

            chart.Width = 800;
            chart.Height = 350;

            chart.BackColor = Color.FromArgb(0, 0, 0);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;

            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            chart.Titles.Add(CreateTitle("Активность"));//todo fix title
            chart.Legends.Add(CreateLegend("- количество принятых клиентов"));
            
            chart.Series.Add(CreateSeries(dates, SeriesChartType.Line, Color.Green));
            chart.ChartAreas.Add(CreateChartArea());

            var ms = new MemoryStream();
            chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }

        public static Title CreateTitle(string titleString)
        {
            Title title = new Title()
            {
                Text = titleString,
                ShadowColor = ShadowColor,
                Font = DefaultFont,
                ShadowOffset = 3,               
                ForeColor = ForeColor
            };
            return title;
        }

        public static Series CreateSeries(IList<Tuple<int, string>> results,
       SeriesChartType chartType,
       Color color)
        {
            var seriesDetail = new Series();
            seriesDetail.Name = "Result Chart";
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = color;
            seriesDetail.ChartType = chartType;
            seriesDetail.BorderWidth = 2;
            seriesDetail["DrawingStyle"] = "Cylinder";
            seriesDetail["PieDrawingStyle"] = "SoftEdge";
            DataPoint point;

            foreach (var result in results)
            {
                point = new DataPoint();
                point.AxisLabel = result.Item2;
                point.YValues = new double[] { result.Item1 };
                seriesDetail.Points.Add(point);
            }
            seriesDetail.ChartArea = "Result Chart";

            return seriesDetail;
        }
        public static Legend CreateLegend(string chartName)
        {
            var legend = new Legend()
            {

                Name = chartName,
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                BackColor = Color.Transparent,
                Font = new Font(new FontFamily("Helvetica"), 9),
                LegendStyle = LegendStyle.Row
            };
            return legend;
        }
        public static ChartArea CreateChartArea()
        {
            var chartArea = new ChartArea();
            chartArea.Name = "Result Chart";
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            return chartArea;
        }
       public static Series GetSeriesPieChart()
       {
            Series series = new Series();
            
            return series;

       }
        #region custom
        public static ChartArea GetChartAreaPieChart(string name)
       {
            ChartArea area = new ChartArea();
            area.Name = name;
            area.Position = new ElementPosition(0, 0, 100, 100);
            return area;
       }
       public static FileContentResult GetPieChart(List<Tuple<double,string>> data, string title, string chartLegendName, string chartAreaName, int width, int height)
       {
            Chart chart = new Chart();
            chart.Titles.Add(CreateTitle(title));

            chart.Width = width;
            chart.Height = height;

            chart.Legends.Add(CreateLegend(chartLegendName));
            chart.Series.Add(GetSeriesPieChart());

            chart.ChartAreas.Add(GetChartAreaPieChart(chartAreaName));

            var ms = new MemoryStream();
            chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        #endregion
    }
}