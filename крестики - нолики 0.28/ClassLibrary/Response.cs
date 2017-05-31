using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// ответ
    /// </summary>
    public class Response : Message
    {
        public static string winnerTag = "V";
        public static string nextPlayerTag = "N";
        public static string areaTag = "A";
        public char[,] area { get; protected set; }
        public string areaRaw { get; protected set; }
        public char winner { get; protected set; }
        public char nextPlayer { get; protected set; }
        public Response() : base()
        {
            nextPlayerTag = "N";
            winnerTag = "V";
            area = new char[0, 0];
            messageType = 'r';
            areaTag = "A";
        }
        public Response(string a, char winner,char next) : this()
        {
            this.areaRaw = a;
            this.winner = winner;
            this.nextPlayer = next;
        }

        public override string GetContent()
        {
            Body += separator + typeTag + tagSeparator + messageType + separator + areaTag + tagSeparator + areaRaw + separator + winnerTag + tagSeparator + winner + separator + nextPlayerTag + tagSeparator + nextPlayer + separator;
            symbNumb = Encoding.UTF8.GetBytes(Body).Length + Encoding.UTF8.GetBytes(Tile).Length;
            return Head + symbNumb + Body + Tile;
        }
        public override void SetContent(Client client)
        {
            Body += separator + typeTag + tagSeparator + messageType + separator;
            string areaF = client.area;
            
            Body += areaTag + tagSeparator + areaF + separator + winnerTag + tagSeparator + client.PlayerName + separator + nextPlayerTag + tagSeparator + nextPlayer + separator;
            symbNumb = Encoding.UTF8.GetBytes(Body).Length + Encoding.UTF8.GetBytes(Tile).Length;
            string Message = Head + symbNumb + Body + Tile;
            Content = Message;
        }
    }
}
