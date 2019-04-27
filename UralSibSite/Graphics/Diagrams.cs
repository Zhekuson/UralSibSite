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
                           new Tuple<int, string> (65, "January"),
                           new Tuple<int, string> (69, "February"),
                           new Tuple<int, string> (90, "March"),
                           new Tuple<int, string> (81, "April"),
                           new Tuple<int, string> (81, "May"),
                           new Tuple<int, string> (55, "June"),
                           new Tuple<int, string> (40, "July")
                    }
             );

            var chart = new Chart();

            chart.Width = 700;
            chart.Height = 300;

            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;

            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            chart.Titles.Add(CreateTitle("FFF"));//todo fix title
            chart.Legends.Add(CreateLegend());
            chart.Series.Add(CreateSeries(dates, SeriesChartType.Pie, Color.Red));
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
        public static Legend CreateLegend()
        {
            var legend = new Legend()
            {

                Name = "Result Chart",
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
    }
}