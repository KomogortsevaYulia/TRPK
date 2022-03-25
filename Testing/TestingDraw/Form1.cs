using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingDraw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label2.Text = (int)nRadius.Value + "";
           
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            int sides = 5;
            int radius = (int)nRadius.Value;
            int angle = 0;
            Point center = new Point(picCanvas.Width / 2, picCanvas.Height / 2);
            Image disposeMe = picCanvas.Image;
            picCanvas.Image = DrawRegularPolygon(sides, radius, angle, center, picCanvas.ClientSize);
            Timer timer = new Timer();
            timer.Interval = 1;
            int count = 0;
            int max = 200;
            timer.Tick += new EventHandler((o, ev) =>
            {
                count++;
                if (count == max)
                {
                    Timer t = o as Timer;
                    t.Stop();
                    picCanvas.Image = null;
                    angle = angle + 45;
                    picCanvas.Image = DrawRegularPolygon(sides, radius, angle, center, picCanvas.ClientSize);
                }
            });
            timer.Start();
            if (disposeMe != null)
                disposeMe.Dispose();
        }

        private Bitmap DrawRegularPolygon(int sides, int radius, int startingAngle, Point center, Size canvasSize)
        {
            Point[] verticies = CalculateVertices(sides, radius, startingAngle, center);
            int x = picCanvas.Width / 2 - radius;
            int y = picCanvas.Height / 2 - radius;
            int width = 2 * radius;
            int height = 2 * radius;
            Bitmap polygon = new Bitmap(canvasSize.Width, canvasSize.Height);
            using (Graphics g = Graphics.FromImage(polygon))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawPolygon(Pens.Black, verticies);
                g.DrawEllipse(Pens.Black, x, y, width, height);


            }

            return polygon;
        }

        private Point[] CalculateVertices(int sides, int radius, int startingAngle, Point center)
        {
            List<Point> points = new List<Point>();
            float step = 360.0f / sides;
            float angle = startingAngle; //starting angle
            for (double i = startingAngle; i < startingAngle + 360.0; i += step) //go in a circle
            {
                points.Add(DegreesToXY(angle, radius, center));
                angle += step;
            }
            return points.ToArray();
        }

        private Point DegreesToXY(float degrees, float radius, Point origin)
        {
            Point xy = new Point();
            double radians = degrees * Math.PI / 180.0;
            xy.X = (int)(Math.Cos(radians) * radius + origin.X);
            xy.Y = (int)(Math.Sin(-radians) * radius + origin.Y);
            return xy;
        }
    }
}
