using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// тестовое сообщения 
    /// </summary>
    public class TestMsg : Message
    {
        public char Name { get; protected set; }
        public char Enemy { get; protected set; }
        public char StartPlayer { get; protected set; }
        public static string NameTag = "C";
        public static string EnemyTag = "L";
        public static string StartTag = "S";
        public TestMsg():base()
        {
            NameTag = "C";
            EnemyTag = "L";
            messageType = 'l';
            StartTag = "S";
        }
        public TestMsg(char n, char e,char s) : this()
        {
            Name = n;
            Enemy = e;
            StartPlayer = s;
        }
        public override string GetContent()
        {
            Body += separator + typeTag + tagSeparator + messageType + separator + NameTag + tagSeparator + Name + separator + EnemyTag + tagSeparator + Enemy + separator+StartTag+tagSeparator+StartPlayer+separator;
            symbNumb = Encoding.UTF8.GetBytes(Body).Length + Encoding.UTF8.GetBytes(Tile).Length;
            return Head + symbNumb + Body + Tile;
        }
    }
}
