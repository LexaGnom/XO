using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class MessageHandler
    {
       
        /// <summary>
        /// формируем строку
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private static string BuildArea(Game game)
        {
            string areaRaw = string.Empty;
            for (int i = 0; i < game.pole.GetLength(0); i++)
            {
                for (int j = 0; j < game.pole.GetLength(1); j++)
                {
                    areaRaw += game.pole[j, i].ToString();
                }
                areaRaw += "-";
            }
            if (areaRaw.IndexOf('\0')==-1)
            {
                game.winner='n';
            }
            return areaRaw;
        }
        /// <summary>
        /// процес с стороны клиенты
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="client"></param>
        /// <param name="area"></param>
        /// <param name="winner"></param>
        /// <param name="next"></param>
        public static void Process(Message msg, Client client,out string area,out char winner,out char next)
        {
            area = "";
            winner = '\0';
            next = '\0';
            if (typeof(Response) == msg.GetType())
            {
                area = (msg as Response).areaRaw;
                winner = (msg as Response).winner;
                next = (msg as Response).nextPlayer;
            }
            if (typeof(TestMsg) == msg.GetType())
            {
                client.EnemyName = (msg as TestMsg).Enemy;
                client.PlayerName = (msg as TestMsg).Name;
                client.nextPlayer = (msg as TestMsg).StartPlayer;
            }
        }
        /// <summary>
        /// процесс со стороны серва
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public static string Process<T>(T message, Game game)
        {
            if (typeof(Request) == message.GetType())
            {
                int x = (message as Request).X;
                int y = (message as Request).Y;
                char name = (message as Request).Name;
                if (game.pole[x, y] == '\0')
                {
                    game.pole[x, y] = name;
                    game.swichPlayer(name);
                    game.Proverka_pobed(name, x, y);
                } 
                Response response = new Response(BuildArea(game), CalcucateWinner(game), game.nextPlayer);

                return response.GetContent();
 
            }
            if (typeof(TestMsg) == message.GetType())//клиент запрашивает данные о себе
            {
                char name = (message as TestMsg).Name;
                char enemy = (message as TestMsg).Enemy;
                if (name == enemy)
                {
                    if (game.FirstIsConnected&&!game.SecondIsConnected)
                    {
                        
                        return new TestMsg(game.FirstPlayerName, game.SecondPlayerName,game.nextPlayer).GetContent();// + new Response(BuildArea(game),"",game.FirstPlayerName).GetContent();
                    }
                    if (game.SecondIsConnected&&game.FirstIsConnected)
                    {
                        return new TestMsg(game.SecondPlayerName, game.FirstPlayerName,game.nextPlayer).GetContent();// + new Response(BuildArea(game), "", game.SecondPlayerName).GetContent();
                    }
                }
                else
                {
                    return new Response(BuildArea(game),CalcucateWinner(game),game.nextPlayer).GetContent(); //(message as TestMsg).GetContent();
                }
            }
            if (typeof(Message) == message.GetType())
            {
                return "TEST\u0001";
            }
            return "TEST\u0001";
        }

        private static char CalcucateWinner(Game game)
        {
            return game.winner;
        }

    }
}
