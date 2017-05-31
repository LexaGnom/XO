using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Request : Message
    {
        public static string PlayerTag = "P";
        public static string EnemyTag = "Q";
        public static string xTag = "x";
        public static string yTag = "y";
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public char Name { get; protected set; }
        public char Enemy { get; protected set; }
        public Request() : base()
        {
            PlayerTag = "P";
            EnemyTag = "Q";
            xTag = "x";
            yTag = "y";
            messageType = 's';
        }
        public Request(char name, string x, string y) : this()
        {
            Name = name;
            X = int.Parse(x);
            Y = int.Parse(y);

        }
        /// <summary>
        /// отправляет сообщение запроса
        /// </summary>
        /// <returns></returns>
        public override string GetContent()
        {
            Body = separator + typeTag + tagSeparator + messageType + separator + PlayerTag + tagSeparator + Name + separator + xTag + tagSeparator + X + separator + yTag + tagSeparator + Y + separator + EnemyTag + tagSeparator + Enemy + separator;
            symbNumb = Encoding.UTF8.GetBytes(Body).Length + Encoding.UTF8.GetBytes(Tile).Length;
            return Head + symbNumb + Body + Tile;

        }
        public override void SetContent(Client client)
        {
            Body = separator + typeTag + tagSeparator + messageType + separator + PlayerTag + tagSeparator + client.PlayerName + separator + xTag + tagSeparator + client.state.X + separator + yTag + tagSeparator + client.state.Y + separator + EnemyTag + tagSeparator + Enemy + separator;
            symbNumb = Encoding.UTF8.GetBytes(Body).Length + Encoding.UTF8.GetBytes(Tile).Length;
            string Message = Head + symbNumb + Body + Tile;
            Content = Message;
        }
    }
}
