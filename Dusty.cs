using System;
using System.Collections.Generic;
using System.Text;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]
    public class Dusty
    {
        public List<Item> stock { get; set; }
        public DateTimeOffset lastRestock { get; set; }

        public Dusty()
        {

        }

        public Dusty(List<Item> stock, DateTimeOffset lastRestock)
        {
            this.stock = stock;
            this.lastRestock = lastRestock;
        }
    }
}
