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
        public class Object
        {
            public int _x, _y;
            public bool _selected;
            public System.Windows.Forms.Button obj = new System.Windows.Forms.Button();
            //public System.Windows.Forms.MouseEventHandler click_circle;
            public Object()
            {
                _x = 10; _y = 10;
                 obj.FlatStyle = FlatStyle.Flat;
                obj.FlatAppearance.BorderSize = 0;
                obj.Location = new System.Drawing.Point(_x - obj.Width / 2, _y - obj.Height / 2);
                obj.BackColor = System.Drawing.Color.Green;

                //circle.Click += new EventHandler(select_circle);

            }
            public Object(int x, int y)
            {
                _x = x; _y = y;
                obj.FlatStyle = FlatStyle.Flat;
                obj.FlatAppearance.BorderSize = 0;
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
                obj.Width = 60;
                obj.Height = 60;

                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddEllipse(0, 0, 60, 60);
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Circle(int x,int y):base(x,y)
            {
                _radius = 30;
                obj.Width = 60;
                obj.Height = 60;
                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddEllipse(0, 0, 60, 60);
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Circle(int x,int y,int radius):base(x,y)
            {
                _radius = radius;
                obj.Width = _radius * 2;
                obj.Height = _radius * 2;
                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddEllipse(0, 0, 60, 60);
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
                    massive[i] = new Circle(x, y);
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
        public Form1()
        {
            InitializeComponent();

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
            Object circle = new Circle(e.X, e.Y);
            storage.add(circle);
            if (circle != null)
            {
                circle.inside().MouseClick += select_circle;
                circle.inside().KeyDown += del_selected_circle;
                this.Controls.Add(circle.inside());
                //                label1.Text = i.ToString();
                storage.select_clear();
                circle.select(true);
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

    }
}
