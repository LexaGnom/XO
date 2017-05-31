using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace крестики___нолики
{
    public partial class RejimIgr : Form
    {
       // Form1 main = this.Owner as Form1;
        
        public RejimIgr()
        {
            InitializeComponent();
            Menu main = this.Owner as Menu;
        }

        //выбор режима игры

        private void button1_Click(object sender, EventArgs e)
        {
            Menu main = this.Owner as Menu;
            main.data = "igrok + igrok";
            label2.Text = "игрок с игроком (3 х 3)";
            //Form2 v = new Form2("igrok + igrok");
            //v.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu main = this.Owner as Menu;
            main.data = "igrok + Computer";
            label2.Text = "игрок с компьютером (3 х 3)";
             //Form2 v = new Form2("igrok + ii");
            //v.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu main = this.Owner as Menu;
            main.data = "Локальная сеть";
            label2.Text = "Локальная сеть";
            this.Close();
            //Form2 v = new Form2("ii + igrok");
            //v.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Menu main = this.Owner as Menu;
            switch (main.data)
            { 
                case "igrok + igrok":
                    label2.Text = "игрок с игроком (3 х 3)";
                    break;
                case "igrok + Computer":
                    label2.Text = "игрок с компьютером (3 х 3)";
                    break;
                case "Локальная сеть":
                    label2.Text = "Локальная сеть";
                    break;
                case "igrok + igroc 20":
                    label2.Text = "игрок с игроком (20 х 20)";
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Menu main = this.Owner as Menu;
            main.data = "igrok + igroc 20";
            label2.Text = "игрок с игроком (20 х 20)";
        }
    }
}
