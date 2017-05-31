using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Server
    {
        public int port = 1111;
        
        private static List<Client> clients = new List<Client>();
        private TcpListener listener;
        Game game;
        public Server()
        {
            game = new Game();
            Thread.Sleep(300);
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            new Thread(Listen).Start();
            new Thread(Update).Start();
        }

        private void Update()
        {
            while (true)
            {
                lock (this)
                {
                    foreach (Client client in clients)
                    {
                        client.Update();
                        if (client.received.Count != 0)
                        {
                            PackMenager.SendMessage(client.tcpClient, MessageHandler.Process(client.received.Dequeue(), game));
                            Thread.Sleep(300);
                        }
                    }
                }
            }
        }

        private void Listen()
        {
            while (true)
            {
                if (listener.Pending())
                {
                    lock (this)
                    {
                        if (game.SetPlayer())
                        {
                            Client client = new Client(listener.AcceptTcpClient(), game);
                            clients.Add(client);
                        }
                    }
                }
                lock (this)
                {
                    for (int i = 0; i < clients.Count; i++)
                    {
                        if (!clients[i].isConnected())
                        {
                            clients.RemoveAt(i); i--;
                        }
                    }
                }
            }
        }

    }
}
