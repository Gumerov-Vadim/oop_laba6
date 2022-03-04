using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Newtonsoft.Json;
using System.IO;
namespace laba_6
{

    public partial class Form1 : Form
    {
        public class Settings
        {
            private int selected_obj;
            private int size;
            private int color;
            public System.EventHandler observers;
            public int pick_obj(int i){ _ = (i >= 0) ? selected_obj = i : selected_obj = 0; observers.Invoke(this, null); return selected_obj;}
            public int pick_obj() { return selected_obj; }
            public Settings()
            {
                selected_obj = 0;
                size = 60;
                color = 0;
            }
        }
        public class Object
        {
            public int _x, _y,_size;
            public bool _selected;
            public System.Windows.Forms.Button obj = new System.Windows.Forms.Button();
            //public System.Windows.Forms.MouseEventHandler click_circle;
            public Object()
            {
                _x = 10; _y = 10; _size=60;
                 obj.FlatStyle = FlatStyle.Flat;
                obj.FlatAppearance.BorderSize = 0;
                obj.Width = 60;
                obj.Height = 60;
                obj.Location = new System.Drawing.Point(_x - obj.Width / 2, _y - obj.Height / 2);
                obj.BackColor = System.Drawing.Color.Green;

                //circle.Click += new EventHandler(select_circle);

            }
            public Object(int x, int y)
            {
                _x = x; _y = y; _size = 60;
                obj.FlatStyle = FlatStyle.Flat;
                obj.FlatAppearance.BorderSize = 0;
                obj.Width = 60;
                obj.Height = 60;
                obj.Location = new System.Drawing.Point(_x - obj.Width / 2, _y - obj.Height / 2);
                obj.BackColor = System.Drawing.Color.Green;

            }
            public Object(int x, int y,int size)
            {
                _x = x; _y = y; _size = size;
                obj.FlatStyle = FlatStyle.Flat;
                obj.FlatAppearance.BorderSize = 0;
                obj.Width = size;
                obj.Height = size;
                obj.Location = new System.Drawing.Point(_x - obj.Width / 2, _y - obj.Height / 2);
                obj.BackColor = System.Drawing.Color.Green;

            }
            virtual public System.Windows.Forms.Button inside()
            {
                return obj;
            }
            public bool select()
            {
                return _selected;
            }
            public bool select(bool _select)
            {
                if (_select)
                {
                    this._selected = true;
                    obj.BackColor = System.Drawing.Color.Purple;
                    return true;
                }
                else
                {
                    this._selected = false;
                    obj.BackColor = System.Drawing.Color.Green;
                    return false;
                }
            }
        }
        public class Circle : Object
        {
            private int _radius;
            public Circle()
            {
                _radius = 30;

                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddEllipse(0, 0, 60, 60);
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Circle(int x,int y):base(x,y)
            {
                _radius = 30;
                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddEllipse(0, 0, 60, 60);
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Circle(int x,int y,int radius):base(x,y,radius*2)
            {
                _radius = radius;
                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddEllipse(0, 0, 60, 60);
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
        }
        public class Square : Object
        {
            public Square(){}
            public Square(int x, int y) : base(x, y){}
            public Square(int x, int y,int size) : base(x, y,size){}
        }
        public class Triangle:Object
        {
            public Triangle()
            {
                obj.Width = 60;
                obj.Height = 60;

                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                Point point1 = new Point(_x+obj.Width/2, _y+obj.Height/2);
                Point point2 = new Point(_x, _y-obj.Height / 2);
                Point point3 = new Point(_x+obj.Width, _y- obj.Height / 2);
                Point[] curvePoints ={ point1, point2, point3 };
                gPath.AddPolygon(curvePoints);
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Triangle(int x, int y) : base(x, y)
            {

                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddPolygon(new[] {
                    new Point(0, obj.Height),
                    new Point(obj.Height, obj.Width),
                    new Point(obj.Width / 2, 0)
                });
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Triangle(int x, int y,int size) : base(x, y,size)
            {

                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddPolygon(new[] {
                    new Point(0, obj.Height),
                    new Point(obj.Height, obj.Width),
                    new Point(obj.Width / 2, 0)
                });
                Region rg = new Region(gPath);
                obj.Region = rg;
            }

        }
        public class Storage
        {
            int _size;
            public Object[] massive;
            public int size() { return _size; }
            public void add(int x, int y)
            {
                int i = 0;
                while (i < _size && massive[i] != null)
                {
                    i++;
                }
                if (i != _size)
                {
                    massive[i] = new Triangle(x, y);
                    massive[i].inside().Name = (i).ToString();
                }
            }
            public void add(Object obj)
            {
                int i = 0;
                while (i < _size && massive[i] != null)
                {
                    i++;
                }
                if (i != _size)
                {
                    massive[i] = obj;
                    massive[i].inside().Name = (i).ToString();
                }
            }
            public void select_clear()
            {
                int i = 0;
                while (i < _size)
                {
                    if (massive[i] != null)
                    {
                        massive[i].select(false);
                    }
                    i++;
                }
            }
            public int del_selected()
            {
                int i = 0;
                int k = 0;
                while (i < _size)
                {
                    if (massive[i] != null && massive[i].select())
                    {
                        massive[i] = null;
                        k++;
                    }
                    i++;
                }
                return k;
            }
            public Object get(int i)
            {
                if (i < _size)
                {
                    return massive[i];
                }
                return null;
            }
            public Storage()
            {
                _size = 100;
                massive = new Object[100];
            }
            public Storage(int n)
            {
                _size = n;
                massive = new Object[n];
            }
        }
        Storage storage = new Storage();
        //int i = 0;
        Settings obj_settings = new Settings();
        public Form1()
        {
            InitializeComponent();
            obj_settings.observers += new EventHandler(this.updatefromsettings);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //storage = File.Exists("save.json") ? JsonConvert.DeserializeObject<Storage>(File.ReadAllText("save.json")) : new Storage();
            int size = storage.size();
            int k = 0;
            while (k < size)
            {
                if (storage.get(k) != null)
                {
                    System.Windows.Forms.Button circle = storage.get(k).inside();
                    circle.MouseClick += select_circle;
                    circle.KeyDown += del_selected_circle;
                }
                k++;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //            i++;
            Object obj=null;
            switch (obj_settings.pick_obj())
            {
                case 0:
                    obj = new Circle(e.X, e.Y);
                    break;
                case 1:
                    obj = new Triangle(e.X, e.Y);
                    break;
                case 2:
                    obj = new Square(e.X, e.Y);
                    break;
                default:
                    obj = null;
                    //impossible
                    break;
            }
            storage.add(obj);
            if (obj != null)
            {
                obj.inside().MouseClick += select_circle;
                obj.inside().KeyDown += del_selected_circle;
                this.Controls.Add(obj.inside());
                //                label1.Text = i.ToString();
                storage.select_clear();
                obj.select(true);
            }
        }
        private void select_circle(object sender, MouseEventArgs e)
        {
            Object circle = null;
            int k = 0;
            int size = storage.size();
            while (k < size)
            {
                if (storage.get(k) != null && sender == storage.get(k).inside())
                {
                    circle = storage.get(k);
                }
                k++;
            }
            if (Control.ModifierKeys == Keys.Control)
            {
                circle.select(!circle.select());
            }
            else
            {
                storage.select_clear();
                circle.select(true);
            }
        }
        private void del_selected_circle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                {
                    Object circle = null;
                    int k = 0;
                    int size = storage.size();
                    while (k < size)
                    {
                        circle = storage.get(k);
                        if (circle != null && circle.select())
                        {
                            Controls.Remove(circle.inside());
                        }
                        k++;
                    }
                    storage.del_selected();
                    //                i = i - storage.del_selected();
                    //                label1.Text = i.ToString();
                }
            }
        }
        private void paint(object sender, EventArgs e)
        {
            int size = storage.size();
            int k = 0;
            Object circle = null;
            Controls.Clear();
            while (k < size)
            {
                circle = storage.get(k);
                if (circle != null)
                {
                    Controls.Add(circle.inside());
                }
                k++;
            }
        }
        private void updatefromsettings(object sender, EventArgs e)
        {
            switch (obj_settings.pick_obj())
            {
                case 0:
                    object_picker.Text = "Круг";
                    break;
                case 1:
                    object_picker.Text = "Треугольник";
                    break;
                case 2:
                    object_picker.Text = "Квадрат";
                    break;
            }
            object_picker.Size = new System.Drawing.Size(100, 20);
        }
        private void кругToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(0);
        }

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(1);
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(2);
        }
    }
}
