using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;

namespace OOP_LABA_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap bmp = new Bitmap(1800, 800);
        MyStorage str = new MyStorage();
        public Bitmap Image { get; internal set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        int action = 0;
        int choice = 0;
        int h = 0;
        int choice_clr = 0;
        int h1 = 0;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            Form1 snd = (Form1)sender;
            if (e.KeyData == Keys.W) 
            {
                if (action == 2)
                {
                    str.Select(1, 0, choice_clr, choice - 1, snd, bmp, g);
                    this.Refresh();
                }
            }
            if (e.KeyData == Keys.S)
            {
                if (action == 2)
                {
                    str.Select(2, 0, choice_clr, choice - 1, snd, bmp, g);
                    this.Refresh();
                }
            }
            if (e.KeyData == Keys.A)
            {
                if (action == 2)
                {
                    str.Select(4, 0, choice_clr, choice - 1, snd, bmp, g);
                    this.Refresh();
                }
            }
            if (e.KeyData == Keys.D)
            {
                if (action == 2)
                {
                    str.Select(3, 0, choice_clr, choice - 1, snd, bmp, g);
                    this.Refresh();
                }
            }
            if (e.KeyData == Keys.PageUp) // увеличить объект
            {
                if (action == 2)
                {
                    str.Select(0, 2, choice_clr, choice - 1, snd, bmp, g);
                    this.Refresh();
                }
            }
            if (e.KeyData == Keys.PageDown) // увеличить объект
            {
                if (action == 2)
                {
                    str.Select(0, 1, choice_clr, choice - 1, snd, bmp, g);
                    this.Refresh();
                }
            }
            if (e.KeyData == Keys.Oemplus)
            {
                label1.Text = "Какой объект добавить? \n1 - круг, 2 - квадрат, 3 - треугольник, 4 - отрезок";
                action = 1;
            }

            if (e.KeyData == Keys.Tab)
            {
                label1.Text = "Введите номер объекта, который надо выбрать";
                textBox1.Visible = true;
                textBox1.Focus();
                action = 0;
            }
            if (action == 1) // добавление объекта
            {
                if (e.KeyData == Keys.D1 || e.KeyData == Keys.D2 || e.KeyData == Keys.D3 || e.KeyData == Keys.D4)
                {
                    choice = e.KeyValue; // 1 - 49,  2 - 50, 3 - 51, 4 - 52
                    str.Add(choice, new Figure(), snd, bmp, g);
                    this.Refresh();
                }
            }
            if (action == 2) // выбор объекта
            {
                if (h1 != 0)
                    label1.Text = "Выберите действие с объектом № " + choice.ToString() + "\nDel - удалить, Home - покрасить, PageUp - увеличить, PageDown - уменьшить, W,A,S,D - перемещение";
                if (h == 1) // если еще не выбрали объект
                {
                    choice_clr = 4;
                    str.Select(0,0,choice_clr, choice - 1, snd, bmp, g);
                    this.Refresh();
                    h = 0;
                    choice_clr = 0;
                }

                if (e.KeyData == Keys.Delete)
                {
                    str.Del(choice - 1, snd, bmp, g);
                    this.Refresh();
                    h1 = 0;
                }

                if (e.KeyData == Keys.Home)
                {
                    label1.Text = "Выберите цвет, в который покрасить объект № " + choice.ToString() + "\n1 - красный, 2 - синий, 3 - желтый";
                    textBox2.Visible = true;
                    textBox2.Focus();
                    this.Refresh();
                }
                if (choice_clr != 0) // вызов метода окраски
                {
                    str.Select(0,0,choice_clr, choice - 1, snd, bmp, g);
                    this.Refresh();
                    choice_clr = 0;
                }
            }


        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            label3.Text = X.ToString();
            label4.Text = Y.ToString();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) // ввод пользователем номера объекта
        {
            if (e.KeyData == Keys.Enter)
            {
                action = 2;
                h = 1;
                h1 = 1;
                label1.Text = "Нажмите Enter для подтверждения выбора";
                if (textBox1.Text != "")
                    choice = Int32.Parse(textBox1.Text); // номер выбранной фигуры
                textBox1.Text = "";
                this.ActiveControl = null;
                textBox1.Visible = false;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                action = 2;
                label1.Text = "Нажмите Enter для подтверждения выбора";
                if (textBox2.Text != "")
                    choice_clr = Int32.Parse(textBox2.Text);
                textBox2.Text = "";
                this.ActiveControl = null;
                textBox2.Visible = false;
            }
        }
    }


    public class Figure
    {
        public int x, y;
        public int count;
        public int rad = 45;
        public int type =0;
        public string clr = "black";
        public bool isDel = false;
        public Figure()
        {

        }
        ~Figure()
        {

        }



        public void DrawCircle(int num, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (type == 1)
            { 
            
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.FromName(clr), 5);
            g.DrawEllipse(pen, rect);
            sender.BackgroundImage = bmp;
            }
            else
            {
                type = 1;
                Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
                Pen pen = new Pen(Color.FromName(clr), 5);
                g.DrawEllipse(pen, rect);
                Brush Br = new SolidBrush(Color.FromName(clr));
                Font font = new Font("Arial", 25, FontStyle.Regular);
                g.DrawString((num + 1).ToString(), font, Br, x - 15, y + 60);
                sender.BackgroundImage = bmp;
            }
        }

        public void DrawSquare(int num, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (type == 2)
            {
                Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
                Pen pen = new Pen(Color.FromName(clr), 5);
                g.DrawRectangle(pen, rect);
            }
            else
            {
                type = 2;
                Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
                Pen pen = new Pen(Color.FromName(clr), 5);
                Font font = new Font("Arial", 25, FontStyle.Regular);
                g.DrawRectangle(pen, rect);
                g.DrawString((num + 1).ToString(), font, Brushes.Black, x - 15, y + 60);
                sender.BackgroundImage = bmp;
            }
        }
        public void DrawTriangle(int num, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (type == 3)
            { 
            Pen p = new Pen(Color.FromName(clr), 4);
            Point p1 = new Point(x, y - 45);
            Point p2 = new Point(x - 50, y + 45);
            Point p3 = new Point(x + 50, y + 45);
            g.DrawLine(p, p1, p2);
            g.DrawLine(p, p2, p3);
            g.DrawLine(p, p1, p3);
            sender.BackgroundImage = bmp;
            }
            else
            {
                type = 3;
                Font font = new Font("Arial", 25, FontStyle.Regular);
                Pen p = new Pen(Color.FromName(clr), 4);
                Point p1 = new Point(x, y - 45);
                Point p2 = new Point(x - 50, y + 45);
                Point p3 = new Point(x + 50, y + 45);
                g.DrawLine(p, p1, p2);
                g.DrawLine(p, p2, p3);
                g.DrawLine(p, p1, p3);
                g.DrawString((num + 1).ToString(), font, Brushes.Black, x - 15, y + 60);
                sender.BackgroundImage = bmp;
            }
        }
        public void DrawLine(int num, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (type == 4)
            { 
            
            Pen p = new Pen(Color.FromName(clr), 4);
            Point p1 = new Point(x - 50, y + 45);
            Point p2 = new Point(x + 50, y - 45);
            g.DrawLine(p, p1, p2);
            sender.BackgroundImage = bmp;
            }
            else
            {
                type = 4;
                Font font = new Font("Arial", 25, FontStyle.Regular);
                Pen p = new Pen(Color.FromName(clr), 4);
                Point p1 = new Point(x - 50, y + 45);
                Point p2 = new Point(x + 50, y - 45);
                g.DrawLine(p, p1, p2);
                g.DrawString((num + 1).ToString(), font, Brushes.Black, x - 15, y + 60);
                sender.BackgroundImage = bmp;
            }
        }

    }

    public class MyStorage
    {
        static public int size = 0;
        static public Figure[] figures;
        static public int x_ = 150;
        static public int y_ = 200;
        static public int count = 0;

        public MyStorage()
        {
            figures = new Figure[100];

        }

        ~MyStorage()
        {

        }

        public void Drawing(int choice, int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (figures[index] != null)
            {
                if (choice == 49)
                    figures[index].DrawCircle(size, sender, bmp, g);
                if (choice == 50)
                    figures[index].DrawSquare(size, sender, bmp, g);
                if (choice == 51)
                    figures[index].DrawTriangle(size, sender, bmp, g);
                if (choice == 52)
                    figures[index].DrawLine(size, sender, bmp, g);


            }
        }

        public void Add(int choice, Figure obj, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (count != 28)
            {

                figures[size] = obj;
                figures[size].x = x_;
                figures[size].y = y_;
                Drawing(choice, size, sender, bmp, g);
                size++;
                x_ += 300;
                count++;
                if (count == 5)
                {
                    y_+=400;
                    x_ = 150;
                }
            }
        }

        public void Select(int move, int resz, int color, int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (figures[index].isDel == false)
            {
                string oldclr;
                oldclr = figures[index].clr;
                int k = figures[index].type;
                if ((move == 2 || move == 1 || move == 3 || move == 4) )
                {
                    figures[index].clr = "white";
                    if (k == 1)
                        figures[index].DrawCircle(index, sender, bmp, g);
                    if (k == 2)
                        figures[index].DrawSquare(index, sender, bmp, g);
                    if (k == 3)
                        figures[index].DrawTriangle(index, sender, bmp, g);
                    if (k == 4)
                        figures[index].DrawLine(index, sender, bmp, g);
                    if (move== 1 && figures[index].y > 190)
                        figures[index].y -= 20;
                    if (move == 2 && figures[index].y < 745)
                        figures[index].y += 20;
                    if (move == 3 && figures[index].x < 1490 )
                        figures[index].x += 20;
                    if (move == 4 && figures[index].x >55)
                        figures[index].x -= 20;
                }
                if (resz == 2 || resz == 1)
                { 
                    figures[index].clr = "white";
                    if (k == 1)
                        figures[index].DrawCircle(index, sender, bmp, g);
                    if (k == 2)
                        figures[index].DrawSquare(index, sender, bmp, g);
                    if (k == 3)
                        figures[index].DrawTriangle(index, sender, bmp, g);
                    if (k == 4)
                        figures[index].DrawLine(index, sender, bmp, g);
                    if (resz == 2)
                    figures[index].rad += 10;
                    else
                        figures[index].rad -= 10;
                }
                if (color == 4)
                    figures[index].clr = "green";
                if (color == 1)
                    figures[index].clr = "red";
                if (color == 2)
                    figures[index].clr = "blue";
                if (color == 3)
                    figures[index].clr = "yellow";
                if (color == 0)
                    figures[index].clr = oldclr;

                if (k == 1)
                    figures[index].DrawCircle(index, sender, bmp, g);
                if (k == 2)
                    figures[index].DrawSquare(index, sender, bmp, g);
                if (k == 3)
                    figures[index].DrawTriangle(index, sender, bmp, g);
                if (k == 4)
                    figures[index].DrawLine(index, sender, bmp, g);
            }
        }

        public void Del(int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            figures[index].clr = "White";
            figures[index].isDel = true;
            int k = figures[index].type;
            if (k == 1)
                figures[index].DrawCircle(index, sender, bmp, g);
            if (k == 2)
                figures[index].DrawSquare(index, sender, bmp, g);
            if (k == 3)
                figures[index].DrawTriangle(index, sender, bmp, g);
            if (k == 4)
                figures[index].DrawLine(index, sender, bmp, g);
        }
    }
}
