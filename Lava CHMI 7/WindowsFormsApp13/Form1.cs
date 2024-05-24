using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double AdamsBashforthMoulton(double t, double y, double h, Func<double, double, double> f)
        {
            // Predictor (Adams-Bashforth 2nd order)
            double y_pred = y + h * f(t, y);

            // Corrector (Adams-Moulton 2nd order)
            double y_corr = y + h * (f(t, y) + f(t + h, y_pred)) / 2;

            return y_corr;
        }

        // Exact solution
        private double ExactSolution(double t)
        {
            return Math.Sqrt(2 - Math.Pow(t, 2));
        }

        // Derivative of y
        private double Derivative(double t, double y)
        {
            return -t / y;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Initial conditions
            double t0 = 1;
            double y0 = 1;

            // Step size
            double h = 0.1;

            // Series for solution without control parameter
            Series seriesA = new Series();
            seriesA.ChartType = SeriesChartType.Line;
            seriesA.BorderWidth = 2;
            seriesA.LegendText = "seriesA";
            for (double t = t0; t <= 1.5; t += h)
            {
                seriesA.Points.AddXY(t, y0);
                y0 = AdamsBashforthMoulton(t, y0, h, Derivative);
            }

            // Series for solution with control parameter
            double controlParameterH = 0.8;
            double y1 = 1;
            Series seriesB = new Series();
            seriesB.ChartType = SeriesChartType.Line;
            seriesB.BorderWidth = 2;
            seriesB.LegendText = "seriesB";
            for (double t = t0; t <= 1.5; t += controlParameterH)
            {
                seriesB.Points.AddXY(t, y1);
                y1 = AdamsBashforthMoulton(t, y1, controlParameterH, Derivative);
            }

            // Series for exact solution
            Series exactSeries = new Series();
            exactSeries.ChartType = SeriesChartType.Line;
            exactSeries.BorderWidth = 2;
            exactSeries.LegendText = "exactSeries";
            for (double t = t0; t <= 1.5; t += 0.01)
            {
                exactSeries.Points.AddXY(t, ExactSolution(t));
            }

            // Adding series to chart
            chart1.Series.Clear();
            chart1.Series.Add(seriesA);
            chart1.Series.Add(seriesB);
            chart1.Series.Add(exactSeries);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Initial conditions
            double t0 = 1;
            double y0 = 1;

            // Step size (double the control parameter)
            double doubledH = 0.2;

            // Series for solution without control parameter with doubled step size
            Series seriesC = new Series();
            seriesC.ChartType = SeriesChartType.Line;
            seriesC.BorderWidth = 2;
            seriesC.LegendText = "seriesС";
            for (double t = t0; t <= 1.5; t += doubledH)
            {
                seriesC.Points.AddXY(t, y0);
                y0 = AdamsBashforthMoulton(t, y0, doubledH, Derivative);
            }

            // Series for solution with control parameter with doubled step size
            double controlParameterDoubledH = 0.1;
            double y2 = 1;
            Series seriesD = new Series();
            seriesD.ChartType = SeriesChartType.Line;
            seriesD.BorderWidth = 2;
            seriesD.LegendText = "seriesD";
            for (double t = t0; t <= 1.5; t += controlParameterDoubledH)
            {
                seriesD.Points.AddXY(t, y2);
                y2 = AdamsBashforthMoulton(t, y2, controlParameterDoubledH, Derivative);
            }

            // Series for exact solution
            Series exactSeries = new Series();
            exactSeries.ChartType = SeriesChartType.Line;
            exactSeries.BorderWidth = 2;
            exactSeries.LegendText = "exactSeries";
            for (double t = t0; t <= 1.5; t += 0.01)
            {
                exactSeries.Points.AddXY(t, ExactSolution(t));
            }

            // Adding series to chart
            chart2.Series.Clear();
            chart2.Series.Add(seriesC);
            chart2.Series.Add(seriesD);
            chart2.Series.Add(exactSeries);
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
