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
    public partial class Form2 : Form
    {
        PictureBox[,] picbox; // = new PictureBox[3, 3];
        //public bool xod = true; // true - крестики, false - нолики
        string data = "";  // режим игры
        krestiki_noliki kr;
        int razmer;
        public Label label1;
       
        //public char[,] pole = new char[3, 3];  //игровое поле
        //public int kolxodov = 0; //количество ходов

        //кнопка начать сначала
        //счетчик побед
      
        //bool perexod = true; //переход от компьютера к человеку     (одно нажатие) ( игрок - занято?(while да - ходи снова) - проверка - компьютер - проверка)(до победы)
      
        public Form2(string data)
        {
            
            this.data = data;            //определяет режим игры
           // int razmer;
            kr = new krestiki_noliki(data, ref picbox, out razmer); //определяет режим игры  
            this.Height = (razmer + 2) * 45;
            this.Width = (razmer + 2) * 45;
            
            InitializeComponent();
        }



        //при увеличении размера окна размер игрового поля не меняется 

        //ничья?

        //сначала столбец, потом строка (??? (вроде наооборот?)) (проверка)
        
        //в классе - private методы

       
        
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

        private void ОТПРАВИТЬ_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text; string text = "";
            kr.serv(ref picbox, s, ref text);
            label2.Text = text;

        }
    }
}
