using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Message
    {
        //public enum MessageType { Test, Request, Response };
        public char separator;//разделитель
        public char tagSeparator;//разделитель тега
        public char messageType;//тип сообщении

        protected string messageTag;// тип сообщения
        public string typeTag;//тип тега
        protected string endTag;//тег сообщения
        protected string Head;//голова
        protected string Body;//тело
        protected string Tile;//хвост
        protected int symbNumb = 2;//количество символов
        /// <summary>
        /// парсирование сообщения
        /// </summary>
        /// <param name="content">строка</param>
        /// <returns></returns>
        public static Message Parse(string content)
        {
            Message msg = new Message();
            string[] tmp = content.Split(msg.separator);
            Dictionary<string, string> fields = new Dictionary<string, string>();
            foreach (string piece in tmp)
            {
                string[] pair = piece.Split(msg.tagSeparator);
                if (pair.Length > 1&&!fields.ContainsKey(pair[0]))
                {
                    fields.Add(pair[0], pair[1]);
                }
                else
                {
                    break;
                }
            }
            string type = string.Empty;
            fields.TryGetValue(msg.typeTag, out type);
            switch (type)
            {
                case "s":
                    string name = "";
                    string x = "";
                    string y = "";
                    fields.TryGetValue(Request.PlayerTag, out name);
                    fields.TryGetValue(Request.xTag, out x);
                    fields.TryGetValue(Request.yTag, out y);
                    return new Request(name.ToCharArray()[0], x, y);
                case "r":
                    string area = "";
                    string winner = "";
                    string next = "";
                    fields.TryGetValue(Response.areaTag, out area);
                    fields.TryGetValue(Response.winnerTag, out winner);
                    fields.TryGetValue(Response.nextPlayerTag, out next);
                    return new Response(area, winner[0], next.ToArray()[0]);
                case "l":
                    string Pname = "";
                    string Penemy = "";
                    string SP = "";
                    fields.TryGetValue(TestMsg.NameTag,out Pname);
                    fields.TryGetValue(TestMsg.EnemyTag,out Penemy);
                    fields.TryGetValue(TestMsg.StartTag, out SP);
                    return new TestMsg(Pname[0], Penemy[0],SP[0]);

                default:
                    return null;
            }
        }

        public string Content { get; protected set; }
        
        public Message()
        {
            separator = '\u0001';
            tagSeparator = '=';
            typeTag = "T";
            messageTag = "M" + tagSeparator;
            endTag = "E" + tagSeparator;
            Head = messageTag;
            Body = "";
            Tile = endTag + separator;
            messageType = 't';
        }

        public virtual void SetContent(Client client)
        {

        }
        public virtual string GetContent()
        {
            return "M=10\u0001T=t\u0001E=\u0001";
        }
    }
}
