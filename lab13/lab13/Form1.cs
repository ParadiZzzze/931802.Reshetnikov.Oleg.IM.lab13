using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public int[] input()
        {
            int[] ab = new int[2];
            ab[0] = Convert.ToInt32(textBox1.Text);
            ab[1] = Convert.ToInt32(textBox2.Text);
            return ab;
        }
        public double calc_ExpectedValue(double[] values, int a)
        {
            double mean = 0;
            for (int i = 0; i<values.Length; i++)
            {
                mean += (i + a) * values[i];
            }
            return mean;
        }
        public double calc_Dispersion(double[] values, int a)
        {
            double mean = calc_ExpectedValue(values, a);
            double dispersion = 0;
            for (int i = 0; i < values.Length; i++)
            {
                dispersion += values[i] * ((i + a) - mean) * ((i + a) - mean);
            }
            return dispersion;
        }
        public double calc_Chi(double[] Statistic, int m, int n)    
        {
            double Chi = 0;
            for (int i = 0; i < Statistic.Length; i++)
            {
                if (Statistic[i]!=0) Chi += (Statistic[i] * Statistic[i])/(m*1/n);
            }
            return (Chi-m);
        }
        public double[] statistick_count(int[] ab, int number_experiment)
        {
            int n = ab[1] - ab[0];
            double[] Statistic = new double[n+1];
            Random rnd = new Random();
            for (int i = 0; i < number_experiment; i++)
            {
                double A = rnd.NextDouble();
                int x = (int)(A * (n + 1));
                Statistic[x]++;
            }
            return Statistic;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] ab = input();
            int experiment_number;            
            experiment_number = Convert.ToInt32(textBox6.Text);
            
            int n = ab[1] - ab[0];
            double[] values = statistick_count(ab, experiment_number);
            double[] Statistic = new double[n+1];
            Array.Copy(values, Statistic, n+1);
            for (int i = 0; i <= n; i++) values[i] /= experiment_number;

            double mean = calc_ExpectedValue(values,ab[0]);
            double dispersion = calc_Dispersion(values, ab[0]);
            label16.Text = Convert.ToString(mean);
            label17.Text = Convert.ToString(dispersion);
         
            double Chi = calc_Chi(Statistic, experiment_number, n+1);
            label19.Text = Chi.ToString("N5");

            chart1.Series.Clear();
            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            series.Name = "series1";
            chart1.Series.Add(series);
            for (int i = 0; i <= n; i++)
            {
                chart1.Series["series1"].Points.AddXY(i + ab[0], values[i]);
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
