using System;
using System.Collections.Generic;
using System.Text;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]
    public class SettlementModule
    {
        public string type { get; set; }
        public int level { get; set; }
        public int cost { get; set; }
        public int slot { get; set; }
        public DateTimeOffset lastCollection { get; set; }

        public SettlementModule()
        { 

        }
        public SettlementModule(string type, int level, int cost, int slot)
        {
            this.type = type;
            this.level = level;
            this.cost = cost;
            this.slot = slot;
        }
        public SettlementModule(string type, int level, int cost, int slot, DateTimeOffset lastCollection)
        {
            this.type = type;
            this.level = level;
            this.cost = cost;
            this.slot = slot;
            this.lastCollection = lastCollection;
        }
    }
}
