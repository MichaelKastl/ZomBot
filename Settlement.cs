using System;
using System.Collections.Generic;
using System.Text;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]
    public class Settlement
    {
        public string name { get; set; }
        public ulong owner { get; set; }
        public int rarity { get; set; }
        public int totalModules { get; set; }
        public int defense { get; set; }
        public int maxDefense { get; set; }
        public bool isIntact;
        public bool isUnderAttack;
        public List<SettlementModule> modules { get; set; }

        public Settlement()
        {

        }
        public Settlement(string name, ulong owner, int rarity, int totalModules, List<SettlementModule> modules, int maxDefense, bool isIntact)
        {
            this.name = name;
            this.owner = owner;
            this.rarity = rarity;
            this.totalModules = totalModules;
            this.modules = modules;
            this.maxDefense = maxDefense;
            this.isIntact = isIntact;
            isUnderAttack = false;
        }
        public Settlement(string name, int rarity, int totalModules, int maxDefense)
        {
            this.name = name;
            this.rarity = rarity;
            this.totalModules = totalModules;
            this.maxDefense = maxDefense;
            isIntact = false;
            isUnderAttack = false;
        }
    }
}
