using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace крестики___нолики
{
    public class krestiki_noliki
    {
        
         string data;
        bool xod = true; // true - крестики, false - нолики
         int kolxodov = 0; //количество ходов
        char[,] pole; // = new char[3, 3];  //игровое поле
        int razmer = 0; //размер игрового поля
        int pobeda;  //количество клеток в линии (для победы)
             
        public krestiki_noliki(string data, ref PictureBox [,] picbox, out int raz)
        {
            this.data = data;
            Razmer_poly(ref picbox);
            Sozdanie_poly(ref picbox);
            raz = razmer;
                       
        }


        public void click(ref PictureBox[,] picbox, out string text, object sender)
        {
            text = "";
            switch (data)
            {                  
                case "igrok + igrok":

                    //igrok_igrok(ref picbox, ref text, sender);
                    //lokal r=new lokal();
                   serv(ref picbox, ref text, sender);
                    break;
                case "igrok + Computer":
                    igrok_Computer(ref picbox, ref text, sender);
                    //igrok_igrok(ref picbox, ref text, sender);
                    break;
                
                case "igrok + igroc 20":
                    igrok_igrok(ref picbox, ref text, sender);
                    break;
                case "Локальная сеть":
                    //click_i_i(ref picbox, ref text, sender);
                    break;

                default:
                    MessageBox.Show("ошибка data", "ошибка");
                    break;
            }
        }
        //protected void igrok_Computer(ref PictureBox[,] picbox, ref string text, object sender)
        public void serv(ref PictureBox[,] picbox, ref string text, object sender)
        {
            string s = null;
            for (int k = 0; k < picbox.GetLength(0); k++)
                for (int o = 0; o < picbox.GetLength(1); o++)
                    if (picbox[k, o] == (PictureBox)sender)
                    { s = "Игрок=1:" + "x=" + k + ":" + "y=" + o; }
            string[] s1 = s.Split(':');
            int x = -1, y = -1;
            string r;
            for (int i = 0; i < s1.Length; i++)
            {
                s = s1[i];
                for (int j = 0; j < s.Length; j++)
                {
                    if (s[j] == 'x')
                    {
                        r = s[j + 2].ToString();
                        x = Convert.ToInt32(r);
                        break;
                    }
                    if (s[j] == 'y')
                    {
                        r = s[j + 2].ToString();
                        y = Convert.ToInt32(r);
                        j = j + 2;
                        goto unter;
                    }
                unter:
                    if (x != -1 & y != -1)
                    {
                        if (xod)
                        {
                            if (picbox[x, y].Image != null) text = "Клетка занята (ход x)";
                            else
                            {
                                picbox[x, y].Image = Image.FromFile(@"x.jpg");
                                pole[x, y] = 'x';
                                xod = false;
                                text = "ход 0";
                                //Proverkakrest(ref text, ref picbox);
                                Proverka_pobed(ref text, ref picbox, 'x', x, y);
                                kolxodov++;
                            }
                        }
                        else
                        {
                            if (picbox[x, y].Image != null) text = "Клетка занята (ход 0)";
                            else
                            {
                                picbox[x, y].Image = Image.FromFile(@"0.jpg");
                                pole[x, y] = '0';
                                xod = true;
                                text = "ход x";
                                //Proverkanolic(ref text, ref picbox);
                                Proverka_pobed(ref text, ref picbox, '0', x, y);
                                kolxodov++;
                            }
                        }
                    }
                }
            }
        }
        protected void igrok_Computer(ref PictureBox[,] picbox, ref string text, object sender)   //подходит для любого поля (игрок с игроком)    (нужно проверить)
        {
            for (int i = 0; i < picbox.GetLength(0); i++)
                for (int j = 0; j < picbox.GetLength(1); j++)
                    if (picbox[i, j] == (PictureBox)sender)
                    {
                        if (xod)
                        {
                            if (picbox[i, j].Image != null) text = "Клетка занята (ход x)";
                            else
                            {
                                picbox[i, j].Image = Image.FromFile(@"x.jpg");
                                pole[i, j] = 'x';
                                xod = false;
                                text = "ход 0";
                                //Proverkakrest(ref text, ref picbox);
                                Proverka_pobed(ref text, ref picbox, 'x', i, j);
                                kolxodov++;
                            }
                        }
                        if (xod==false)
                        {
                            if (kolxodov == 1)
                            {
                                if ((i == 0||i==2)&&(j == 0||j==2))
                                {
                                    picbox[1, 1].Image = Image.FromFile(@"0.jpg");
                                    pole[1,  1] = '0';
                                    xod = true;
                                    text = "ход x";
                                    kolxodov++;
                                    break;
                                }
                             }
                            if (kolxodov == 3)
                            {

                                if ((i == 0 || i == 2) && j == 0)
                                {
                                    picbox[i, j+1].Image = Image.FromFile(@"0.jpg");
                                    pole[i,j+1] = '0';
                                    xod = true;
                                    text = "ход x";
                                    kolxodov++;
                                    break;
                                }
                                if ((i == 0 || i == 2) && j == 2)
                                {

                                    picbox[i, j - 1].Image = Image.FromFile(@"0.jpg");
                                    pole[i, j - 1] = '0';
                                    xod = true;
                                    text = "ход x";
                                    kolxodov++;
                                    break;
                                }

                            }
                            if (kolxodov == 5)
                            {
                                if ((i == 0 || i == 2) && j == 0)
                                {
                                    picbox[i, j + 1].Image = Image.FromFile(@"0.jpg");
                                    pole[i, j + 1] = '0';
                                    xod = true;
                                    text = "ход x";
                                    kolxodov++;
                                    break;
                                }
                                if ((i == 0 || i == 2) && j == 2)
                                {

                                    picbox[i, j - 1].Image = Image.FromFile(@"0.jpg");
                                    pole[i, j - 1] = '0';
                                    xod = true;
                                    text = "ход x";
                                    kolxodov++;
                                    break;
                                }
                               

                            }
                            
                            picbox[i-1, j-1].Image = Image.FromFile(@"0.jpg");
                            pole[i-1, j-1] = '0';
                            xod = true;
                            text = "ход x";
                        }
                        //else
                        //{
                        //    if (picbox[i, j].Image != null) text = "Клетка занята (ход 0)";
                        //    else
                        //    {
                        //        picbox[i, j].Image = Image.FromFile(@"0.jpg");
                        //        pole[i, j] = '0';
                        //        xod = true;
                        //        text = "ход x";
                        //        //Proverkanolic(ref text, ref picbox);
                        //        Proverka_pobed(ref text, ref picbox, '0', i, j);
                        //        kolxodov++;
                        //    }
                        //}
                        goto link1;
                    }
        link1:
            if (kolxodov == pole.Length && text[0] == 'х')
            {
                text = "ничья";
                Blokirovka(ref picbox);
            }
        }
        protected void igrok_igrok(ref PictureBox[,] picbox, ref string text, object sender)   //подходит для любого поля (игрок с игроком)    (нужно проверить)
        {
            //(sender as PictureBox).Image=Image.FromFile(@"x.jpg");
            
            for (int i = 0; i < picbox.GetLength(0); i++)
                for (int j = 0; j < picbox.GetLength(1); j++)
                    if (picbox[i, j] == (PictureBox)sender)
                    {
                        if (xod)
                        {
                            if (picbox[i, j].Image != null) text = "Клетка занята (ход x)";
                            else
                            {
                                picbox[i, j].Image = Image.FromFile(@"x.jpg");
                                pole[i, j] = 'x';
                                xod = false;
                                text = "ход 0";
                                //Proverkakrest(ref text, ref picbox);
                                Proverka_pobed(ref text, ref picbox, 'x', i, j);
                                kolxodov++;
                            }
                        }
                        else
                        {
                            if (picbox[i, j].Image != null) text = "Клетка занята (ход 0)";
                            else
                            {
                                picbox[i, j].Image = Image.FromFile(@"0.jpg");
                                pole[i, j] = '0';
                                xod = true;
                                text = "ход x";
                                //Proverkanolic(ref text, ref picbox);
                                Proverka_pobed(ref text, ref picbox, '0', i, j);
                                kolxodov++;
                            }
                        }
                        goto link1;
                    }
        link1:
            if (kolxodov == pole.Length && text[0] == 'х')
            {
                text = "ничья";
                Blokirovka(ref picbox);
            }
        }

       
        protected void Blokirovka(ref PictureBox[,] picbox)
        {
            //игра закончилась (блокируем поле) (в отдельный метод)           
            for (int i = 0; i < picbox.GetLength(0); i++)
                for (int j = 0; j < picbox.GetLength(1); j++)
                {
                    picbox[i, j].Enabled = false;
                    //код, блокирующий ходы (игра закончилась)  (нажал кнопку "начать сначала" - обнуляем image и поле, разблокируем игру, kolxodov = 0)
                }
        }

        //проверка победы
        protected void Proverka_pobed(ref string text, ref PictureBox[,] picbox, char sim, int lew, int praw)  //текст в label, массив (для блокировки), символ проверки(что поставили), 2 индекса (координаты)
        {
            
            bool prowerka = false;  //победили?
            int count, lewpr, prawpr;   //количество нужных символов в линии (длина линии), 2 индекса (координаты)
            for (int i = 0; i < 4; i++)    //4 линии
            {
                count = 0;   
                lewpr = lew;   //i
                prawpr = praw;  //j
             //   if (!prowerka)
                    switch (i)      //4 линии
                    {
                        case 0:      //л - п  (строка)  (+)
                            while (count < pobeda && lewpr >= 0)    //cлева + в середине
                            {
                                if (pole[lewpr, prawpr] == sim)
                                {
                                    count++;
                                    lewpr--;
                                }
                                else break;
                            }
                            if (count == pobeda)
                            {
                                prowerka = true;
                            }
                            else
                            {
                                lewpr = lew + 1;   //i
                             //   prawpr = praw;  //j
                                while (count < pobeda && lewpr < pole.GetLength(0))    //cправа
                                {
                                    if (pole[lewpr, prawpr] == sim)
                                    {
                                        count++;
                                        lewpr++;
                                    }
                                    else break;
                                }
                                if (count == pobeda)
                                {
                                    prowerka = true;
                                }
                            }
                            break;

                        case 1:      // в-н  (столбец) (+)
                            while (count < pobeda && prawpr >= 0)    
                            {
                                if (pole[lewpr, prawpr] == sim)
                                {
                                    count++;
                                    prawpr--;
                                }
                                else break;
                            }
                            if (count == pobeda)
                            {
                                prowerka = true;
                            }
                            else
                            {
                            //    lewpr = lew + 1;   //i
                                prawpr = praw + 1;  //j
                                while (count < pobeda && prawpr < pole.GetLength(1))    
                                {
                                    if (pole[lewpr, prawpr] == sim)
                                    {
                                        count++;
                                        prawpr++;
                                    }
                                    else break;
                                }
                                if (count == pobeda)
                                {
                                    prowerka = true;
                                }
                            }
                            break;

                        case 2:      //л.н - п.в   (1 диагональ)
                            while (count < pobeda && prawpr >= 0 && lewpr < pole.GetLength(0))    
                            {
                                if (pole[lewpr, prawpr] == sim)
                                {
                                    count++;
                                    prawpr--;
                                    lewpr++;
                                }
                                else break;
                            }
                            if (count == pobeda)
                            {
                                prowerka = true;
                            }
                            else
                            {
                                lewpr = lew - 1;   //i
                                prawpr = praw + 1;  //j
                                while (count < pobeda && lewpr >= 0 && prawpr < pole.GetLength(1))   
                                {
                                    if (pole[lewpr, prawpr] == sim)
                                    {
                                        count++;
                                        prawpr++;
                                        lewpr--;
                                    }
                                    else break;
                                }
                                if (count == pobeda)
                                {
                                    prowerka = true;
                                }
                            }
                            break;

                        case 3:      //л.в - п.н   (2 диагональ)
                            while (count < pobeda && prawpr < pole.GetLength(1) && lewpr < pole.GetLength(0))    
                            {
                                if (pole[lewpr, prawpr] == sim)
                                {
                                    count++;
                                    prawpr++;
                                    lewpr++;
                                }
                                else break;
                            }
                            if (count == pobeda)
                            {
                                prowerka = true;
                            }
                            else
                            {
                                lewpr = lew - 1;   //i
                                prawpr = praw - 1;  //j
                                while (count < pobeda && lewpr >= 0 && prawpr >= 0)   
                                {
                                    if (pole[lewpr, prawpr] == sim)
                                    {
                                        count++;
                                        prawpr--;
                                        lewpr--;
                                    }
                                    else break;
                                }
                                if (count == pobeda)
                                {
                                    prowerka = true;
                                }
                            }
                            break;

                        default:
                            MessageBox.Show("ошибка проверки", "ошибка");
                            break;
                    }
                
                    if (prowerka)   //победа
                    {
                        text = "победили" + " " + sim.ToString();
                        //Blokirovka();
                        Blokirovka(ref picbox);
                        break;
                    }

            }
              
            
            
        }



        
        protected void Razmer_poly(ref PictureBox[,] picbox)  // + к созданию поля
        {
            //int razmer = 0; //размер игрового поля
            switch (data)
            {
                case "igrok + igrok":
                    razmer = 3;
                    pobeda = 3;
                    break;
                case "igrok + Computer":
                    razmer = 3;
                    pobeda = 3;
                    
                    break;

                case "igrok + igroc 20":
                    razmer = 20;
                    pobeda = 5;
                    break;
                case "Локальная сеть":
                    razmer = 10;
                    pobeda = 7;
                    break;

                default:
                    MessageBox.Show("ошибка Razmer", "ошибка");
                    break;
            }
            pole = new char[razmer, razmer];
            picbox = new PictureBox[razmer, razmer];
            //f.Size = new Size((razmer + 1) * 45, (razmer + 1) * 45);
        }


        private void Sozdanie_poly(ref PictureBox[,] picbox)  //создание поля   (pole[i,j] (i - номер столбца(lew), j - номер строки(praw))
        {
            //создание игрового поля                   
            int d = 10, v = 10;
            for (int i = 0; i < picbox.GetLength(0); i++)
            {

                for (int j = 0; j < picbox.GetLength(1); j++)
                {
                    picbox[i, j] = new PictureBox();
                    picbox[i, j].Location = new Point(d, v);
                    picbox[i, j].Size = new Size(45, 45);
                    picbox[i, j].TabIndex = 0;
                    picbox[i, j].TabStop = false;
                    picbox[i, j].BorderStyle = BorderStyle.FixedSingle;
                    v = v + 45;//двигаем вниз
                }
                d = d + 45;//двигаем вправо
                v = 10;
            }          

        }
     
        
   }
}
