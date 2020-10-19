using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class PlayerInventory
    {
        readonly ulong playerID;
        readonly string playerName;
        public List<Item> items;

        //Constructors

        public PlayerInventory()
        {

        }

        public PlayerInventory(string playerName, List<Item> items, ulong playerID)
        {
            this.playerName = playerName;
            this.items = items;
            this.playerID = playerID;
        }

        public PlayerInventory(string playerName, Item item, ulong playerID)
        {
            this.playerName = playerName;
            this.playerID = playerID;
            items.Add(item);
        }

        //Methods

        public List<Item> GetInventory()
        {
            return items;
        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public ulong GetPlayerID()
        {
            return playerID;
        }
    }
}
