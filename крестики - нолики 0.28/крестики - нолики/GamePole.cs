using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace крестики___нолики
{
    public partial class GamePole : Form
    {
        PictureBox[,] picbox; // = new PictureBox[3, 3];
        string data = "";  // режим игры
        krestiki_noliki kr;
        int count = 0;
        int razmer;
        public Label label1;
       
            
        public GamePole(string data)
        {
            
            this.data = data;            //определяет режим игры
           
            kr = new krestiki_noliki(data, ref picbox, out razmer); //определяет режим игры  
            this.Height = (razmer + 2) * 45;
            this.Width = (razmer + 2) * 45;
            
            InitializeComponent();
            
        }
        
       
        
        public void provfile()     //проверка не работает тут
        {
            bool z1 = true, z2 = true; //z1 - крестики , z2 - нолики    
            if (System.IO.File.Exists(@"x.jpg") == false) z1 = false;
            if (System.IO.File.Exists(@"0.jpg") == false) z2 = false;
            if (z1 == false)
            {
                MessageBox.Show("File not found", "x");     //???
                Close();
            }
            else
                if (z2 == false)
                {
                    MessageBox.Show("File not found", "0");    //???
                    Close();
                }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            provfile();
                   
            for (int i = 0; i < picbox.GetLength(0); i++)
            {
                for (int j = 0; j < picbox.GetLength(1); j++)
                {
                    picbox[i, j].Click += Form2_Click2;
                    Controls.Add(picbox[i, j]);
                }
            }
                Button bt = new Button();
                bt.Name = "button1";
                bt.Text = "В меню";
                bt.Left = razmer*45+30;
                bt.Top = 15;
                bt.Click += this.button1_Click;
                this.Controls.Add(bt);

                label2.Left = 15;
                label2.Top = razmer * 45 + 30;

           }

        
        
        
        void Form2_Click2(object sender, EventArgs e)
        {
            string text;
            kr.click(ref picbox,out text, sender);
           // label1.Text = text;
            label2.Text = text;
        

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       
        private void Buld_area(string area)
        {
            
            
            if (area == ""||area==null)
                return ;
            string[] mas = area.Split('-');
            
            for (int k = 0; k < picbox.GetLength(0); k++)
                for (int o = 0; o < picbox.GetLength(1); o++)
                {

                    switch (mas[o][k])
                    {
                        case '1':
                            picbox[k, o].Image = Image.FromFile(@"x.jpg");
                            count++;
                            break;
                        case '2':
                            picbox[k, o].Image = Image.FromFile(@"0.jpg");
                            count++;
                            break;
                        default:
                            picbox[k, o].Image = null; 
                            break;
                    }
                        
                }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (kr.Igrok.isConnected())
            {

                kr.Igrok.Update();
                kr.Igrok.Process();
                Buld_area(kr.Igrok.area);
                

                if (kr.Igrok.winner != '\0')
                {
                    label2.Text = "Победил " + kr.Igrok.winner;
                    this.Text = "Победил " + kr.Igrok.winner;
                }
                else
                {
                    label2.Text = "Ход" + kr.Igrok.nextPlayer;

                }
                if (kr.Igrok.winner=='n')
                {
                    label2.Text = "Ничья";
                    this.Text = "Ничья";
                }
                
                
                
                
                
            }
        }
    }
}
