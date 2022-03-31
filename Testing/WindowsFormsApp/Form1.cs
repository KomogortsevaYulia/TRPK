using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private int vertices = 5;
        private double pix = 37.936;
        int _oldWidth, _oldHeight;
        int _oldTrackBar;
        float proportion = 1f; 

        public Form1()
        {
            InitializeComponent();

            double radius = (double)numericUpDown1.Value * pix;
            DrawPolygon_Ellipse(radius);
            _oldTrackBar = trackBar1.Value;
        }
        private void DrawPolygon_Ellipse(double radius)
        {
            int angle = 18;
            
            Point center = new Point(picCanvas.Width / 2, picCanvas.Height / 2);

            Point[] verticies = CalculateVertices(radius, angle, center);
            
            Bitmap polygon = new Bitmap(picCanvas.ClientSize.Width, picCanvas.ClientSize.Height);

            using (Graphics g = Graphics.FromImage(polygon))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                
                 HatchBrush hBrush = new HatchBrush(
                   HatchStyle.BackwardDiagonal,
                   Color.Red,
                   Color.White);
                
                g.DrawEllipse(Pens.Black, center.X - (int)radius, center.Y - (int)radius, 2 * (int)radius, 2 * (int)radius);
                g.FillEllipse(hBrush, center.X-(int)radius, center.Y- (int)radius, 2 * (int)radius, 2 * (int)radius);
                
                g.DrawPolygon(Pens.Black, verticies);

                SolidBrush brush = new SolidBrush(Color.White);
                g.FillPolygon(brush, verticies);

                g.DrawEllipse(Pens.Blue, center.X, center.Y-50, 3, 3);
            }

            picCanvas.Image = polygon;

            label4.Text="S="+ AreaCalculation((double)numericUpDown1.Value).ToString();
        }
        
        private Point[] CalculateVertices(double radius, int startingAngle, Point center)
        {
            List<Point> points = new List<Point>();
            float step = 360.0f / vertices;
            float angle = startingAngle; 
            for (double i = startingAngle; i < startingAngle + 360.0; i += step) //go in a circle
            {
                Point xy = new Point();
                double radians = angle * Math.PI / 180.0;
                xy.X = (int)(Math.Cos(radians) * radius + center.X);
                xy.Y = (int)(Math.Sin(-radians) * radius + center.Y);
                points.Add(xy);
                angle += step;
            }
            return points.ToArray();
        }

        private double AreaCalculation(double radius) {
            return Math.Round(AreaCalculationEllipse(radius) - AreaCalculationPolygon(radius),3);
        }

        private double AreaCalculationEllipse(double radius)
        {
            return Math.PI * Math.Pow(radius, 2);
        }

        private double AreaCalculationPolygon(double radius)
        {
            double a = radius * 10 / (Math.Sqrt(50 + 10 * Math.Sqrt(5)));
            return Math.Sqrt(25+10* Math.Sqrt(5)) / 4 * Math.Pow(a, 2);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            radiusChanged(trackBar1.Value,_oldTrackBar); 
            double radius = (double)numericUpDown1.Value* pix;
            DrawPolygon_Ellipse(radius);
            _oldTrackBar = trackBar1.Value;
        }

        private void radiusChanged(int a,int b) { 
            if (a > b) {
                numericUpDown1.Value = numericUpDown1.Value+(numericUpDown1.Value * (decimal)0.1);
            }
            else if(a < b)
            {
                numericUpDown1.Value = numericUpDown1.Value- (numericUpDown1.Value * (decimal)0.1);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double radius = (double)numericUpDown1.Value * pix;
            DrawPolygon_Ellipse(radius);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            double radius = (double)numericUpDown1.Value * pix;
            DrawPolygon_Ellipse(radius);
        }
       
        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            _oldWidth = Width;
            _oldHeight = Height;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            // если изменилась ширина с прошлого раза
            if (_oldWidth != Width)
            {
                control.Size = new Size(control.Size.Width, (int)(control.Size.Width * 1f / proportion));
                double koeff = (double)control.Size.Width / (double)_oldWidth  ;
                numericUpDown1.Value = (numericUpDown1.Value* (decimal)koeff);

            }
            // если изменилась высота с прошлого раза
            if (_oldHeight != Height)
            {
                control.Size = new Size((int)(control.Size.Height * proportion), control.Size.Height);
                double koeff = (double)control.Size.Height / (double)_oldHeight;
                numericUpDown1.Value =  numericUpDown1.Value   * (decimal)koeff;
                           }
        }
    }
}
