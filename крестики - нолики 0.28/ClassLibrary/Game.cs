using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Game

    {
        public char winner { get;  set; }
        public char[,] pole { get; protected set; } // = new char[3, 3];  //игровое поле
        int razmer = 0; //размер игрового поля
        int pobeda=3; //количество клеток в линии (для победы)
        public char FirstPlayerName { get; protected set; }//имя первого
        public char SecondPlayerName { get; protected set; }//имя второго
        public char nextPlayer { get; protected set; }
        public bool FirstIsConnected { get; protected set; }//индикатор певого подключениного
        public bool SecondIsConnected { get; protected set; }
        public Game( ref char[,] area, out int raz)
        {
           
            raz = razmer;
        }
        /// <summary>
        /// переключение игрока
        /// </summary>
        /// <param name="name"></param>
        public void swichPlayer(char name)
        {
            if (name == FirstPlayerName)
            {
                nextPlayer = SecondPlayerName;
            }
            if (name == SecondPlayerName)
            {
                nextPlayer = FirstPlayerName;
            }
        }
        /// <summary>
        /// инициализация поля и игроков
        /// </summary>
        public Game()
        {
            pole = new char[3, 3];
            FirstPlayerName = '1';
            SecondPlayerName = '2';
            nextPlayer = FirstPlayerName;
        }
        /// <summary>
        /// установка подключения игроков
        /// </summary>
        /// <returns></returns>
        public bool SetPlayer()
        {
            if (!FirstIsConnected)
            {
                FirstIsConnected = true;
                return true;
            }
            else if (!SecondIsConnected)
            {
                SecondIsConnected = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// проверка победы или ничьей
        /// </summary>
        /// <param name="sim">текущий символ</param>
        /// <param name="lew">1 координата</param>
        /// <param name="praw">2 координата</param>
        public void Proverka_pobed( char sim, int lew, int praw)  
        {

            bool prowerka = false;  
            int count, lewpr, prawpr;   //количество нужных символов в линии (длина линии), 2 индекса (координаты)
            for (int i = 0; i < 4; i++)    //4 линии
            {
                count = 0;
                lewpr = lew;   //i
                prawpr = praw;  //j
                
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
                        throw new Exception("Ошибка проверка");
                        
                }

                if (prowerka)   //победа
                {
                    winner = sim;


                    break;
                }
                

            }



        }
    }
}
