using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice3
{
    public partial class Form1 : Form
    {
        static Graphics holst;
        const int scale = 8;
        public double[,] CoordsG()
        {
            double[,] Matr = new double[6, 3]
            {
                {-2, 2, 1},
                {-2, -2, 1 },
                {2, -2, 1 },
                {2.5, -2.5, 1},
                {3, 0, 1},
                {2, 2, 1}
            };
            return Matr;
        }

        public double[,] scaleMatrix(double alpha, double beta)
        {
            double[,] mash = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mash[i, j] = 0;
                }
            }
            mash[0, 0] = alpha;
            mash[1, 1] = beta;
            mash[2, 2] = 1;
            return mash;
        }

        public double[,] transferMatrix(double delta, double myu)
        {
            double[,] mash = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == j)
                    {
                        mash[i, j] = 1;
                    }
                    else
                    {
                        mash[i, j] = 0;
                    }
                }
            }
            mash[2, 0] = delta;
            mash[2, 1] = myu;
            return mash;
        }

        public double[,] mirrorMatrix(double x, double y)
        {
            double[,] mash = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mash[i, j] = 0;
                }
            }
            mash[0, 0] = x;
            mash[1, 1] = y;
            mash[2, 2] = 1;
            return mash;
        }

        public double[,] rotationMatrix(double degree)
        {
            double[,] mash = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mash[i, j] = 0;
                }
            }
            mash[0, 0] = Math.Cos((Math.PI / 180) * -degree);
            mash[0, 1] = Math.Sin((Math.PI / 180) * -degree);

            mash[1, 0] = -Math.Sin((Math.PI / 180) * -degree);
            mash[1, 1] = Math.Cos((Math.PI / 180) * -degree);

            mash[2, 2] = 1;

            return mash;
        }

        public double[,] multiply(double[,] A, double[,] B)
        { 
            int rows = A.GetLength(0);
            int cols = B.GetLength(1);
            double[,] C = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < cols; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    C[i, j] = sum;
                }

            }
            return C;
        }


        public float toDisplayX(double x)
        {
            return (float)(x * (pictureBox1.ClientSize.Width / scale) + pictureBox1.ClientSize.Width / 2);
        }

        public float toDisplayY(double y)
        {
            return (float)(-y * (pictureBox1.ClientSize.Height / scale) + pictureBox1.ClientSize.Height / 2);
        }
        public void drawFigure(double [,] Matr)
        {
            PointF x1, x2;
            Pen penRed = new Pen(Color.Red, 2);
            int nDots = Matr.GetLength(0);
            for (int i = 0; i < nDots - 1; i++)
            {
                x1 = new PointF(toDisplayX(Matr[i,0]),toDisplayY(Matr[i,1]));
                x2 = new PointF(toDisplayX(Matr[i+1, 0]), toDisplayY(Matr[i+1, 1]));
                holst.DrawLine(penRed, x1, x2);
            }
            x1 = new PointF(toDisplayX(Matr[nDots - 1, 0]), toDisplayY(Matr[nDots - 1, 1]));
            x2 = new PointF(toDisplayX(Matr[0, 0]), toDisplayY(Matr[0, 1]));

            holst.DrawLine(penRed, x1, x2);
        }
        public Form1()
        {
            InitializeComponent();
        }

        public void drawAxis()
        {
            Refresh();
            Pen penOXY = new Pen(Color.Black, 2);
            PointF x1 = new PointF(0, pictureBox1.ClientSize.Height/2);
            PointF x2 = new PointF(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height/ 2);
            PointF y1 = new PointF(pictureBox1.ClientSize.Width / 2, 0);
            PointF y2 = new PointF(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height);
            holst.DrawLine(penOXY, x1, x2);
            holst.DrawLine(penOXY, y1, y2);
            int step = pictureBox1.ClientSize.Width / scale;
            int h = pictureBox1.ClientSize.Width / (scale * 20);
            for (int i = step; i < pictureBox1.ClientSize.Width; i += step)
            {
                holst.DrawLine(penOXY, i, h + pictureBox1.ClientSize.Height / 2, i, -h + pictureBox1.ClientSize.Height / 2);
            }
            step = pictureBox1.ClientSize.Height / scale;
            for (int i = step; i < pictureBox1.ClientSize.Height; i += step)
            {
                holst.DrawLine(penOXY, -h + pictureBox1.ClientSize.Width / 2, i, h + pictureBox1.ClientSize.Width / 2, i);
            }
        }

        public double[,] transformMatr(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7)
        {
            double[,] Matr = CoordsG();
            Matr = multiply(Matr, scaleMatrix(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text)));
            Matr = multiply(Matr, transferMatrix(Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text)));
            Matr = multiply(Matr, mirrorMatrix(Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text)));
            Matr = multiply(Matr, rotationMatrix(Convert.ToDouble(textBox7.Text)));
            return Matr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            holst = Graphics.FromHwnd(pictureBox1.Handle);
            drawAxis();
            double[,] Coords = transformMatr(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7);
            drawFigure(Coords);
        }

        private void Form1_ResizeBegin_1(object sender, EventArgs e)
        {

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_ClientSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
