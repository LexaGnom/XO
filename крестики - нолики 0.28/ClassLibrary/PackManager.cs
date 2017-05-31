using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// отправляет и получает сообшения
    /// </summary>
    public static class PackMenager
    {
        public static string GetMessage(TcpClient client)
        {
            Message msg = new Message();
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            List<byte> content = new List<byte>();
            while(stream.DataAvailable)
            {
                byte[] buffer = new byte[1];
                reader.Read(buffer,0,1);
                content.Add(buffer[0]);
            }
           
            return Encoding.UTF8.GetString(content.ToArray());
        }
       
        /// <summary>
        /// отправление сообщения
        /// </summary>
        /// <param name="client"></param>
        /// <param name="content"></param>
        public static void SendMessage(TcpClient client, string content)
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            writer.Write(buffer,0,buffer.Length);
            //Thread.Sleep(300);
        }
    }
}
