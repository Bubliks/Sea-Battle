using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Bitmap bm, bm2;
        Graphics gr, gr2;
        int chgamer = 0;
        int chcomp = 0;

        int win = 0;
        int unwin = 0;

        int[,] gamer = new int[10, 10];
        int[,] comp = new int[10, 10];

        int x_nach = 0;
        int y_nach = 0;

        int x_kon = 0;
        int y_kon = 0;

        int x_tek = 0;
        int y_tek = 0;

        bool nachalo = true;

        int k4 = 1;
        int k3 = 2;
        int k2 = 3;
        int k1 = 4;

        public void DrawMap(Graphics gr, PictureBox pictureBox)
        {
            for (int i = 1; i < 12; i++)
            {
                gr.DrawLine(Pens.Black, i * pictureBox.Width / 10, 0, i * pictureBox.Width / 10, pictureBox.Height);
                gr.DrawLine(Pens.Black, 0, i * pictureBox.Height / 10, pictureBox.Width, i * pictureBox.Height / 10);
            }
        }

        public void ClearMap(int[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bm);

            bm2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            gr2 = Graphics.FromImage(bm2);

            DrawMap(gr, pictureBox1);
            DrawMap(gr2, pictureBox2);

            ClearMap(gamer);
            ClearMap(comp);

            pictureBox1.Image = bm;
            pictureBox2.Image = bm2;
            button2.Enabled = false;
            button3.Enabled = false;
        }
 
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
            {
                int kolp = 0;

                if (radioButton1.Checked) { kolp = 4; button3.Enabled = false; }
                if (radioButton2.Checked) { kolp = 3; button3.Enabled = false; }
                if (radioButton3.Checked) { kolp = 2; button3.Enabled = false; }
                if (radioButton4.Checked)
                {
                    button3.Enabled = false;
                    kolp = 1;

                    int kol1 = 0;

                    for (int l = 0; l < gamer.GetLength(0); l++)
                    {
                        for (int g = 0; g < gamer.GetLength(1); g++)
                        {
                            if (e.X > g * pictureBox1.Width / 10 & e.X < (g + 1) * pictureBox1.Width / 10 & e.Y > (l) * pictureBox1.Height / 10 & e.Y < (l + 1) * pictureBox1.Height / 10)
                            {
                                for (int i = Math.Max(0, l - 1); i < Math.Min(10, l + 2); i++)
                                {
                                    for (int j = Math.Max(0, g - 1); j < Math.Min(10, g + 2); j++)
                                    {
                                        if (gamer[i, j] == 1)
                                        {
                                            kol1++;
                                        }
                                    }
                                }

                                if (kol1 == 0)
                                {
                                    gamer[l, g] = 1;

                                    gr.FillRectangle(Brushes.Gray, g * pictureBox1.Width / 10 + 1, l * pictureBox1.Height / 10 + 1, pictureBox1.Width / 10 - 1, pictureBox1.Height / 10 - 1);
                                }
                                else
                                {
                                    nachalo = true;
                                }
                            }
                        }
                    }

                    if (kol1 == 0)
                    {
                        k1--;

                        if (k1 == 0)
                        {
                            radioButton4.Enabled = false;
                            radioButton4.Checked = false;
                        }
                    }
                }
                else
                {
                    if (nachalo)
                    {
                        int kol2 = 0;

                        for (int i = 0; i < gamer.GetLength(0); i++)
                        {
                            for (int j = 0; j < gamer.GetLength(1); j++)
                            {
                                if (e.X > (j) * pictureBox1.Width / 10 & e.X < (j + 1) * pictureBox1.Width / 10 & e.Y > (i) * pictureBox1.Height / 10 & e.Y < (i + 1) * pictureBox1.Height / 10)
                                {
                                    for (int l = Math.Max(0, i - 1); l < Math.Min(10, i + 2); l++)
                                    {
                                        for (int g = Math.Max(0, j - 1); g < Math.Min(10, j + 2); g++)
                                        {
                                            if (gamer[l, g] == 1)
                                            {
                                                kol2++;
                                            }
                                        }
                                    }

                                    if (kol2 == 0)
                                    {
                                        gr.FillRectangle(Brushes.Gray, j * pictureBox1.Width / 10 + 1, i * pictureBox1.Height / 10 + 1, pictureBox1.Width / 10 - 1, pictureBox1.Height / 10 - 1);

                                        x_nach = e.X;
                                        y_nach = e.Y;

                                        x_tek = e.X;
                                        y_tek = e.Y;

                                        nachalo = false;
                                    }
                                    else
                                    {
                                        nachalo = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int i_nach = 0;
                        int i_kon = 0;

                        int j_nach = 0;
                        int j_kon = 0;

                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (x_nach < ((pictureBox1.Width / 10) * (j + 1)) && x_nach > ((pictureBox1.Width / 10) * j) && y_nach < ((pictureBox1.Height / 10) * (i + 1)) && y_nach > ((pictureBox1.Height / 10) * i))
                                {
                                    i_nach = i;
                                    j_nach = j;
                                }

                                if (e.X < ((pictureBox1.Width / 10) * (j + 1)) && e.X > ((pictureBox1.Width / 10) * j) && e.Y < ((pictureBox1.Height / 10) * (i + 1)) && e.Y > ((pictureBox1.Height / 10) * i))
                                {
                                    i_kon = i;
                                    j_kon = j;
                                }
                            }
                        }

                        if ((i_nach == i_kon) & (j_kon != j_nach))
                        {
                            if (j_kon > j_nach)
                            {
                                if (j_nach + kolp <= 10)
                                {
                                    int kol3 = 0;

                                    for (int l = Math.Max(0, i_nach - 1); l < Math.Min(10, i_nach + 2); l++)
                                    {
                                        for (int g = Math.Max(0, j_nach - 1); g < Math.Min(10, j_nach + kolp + 1); g++)
                                        {
                                            if (gamer[l, g] == 1) kol3++;
                                        }
                                    }

                                    if (kol3 == 0)
                                    {
                                        x_kon = x_nach + (kolp - 1) * pictureBox1.Width / 10;

                                        for (int j = j_nach; j < j_nach + kolp; j++)
                                        {
                                            y_kon = y_nach;

                                            x_tek = x_kon;
                                            y_tek = y_kon;

                                            gamer[i_nach, j] = 1;

                                            gr.FillRectangle(Brushes.Gray, ((pictureBox1.Width / 10) * j), ((pictureBox1.Height / 10) * i_nach), pictureBox1.Width / 10, pictureBox1.Height / 10);
                                        }
                                        if ((radioButton1.Checked) && (nachalo == false))
                                        {
                                            k4--;
                                            if (k4 == 0)
                                            {
                                                radioButton1.Checked = false;
                                                radioButton1.Enabled = false;
                                            }
                                        }
                                        if ((radioButton2.Checked) && (nachalo == false))
                                        {
                                            k3--;
                                            if (k3 == 0)
                                            {
                                                radioButton2.Checked = false;
                                                radioButton2.Enabled = false;
                                            }
                                        }
                                        if ((radioButton3.Checked) && (nachalo == false))
                                        {
                                            k2--;
                                            if (k2 == 0)
                                            {
                                                radioButton3.Checked = false;
                                                radioButton3.Enabled = false;
                                            }
                                        }
                                        if ((radioButton4.Checked) && (nachalo == false))
                                        {
                                            k1--;
                                            if (k1 == 0)
                                            {
                                                radioButton4.Checked = false;
                                                radioButton4.Enabled = false;
                                            }
                                        }
                                        nachalo = true;

                                    }
                                }

                            }
                            else
                            {
                                if (j_nach - (kolp - 1) >= 0)
                                {
                                    int kol4 = 0;

                                    for (int l = Math.Max(0, i_nach - 1); l < Math.Min(10, i_nach + 2); l++)
                                    {
                                        for (int g = Math.Max(0, j_nach - kolp); g < Math.Min(10, j_nach + 2); g++)
                                        {
                                            if (gamer[l, g] == 1) kol4++;
                                        }
                                    }

                                    if (kol4 == 0)
                                    {
                                        x_kon = x_nach - (kolp - 1) * pictureBox1.Width / 10;

                                        for (int j = j_nach - kolp; j < j_nach; j++)
                                        {
                                            y_kon = y_nach;

                                            x_tek = x_kon;
                                            y_tek = y_kon;

                                            gamer[i_nach, j + 1] = 1;

                                            gr.FillRectangle(Brushes.Gray, ((pictureBox1.Width / 10) * (j + 1)), ((pictureBox1.Height / 10) * i_nach), pictureBox1.Width / 10, pictureBox1.Height / 10);
                                        }

                                        if ((radioButton1.Checked) && (nachalo == false))
                                        {
                                            k4--;

                                            if (k4 == 0)
                                            {
                                                radioButton1.Checked = false;
                                                radioButton1.Enabled = false;
                                            }
                                        }
                                        if ((radioButton2.Checked) && (nachalo == false))
                                        {
                                            k3--;

                                            if (k3 == 0)
                                            {
                                                radioButton2.Checked = false;
                                                radioButton2.Enabled = false;

                                            }
                                        }
                                        if ((radioButton3.Checked) && (nachalo == false))
                                        {
                                            k2--;

                                            if (k2 == 0)
                                            {
                                                radioButton3.Checked = false;
                                                radioButton3.Enabled = false;
                                            }
                                        }
                                        if ((radioButton4.Checked) && (nachalo == false))
                                        {
                                            k1--;

                                            if (k1 == 0)
                                            {
                                                radioButton4.Checked = false;
                                                radioButton4.Enabled = false;
                                            }
                                        }
                                        nachalo = true;
                                    }
                                }
                            }
                        }

                        if ((j_nach == j_kon) & (i_nach != i_kon))
                        {
                            if (i_kon > i_nach)
                            {
                                if (i_nach + kolp <= 10)
                                {

                                    int kol5 = 0;

                                    for (int l = Math.Max(0, i_nach - 1); l < Math.Min(10, i_nach + kolp + 1); l++)
                                    {
                                        for (int g = Math.Max(0, j_nach - 1); g < Math.Min(10, j_nach + 2); g++)
                                        {
                                            if (gamer[l, g] == 1) kol5++;
                                        }
                                    }

                                    if (kol5 == 0)
                                    {
                                        y_kon = y_nach + (kolp - 1) * pictureBox1.Height / 10;

                                        for (int i = i_nach; i < i_nach + kolp; i++)
                                        {
                                            x_kon = x_nach;

                                            x_tek = x_kon;
                                            y_tek = y_kon;

                                            gamer[i, j_nach] = 1;

                                            gr.FillRectangle(Brushes.Gray, ((pictureBox1.Width / 10) * j_nach), ((pictureBox1.Height / 10) * (i)), pictureBox1.Width / 10, pictureBox1.Height / 10);

                                        }

                                        if ((radioButton1.Checked) && (nachalo == false))
                                        {
                                            k4--;

                                            if (k4 == 0)
                                            {
                                                radioButton1.Checked = false;
                                                radioButton1.Enabled = false;
                                            }
                                        }
                                        if ((radioButton2.Checked) && (nachalo == false))
                                        {
                                            k3--;

                                            if (k3 == 0)
                                            {
                                                radioButton2.Checked = false;
                                                radioButton2.Enabled = false;
                                               
                                            }
                                        }
                                        if ((radioButton3.Checked) && (nachalo == false))
                                        {
                                            k2--;

                                            if (k2 == 0)
                                            {
                                                radioButton3.Checked = false;
                                                radioButton3.Enabled = false;
                                            }
                                        }
                                        if ((radioButton4.Checked) && (nachalo == false))
                                        {
                                            k1--;

                                            if (k1 == 0)
                                            {
                                                radioButton4.Checked = false;
                                                radioButton4.Enabled = false;
                                            }
                                        }

                                        nachalo = true;

                                    }
                                }

                            }
                            else
                            {
                                if (i_nach - (kolp - 1) >= 0)
                                {
                                    int kol6 = 0;

                                    for (int l = Math.Max(0, i_nach - kolp - 1); l < Math.Min(10, i_nach + 1); l++)
                                    {
                                        for (int g = Math.Max(0, j_nach - 1); g < Math.Min(10, j_nach + 2); g++)
                                        {
                                            if (gamer[l, g] == 1) kol6++;
                                        }
                                    }

                                    if (kol6 == 0)
                                    {
                                        y_kon = y_nach - (kolp - 1) * pictureBox1.Width / 10;

                                        for (int i = i_nach - kolp; i < i_nach; i++)
                                        {
                                            x_kon = x_nach;

                                            x_tek = x_kon;
                                            y_tek = y_kon;

                                            gamer[i + 1, j_nach] = 1;

                                            gr.FillRectangle(Brushes.Gray, ((pictureBox1.Width / 10) * j_nach), ((pictureBox1.Height / 10) * (i + 1)), pictureBox1.Width / 10, pictureBox1.Height / 10);
                                        }

                                        if ((radioButton1.Checked) && (nachalo == false))
                                        {
                                            k4--;
                                            if (k4 == 0)
                                            {
                                                radioButton1.Checked = false;
                                                radioButton1.Enabled = false;
                                            }
                                        }
                                        if ((radioButton2.Checked) && (nachalo == false))
                                        {
                                            k3--;
                                            if (k3 == 0)
                                            {
                                                radioButton2.Checked = false;
                                                radioButton2.Enabled = false;
                                            }
                                        }
                                        if ((radioButton3.Checked) && (nachalo == false))
                                        {
                                            k2--;
                                            if (k2 == 0)
                                            {
                                                radioButton3.Checked = false;
                                                radioButton3.Enabled = false;
                                            }
                                        }
                                        if ((radioButton4.Checked) && (nachalo == false))
                                        {
                                            k1--;
                                            if (k1 == 0)
                                            {
                                                radioButton4.Checked = false;
                                                radioButton4.Enabled = false;
                                            }
                                        }
                                        nachalo = true;
                                    }
                                }
                            }
                        }

                        else
                        {
                            x_tek = x_nach;
                            y_tek = y_nach;
                        }
                    }
                }
                textBox1.Text = "";
                for (int i = 0; i < gamer.GetLength(0); i++)
                {
                    for (int j = 0; j < gamer.GetLength(1); j++)
                    {
                        textBox1.Text += gamer[i, j].ToString() + " ";
                    }

                    textBox1.Text += Environment.NewLine;
                }

                pictureBox1.Image = bm;
            }
            else
            {
                MessageBox.Show("Выберите вид корабля!");
            }
        }

        bool hodgamer = false;
        bool hodkomp = false;

        public Pair<int, int> GetPosition(MouseEventArgs e, PictureBox pictureBox)
        {
            int WIDTH_BLOCK = pictureBox.Width / 10;
            int HEIGHT_BLOCK = pictureBox.Height / 10;

            return new Pair<int, int>(e.X / WIDTH_BLOCK, e.Y / HEIGHT_BLOCK);
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (hodgamer == false)
            {
                hodgamer = true;
                for (int l = 0; l < comp.GetLength(0); l++)
                    for (int g = 0; g < comp.GetLength(1); g++)
                    {
                        if (hodgamer)
                        {
                            if (e.X > g * pictureBox2.Width / 10 & e.X < (g + 1) * pictureBox2.Width / 10 & e.Y > (l) * pictureBox2.Height / 10 & e.Y < (l + 1) * pictureBox2.Height / 10)
                            {
                                if (comp[l, g] == 0)   
                                {
                                comp[l, g] = 2; hodgamer = true; hodkomp = false;
                                gr2.FillEllipse(Brushes.Blue, (g) * pictureBox2.Width / 10 + 1 + (pictureBox2.Width / 10) / 4, (l) * pictureBox2.Height / 10 + 1 + (pictureBox2.Width / 10) / 4, pictureBox2.Width / 10 - 1 - (pictureBox2.Width / 10) / 2, pictureBox2.Height / 10 - 1 - (pictureBox2.Width / 10) / 2);
                                    chgamer = chgamer + 1;
                                    //SoundPlayer sp = new SoundPlayer("D:\\gun8.wav");
                                    //sp.Play();
                            }
                            else if (comp[l, g] == 1 )
                            {
                                comp[l, g] = 3; hodgamer = true; hodkomp = true;
                                gr2.FillRectangle(Brushes.Red, g * pictureBox2.Width / 10 + 1, l * pictureBox2.Height / 10 + 1, pictureBox2.Width / 10 - 1, pictureBox2.Height / 10 - 1);
                                chgamer++;
                                win++;
                                //SoundPlayer sp = new SoundPlayer("@D:\\gun8.wma");
                                //sp.Play();
                            }
                            else
                            { 
                                hodgamer = false; 
                            }
                        }
                    }
            }

            if (hodkomp) hodgamer = false;
                Random r = new Random();
               
                while (!hodkomp)
                { 
                    hodkomp = true;
                    
                    int i = r.Next(0, 10);
                    int j = r.Next(0, 10);
                    if (gamer[i, j] == 0)
                    {
                        gr.FillEllipse(Brushes.Blue, (j) * pictureBox1.Width / 10 + 1 + (pictureBox1.Width / 10) / 4, (i) * pictureBox1.Height / 10 + 1 + (pictureBox1.Width / 10) / 4, pictureBox1.Width / 10 - 1 - (pictureBox1.Width / 10) / 2, pictureBox1.Height / 10 - 1 - (pictureBox1.Width / 10) / 2);
                        gamer[i, j] = 2; hodkomp = true;   hodgamer = false;
                        chcomp = chcomp + 1;
                        //SoundPlayer sp = new SoundPlayer("D:\\gun8.wav");
                        //sp.Play();
                   
                    }
                    else
                        if (gamer[i, j] == 1)
                        {
                            gamer[i, j] = 3; hodkomp = true; hodgamer = true;
                            gr.FillRectangle(Brushes.Red, j * pictureBox1.Width / 10+1, (i) * pictureBox1.Height / 10+1, pictureBox1.Width / 10-1, pictureBox1.Height / 10-1);
                            chcomp = chcomp + 1;
                            unwin = unwin + 1;
                            hodkomp = false;
                            //SoundPlayer sp = new SoundPlayer("@D:\\gun8.wma");
                            //sp.Play();
                        }
                }
             
                
                textBox2.Text = "";
                for (int l = 0; l < comp.GetLength(0); l++)
                {
                    for (int g = 0; g < comp.GetLength(1); g++)
                    { textBox2.Text += comp[l, g].ToString() + " "; }
                    textBox2.Text += Environment.NewLine;

                }

                textBox1.Text = "";
                for (int i = 0; i < gamer.GetLength(0); i++)
                {
                    for (int j = 0; j < gamer.GetLength(1); j++)
                    { textBox1.Text += gamer[i, j].ToString() + " "; }
                    textBox1.Text += Environment.NewLine;
                }
                label1.Text = chgamer.ToString();
                label2.Text = chcomp.ToString();
            //}
            pictureBox2.Image = bm2;
            pictureBox1.Image = bm;

            //if (win == 10)
            //{
            //    MessageBox.Show("Капитан, нужно еще чуть-чуть поднажать!");
            //}


            if ( win == 20) 
            {
                pictureBox1.Enabled = false;
                pictureBox2.Enabled = false;
                MessageBox.Show("Победа за нами, командир!");
                label3.Text = "Победа за нами, командир!";
             }
             if (unwin ==20)
             {
                 pictureBox1.Enabled = false;
                 pictureBox2.Enabled = false;
                 MessageBox.Show("Мы проиграли битву, но не войну!");
                 
             }
             if (win == 1)
             {
                 label3.Text = "Прямо в яблочко!";
             }

        } 

        private void button1_Click(object sender, EventArgs e)
        {   win = 0; 
            unwin = 0;

            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bm2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            gr = Graphics.FromImage(bm);
            gr2 = Graphics.FromImage(bm2);

            ClearMap(gamer);
            ClearMap(comp);
                
            DrawMap(gr, pictureBox1);
            DrawMap(gr2, pictureBox2);

            textBox1.Text = "";
            for (int i = 0; i < gamer.GetLength(0); i++)
            {
                for (int j = 0; j < gamer.GetLength(1); j++)
                { 
                    textBox1.Text += gamer[i, j].ToString() + " ";
                }

                textBox1.Text += Environment.NewLine;
            }

            k4 = 1;
            k3 = 2;
            k2 = 3;
            k1 = 4;
           
            radioButton3.Checked = true;
            radioButton3.Enabled = true;

            radioButton2.Checked = true;
            radioButton2.Enabled = true;

            radioButton1.Checked = true;
            radioButton1.Enabled = true;

            radioButton4.Checked = true;
            radioButton4.Enabled = true;

            pictureBox1.Image = bm;


            textBox2.Text = "";

            for (int l = 0; l < comp.GetLength(0); l++)
            {
                for (int g = 0; g < comp.GetLength(1); g++)
                    textBox2.Text += comp[l, g].ToString() + " "; 
                textBox2.Text += Environment.NewLine;

            }
            button2.Enabled = true;
            button3.Enabled = true;
            pictureBox1.Enabled = true;
            pictureBox2.Enabled = true;
            pictureBox2.Image = bm2;
            label3.Text = "Капитан, выберите тактику!";
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x_n = 0;
            int y_n = 0;

            int napr = 0;

            Random r = new Random();

            for (int kp = 1; kp <= 4; kp++) 
            {
                for (int kk = 1; kk <= 4 - kp + 1; kk++) 
                {
                    bool hod = false;

                    while (hod == false)
                    {                        
                        x_n = r.Next(0, 10);
                        y_n = r.Next(0, 10);

                        napr = r.Next(0, 2);

                        hod = true;

                        if (napr == 0) // горизонтальное
                        {
                            if (x_n + kp - 1 > 9) 
                            {
                                hod = false;
                            }

                            

                            for (int c_x = Math.Max(0, x_n - 1); c_x < Math.Min(10, x_n + kp + 2); c_x++)
                            {
                                for (int c_y = Math.Max(0, y_n - 1); c_y < Math.Min(10, y_n + 2); c_y++)
                                {
                                    if (comp[c_y, c_x] != 0)
                                    {
                                        hod = false;
                                    }
                                }
                            }

                            if (hod)
                            {
                                for (int x_1 = x_n; x_1 < x_n + kp; x_1++) 
                                {
                                    comp[y_n, x_1] = 1; 

                                    gr2.FillRectangle(Brushes.Gray, x_1 * (pictureBox2.Width / 10) + 1, y_n * (pictureBox2.Height / 10) + 1, pictureBox2.Width / 10 - 1, pictureBox2.Height / 10 - 1);
                                }
                            }
                        }
                        else // вертикальное
                        {
                            if (y_n + kp - 1 > 9) 
                            {
                                hod = false;
                            }

                           

                            for (int c_x = Math.Max(0, x_n - 1); c_x < Math.Min(10, x_n + 2); c_x++)
                            {
                                for (int c_y = Math.Max(0, y_n - 1); c_y < Math.Min(10, y_n + kp + 2); c_y++)
                                {
                                    if (comp[c_y, c_x] != 0)
                                    {
                                        hod = false;
                                    }
                                }
                            }

                            if (hod)
                            {
                                for (int y_1 = y_n; y_1 < y_n + kp; y_1++) 
                                {
                                    comp[y_1, x_n] = 1;

                                    gr2.FillRectangle(Brushes.Gray, x_n * (pictureBox2.Width / 10) + 1, y_1 * (pictureBox2.Height / 10) + 1, pictureBox2.Width / 10 - 1, pictureBox2.Height / 10 - 1);
                                }
                            }
                        }
                    }
                }
            }

            textBox2.Text = "";

            for (int l = 0; l < comp.GetLength(0); l++)
            {
                for (int g = 0; g < comp.GetLength(1); g++)
                {
                    textBox2.Text += comp[l, g].ToString() + " ";
                }

                textBox2.Text += Environment.NewLine;
            }

            pictureBox2.Image = bm2;
            button2.Enabled = false; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            int x_x = 0;
            int y_y = 0;

            int naprgamer = 0;

            Random r = new Random();

            for (int kp2 = 1; kp2 <= 4; kp2++)
            {
                for (int kk2 = 1; kk2 <= 4 - kp2 + 1; kk2++)
                {
                    bool hod = false;

                    while (hod == false)
                    {
                        x_x = r.Next(0, 10);
                        y_y = r.Next(0, 10);

                        naprgamer = r.Next(0, 2);

                        hod = true;

                        if (naprgamer == 0) // горизонтальное
                        {
                            if (x_x + kp2 - 1 > 9)
                            {
                                hod = false;
                            }

                            for (int a_x = Math.Max(0, x_x - 1); a_x < Math.Min(10, x_x + kp2 + 2); a_x++)
                            {
                                for (int a_y = Math.Max(0, y_y - 1); a_y < Math.Min(10, y_y + 2); a_y++)
                                {
                                    if (gamer[a_y, a_x] != 0)
                                    {
                                        hod = false;
                                    }
                                }
                            }

                            if (hod)
                            {
                                for (int x_2 = x_x; x_2 < x_x + kp2; x_2++)
                                {
                                    gamer[y_y, x_2] = 1;

                                    gr.FillRectangle(Brushes.Gray, x_2 * (pictureBox1.Width / 10) + 1, y_y * (pictureBox1.Height / 10) + 1, pictureBox1.Width / 10 - 1, pictureBox1.Height / 10 - 1);
                                }
                            }
                        }
                        else // вертикальное
                        {
                            if (y_y + kp2 - 1 > 9)
                            {
                                hod = false;
                            }



                            for (int a_x = Math.Max(0, x_x - 1); a_x < Math.Min(10, x_x + 2); a_x++)
                            {
                                for (int a_y = Math.Max(0, y_y - 1); a_y < Math.Min(10, y_y + kp2 + 2); a_y++)
                                {
                                    if (gamer[a_y, a_x] != 0)
                                    {
                                        hod = false;
                                    }
                                }
                            }

                            if (hod)
                            {
                                for (int y_2 = y_y; y_2 < y_y + kp2; y_2++)
                                {
                                    gamer[y_2, x_x] = 1;

                                    gr.FillRectangle(Brushes.Gray, x_x * (pictureBox1.Width / 10) + 1, y_2 * (pictureBox1.Height / 10) + 1, pictureBox1.Width / 10 - 1, pictureBox1.Height / 10 - 1);
                                }
                            }
                        }
                    }
                }
            }
            radioButton2.Checked = false;
            radioButton2.Enabled = false;
            radioButton1.Checked = false;
            radioButton1.Enabled = false;
            radioButton3.Checked = false;
            radioButton3.Enabled = false;
            radioButton4.Checked = false;
            radioButton4.Enabled = false;
            textBox1.Text = ""; 
            button3.Enabled = false;

            for (int i = 0; i < gamer.GetLength(0); i++)
            {
                for (int j = 0; j < gamer.GetLength(1); j++)
                {
                    textBox1.Text += gamer[i, j].ToString() + " ";
                }

                textBox1.Text += Environment.NewLine;
            }

            pictureBox1.Image = bm;
            label3.Text = "Пушки готовы к бою!"; 
        }
    }
}
