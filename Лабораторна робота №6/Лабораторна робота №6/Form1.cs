using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lava_CHMI_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            PlotSolution(0.02, chart1, "y(t)");
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlotSolution(0.02, chart1, "h");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PlotSolution(0.02 / 2, chart1, "h/2");
        }

        private void PlotSolution(double h, Chart chart, string seriesName)
        {
            double t = 1;
            double y = Math.Sqrt(2 - t * t); // Початкове значення y

            List<double> tValues = new List<double>();
            List<double> yValues = new List<double>();

            while (t <= 1.5)
            {
                tValues.Add(t);
                yValues.Add(y);

                // Виправлений метод Ейлера
                double y_pred = y + h * (-t / y);
                y += h * (-(t + h) / (y + y_pred)) / 2;

                t += h;
            }

            PlotGraph(chart, seriesName, tValues, yValues);
        }



        private void PlotGraph(Chart chart, string seriesName, List<double> xValues, List<double> yValues)
        {
            Series series = new Series(seriesName);
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 5; // встановлення товщини лінії

            for (int i = 0; i < xValues.Count; i++)
            {
                series.Points.AddXY(xValues[i], yValues[i]);
            }

            chart.Series.Add(series);
        }
    }
}
