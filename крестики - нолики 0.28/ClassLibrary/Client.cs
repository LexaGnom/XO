using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClassLibrary
{
    public class Client
    {
        public char PlayerName { get; set; }
        public char EnemyName { get; set; }//враг
        public Point state { get; set; }//Куда был произведен ход
      
        public TcpClient tcpClient { get; protected set; }//сетевое взаимодействиие
        public string area;
        public Queue<Message> received = new Queue<Message>();//принятые сообщения клиентом
        public char winner;
        public char nextPlayer;
        /// <summary>
        /// регистрирует клиента
        /// </summary>
        public Client()
        {

            
            this.tcpClient = new TcpClient();

        }
        /// <summary>
        /// Запрос на получение имени игрока 
        /// </summary>
        public void LogOn()
        {
            PackMenager.SendMessage(tcpClient, new TestMsg(PlayerName, EnemyName,'\0').GetContent());
        }
        /// <summary>
        /// подключение
        /// </summary>
        /// <param name="ip">айпи</param>
        public void Connect(IPAddress ip)
        {
            tcpClient.Connect(ip, 1111);
        }
        
        /// <summary>
        /// конструктор для серверной части
        /// получение кто крестик кто нолик(первый был подключен )
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <param name="game"></param>
        public Client(TcpClient tcpClient, Game game)
        {
            this.tcpClient = tcpClient;
            if (game.FirstIsConnected&&!game.SecondIsConnected)
            {
                PlayerName = game.FirstPlayerName;
                EnemyName = game.SecondPlayerName;
            }
            if (game.FirstIsConnected && game.SecondIsConnected)
            {
                PlayerName = game.SecondPlayerName;
                EnemyName = game.FirstPlayerName;
            }

        }
        /// <summary>
        /// обновление клиента
        /// читает сообщения  из сети парсит и добавляет сообщение 
        /// </summary>
        public void Update()
        {
            if (tcpClient.GetStream().DataAvailable)
            {
                received.Enqueue(Message.Parse(PackMenager.GetMessage(tcpClient)));

            }
            else
            {
                PackMenager.SendMessage(tcpClient, new TestMsg(PlayerName,EnemyName,nextPlayer).GetContent());
                Thread.Sleep(300);

            }

        }
        /// <summary>
        /// поулучает парсит ставит
        /// обновляются данные об игроке победители и игровом поле
        /// </summary>
        public void Process()
        {
            for (int i = 0; i < received.Count; i++)
            {
                Message msg = received.Dequeue();
                MessageHandler.Process(msg,this,out area,out winner,out nextPlayer);
            }

        }
       
        /// <summary>
        /// статус о соединении
        /// </summary>
        /// <returns></returns>
        public bool isConnected()
        {
            return tcpClient.Connected;
        }
    }
}