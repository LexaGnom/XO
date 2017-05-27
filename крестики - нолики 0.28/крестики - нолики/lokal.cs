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
    public class lokal
    {
        int pobeda;
        char [,] pole=new char[3,3];
        protected void Proverka_pobed(ref string text, ref char[,] picbox, char sim, int lew, int praw)  //текст в label, массив (для блокировки), символ проверки(что поставили), 2 индекса (координаты)
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
                    //Blokirovka(ref picbox);
                    break;
                }

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


    }
}
