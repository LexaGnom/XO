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
    public partial class Form1 : Form
    {
        public string data = "igrok + igrok";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 v = new Form2(data);            
            v.ShowDialog(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 v = new Form3();
            v.Owner = this;
            v.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool z = true;
            if (System.IO.File.Exists(@"правила.txt") == false) z = false;
            if (z == false)
            {
                MessageBox.Show("файл не найден");
            }
            else
            {
                System.Diagnostics.Process.Start(@"правила.txt");
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
