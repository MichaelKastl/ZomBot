using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class Item
    {
        public string itemName;
        public int itemQty;
        public int cost;

        //Constructors
        public Item()
        {

        }

        public Item(string itemName, int itemQty)
        {
            this.itemName = itemName;
            this.itemQty = itemQty;
        }

        public Item(string itemName, int itemQty, int cost)
        {
            this.itemName = itemName;
            this.itemQty = itemQty;
            this.cost = cost;
        }

        //Methods

        public string GetItemName()
        {
            return itemName;
        }

        public int GetItemQty()
        {
            return itemQty;
        }
    }
}
